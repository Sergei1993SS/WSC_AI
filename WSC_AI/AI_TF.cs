using NumSharp;
using Tensorflow;
using System.IO;
using static Tensorflow.Binding;
using System; 
using System.Collections.Generic;
using OpenCvSharp;

namespace WSC_AI
{
    class AI_TF
    {
        

        public void Run()
        {

            String path = "C:\\Users\\User\\PycharmProjects\\AI_WCS_TRAIN\\utils/frozen_graph_defects.pb";

            Graph graph = new Graph();
            bool result = graph.Import(path);
            //Tensor image = ReadTensorFromImageFile("C:\\Users\\User\\Desktop\\DataSet_cls_def\\Set_no_weld\\IMAGES\\JPEG/000a4bcdd.jpg");

            string input_name = "x";
            string output_name = "Identity";
            Operation input_operation = graph.OperationByName(input_name);
            Operation output_operation = graph.OperationByName(output_name);

            String[] subdirectoryEntries = Directory.GetDirectories("C:\\Users\\User\\Desktop\\DataSet_cls_def\\Set1\\IMAGES");
            var Volume_Names = new List<string>();

            for (int i = 0; i < subdirectoryEntries.Length; i++)
            {
                Volume_Names.AddRange(Directory.GetFiles(subdirectoryEntries[i]));
            }


            // Convert list of volumes to string array
            String[] volume_names = Volume_Names.ToArray();


            for (int i = 0; i < volume_names.Length; i++)
            {
                // load volume
                Size size = new Size(1748, 1348); //1748, 1348 //612, 512

                NDArray vol = load_vol(volume_names[i], size);

               
                   

                    using (var sess = tf.Session(graph).as_default())
                    {
                     
                     NDArray network_out = sess.run(output_operation.outputs[0], new FeedItem(input_operation.outputs[0], vol));
                     network_out =  np.round_(network_out[0]);
                     var res = np.nonzero(network_out);
                     int pp = 0;
                    
                    }
       

            }

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
       
    }
}
