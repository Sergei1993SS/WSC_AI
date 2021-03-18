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
            //Opc.UaFx.Licenser.LicenseKey = "AALSA4IRQPJKOGVQOZP32DTKRRNAIWS3CQLTG75JF7VGJHNQBWPDAKWOJUDMZNFK62UNQOEUY5KDHHJWYQSTXUITOEOGOOFL2R5TEIIMAWG45JF6BQNDDENE3D427GF5T4YNSD7UIS5L2HI2V2HYXG5TXMFQPZUW7HFP6CWPSXT5BW4INKYDGVAUX6A5OZIKRVWV4WXW46FUFH2MPIEPRPR2CSLVAP75YKJ2NO2J4VPCCUXKDN255RH66IS2C2GUAIADK52AHKJRSW2LMTHPFMHHJ2O6ROVHZEEMVD7WSYXG2LDCH333ND73AJT3QJZ5V6H2C75CPMFYOD6OAJ5UGSMSSAMLUYWOJAAE4CA7IDHQI6LTVC5VNMZKHKB3BLPQUFEUVFBJ5TAMC4VRR5KO54RULUCVCUYDXLNTOMUOMFDMSG4PUR6OUEG5Q6D427ZPWJMFRLDGUOGH6L4SHD2PVITC5AAWYPVDBFOVY6NL65M2YWYN3UC5TQXLSQLOKLAKMJANUBSBCUU2OTBDZLYMCQSC2PH36DRZTVUOOJ2BMPXAA6RDH4DLWPCELF3VP7OYJRDRZTMPWKTGPO4PDHDZ4VCEO2IB3JWVMNE7VO43JJX4LNFACEVEKEI2YPV6SOXQOCP5TQERRYPR76SXQFX6B4FRF7RINRH7XSF26TJXZSMWJGRZLBQ2WAJDVBDNC3F3UFEQUCTRLSNC6C7UKE64IFIJGPRJD7QLUA6ZVVCUW3YUC6UND6RUIH5UE2GAQI3LCHUCFTO4Y4SAPNXKLDBIMNJUWW6PVDMD2S2SW4ZFEG5VQJ7PCFGJME5PVB7EKU3JBY3LCTIIKBJSBIIZ6FBJF2OMNDWY7BZ6NAUV3U7O7GJKNNNZDYDSEM7K6KYTOVSHYAFBTAPMNZCXJF45KCCIHN2DOCMM5LAZAS3PRBDN7IY4CTMXVJOF45KJC6KTVYK3VCADUANHRKTTNPVUMD5Z6BSVV4EYKMGHNLHIRYLAFDYPJBTVWDQWLPCRIVCHIDVF5YLAUOV2HWOGEG7OTEZFYXSRGCXI75JCJASXGBE62CMFSJ4FSVSXJZFIXEQOMMSE3RWJWBLLWSXPH7ZU7XMSZCAEYK5WQSMAYAQTJYLA47YM7C5I77NPYLNXZ7GB6Y2CKJSI3ACIGOZ3VIKRGHB5BTZYT5PYBVZKG6GECTXXFXWBYB7E2H4GV46KISEOZJZVEISV7NWLHUJ7DIYLYS7DVKPEGRMQJPTMEM5BCPODAP5X4FODBO4X5N2ZIK24JVEBC2FCO26Z6TTC2OXG3R65QRYDMK5LMZM73NRJ5BCJ72N7ZW34YHZT7FLIGF7IO2OHUYMVSHMRAVHJJHM6MKXXHLFEBH7U4X66O7HQCUA57XRF26JREXEQCDLJZBJNX5LFEDAKXWG6SDBRKWA2WVQ5NFC7";
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
                Opc.UaFx.OpcValue value = Client.ReadNode("ns=1;s=MainChannel.MetrologPC.DetalCodeFromHMI");
                if (value.Status.IsGood)
                {
                    OPC_Connecting = true;
                    return (string)value.Value;
                }
                else
                {
                    LogWriter log = new LogWriter("Ошибка получения DetalFromHMI");
                    goto get_node;

                }


            }
            catch (Exception)
            {

                LogWriter log = new LogWriter("Ошибка получения DetalFromHMI(Исключение). Переподключение к серверу");
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
                    return (string)value.Value;
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
