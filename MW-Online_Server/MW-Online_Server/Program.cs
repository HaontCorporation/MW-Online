using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MW_Online_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(GcCollect).Start();

            Console.Title = "MW-Online Server";
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif
            config.InitConfig();
            Console.WriteLine("Starting MW-Online server on port " + config.Port);
            Server.StartListening();


            string cmd;
            while (true)
            {
                try
                {
                    cmd = Console.ReadLine();
                    CommandProcessing(cmd);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private static void RCWrite(string text, TcpClient tcp = null)
        {
            Console.WriteLine(text);
            if(tcp != null)Server.SendToUser(tcp, text, true);
        }
        public static void CommandProcessing(string cmd, TcpClient tcp = null)
        {
            if (cmd.ToLower().StartsWith("setai"))
            {
                string[] r = cmd.Split(' ');
                if (r.Length < 2)
                {
                    RCWrite("Usage: SetAI <player id/all> <memory addr (1-50)/all> <type|string>", tcp);
                }
                else
                {
                    int pid = int.Parse(r[1]);
                    int mad = -1;
                    if (!r[2].ToLower().StartsWith("all")) mad = int.Parse(r[2]);
                    string type = r[3];

                    if (mad == -1)
                    {
                        int t = 1;
                        while (t != 50)
                        {
                            Server.SendToUser(Server.TcpById(pid), "SetAI#" + t + "#" + type);
                        }
                    }
                    else
                    {
                        Server.SendToUser(Server.TcpById(pid), "SetAI#" + mad + "#" + type);
                    }
                }
            }
            else if (cmd.ToLower().StartsWith("players"))
            {
                RCWrite("Total: " + Server.Users.Count, tcp);
                for (int a = 0; a < Server.Users.Count; a++)
                {
                    RCWrite(String.Format("ID: {0}, Nick: {1}, IP: {2}", Server.Users[a].PlayerID, Server.Users[a].Nickname, Server.Users[a].IP.ToString()), tcp);
                }
            }
            else if (cmd.ToLower().StartsWith("ban"))
            {
                string[] r = cmd.Split(' ');
                if (r.Length < 1)
                {
                    RCWrite("Usage: ban <player id> <reason/nothing>", tcp);
                }
                else
                {
                    int id = int.Parse(r[1]);
                    string reason = "Without reason";
                    // ban 0; id 1; reason >=2...
                    if (r.Length > 1)
                    {
                        // OMG..
                        int idx = 2;
                        while (idx != r.Length)
                        {
                            reason += r[idx];
                            idx++;
                        }


                    }
                    RCWrite("NOT IMPLEMENTED!!!", tcp);
                    RCWrite(Server.InfoFromTcp(Server.TcpById(id)).Nickname + " was banned with reason: " + reason, tcp);
                }
            }
            else if (cmd.ToLower().StartsWith("kick"))
            {
                string[] r = cmd.Split(' ');
                if (r.Length < 1)
                {
                    RCWrite("Usage: kick <player id> <reason/nothing>", tcp);
                }
                else
                {
                    int id = int.Parse(r[1]);
                    string reason = "Without reason";
                    // ban 0; id 1; reason >=2...
                    if (r.Length > 1)
                    {
                        // OMG..
                        int idx = 2;
                        while (idx != r.Length)
                        {
                            reason += r[idx];
                            idx++;
                        }


                    }
                    //RCWrite("NOT IMPLEMENTED!!!");
                    RCWrite(Server.InfoFromTcp(Server.TcpById(id)).Nickname + " was kicked with reason: " + reason, tcp);
                    Server.RemoveUser(Server.TcpById(id));
                }
            }
        }

        static void GcCollect()
        {
            while (true)
            {
                Thread.Sleep(1000);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Server.WriteErrorMessage(((Exception)e.ExceptionObject).ToString());
            StreamWriter writer = new StreamWriter("server_errorlog.log");
            string l = String.Format("{0}\r\n\r\n{1}", ((Exception)e.ExceptionObject).Message, ((Exception)e.ExceptionObject).StackTrace);
            writer.Write(l);
            writer.Flush();
            writer.Close();
            Server.WriteErrorMessage("SERVER CRASHED!");
            Console.Beep();
            Thread.Sleep(30000);
        }
    }
}
