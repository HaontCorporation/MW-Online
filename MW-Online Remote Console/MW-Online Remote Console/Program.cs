using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MW_Online_Remote_Console
{
    class Program
    {
        public static string pass;
        public static string[] serv;


        public static bool Connected = false;
        public static StreamWriter swSender;
        public static StreamReader srReceiver;
        public static TcpClient tcpServer;


        static void Main(string[] args)
        {
            while (true)
            {

                try
                {

                    Console.Write("Server IP:Port - ");
                    serv = Console.ReadLine().Split(':');

                    Console.Write("RCON Password - ");
                    pass = Console.ReadLine();

                    Console.WriteLine("Connecting to " + serv[0] + ":" + serv[1] + ", with pass: " + pass);
                    Console.WriteLine();

                    tcpServer = new TcpClient();
                    tcpServer.Connect(serv[0], int.Parse(serv[1]));
                    tcpServer.SendTimeout = 30000;
                    Connected = true;
                    new Thread(msgWork).Start();

                    SendToServer("rcon|" + pass);
                    Thread.Sleep(500);
                    SendToServer("help");

                    while (Connected)
                    {
                        SendToServer(Console.ReadLine());
                    }

                }
                catch (Exception Exception) { Console.WriteLine(Exception.ToString()); Connected = false; }
            }
        }

        private static void msgWork()
        {
            try
            {
                srReceiver = new StreamReader(tcpServer.GetStream());
                while (true)
                {
                    string msg = srReceiver.ReadLine();
                    Console.WriteLine(msg);


                }
            }
            catch (Exception Exception) { Console.WriteLine(Exception.ToString()); Connected = false; }

        }
        public static void SendToServer(object text)
        {
            text = text.ToString();
            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(text.ToString());
            swSender.Flush();
            //Llog("MW-Online", "To server: " + text.ToString());
        }
    }
}
