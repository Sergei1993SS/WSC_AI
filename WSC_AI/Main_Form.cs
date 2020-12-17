using System;
using System.Windows.Forms;
using Basler.Pylon;
using System.Collections.Generic;
using OpenCvSharp;


namespace WSC_AI
{
    public partial class Main_Form : Form
    {
        Capture cap;

        public Main_Form()
        {
            InitializeComponent();
            cap = new Capture();
        }

        private void button_basler_Click(object sender, EventArgs e)
        {
            
            cap.SetConfig();

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

                
                List<IGrabResult> list_grab = new List<IGrabResult>();
                //cap.Basler_camera.StreamGrabber.Start();

                for (int i = 0; i < 50; i++)
                {
                    
                    
                    //cap.CameraSnapshot();
                    list_grab.Add(cap.CameraSnapshot());                   
                    
                   

                    System.Threading.Thread.Sleep(500);
                    GC.Collect();
                    pictureBox_cam.Refresh();

                }
                //cap.Basler_camera.StreamGrabber.Stop();
                cap.Basler_camera.Close();
                cap.Basler_camera.Dispose();

            }
            else
            {
                pictureBox_cam.BackgroundImage = WSC_AI.Properties.Resources.cross;
                pictureBox_cam.Refresh();
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
            }
        }
    }
}
