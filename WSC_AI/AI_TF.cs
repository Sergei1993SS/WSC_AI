using NumSharp;
using Tensorflow;
using System.IO;
//using static Tensorflow.Binding;
using System; 
using System.Collections.Generic;
using OpenCvSharp;
using System.Diagnostics;
using System.Windows.Forms;

namespace WSC_AI
{
    class AI_TF: Globals
    {
        private Session Session_Presence_Weld;
        private Session Session_Defects_Weld;

        Graph Graph_Presence_Weld;
        Graph Graph_Defects_Weld;

        private string input_name = "x";
        private string output_name = "Identity";
        private Operation input_operation_Presence_Weld;
        private Operation output_operation_Presence_Weld;

        private Operation input_operation_Defects_Weld;
        private Operation output_operation_Defects_Weld;

        public bool load_graph_Presence_Weld = false;
        public bool load_graph_Defects_Weld = false;
        public bool load_Session_Presence_Weld = false;
        public bool load_Session_Defects_Weld = false;

        public AI_TF()
        {
            this.Graph_Presence_Weld = new Graph();
            this.Graph_Defects_Weld = new Graph();

            ConfigProto config_Presence_Weld = new ConfigProto()
            {
                GpuOptions = new GPUOptions()
                {
                    VisibleDeviceList = "0", PerProcessGpuMemoryFraction = GPU_memory_Presence_Weld,
                    AllowGrowth = true,
                    
                }
            };

            ConfigProto config_Defects_Weld = new ConfigProto()
            {
                GpuOptions = new GPUOptions()
                {
                    VisibleDeviceList = "0", PerProcessGpuMemoryFraction = GPU_memory_Defects_Weld,
                    AllowGrowth = true,
                }
            };


            ////////////////GRAPH 1//////////////////
        draph_1:
            try
            {
                bool result = Graph_Presence_Weld.Import(WSC_AI.Properties.Resources.frozen_graph);

                if (result)
                {
                    LogWriter log = new LogWriter("Граф НС 1 успешно загружен");
                    this.load_graph_Presence_Weld = true;
                }
                else
                {
                    LogWriter log = new LogWriter("Граф НС 1 не загружен(без исключения)");
                    DialogResult res = MessageBox.Show(caption: "Ошибка загрузки графа для НС 1",
                        text: "Граф для НС не загружен!" + System.Environment.NewLine + "Повторить загрузку?",
                        buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                    if (res == DialogResult.Yes)
                    {
                        goto draph_1;
                    }

                    else
                    {
                        this.load_graph_Presence_Weld = false;
                    }
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Граф НС 1 не загружен(исключение)");
                DialogResult res = MessageBox.Show(caption: "Ошибка загрузки графа для НС 1",
                    text: "Граф для НС не загружен!" + System.Environment.NewLine + "Повторить загрузку?",
                    buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                if (res == DialogResult.Yes)
                {
                    goto draph_1;
                }

                else
                {
                    this.load_graph_Presence_Weld = false;
                }
            }

            this.input_operation_Presence_Weld = this.Graph_Presence_Weld.OperationByName(this.input_name);
            this.output_operation_Presence_Weld = this.Graph_Presence_Weld.OperationByName(this.output_name);

        ////////////////GRAPH 2//////////////////
        draph_2:
            try
            {
                bool result = Graph_Defects_Weld.Import(WSC_AI.Properties.Resources.frozen_graph_defects);

                if (result)
                {
                    LogWriter log = new LogWriter("Граф НС 2 успешно загружен");
                    this.load_graph_Defects_Weld = true;
                }
                else
                {
                    LogWriter log = new LogWriter("Граф НС 2 не загружен(без исключения)");
                    DialogResult res = MessageBox.Show(caption: "Ошибка загрузки графа для НС 2",
                        text: "Граф для НС не загружен!" + System.Environment.NewLine + "Повторить загрузку?",
                        buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                    if (res == DialogResult.Yes)
                    {
                        goto draph_2;
                    }

                    else
                    {
                        this.load_graph_Defects_Weld = false;
                    }
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Граф НС 2 не загружен(исключение)");
                DialogResult res = MessageBox.Show(caption: "Ошибка загрузки графа для НС 2",
                    text: "Граф для НС не загружен!" + System.Environment.NewLine + "Повторить загрузку?",
                    buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                if (res == DialogResult.Yes)
                {
                    goto draph_2;
                }

                else
                {
                    this.load_graph_Defects_Weld = false;
                }
            }


            this.input_operation_Defects_Weld = this.Graph_Defects_Weld.OperationByName(this.input_name);
            this.output_operation_Defects_Weld = this.Graph_Defects_Weld.OperationByName(this.output_name);


        ////////////////SESSION 1//////////////////
        sess_1:
            try
            {
                Session_Presence_Weld = new Session(this.Graph_Presence_Weld, config_Presence_Weld);
                LogWriter log = new LogWriter("Сессия НС 1 запущена");
                this.load_Session_Presence_Weld = true;

            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Сессия НС 1 не запущена(исключение)");
                DialogResult res = MessageBox.Show(caption: "Ошибка запуска сессии для НС 1",
                    text: "Сессия для НС не запущена!" + System.Environment.NewLine + "Повторить запуск?",
                    buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                if (res == DialogResult.Yes)
                {
                    goto sess_1;
                }

                else
                {
                    this.load_Session_Presence_Weld = false;
                }
            }

        ////////////////SESSION 2//////////////////
        sess_2:
            try
            {
                this.Session_Defects_Weld = new Session(Graph_Defects_Weld, config_Defects_Weld);
                LogWriter log = new LogWriter("Сессия НС 2 запущена");
                this.load_Session_Defects_Weld = true;

            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Сессия НС 2 не запущена(исключение)");
                DialogResult res = MessageBox.Show(caption: "Ошибка запуска сессии для НС 2",
                    text: "Сессия для НС не запущена!" + System.Environment.NewLine + "Повторить запуск?",
                    buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                if (res == DialogResult.Yes)
                {
                    goto sess_2;
                }

                else
                {
                    this.load_Session_Defects_Weld = false;
                }
            }



        }

        public void Run()
        {

            String path = "C:\\Users\\User\\PycharmProjects\\AI_WCS_TRAIN\\utils/frozen_graph_defects.pb";

            Graph graph = new Graph();
            ConfigProto config = new ConfigProto()
            {
                GpuOptions = new GPUOptions()
                {
                    VisibleDeviceList = "0"//, PerProcessGpuMemoryFraction = 1,
                    //AllowGrowth = true,
                } 
            };


            
            

            bool result = graph.Import(path); //WSC_AI.Properties.Resources.frozen_graph_defects

            string input_name = "x";
            string output_name = "Identity";
            Operation input_operation = graph.OperationByName(input_name);
            Operation output_operation = graph.OperationByName(output_name);

            String[] subdirectoryEntries = Directory.GetDirectories("C:\\Users\\User\\Desktop\\DataSet_cls_def\\Set45\\IMAGES");
            var Volume_Names = new List<string>();

            for (int i = 0; i < subdirectoryEntries.Length; i++)
            {
                Volume_Names.AddRange(Directory.GetFiles(subdirectoryEntries[i]));
            }


            // Convert list of volumes to string array
            String[] volume_names = Volume_Names.ToArray();
            Stopwatch sw = new Stopwatch();

            
            Session sess_1 = new Session(graph, config);
            

            //using (Session sess = tf.Session(graph, config))
            //{

                for (int i = 0; i < volume_names.Length; i++)
                {
                    // load volume
                    Size size = new Size(1748, 1348); //1748, 1348 //612, 512

                    NDArray vol = load_vol(volume_names[i], size);




                    sw.Start();

                    NDArray network_out = sess_1.run(output_operation.outputs[0], new FeedItem(input_operation.outputs[0], vol));
                    network_out = np.round_(network_out[0]);
                    




                    sw.Stop();
                    long time = sw.ElapsedMilliseconds;
                    sw.Reset();
                    GC.Collect();
                    int ppp = 0;


                }
            //}

            
            
            int p = 0;
            
        }


        private NDArray load_vol(String file_name, Size size)
        {
            Mat img = Cv2.ImRead(file_name);
            Cv2.Resize(img, img, size);
            Cv2.CvtColor(img, img, ColorConversionCodes.BGR2RGB);
            Mat newimg  = img.Reshape(1);

            
            byte[] imageArray = new byte[img.Height*img.Width* 3];

            newimg.GetArray(0, 0, imageArray);
            NDArray Res = np.array(imageArray);
            Res = Res.reshape(new Shape(img.Height, img.Width, 3));
            

            Res = np.divide(Res, 255.0);

            //byte[] data = Res.ToByteArray();
            //Mat immm = new Mat(size.Height, size.Width, MatType.CV_8UC3, data);
            //Cv2.ImShow("test", immm.Clone());
            //Cv2.WaitKey();

            Res = np.expand_dims(Res, axis: 0);
            
            return Res;
        }

        public NumSharp.NDArray load_vol(Mat img, Size size)
        {
            Mat Image = new Mat();
            img.CopyTo(Image);
            Cv2.Resize(Image, Image, size);
            Cv2.CvtColor(Image, Image, ColorConversionCodes.BGR2RGB);
            Mat newimg = Image.Reshape(1);
            byte[] imageArray = new byte[Image.Height * Image.Width * 3];

            newimg.GetArray(0, 0, imageArray);
            NumSharp.NDArray Res = np.array(imageArray);
            Res = Res.reshape(new Shape(Image.Height, Image.Width, 3));

            Res = np.divide(Res, 255.0);
            Res = np.expand_dims(Res, axis: 0);

            return Res;
        }


        public NDArray weld_defects(NumSharp.NDArray arr)
        {
            NumSharp.NDArray network_out = this.Session_Defects_Weld.run(this.output_operation_Defects_Weld.outputs[0], new FeedItem(this.input_operation_Defects_Weld.outputs[0], arr));
            return network_out[0];
        }

        public bool weld_in_place(NDArray arr)
        {
            NumSharp.NDArray network_out = this.Session_Presence_Weld.run(this.output_operation_Presence_Weld.outputs[0], new FeedItem(this.input_operation_Presence_Weld.outputs[0], arr));
            float score = (float)network_out[0][0];

            if (score > threshold_weld)
            {
                return true;
            }
            else return false;
        }

        

    }
}
