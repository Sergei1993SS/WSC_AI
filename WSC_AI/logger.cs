using System;
using System.IO;
using System.Reflection;

namespace WSC_AI
{
    class LogWriter
    {
        private string m_exePath = string.Empty;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//log";

            if (Directory.Exists(m_exePath))
            {
                try
                {
                    String date = DateTime.Now.ToShortDateString();
                    using (StreamWriter w = File.AppendText(m_exePath + "\\" + date + ".txt"))
                    {
                        Log(logMessage, w);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                Directory.CreateDirectory(m_exePath);
                try
                {
                    String date = DateTime.Now.ToShortDateString();
                    using (StreamWriter w = File.AppendText(m_exePath + "\\" + date + ".txt"))
                    {
                        Log(logMessage, w);
                    }
                }
                catch (Exception ex)
                {

                }
            }

           
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
