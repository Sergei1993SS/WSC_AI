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
        Opc.UaFx.Client.Licenser.LicenseKey = "AALOERR5OO7EKFNQCABINGCH6TYOVHPLFC2QCUIATTIA35ZH2FBB4RMJWSGEODKTOKZ5BJWGHUWWXQ5HKWI7OFVYYMERDPQDC7ZW7ZTLI3NLGIMY3JMHD7IR46XMUIW2YBBPBRGK6SBRI4DBXF4NGVKZUATMW3VI7EALG5FQN2ETIIPJG7OTOCL2FPO55TO5D4GPJROX5FHSUSALQX56E6NCBRCDX35VJBWLQDD4QANXIWUKO7D3Q7SWDDL55ZZCSN7NLHKB3W5O524VIXFPLVJIYKM5EFDDR4IC56Y43VTNGYNZZWB4NIKTRJS2N2MBDLYUUDDNUH5YP42N7JQCOCZZHP3WWLGR2XUCXTZTA6MWSNGKK5LZDJUAEBGZIS5TKIITSHS5GC5ETEA44EFXX3CAE2ON35X5EUOMRWQAV6ADNRYIWUVOJA4ZRTT6D6ZPFCO4VUPEMTK3YLTINJTIAYT7K6AUKRF2UDQZBQ4GXQYJLV3JC6WT47BEXCKNARSUOY76L25CD6OC32YH64Q3ERU2X4724A5TF2QGZCDW6KIZP4M4WXA4HU5MNCV5424EHGDU4CHTYY2TL5GUCW6D26O4DVV54GT36HY5EO4MKCENXAJO7SBEYTNOUVCWNRI7E7DE2FSFGSF75MEVWF5UZP2JZA5FL7C33JTXDSPHMNYPL4KMJSDQPHR2KQ4ZHIGAFXKTY5AE34DLEHG4TOYMWKLFWRYEUE5Z7D5JH6L3TB66BTGAOWHZDBBXVET5WYRM74CPGXJ5CYPP3SIBJQZQZRHFU5EC2R4SIMFICZAD4WGZZ3NXJ2DU6RTVFWAVDRMFKAQNKO7PR2NVFMHJPGCGAU5NXL6MWYN2YVH4FQ6IZNDU3ZMYSMFYJIWFTMV3MSJ2IKZIFLV4SM474UNZQGGVNLXLXE5OUPAM3CCH2QQBEOFIX4LPMCD5G3X32XE3VABJKS7MDDET4POOHBOD2AANZEU5ZIJPXQYTMF2VNAFVOEJDJYF6HTRV4ZCM2GSIKSP4OWGFX7RST7OXL5EI2ITZHJ3COH5GLJ6XAXIVWDJWJMG2X75P2PK4BDMPPETA4NT2ASWX2CUOWJCB6OB2ZC2HYWAZVMMQF4A3AMNKKGLNZ4CGLFVWSK34ORZAZVQCMOMH3BEBAN6EBVMQLS33TRZSBGJVFE7YFP7GLPTO4OKMDTF457QB43QIY3YJQ6LXVVDKL2I5SMJ57QT6Y3VSF6ZMOLFGCXOU5GZI7QHNGHLUJPOSNM4H6IUA";
        connect_opc:
            Client = new OpcClient(Server_Name, new Opc.UaFx.OpcSecurityPolicy(Opc.UaFx.OpcSecurityMode.None));

            

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


        public bool GetisCameraVideoReady()
        {
        get_node:

            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.isCameraVideoReady");
                if (value.Status.IsGood)
                {
                    OPC_Connecting = true;
                    return (bool)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения isCameraVideoReady");
                    goto get_node;

                }


            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения isCameraVideoReady(Исключение). Переподключение к серверу");
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

                
                goto get_node;
            }

        }

        public string GetDetalFromHMI()
        {
        get_node:

            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.DetalCodeForHMI");
                if (value.Status.IsGood)
                {
                    OPC_Connecting = true;
                    return (string)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения DetalCodeForHMI");
                    goto get_node;

                }


            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения DetalCodeForHMI(Исключение). Переподключение к серверу");
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


                goto get_node;
            }

        }

        public void SetnisCameraVideoReadyFalse()
        {
        set_node:

            try
            {
                Opc.UaFx.OpcStatus status = Client.WriteNode("ns=1;s=MainChannel.MetrologPC.isCameraVideoReady", false);
                if (!status.IsGood)
                {
                    LogWriter log = new LogWriter("Ошибка записи isCameraVideoReady");

                    goto set_node;
                }
                else
                {
                    OPC_Connecting = true;
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка записи isCameraVideoReady(Исключение). Переподключение");
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

        public string GetDetalCodeFromHMI()
        {
        get_node:
            try
            {
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.DetalCodeFromHMI");
                if (value.Status.IsGood)
                {
                    OPC_Connecting = true;
                    String Value = (String)value.Value;
                    return Value.Replace("\0", string.Empty); ;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения DetalCodeFromHMI");
                    goto get_node;
                }
            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения DetalCodeFromHMI(Исключение). Переподключение");
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
