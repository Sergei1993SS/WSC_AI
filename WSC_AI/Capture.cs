using Basler.Pylon;
using System.Windows.Forms;
using System;
using OpenCvSharp;



namespace WSC_AI
{
    public class Capture : Globals
    {
        public Camera Basler_camera;
        public bool IsFind = false;
        public bool IsSetConfig = false;


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
        public void SetConfig(String ConfigPath)
        {
            
        set_config:
                try
                {
                    this.Basler_camera.Open();
                    this.Basler_camera.Parameters.Load(ConfigPath, ParameterPath.CameraDevice);
                    LogWriter log = new LogWriter("Загружен конфиг: " + ConfigPath);
                    this.IsSetConfig = true;

 
                    if (!this.Basler_camera.Parameters[PLCamera.ChunkModeActive].TrySetValue(true))
                    {
                        throw new Exception("The camera doesn't support chunk features");
                    }

  
                    this.Basler_camera.Parameters[PLCamera.ChunkSelector].SetValue(PLCamera.ChunkSelector.Timestamp);
                    this.Basler_camera.Parameters[PLCamera.ChunkEnable].SetValue(true);
                    this.Basler_camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(MaxBufferSize);

                    this.Basler_camera.Parameters[PLCamera.ChunkSelector].SetValue(PLCamera.ChunkSelector.PayloadCRC16);
                    this.Basler_camera.Parameters[PLCamera.ChunkEnable].SetValue(true);



            }
                catch (Exception)
                {
                   try
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
                   catch (Exception)
                    {

                    }
                    

                }
            
        }

        public IGrabResult CameraSnapshot()
        {

        Grab:
            try
            {

                IGrabResult grabResult = this.Basler_camera.StreamGrabber.GrabOne(5000, TimeoutHandling.ThrowException);
                using (grabResult)
                {
  
                    if (grabResult.GrabSucceeded)
                    {

                        if (PayloadType.ChunkData != grabResult.PayloadTypeValue)
                        {
                            LogWriter log = new LogWriter("Буфер, содержащий метаданные не был получен");
                            goto Grab;
                        }


                        if (grabResult.HasCRC && grabResult.CheckCRC() == false)
                        {
                            LogWriter log = new LogWriter("Нарушена целостность передаваемых данных");
                            goto Grab;
                        }

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

                    return grabResult.Clone();
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Получено исключение при попытке захвата кадра");
                goto Grab;
            }
            

        }

        public Mat convertToMat(IGrabResult rtnGrabResult)
        {
            PixelDataConverter converter = new PixelDataConverter();
            converter.OutputPixelFormat = PixelType.BGR8packed; //BGR8packed
            byte[] buffer = new byte[converter.GetBufferSizeForConversion(rtnGrabResult)];
            converter.Convert(buffer, rtnGrabResult);
            return new Mat(rtnGrabResult.Height, rtnGrabResult.Width, MatType.CV_8UC3, buffer); 
        }

    }
}
