using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefectMessageNamespace;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using OpenCvSharp;

namespace WSC_AI
{
    class SaverSession: Globals
    {
        public static void Save(List<Defect> defect_out, String camera_config, String PathSaveAppData)
        {
            AppData content = new AppData();
            content.camera_config = camera_config;
            content.defect_out = new List<Defect>();
            content.defect_out.AddRange(defect_out);

            String jsonString = JsonSerializer.Serialize(content);

            if (Directory.Exists(PathSaveAppData))
            {
                File.WriteAllText(PathSaveAppData + "\\" + "AppData.json", jsonString);
            }
            else
            {
                Directory.CreateDirectory(PathSaveAppData);
                File.WriteAllText(PathSaveAppData + "\\" + "AppData.json", jsonString);
            }
            LogWriter log = new LogWriter("Сессия сохранена(по завершению работы программы)");

            

        }

        public static AppData Load(String path_AppData)
        {
            String JsonString = File.ReadAllText(path_AppData);
            AppData content = JsonSerializer.Deserialize<AppData>(JsonString);

            return content;
        }

        public static List<Defect> TestSaver(String cam_config)
        {
            AppData content = new AppData();
            content.camera_config = cam_config;
            content.defect_out = new List<Defect>();

            Defect def = new Defect();
            def.DefectCoordinates = new DefectCoordinates();
            def.DefectCoordinates.X = 0;
            def.DefectCoordinates.Y = 1;
            def.DefectCoordinates.Z = 2;

            def.DefectId = 40;
            def.Descriptions = new List<String> { "Жопа гуся", "Страусиная пиписька" };

            Mat img = Cv2.ImRead("C:\\Users\\User\\OneDrive - 2050-integrator.com\\Marking DataSet Weld\\DataSet\\Set12\\IMAGES\\JPEG\\22.7.2020_13.8.41.jpg");
            def.ImageBase64 = Base64Image.Base64Encode(img);

            content.defect_out.Add(def);

            return content.defect_out;
        }


    }
}
