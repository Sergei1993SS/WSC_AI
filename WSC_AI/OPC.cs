using System;
using Opc.UaFx.Client;
using System.Windows.Forms;


namespace WSC_AI
{
    class OPC : Globals
    {
        public OpcClient Client;
         
        public OPC()
        {
        connect_opc:
            Client = new OpcClient(Server_Name);
            //Client.DisconnectTimeout = 1000;
            //Client.SessionTimeout = 2147483647;  //2147483647     
            //Client.ReconnectTimeout = 20;
            

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
                    OPC_Connecting = true;
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

                LogWriter log = new LogWriter("Ошибка получения isCameraInPosition(Исключение). Переподключение к серверу");
                OPC_Connecting = false;
                try
                {
                    log = new LogWriter("Разрываем соеденение: " + Client.State.ToString());
                    Client.Disconnect();
                    log = new LogWriter("Разорвали соеденение: " + Client.State.ToString());
                }
                catch (Exception)
                {
                    log = new LogWriter("Ошибка разрыва: " + Client.State.ToString());

                    try
                    {
                        log = new LogWriter("Удаляем клиента ");
                        Client.Dispose();

                        log = new LogWriter("Создаем нового ");
                        Client = new OpcClient(Server_Name);
                        //Client.DisconnectTimeout = 1000;
                        //Client.SessionTimeout = 2147483647;  //2147483647     
                        //Client.ReconnectTimeout = 20;

                        try
                        {
                            log = new LogWriter("Соединяем " + Client.State.ToString());
                            Client.Connect();
                        }
                        catch (Exception)
                        {
                            log = new LogWriter("Неудчная попытка соеденения " + Client.State.ToString());

                        }
                    }
                    catch (Exception)
                    {

                        
                    }
                    

                    

                }

                Reconnect();
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
                else
                {
                    OPC_Connecting = true;
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка записи isCameraInPosition(Исключение). Переподключение");
                OPC_Connecting = false;
                try
                {
                    Client.Disconnect();
                }
                catch (Exception)
                {

                }

                Reconnect();
                goto set_node;
            }
            

        }

        public void SetisCameraShotComplete()
        {

            set_node:
            try
            {
                Opc.UaFx.OpcStatus status = Client.WriteNode("ns=1;s=MainChannel.MetrologPC.isCameraShotComplete", true);
                OPC_Connecting = false;
                if (!status.IsGood)
                {
                    LogWriter log = new LogWriter("Ошибка записи isCameraShotComplete");
                    goto set_node;
                }
                else
                {
                    OPC_Connecting = true;
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка записи isCameraShotComplete(Исключение). Переподключение");
                OPC_Connecting = false;
                try
                {
                    Client.Disconnect();
                }
                catch (Exception)
                {

                }

                Reconnect();
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
                    OPC_Connecting = true;
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

                LogWriter log = new LogWriter("Ошибка получения ToolX(Исключение). Переподключение");
                OPC_Connecting = false;
                try
                {
                    Client.Disconnect();
                }
                catch (Exception)
                {

                }

                Reconnect();
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
                    OPC_Connecting = true;
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

                LogWriter log = new LogWriter("Ошибка получения ToolY(Исключение). Переподключение");
                OPC_Connecting = false;
                try
                {
                    Client.Disconnect();
                }
                catch (Exception)
                {

                }

                Reconnect();
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
                    OPC_Connecting = true;
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

                LogWriter log = new LogWriter("Ошибка получения ToolZ(Исключение). Переподключение");
                OPC_Connecting = false;
                try
                {
                    Client.Disconnect();
                }
                catch (Exception)
                {

                }

                Reconnect();
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
                    OPC_Connecting = true;
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

                LogWriter log = new LogWriter("Ошибка получения ToolW(Исключение). Переподключение");
                OPC_Connecting = false;
                try
                {
                    Client.Disconnect();
                }
                catch (Exception)
                {

                }

                Reconnect();
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
                    OPC_Connecting = true;
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

                LogWriter log = new LogWriter("Ошибка получения ToolR(Исключение). Переподключение");
                OPC_Connecting = false;
                try
                {
                    Client.Disconnect();
                }
                catch (Exception)
                {

                }

                Reconnect();
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
                    OPC_Connecting = true;
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

                Reconnect();
                goto get_node;  
            }
            
        }

        private void Reconnect()
        {
            try
            {
                LogWriter log = new LogWriter("Подключаемся заново: " + Client.State.ToString());
                Client.Connect();
            }
            catch (Exception)
            {
                LogWriter log = new LogWriter("Неудачная попытка " + Client.State.ToString());
            }
        }



    }
}
