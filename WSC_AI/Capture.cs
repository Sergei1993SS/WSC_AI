using Basler.Pylon;
using System.Windows.Forms;
using System;
using OpenCvSharp;


namespace WSC_AI
{
    class Capture : Globals
    {
        public Camera Basler_camera;
        public bool IsFind = false;
        public bool IsSetConfig = false;
        //public Globals GlobalVar = new Globals();


        public Capture()
        {
        connect_cam:
                try
                {   
                    Basler_camera = new Camera();
                    this.IsFind = true;
                    LogWriter log = new LogWriter("Установлена связь с камерой: | Model: " + Basler_camera.CameraInfo[CameraInfoKey.ModelName] + ", IP:  " + Basler_camera.CameraInfo[CameraInfoKey.DeviceIpAddress] + "|");
                }
                catch (System.Exception)
                {

                    LogWriter log = new LogWriter("Камера не обнаружена!");
                    DialogResult res = MessageBox.Show(caption: "Ошибка обнаружения камеры",
                        text: "Камера не обнаружена!" + System.Environment.NewLine + "Повторить подключение?",
                        buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                    if (res == DialogResult.Yes)
                    {
                        goto connect_cam; 
                    }

                    else
                    {
                        this.IsFind = false;            
                    }
                
                }
            
        }

        /// <summary>
        /// УСТАНАВЛИВАЕМ НУЖНУЮ КОНФИГУРАЦИЮ
        /// </summary>
        public void SetConfig()
        {
            
        set_config:
                try
                {
                    this.Basler_camera.Open();
                    this.Basler_camera.Parameters.Load(CamConfigPath, ParameterPath.CameraDevice);
                    LogWriter log = new LogWriter("Загружен конфиг: " + CamConfigPath);
                    this.IsSetConfig = true;

                    // Enable the chunk mode.
                    if (!this.Basler_camera.Parameters[PLCamera.ChunkModeActive].TrySetValue(true))
                    {
                        throw new Exception("The camera doesn't support chunk features");
                    }

                    // Enable time stamp chunks.
                    this.Basler_camera.Parameters[PLCamera.ChunkSelector].SetValue(PLCamera.ChunkSelector.Timestamp);
                    this.Basler_camera.Parameters[PLCamera.ChunkEnable].SetValue(true);
                    this.Basler_camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(MaxBufferSize);

                    // Enable CRC checksum chunks.
                    this.Basler_camera.Parameters[PLCamera.ChunkSelector].SetValue(PLCamera.ChunkSelector.PayloadCRC16);
                    this.Basler_camera.Parameters[PLCamera.ChunkEnable].SetValue(true);



            }
                catch (Exception)
                {
                    LogWriter log = new LogWriter("Ошибка загрузки конфигурации камеры!");
                    DialogResult res = MessageBox.Show(caption: "Ошибка загрузки конфигурации камеры",
                        text: "Конфиг не установлен" + System.Environment.NewLine + "Повторить установку?",
                        buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                    this.Basler_camera.Close();

                    if (res == DialogResult.Yes)
                    {
                    goto set_config;
                    }

                    else
                    {
                        this.IsSetConfig = false;
                    }

                }
            
        }

        public IGrabResult CameraSnapshot()
        {

        Grab:
            // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
            IGrabResult grabResult = this.Basler_camera.StreamGrabber.GrabOne(5000, TimeoutHandling.ThrowException);
            using (grabResult)
            {
                // Image grabbed successfully?
                if (grabResult.GrabSucceeded)
                {
                    // Check to see if a buffer containing chunk data has been received.
                    if (PayloadType.ChunkData != grabResult.PayloadTypeValue)
                    {
                        LogWriter log = new LogWriter("Буфер, содержащий метаданные не был получен");
                        goto Grab;
                    }

                    // Because we have enabled the CRC Checksum feature, we can check
                    // the integrity of the buffer.
                    // Note: Enabling the CRC Checksum feature is not a prerequisite for using chunks.
                    // Chunks can also be handled when the CRC Checksum feature is disabled.
                    if (grabResult.HasCRC && grabResult.CheckCRC() == false)
                    {
                        LogWriter log = new LogWriter("Нарушена целостность передаваемых данных");
                        goto Grab;
                    }

                    // Access the chunk data attached to the result.
                    // Before accessing the chunk data, you should check to see
                    // if the chunk is readable. If it is readable, the buffer
                    // contains the requested chunk data.
                    if (!grabResult.ChunkData[PLChunkData.ChunkTimestamp].IsReadable)
                    {
                        LogWriter log = new LogWriter("Время съемки кадра невозможно прочитать");
                        goto Grab;
                    }


                }
                else
                {
                    LogWriter log = new LogWriter("Неудачная попытка захвата кадра");
                    goto Grab;
                }

                //IGrabResult res = grabResult.Clone();
                //grabResult.Dispose();
                
                return grabResult.Clone();
            }

        }

        public Mat convertToMat(IGrabResult rtnGrabResult)
        {
            PixelDataConverter converter = new PixelDataConverter();
            converter.OutputPixelFormat = PixelType.BGR8packed;
            byte[] buffer = new byte[converter.GetBufferSizeForConversion(rtnGrabResult)];
            converter.Convert(buffer, rtnGrabResult);
            return new Mat(rtnGrabResult.Height, rtnGrabResult.Width, MatType.CV_8UC3, buffer);
        }

    }
}
