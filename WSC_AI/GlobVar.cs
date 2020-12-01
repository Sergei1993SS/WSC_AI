using System;
using System.Reflection;
using System.IO;


namespace WSC_AI
{
    public class Globals
    {

        //Путь к конфигу камеры
        public String CamConfigPath = "config/acA2440-20gc.pfs";

        //Максимальное количество снимков в буфере
        public UInt16 MaxBufferSize = 10; 
    }

    struct GrabImages
    {
        public byte[] buffer;
        public long time;
        public ushort Width;
        public ushort Height;

    }
    
}
