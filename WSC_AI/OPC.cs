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

        public void SetisCameraShotComplete()
        {

            set_node:
            Opc.UaFx.OpcStatus status = Client.WriteNode("ns=1;s=MainChannel.MetrologPC.isCameraShotComplete", 1);
            if (!status.IsGood)
            {
                LogWriter log = new LogWriter("Ошибка записи isCameraShotComplete");
                goto set_node;
            }
        }

        public float GetX()
        {
        get_node:
            Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolX");
            if (value.Status.IsGood)
            {
                return (float)value.Value;
            }
            else
            {
                LogWriter log = new LogWriter("Ошибка получения ToolX");
                goto get_node;

            }
        }

        public float GetY()
        {
        get_node:
            Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolY");
            if (value.Status.IsGood)
            {
                return (float)value.Value;
            }
            else
            {
                LogWriter log = new LogWriter("Ошибка получения ToolY");
                goto get_node;

            }
        }

        public float GetZ()
        {
        get_node:
            Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolZ");
            if (value.Status.IsGood)
            {
                return (float)value.Value;
            }
            else
            {
                LogWriter log = new LogWriter("Ошибка получения ToolZ");
                goto get_node;

            }
        }

        public float GetRx()
        {
        get_node:
            Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolW");
            if (value.Status.IsGood)
            {
                return (float)value.Value;
            }
            else
            {
                LogWriter log = new LogWriter("Ошибка получения ToolW");
                goto get_node;

            }
        }

        public float GetRy()
        {
        get_node:
            Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolR");
            if (value.Status.IsGood)
            {
                return (float)value.Value;
            }
            else
            {
                LogWriter log = new LogWriter("Ошибка получения ToolR");
                goto get_node;

            }
        }

        public float GetRz()
        {
        get_node:
            Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.ToolP");
            if (value.Status.IsGood)
            {
                return (float)value.Value;
            }
            else
            {
                LogWriter log = new LogWriter("Ошибка получения ToolP");
                goto get_node;

            }
        }



    }
}
