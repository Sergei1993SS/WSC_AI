using System;
using System.Threading.Tasks;
using Grapevine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DefectMessageNamespace;
using System.Text.Json;


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
                    Main_Form.defect_out.Add(result);
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
            for (var i = start; i < end && i < Main_Form.defect_out.Count; i++)
            {

                defectMessage.Defects.Add(Main_Form.defect_out[i]);

            }

            string jsonString = JsonSerializer.Serialize(defectMessage);
            Main_Form.REST_RUN = true;
            await context.Response.SendResponseAsync(jsonString);
        }

        
    }
}
