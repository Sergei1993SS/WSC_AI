using System;
using System.Threading.Tasks;
using Grapevine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DefectMessageNamespace;
using System.Text.Json;
using OpenCvSharp;


namespace WSC_AI
{
    [RestResource]
    class REST
    {
        
        public REST()
        {
            Action<IServiceCollection> configServices = (services) =>
            {
                services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace);
            };

            Action<IRestServer> configServer = (server) =>
            {
                server.Prefixes.Add("http://localhost:5000/");
            };
            IRestServer serv = new RestServerBuilder(new ServiceCollection(), configServices, configServer).Build();

            try
            {
                serv.Start();
                Main_Form.REST_RUN = true;
                LogWriter log = new LogWriter("REST API сервер запущен");
                
                
                


            }
            catch (Exception)
            {

                LogWriter log = new LogWriter(" Не удалось запустить REST API сервер");
                Main_Form.REST_RUN = false;


            }
        }

        public double lengh_S(double X1, double Y1, double Z1, double X2, double Y2, double Z2)
        {
            double S = Math.Sqrt(Math.Pow(X1 - X2, 2.0) + Math.Pow(Y1 - Y2, 2.0) + Math.Pow(Z1 - Z2, 2.0));
            return S;
        }

        private Mat VerticalConcat(Mat image1, Mat image2)
        {
            var smallImage = image1.Cols < image2.Cols ? image1 : image2;
            var bigImage = image1.Cols > image2.Cols ? image1 : image2;
            Mat combine = Mat.Zeros(new Size(Math.Abs(image2.Cols - image1.Cols), smallImage.Height), image2.Type());
            Cv2.HConcat(smallImage, combine, combine);
            Cv2.VConcat(bigImage, combine, combine);
            return combine;
        }

        /// <summary>
        /// REST API
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [RestRoute("Get", "/defects/")]
        public async Task Test(IHttpContext context)
        {

            while (Main_Form.defects.Count != 0)
            {
                Defect result;
                if (Main_Form.defects.TryDequeue(out result))
                {
                    if (Main_Form.defect_out.Count>0)
                    {

                        if (lengh_S(Main_Form.defect_out[Main_Form.defect_out.Count - 1].DefectCoordinates.X,
                            Main_Form.defect_out[Main_Form.defect_out.Count - 1].DefectCoordinates.Y,
                            Main_Form.defect_out[Main_Form.defect_out.Count - 1].DefectCoordinates.Z,
                            result.DefectCoordinates.X,
                            result.DefectCoordinates.Y,
                            result.DefectCoordinates.Z) < 25.0) 
                        {

                            foreach (var item in result.Descriptions)
                            {
                                if (!Main_Form.defect_out[Main_Form.defect_out.Count - 1].Descriptions.Contains(item))
                                {
                                    Main_Form.defect_out[Main_Form.defect_out.Count - 1].Descriptions.Add(item);
                                }
                            }

                            Main_Form.defect_out[Main_Form.defect_out.Count - 1].ImageBase64 = Base64Image.Base64Encode(
                                VerticalConcat(Base64Image.Base64Decode(Main_Form.defect_out[Main_Form.defect_out.Count - 1].ImageBase64),
                                Base64Image.Base64Decode(result.ImageBase64)));
                        }
                        else
                        {
                            Main_Form.defect_out.Add(result);
                        }
                    }
                    else
                    {
                        Main_Form.defect_out.Add(result);
                    }
                    
                }
            }

            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With");

            var start = 0;
            var end = 100;
            var model = "875.004.01";



            if (context.Request.QueryString["start"] != null)
            {
                start = int.Parse(context.Request.QueryString["start"]);
            }
            if (context.Request.QueryString["end"] != null)
            {
                end = int.Parse(context.Request.QueryString["end"]);
            }
            if (context.Request.QueryString["model"] != null)
            {
                model = context.Request.QueryString["model"];
            }

            var defectMessage = new DefectMessage();
            defectMessage.ModelId = model;
            defectMessage.TotalDefects = Main_Form.defect_out.Count;
            for (var i = start;  i < Main_Form.defect_out.Count; i++)
            {

                defectMessage.Defects.Add(Main_Form.defect_out[i]);

            }

            string jsonString = JsonSerializer.Serialize(defectMessage);
            Main_Form.REST_RUN = true;
            await context.Response.SendResponseAsync(jsonString);
        }

        
    }
}
