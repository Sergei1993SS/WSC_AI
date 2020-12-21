using System;
using Opc.UaFx.Client;
using System.Windows.Forms;


namespace WSC_AI
{
    class OPC : Globals
    {
        OpcClient Client;
         
        public OPC()
        {
            connect_opc:
            Client = new OpcClient(Server_Name);

            try
            {
                Client.Connect();
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Невозможно подключиться к OPC-серверу!");
                DialogResult res = MessageBox.Show(caption: "Ошибка подключения к OPC",
                    text: "Невозможно подключиться к OPC-серверу!" + System.Environment.NewLine + "Повторить подключение?",
                    buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Error, defaultButton: MessageBoxDefaultButton.Button3, options: MessageBoxOptions.ServiceNotification);

                if (res == DialogResult.Yes)
                {
                    goto connect_opc;
                    
                }
            }
        }

        public bool GetnisCameraInPosition()
        {
            get_node:

            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.isCameraInPosition");
                if (value.Status.IsGood)
                {
                    return (bool)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения isCameraInPosition");
                    goto get_node;

                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения isCameraInPosition");
                goto get_node;
            }
            
        }

        public void SetnisCameraInPositionFalse()
        {
        set_node:

            try
            {
                Opc.UaFx.OpcStatus status = Client.WriteNode("ns=1;s=MainChannel.MetrologPC.isCameraInPosition", false);
                if (!status.IsGood)
                {
                    LogWriter log = new LogWriter("Ошибка записи isCameraInPosition");
                    goto set_node;
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка записи isCameraInPosition");
                goto set_node;
            }
            

        }

        public void SetisCameraShotComplete()
        {

            set_node:
            try
            {
                Opc.UaFx.OpcStatus status = Client.WriteNode("ns=1;s=MainChannel.MetrologPC.isCameraShotComplete", true);
                if (!status.IsGood)
                {
                    LogWriter log = new LogWriter("Ошибка записи isCameraShotComplete");
                    goto set_node;
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка записи isCameraShotComplete");
                goto set_node;
            }
            
        }

        public double GetX()
        {
        get_node:

            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolX");
                if (value.Status.IsGood)
                {
                    return (double)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения ToolX");
                    goto get_node;

                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения ToolX");
                goto get_node;
            }
            
        }

        public double GetY()
        {
        get_node:
            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolY");
                if (value.Status.IsGood)
                {
                    return (double)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения ToolY");
                    goto get_node;

                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения ToolY");
                goto get_node;
            }
            
        }

        public double GetZ()
        {
        get_node:
            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolZ");
                if (value.Status.IsGood)
                {
                    return (double)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения ToolZ");
                    goto get_node;

                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения ToolZ");
                goto get_node;
            }
            
        }

        public double GetRx()
        {
        get_node:
            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolW");
                if (value.Status.IsGood)
                {
                    return (double)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения ToolW");
                    goto get_node;

                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения ToolW");
                goto get_node;
            }
            
        }

        public double GetRy()
        {
        get_node:
            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolR");
                if (value.Status.IsGood)
                {
                    return (double)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения ToolR");
                    goto get_node;

                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения ToolR");
                goto get_node;
            }
            
        }

        public double GetRz()
        {
        get_node:
            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolP");
                if (value.Status.IsGood)
                {
                    return (double)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения ToolP");
                    goto get_node;

                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения ToolP");
                goto get_node;  
            }
            
        }



    }
}
