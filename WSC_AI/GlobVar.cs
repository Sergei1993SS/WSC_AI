using System;
using Basler.Pylon;


namespace WSC_AI
{
    public class Globals
    {

        //Путь к конфигу камеры
        public String CamConfigPath = "config/acA2440-20gc.pfs";

        //Максимальное количество снимков в буфере
        public UInt16 MaxBufferSize = 10; 

        public String Server_Name= "opc.tcp://127.0.0.1:4334";
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
    
}
