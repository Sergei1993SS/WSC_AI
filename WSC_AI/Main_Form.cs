using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using OpenCvSharp;
using Basler.Pylon;
using System.Threading.Tasks;
using System.Collections.Generic;
using DefectMessageNamespace;
using System.Management;
using System.Text;
using NumSharp;


namespace WSC_AI
{
    
    public partial class Main_Form : Form
    {
        Capture cap;
        OPC OPC_client;
        ConcurrentQueue<TScan_and_Images> Images;
        AI_TF AI;
        static public ConcurrentQueue<Defect> defects = new ConcurrentQueue<Defect>();
        static public List<Defect> defect_out = new List<Defect>();
        static public volatile bool REST_RUN;

        /// <summary>
        /// 
        /// </summary>
        public Main_Form()
        {
            

            cap = new Capture();
            cap.SetConfig();
            OPC_client = new OPC();
            Images = new ConcurrentQueue<TScan_and_Images>();

            OPC_client.OPC_Connecting = false;

            AI = new AI_TF();
            Task.Run(() => RunREST());

            InitializeComponent();

            if (REST_RUN)
            {
                label_REST.Text = "http://localhost:5000";
                label_REST.Refresh();
                pictureBox_REST.BackgroundImage = Properties.Resources.green;
                pictureBox_REST.Refresh();
            }
            else
            {
                label_REST.Text = "http://localhost:5000";
                label_REST.Refresh();
                pictureBox_REST.BackgroundImage = Properties.Resources.red;
                pictureBox_REST.Refresh();
            }

            

            if (AI.load_graph_Presence_Weld)
            {
                  pictureBox_NS_1.BackgroundImage = WSC_AI.Properties.Resources.green;
            }
            else pictureBox_NS_1.BackgroundImage = WSC_AI.Properties.Resources.red;
            if (AI.load_graph_Defects_Weld)
            {
                pictureBox_NS_2.BackgroundImage = WSC_AI.Properties.Resources.green;
            }
            else pictureBox_NS_2.BackgroundImage = WSC_AI.Properties.Resources.red;
            if (AI.load_Session_Presence_Weld)
            {
                pictureBox_sess_1.BackgroundImage = WSC_AI.Properties.Resources.green;
            }
            else pictureBox_sess_1.BackgroundImage = WSC_AI.Properties.Resources.red;
            if (AI.load_Session_Defects_Weld)
            {
                pictureBox_sess_2.BackgroundImage = WSC_AI.Properties.Resources.green;
            }
            else pictureBox_sess_2.BackgroundImage = WSC_AI.Properties.Resources.red;
            pictureBox_NS_1.Refresh();
            pictureBox_NS_2.Refresh();
            pictureBox_sess_1.Refresh();
            pictureBox_sess_2.Refresh();



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
                Task.Run(() => REST_Indication());



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
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            pictureBox_opc.BackgroundImage = WSC_AI.Properties.Resources.green;
                            pictureBox_opc.Refresh();
                        }));
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                }
                else
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            pictureBox_opc.BackgroundImage = WSC_AI.Properties.Resources.red;
                            pictureBox_opc.Refresh();
                        }));
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                }
                
                Thread.Sleep(600);
                try
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        pictureBox_opc.BackgroundImage = WSC_AI.Properties.Resources.gray;
                        pictureBox_opc.Refresh();
                    }));
                }
                catch (Exception)
                {

                    
                }
                
                Thread.Sleep(600);
            }
        }

        private void REST_Indication()
        {
            while (true)
            {
                if (REST_RUN)
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            pictureBox_REST.BackgroundImage = Properties.Resources.green;
                            pictureBox_REST.Refresh();
                        }));
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                }
                else
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            pictureBox_REST.BackgroundImage = Properties.Resources.red;
                            pictureBox_REST.Refresh();
                        }));
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                }

                Thread.Sleep(600);
                try
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        pictureBox_REST.BackgroundImage = WSC_AI.Properties.Resources.gray;
                        pictureBox_opc.Refresh();
                    }));
                }
                catch (Exception)
                {

                  
                }
                
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
                        Mat image = cap.convertToMat(sample.GrabImage);

                        SaveImage(image, sample.GrabImage.Timestamp.ToString());

                        NumSharp.NDArray arr_weld = AI.load_vol(image, AI.size_weld_presence);

                        bool find_place = AI.weld_in_place(arr_weld);
                        if (find_place)
                        {
                            arr_weld = AI.load_vol(image, AI.size_weld_defect);
                            NumSharp.NDArray res = AI.weld_defects(arr_weld);

                            if (res.max() == 1)
                            {
                                var defectCoordinates = new DefectCoordinates();
                                double a = sample.Rx * (Math.PI / 180);
                                double b = sample.Ry * (Math.PI / 180);
                                double c = sample.Rz * (Math.PI / 180);

                                NDArray Rx = np.array(new double[,] {
                                { 1, 0, 0 },
                                { 0, Math.Cos(a), -Math.Sin(a)},
                                { 0, Math.Sin(a), Math.Cos(a)},
                                //{ 0, 0, 0, 1 }
                            });

                                NDArray Ry = np.array(new double[,] {
                                { Math.Cos(b), 0, Math.Sin(b) },
                                { 0, 1, 0},
                                { -Math.Sin(b), 0, Math.Cos(b)},
                                //{ 0, 0, 0, 1 }
                            });

                                NDArray Rz = np.array(new double[,] {
                                { Math.Cos(c), -Math.Sin(c), 0 },
                                { Math.Sin(c), Math.Cos(c), 0},
                                { 0, 0, 1},
                                //{ 0, 0, 0, 1 }
                            });


                                ////////////////////////////////////

                                NDArray V = np.array(new double[,] { { sample.X }, { sample.Y }, { sample.Z } });//, { 1 }



                                NDArray M = np.matmul(Rx, Ry);


                                M = np.matmul(M, Rz);
                                NDArray new_V = np.matmul(M, V);


                                defectCoordinates.X = new_V[0][0];
                                defectCoordinates.Y = new_V[1][0];
                                defectCoordinates.Z = new_V[2][0];
                                defectCoordinates.Xr = sample.Rx;
                                defectCoordinates.Yr = sample.Ry;
                                defectCoordinates.Zr = sample.Rz;

                                var defect = new Defect();
                                defect.DefectId = AI.INDEX_DEFECT;
                                defect.DefectCoordinates = defectCoordinates;

                                for (int i = 0; i < res.Shape[0]; i++)
                                {
                                    if (res[i] == 1)
                                    {

                                        string find_defect;
                                        bool result_find = AI.DICTIONARY_DEFECTS.TryGetValue(i, out find_defect);
                                        if (result_find)
                                        {
                                            defect.Descriptions.Add(find_defect);
                                        }
                                    }

                                }

                                Mat To_Base = new Mat();
                                Cv2.Resize(image, To_Base, AI.image2base);
                                //Cv2.CvtColor(To_Base, To_Base, ColorConversionCodes.BGR2RGB);

                                defect.ImageBase64 = Base64Image.Base64Encode(To_Base);

                                defects.Enqueue(defect);
                                AI.INDEX_DEFECT++;

                            }
                            
                        }
                        else
                        {
                            
                            

                            var defectCoordinates = new DefectCoordinates();
                            double a = sample.Rx * (Math.PI / 180);
                            double b = sample.Ry * (Math.PI / 180);
                            double c = sample.Rz * (Math.PI / 180);

                            NDArray Rx = np.array(new double[,] {
                                { 1, 0, 0 },
                                { 0, Math.Cos(a), -Math.Sin(a)},
                                { 0, Math.Sin(a), Math.Cos(a)},
                                //{ 0, 0, 0, 1 }
                            });

                            NDArray Ry = np.array(new double[,] {
                                { Math.Cos(b), 0, Math.Sin(b) },
                                { 0, 1, 0},
                                { -Math.Sin(b), 0, Math.Cos(b)},
                                //{ 0, 0, 0, 1 }
                            });

                            NDArray Rz = np.array(new double[,] {
                                { Math.Cos(c), -Math.Sin(c), 0 },
                                { Math.Sin(c), Math.Cos(c), 0},
                                { 0, 0, 1},
                                //{ 0, 0, 0, 1 }
                            });


                            ////////////////////////////////////

                            NDArray V = np.array(new double[,] { {sample.X}, { sample.Y }, { sample.Z } });//, { 1 }



                            NDArray M = np.matmul(Rx, Ry);


                            M = np.matmul(M, Rz);
                            NDArray new_V = np.matmul(M, V);


                            defectCoordinates.X = new_V[0][0];
                            defectCoordinates.Y = new_V[1][0];
                            defectCoordinates.Z = new_V[2][0];
                            defectCoordinates.Xr = sample.Rx;
                            defectCoordinates.Yr = sample.Ry;
                            defectCoordinates.Zr = sample.Rz;

                            var defect = new Defect();
                            defect.DefectId = AI.INDEX_DEFECT;
                            defect.DefectCoordinates = defectCoordinates;
                            defect.Descriptions.Add("Шов не обнаружен");

                            Mat To_Base = new Mat();
                            Cv2.Resize(image, To_Base, AI.image2base);
                            //Cv2.CvtColor(To_Base, To_Base, ColorConversionCodes.BGR2RGB);

                            defect.ImageBase64 = Base64Image.Base64Encode(To_Base);
                            
                            defects.Enqueue(defect);
                            AI.INDEX_DEFECT++; 
                        }

                        

                        GC.Collect();

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
            if (!AI.ERR_CONNECT)
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

                        OPC_client.Client.Disconnect();
                        OPC_client.Client.Dispose();

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
            else e.Cancel = false;


        }

        private void SaveImage(Mat img, String Timestamp)
        {
            String m_exePath = cap.ImageSavePath + "//" + DateTime.Now.ToShortDateString();
            String img_path = m_exePath + "//" + Timestamp + ".jpg";

            if (Directory.Exists(m_exePath))
            {
                try
                {
                    
                    Cv2.ImWrite(img_path, img);
                    
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
                    Cv2.ImWrite(img_path, img);
                }
                catch (Exception ex)
                {
                    LogWriter log = new LogWriter("Не удалось сохранить фотографию: " + img_path);
                }
            }
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            string code = UniqueMachineId();

            if (code!= "LENOVO:PF10F6F2" && code != "American Megatrends Inc.:Default string") //"LENOVO:PF10F6F2"
            {
                DialogResult res = MessageBox.Show(caption: "Ошибка доступа",
                        text: "ПО не предназначено для работы на этом ПК." + System.Environment.NewLine + "Обратитесь за поддержкой к разработчику: sergei.sisyukin@gmail.com",
                        buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                AI.ERR_CONNECT = true;
                this.Close();
            }
            
        }

        private void RunREST()
        {
            REST server = new REST();
        }

        string UniqueMachineId()
        {
            StringBuilder builder = new StringBuilder();

            String query = "SELECT * FROM Win32_BIOS";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            //  This should only find one
            foreach (ManagementObject item in searcher.Get())
            {
                Object obj = item["Manufacturer"];
                builder.Append(Convert.ToString(obj));
                builder.Append(':');
                obj = item["SerialNumber"];
                builder.Append(Convert.ToString(obj));
            }

            return builder.ToString();
        }

    }
}
