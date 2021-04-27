using System;
using Basler.Pylon;
using OpenCvSharp;
using System.Collections.Generic;
using DefectMessageNamespace;


namespace WSC_AI
{
    public class Globals
    {



        //Максимальное количество снимков в буфере
        public UInt16 MaxBufferSize = 10;

        public String Server_Name = "opc.tcp://127.0.0.1:4334";

        public String ImageSavePath = "D:\\Images";
        public int SleepProcessCam = 500;
        public int SleepProcessImage = 1000;
        public volatile bool OPC_Connecting;
        public volatile int INDEX_DEFECT = 0;
        public bool ERR_CONNECT = false;

        public double GPU_memory_Presence_Weld = 0.1;
        public double GPU_memory_Defects_Weld = 0.9;

        public Size size_weld_defect = new Size(2448, 2048);
        public Size size_weld_presence = new Size(948, 548);
        public Size image2base = new Size(948, 548);


        public float threshold_weld = 0.3f;
        public float threshold_defect = 0.65f;

        public Dictionary<int, String> DICTIONARY_DEFECTS = new Dictionary<int, string> {
            { 0, "Шлаклаковые включения" },
            { 1, "Прожог или Поры или свищ" },
            { 2, "Брызги металла"},
            { 3, "Кратер или раковина"},
            { 4, "Шов отсутствует"} };


        // Пути выгрузки статистики в Excel
        public String path_stat_defects = "C:\\Users\\0\\YandexDisk\\Статистика дефектов";
        public String NameExcelBook = "Статистика дефектов.xlsx";

        //Списки управляющих программ Metrolog from HMI(Заполнить)
        public List<String> Programms_Metro = new List<String> {};
        public List<String> Programms_875 = new List<String> { "875.00w", "875.01w" };

        //Путь к конфигу камеры
        public String CamConfigPath_875 = "config/acA2440-20gc.pfs";
        public String CamConfigPath_Metro = "config/acA2440-20gc.pfs";
        public static String CurrentCamConfig = "";

        public static String PathSaveAppData = ".AppData";
    }

    struct TScan_and_Images
    {
        public double X;
        public double Y;
        public double Z;
        public double Rx;
        public double Ry;
        public double Rz;
        public IGrabResult GrabImage;

        public TScan_and_Images(IGrabResult DRI, double _X, double _Y, double _Z, double _Rx, double _Ry, double _Rz)
        {
            GrabImage = DRI;
            X = _X;
            Y = _Y;
            Z = _Z;
            Rx = _Rx;
            Ry = _Ry;
            Rz = _Rz;

        }
    }

    struct AppData
    {
        public List<Defect> defect_out { get; set; }
        public String camera_config { get; set; }
    }
    
}
