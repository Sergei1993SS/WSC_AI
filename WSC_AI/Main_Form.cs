using System;
using System.Windows.Forms;
using Basler.Pylon;
using System.Collections.Generic;
using OpenCvSharp;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using Opc.UaFx.Client;


namespace WSC_AI
{
    public partial class Main_Form : Form
    {
        Capture cap;
        OPC OPC_client;
        ConcurrentQueue<TScan_and_Images> Images;
        PixelDataConverter converter;

        

        public Main_Form()
        {
            InitializeComponent();
            cap = new Capture();
            cap.SetConfig();
            OPC_client = new OPC();
            Images = new ConcurrentQueue<TScan_and_Images>();
            converter = new PixelDataConverter();

            OPC_client.OPC_Connecting = false;

            if (cap.IsFind && cap.IsSetConfig)
            {
                pictureBox_cam.BackgroundImage = WSC_AI.Properties.Resources.green;
                pictureBox_opc.BackgroundImage = WSC_AI.Properties.Resources.green;
                pictureBox_cam.Refresh();

                label_cam_model.Text = cap.Basler_camera.CameraInfo[CameraInfoKey.ModelName];
                label_sn_cam.Text = cap.Basler_camera.CameraInfo[CameraInfoKey.SerialNumber];
                label_api_cam.Text = cap.Basler_camera.CameraInfo[CameraInfoKey.DeviceIpAddress];
                label_opc.Text = OPC_client.Server_Name;
                label_opc.Refresh();
                label_api_cam.Refresh();
                label_cam_model.Refresh();
                label_sn_cam.Refresh();
                
                Task.Run(() => CameraProcess());
                Task.Run(() => ImageProcess());
                Task.Run(() => OPC_Indication());


            }
            else
            {
                pictureBox_cam.BackgroundImage = WSC_AI.Properties.Resources.red;
                pictureBox_opc.BackgroundImage = WSC_AI.Properties.Resources.red;
                pictureBox_cam.Refresh();
            }
        }


        /// <summary>
        /// Процесс съемки
        /// </summary>
        private void CameraProcess()
        {
            while (true)
            {
                if (OPC_client.GetnisCameraInPosition())
                {

                    Images.Enqueue(new TScan_and_Images(cap.CameraSnapshot(), OPC_client.GetX(), OPC_client.GetY(), OPC_client.GetZ(),
                                                    OPC_client.GetRx(), OPC_client.GetRy(), OPC_client.GetRz()));

                    
                    OPC_client.SetnisCameraInPositionFalse();
                    OPC_client.SetisCameraShotComplete();
                    GC.Collect();

                }
                else
                {
                    Thread.Sleep(cap.SleepProcessCam);
                }

                

            }

        }


        private void OPC_Indication()
        {
            while (true)
            {
                if (OPC_client.OPC_Connecting)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        pictureBox_opc.BackgroundImage = WSC_AI.Properties.Resources.green;
                        pictureBox_opc.Refresh();
                    }));
                }
                else
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        pictureBox_opc.BackgroundImage = WSC_AI.Properties.Resources.red;
                        pictureBox_opc.Refresh();
                    }));
                }
                
                Thread.Sleep(600);
                this.Invoke(new MethodInvoker(() =>
                {
                    pictureBox_opc.BackgroundImage = WSC_AI.Properties.Resources.gray;
                    pictureBox_opc.Refresh();
                }));
                Thread.Sleep(600);
            }
        }

        private void ImageProcess()
        {
            
            while (true)
            {

                try
                {
                    if (!Images.IsEmpty)
                    {
                        TScan_and_Images sample;
                        Images.TryDequeue(out sample);
                        
                        SaveImage(sample.GrabImage);


                        GC.Collect();



                        //Bitmap bitmap = new Bitmap(sample.GrabImage.Width, sample.GrabImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        //System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        //converter.OutputPixelFormat = PixelType.BGRA8packed;
                        //IntPtr ptrBmp = bmpData.Scan0;
                        //converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, sample.GrabImage);
                        //bitmap.UnlockBits(bmpData);
                        //BeginInvoke(new InvokeDelegate(InvokeMethod), bitmap);
                    }
                    else
                    {
                        Thread.Sleep(cap.SleepProcessImage);
                    }
                   
                }
                catch (InvalidOperationException) { }

                
                
            }

            
        }

        /// <summary>
        /// ПЕРЕД ЗАКРЫТИЕМ ФОРМЫ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show(caption: "Закрытие программы",
                    text: "Уверены, что хотите закрыть программу? Это приведет к неработоспособности системы контроля шва",
                    buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);
            if (res == DialogResult.Yes)
            {
                try
                {
                    if (cap.Basler_camera.IsOpen)
                    {
                        cap.Basler_camera.Close();
                        cap.Basler_camera.Dispose();
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                e.Cancel = true;
            }
            
            
        }

        private void SaveImage(IGrabResult gbr)
        {
            String m_exePath = cap.ImageSavePath + "//" + DateTime.Now.ToShortDateString();
            String img_path = m_exePath + "//" + gbr.Timestamp + ".jpg";

            if (Directory.Exists(m_exePath))
            {
                try
                {
                    Mat img = cap.convertToMat(gbr);
                    
                    Cv2.ImWrite(img_path, cap.convertToMat(gbr));
                    
                }
                catch (Exception ex)
                {
                    LogWriter log = new LogWriter("Не удалось сохранить фотографию: " + img_path);
                }
            }
            else
            {
                
                try
                {
                    Directory.CreateDirectory(m_exePath);
                    Cv2.ImWrite(img_path, cap.convertToMat(gbr));
                }
                catch (Exception ex)
                {
                    LogWriter log = new LogWriter("Не удалось сохранить фотографию: " + img_path);
                }
            }
        }

    }
}
