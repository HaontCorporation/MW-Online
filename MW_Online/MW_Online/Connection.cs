using NFSScript;
using NFSScript.MW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MW_Online
{
    public static class Connection
    {

        public static IPAddress connectIp;
        public static int connectPort;

        public static bool Connected = false;
        public static StreamWriter swSender;
        public static StreamReader srReceiver;
        public static TcpClient tcpServer;

        public static string NickName = "Player";

        public static void StartMWOConnection()
        {
            if (!Connected)
            {
                MW_Online.ShowLoading();


                try
                {
                    tcpServer = new TcpClient();
                    tcpServer.Connect(connectIp, connectPort);
                    tcpServer.SendTimeout = 30000;


                    MW_Online.InitializeCarSlots();

                    Connected = true;

                    MsgWorker.StartWorker();

                    SendToServer(NickName);



                    MW_Online.HideLoading();

                }
                catch (Exception ex)
                {
                    string m = String.Format("Connection error: {0}", ex.Message); Log.Print("MW-Online", m+"\r\n"+ex.ToString());
                    MW_Online.HideLoading();
                    Thread.Sleep(1000);
                    GameDialog.ShowStableDialog(0, m);
                }


            }
            else
            {
                UI.ShowTextScreenMessage("Sorry, but you need to disconnect firstly.\nPress N to disconnect!");
            }
        }
        public static void SendToServer(object text)
        {
            text = Privatcom_Basic_Server.Base64.Encrypt(text.ToString());
            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(text.ToString());
            swSender.Flush();
            //Llog("MW-Online", "To server: " + text.ToString());
        }
        public static void CloseConnection()
        {
            if (Connected)
            {

                try
                {
                    Connected = false;
                    try
                    {
                        swSender.Close();
                    }
                    catch { }
                    try
                    {
                        srReceiver.Close();
                    }
                    catch { }
                    try
                    {
                        tcpServer.Close();
                    }
                    catch { }

                }
                catch { }
            }
            Thread.Sleep(500);
            MW_Online.InitConnection();
        }
        // todo
    }
}
