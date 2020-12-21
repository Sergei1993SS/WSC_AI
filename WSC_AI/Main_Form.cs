using System;
using System.Windows.Forms;
using Basler.Pylon;
using System.Collections.Generic;
using OpenCvSharp;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Drawing;


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

            if (cap.IsFind && cap.IsSetConfig)
            {
                pictureBox_cam.BackgroundImage = WSC_AI.Properties.Resources.tick;
                pictureBox_cam.Refresh();

                label_cam_model.Text = cap.Basler_camera.CameraInfo[CameraInfoKey.ModelName];
                label_sn_cam.Text = cap.Basler_camera.CameraInfo[CameraInfoKey.SerialNumber];
                label_api_cam.Text = cap.Basler_camera.CameraInfo[CameraInfoKey.DeviceIpAddress];
                label_api_cam.Refresh();
                label_cam_model.Refresh();
                label_sn_cam.Refresh();

                
            }
            else
            {
                pictureBox_cam.BackgroundImage = WSC_AI.Properties.Resources.cross;
                pictureBox_cam.Refresh();
            }
        }

        public delegate void InvokeDelegate(System.Drawing.Bitmap img);

        public void InvokeMethod(System.Drawing.Bitmap img)
        {
            pictureBox_main.BackgroundImage = img;
            pictureBox_main.Refresh();
        }

        private void button_basler_Click(object sender, EventArgs e)
        {

            Task.Run(() => CameraProcess());
            Task.Run(() => ImageProcess());

            


            int p = 0;
                

            
            

         
            
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
                    Thread.Sleep(1000);
                }

            }

            // See documentation for this method.
            // Images.CompleteAdding();
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
                        Bitmap bitmap = new Bitmap(sample.GrabImage.Width, sample.GrabImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        converter.OutputPixelFormat = PixelType.BGRA8packed;
                        IntPtr ptrBmp = bmpData.Scan0;
                        converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, sample.GrabImage);
                        bitmap.UnlockBits(bmpData);
                        BeginInvoke(new InvokeDelegate(InvokeMethod), bitmap);
                        GC.Collect();


                    }
                    else
                    {
                        Thread.Sleep(1000);
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
            if (cap.Basler_camera.IsOpen)
            {
                cap.Basler_camera.Close();
                cap.Basler_camera.Dispose();
            }
        }

        
    }
}
