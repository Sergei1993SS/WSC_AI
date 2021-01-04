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

            String path = "C:\\Users\\User\\PycharmProjects\\AI_WCS_TRAIN\\utils/frozen_graph.pb";

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
                Tuple<int, int> size = new Tuple<int, int>(512, 612);
                
                NDArray vol = load_vol(volume_names[i], size);

               
                   

                    using (var sess = tf.Session(graph).as_default())
                    {
                        //stopwatch.Start();
                        var network_out = sess.run(output_operation.outputs[0], new FeedItem(input_operation.outputs[0], vol));
                        //stopwatch.Stop();

                        //Console.WriteLine("\n");
                        //Console.Write("Time elapsed = ");
                        //Console.WriteLine(stopwatch.ElapsedMilliseconds);
                        //Console.WriteLine("\n");

                        //stopwatch.Reset();

                    }
       

            }

            int p = 0;
            
        }


        private NDArray load_vol(String file_name, Tuple<int, int> size)
        {
            Mat img = Cv2.ImRead(file_name);

            byte[] imageArray = new byte[img.Height*img.Width*3];
            Mat newimage = img.Reshape(1);
            newimage.GetArray(0,0, imageArray);
            NDArray Res = np.array(imageArray, dtype: np.uint8).reshape(img.Rows, img.Cols, img.Channels());
            
            return Res;
        }
       
    }
}
