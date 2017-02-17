using NFSScript.Math;
using Privatcom_Basic_Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


/*
 * public static string mmtext = "Info about connection\nAll players\nStop current mission";
        if (msg.StartsWith("GiveMeMainMenuText"))
            {
                SendToUser(user, "MmText#" + mmtext);
            }*/
namespace MW_Online_Server
{
    class Server
    {
        public static System.Timers.Timer updPlayersTimer = new System.Timers.Timer(1);
        public static StreamWriter packetWriter;
        public static List<PlayerInfo> Users = new List<PlayerInfo>();

        private static Thread thrListener;
        private static TcpListener tlsClient;
        public static bool ServRunning = false;
        private static TcpClient tcpClient;

        public static string mmtext = "Info about connection\nConnected players\nStop current mission\nDisconnect";

        public static PlayerInfo InfoFromTcp(TcpClient user)
        {

            for (int a = 0; a < Users.Count; a++)
            {
                if (Users[a].TCP == user) return Users[a];
            }
            return null;
        }
        public static TcpClient TcpFromInfo(PlayerInfo info)
        {
            return info.TCP;
        }
        public static TcpClient TcpById(int id)
        {
            for (int a = 0; a < Users.Count; a++)
            {
                if (Users[a].PlayerID == id) return Users[a].TCP;
            }
            return null;
        }

        public static bool IsUserNorm(TcpClient user)
        {
            for (int a = 0; a < Users.Count; a++)
            {
                if (Users[a].TCP == user) return true;
            }
            return false;
        }


        public static void WriteErrorMessage(object text)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text.ToString());
            Console.ForegroundColor = old;
        }
        public static void StartListening()
        {
            IPAddress ipaLocal = IPAddress.Any;
            packetWriter = new StreamWriter("packets.log");
            packetWriter.AutoFlush = true;
            try
            {
                tlsClient = new TcpListener(ipaLocal, config.Port);

                tlsClient.Start();

                ServRunning = true;

                thrListener = new Thread(Listening);
                thrListener.Start();

                updPlayersTimer.Elapsed += updPlayersTimer_Elapsed;
                updPlayersTimer.Start();
            }
            catch (Exception ex)
            {
                ServRunning = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }

        static void updPlayersTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            updPlayersTimer.Stop();
            try
            {
                for (int mp = 0; mp < Users.Count; mp++)
                {
                    for (int op = 0; op < Users.Count; op++)
                    {
                        if (Users[op].TCP != Users[mp].TCP)
                        {
                            if (Users[op].Position.x != 0 && Users[op].Position.y != 0 && Users[op].Position.z != 0)
                            {
                                SendToUser(Users[mp].TCP, string.Format("PlayerUpdate#{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}#{9}#{10}",
                                   Users[op].PlayerID,
                                   Users[op].Position.x,
                                   Users[op].Position.y,
                                   Users[op].Position.z,
                                   Users[op].Rotation.x,
                                   Users[op].Rotation.y,
                                   Users[op].Rotation.z,
                                   Users[op].Rotation.w,
                                   Users[op].SpeedX,
                                   Users[op].SpeedY,
                                   Users[op].Spin
                                   ));
                            }
                        }
                    }
                }
            }
            catch { }
            updPlayersTimer.Start();
        }
        public static void ShowListDialog(TcpClient user, object lines, string response)
        {
            SendToUser(user, "ShowListDialog#" + response + "#" + lines.ToString());
        }
        public static void SendToUser(TcpClient user, object text, bool rcon = false)
        {
            if (text.ToString().Length >= 1)
            {
                try
                {
                    text = text.ToString().Replace("\r\n", "~n~").Replace("\n", "~n~");
                    if (!rcon)
                    {
                        packetWriter.WriteLine("Message to " + InfoFromTcp(user).Nickname + ": " + text.ToString());
                        text = Privatcom_Basic_Server.Base64.Encrypt(text.ToString());
                    }
                    StreamWriter swSenderSender;
                    swSenderSender = new StreamWriter(user.GetStream());
                    swSenderSender.WriteLine(text.ToString());
                    swSenderSender.Flush();
#if DEBUG
                    Console.WriteLine("Message to " + htConnections[user] + ": " + text.ToString());
#endif
                    
                }
                catch (Exception ex) { WriteErrorMessage(ex.ToString()); packetWriter.WriteLine(ex.ToString());  RemoveUser(user); }
            }
        }
        public static void Broadcast(object message)
        {
            for(int a = 0; a < Users.Count; a++)
            {
                SendToUser(Users[a].TCP, message);
            }
        }
        public static void ShowDialogForUser(TcpClient user, int type, string text)
        {
            SendToUser(user, String.Format("ShowDialog#{0}#{1}", type.ToString(), text));
        }
        public static void ShowTextForUser(TcpClient user, string text)
        {
            SendToUser(user, String.Format("ShowText#{0}", text));
        }
        public static void AddUser(TcpClient tcpUser, string nickname)
        {
            int id = GetFreeId();
            Users.Add(new PlayerInfo(id, nickname, tcpUser));
            Console.WriteLine("New connection: " + nickname);
            if (Users.Count >= 2)
                MeetUp.SendMeetUpInfo();
            //ShowDialogForUser(tcpUser, 0, InfoFromTcp(tcpUser).Nickname + ", welcome to MW-Online!\n[!] This version is unstable");
            for (int a = 0; a < Users.Count; a++)
            {
                if (Users[a].TCP != tcpUser) ShowTextForUser(Users[a].TCP, String.Format("{0} [{1}] - connected", nickname, id));
            }
            //SendToUser(tcpUser, "SetPos#1000#50#1000");
        }
        public static void RemoveUser(TcpClient tcpUser)
        {
            // If the user is there
            if (IsUserNorm(tcpUser))
            {
                PlayerInfo pInfo = InfoFromTcp(tcpClient);
                for (int a = 0; a < Users.Count; a++)
                {
                    if (Users[a].TCP != tcpUser) ShowTextForUser(Users[a].TCP, String.Format("{0} [{1}] - disconnected", pInfo.Nickname, pInfo.PlayerID));
                }
                Console.WriteLine("Connection closed for " + pInfo.Nickname);

                PlayerInfo.usedIds.Remove(pInfo.PlayerID);
               
                Users.Remove(pInfo);

                tcpUser.Close();

                
            }
        }
        private static void Listening()
        {
            while (ServRunning == true)
            {
                try
                {
                    tcpClient = tlsClient.AcceptTcpClient();
                    Connection newConnection = new Connection(tcpClient);
                }
                catch { }
            }
        }
        public static void WorkOnMessage(TcpClient user, string msg)
        {
#if DEBUG
            Console.WriteLine("Message from " + htConnections[user] + ": " + msg);
#endif
            msg = Privatcom_Basic_Server.Base64.Decrypt(msg);
            packetWriter.WriteLine("Message from " + InfoFromTcp(user).Nickname + ": " + msg);
            if (msg.StartsWith("TpToIdx#"))
            {
                string[] s = msg.Split('#');
                // s[1] = int idx, s[2] = line text
                // line text example:
                // 5 - Developer
                // ID - Nickname
                int toid = int.Parse(s[2].Trim().Split('-')[0]);
                if (TcpById(toid) != null)
                {
                    PlayerInfo pInfo = InfoFromTcp(TcpById(toid));
                    ShowTextForUser(user, "Teleporting to " + pInfo.Nickname);
                    SendToUser(user, String.Format("SetPos#{0}#{1}#{2}", pInfo.Position.x, pInfo.Position.y,  pInfo.Position.z));
                }
                else
                {
                    ShowDialogForUser(user, 0, "Sorry, but this user is not connected :(");
                }
            }
            if (msg.StartsWith("MenuIdx#"))
            {
                string[] s = msg.Split('#');
                int idx = int.Parse(s[1]);
                PlayerInfo pInfo = InfoFromTcp(user);
                if (idx == 0)
                {
                    ShowDialogForUser(user, 0, String.Format("{0} - {1}\nX: {2} Y: {3} Z: {4}", pInfo.PlayerID, pInfo.Nickname, pInfo.Position.x, pInfo.Position.y, pInfo.Position.z));
                }
                else if (idx == 1)
                {
                    StringBuilder builder = new StringBuilder();
                    for(int a = 0; a < Users.Count; a++)
                    {
                        builder.AppendLine(String.Format("{0} - {1}", Users[a].PlayerID, Users[a].Nickname));
                    }
                    ShowDialogForUser(user, 0, builder.ToString());
                }
                else if (idx == 2)
                {
                    Broadcast("ShowText#Prepare to evade. POLICEEEE!^*whispers* RUN!");
                    Broadcast("911Call");
                }
                else if (idx == 3)
                {
                    SendToUser(user, "Disconnect");
                }
            }
            if (msg.StartsWith("GiveMeMainMenuText"))
            {
                ShowListDialog(user, mmtext, "MenuIdx");
            }
            if (msg.StartsWith("PlayerPacket#"))
            {
                // PlayerPacket0#PosX1#PosY2#PosZ3#Rotx4#roty5#rotz6

                PlayerInfo pInfo = InfoFromTcp(user);
                int id = pInfo.PlayerID;
                string[] s = msg.Split('#');
                try
                {
                    float x = float.Parse(s[1]);
                    float y = float.Parse(s[2]);
                    float z = float.Parse(s[3]);
                    float rx = float.Parse(s[4]);
                    float ry = float.Parse(s[5]);
                    float rz = float.Parse(s[6]);
                    float rw = float.Parse(s[7]);
                    float spdx = float.Parse(s[8]);
                    float spdy = float.Parse(s[9]);
                    float whrot = float.Parse(s[10]);

                    if (!float.IsNaN(x) || !float.IsNaN(y) || !float.IsNaN(z) || !float.IsNaN(rx) || !float.IsNaN(ry) || !float.IsNaN(rz) || !float.IsNaN(spdx) || !float.IsNaN(spdy) || !float.IsNaN(whrot))
                    {


                        if (x == 0 && y == 0 && z == 0)
                        {
                            return;
                        }

                        pInfo.Position = new Vector3(x, y, z);
                        pInfo.Rotation = new Quaternion(rx, ry, rz, rw);
                        pInfo.SpeedX = spdx; pInfo.SpeedY = spdy;
                        pInfo.Spin = whrot;
                    }
                    /*foreach (var con in htConnections)
                    {
                        if (con.Key != user)
                        {
                            if(PlayerToPoint(1000, htConnections[user], htConnections[con.Key]))
                                SendToUser(con.Key, string.Format("PlayerUpdate#{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}#{9}#{10}", id, x, y, z, rx, ry, rz, rw, spdx, spdy, povorot));
                        }
                    }
                     */
                }
                catch { }
            }
        }
        public static int GetFreeId()
        {
            int kk = 1;
            while (kk != -2)
            {
                if (!PlayerInfo.usedIds.Contains(kk)) return kk;
                kk++;
            }
            return -1;
        }
        public static bool PlayerToPoint(float radi, int playerid, int targetid)// aw god 
        {
            float posx, posy, posz, oldposx, oldposy, oldposz, tempposx, tempposy, tempposz;
            PlayerInfo pInfo = InfoFromTcp(TcpById(playerid)), tInfo = InfoFromTcp(TcpById(targetid));
            oldposx = pInfo.Position.x; oldposy = pInfo.Position.y; oldposz = pInfo.Position.z;
            posx = tInfo.Position.x; posy = tInfo.Position.y; posz = tInfo.Position.z;
            tempposx = (oldposx - posx);
            tempposy = (oldposy - posy);
            tempposz = (oldposz - posz);
            if (((tempposx < radi) && (tempposx > -radi)) && ((tempposy < radi) && (tempposy > -radi)) && ((tempposz < radi) && (tempposz > -radi)))
            {
                return true;
            } 
            return false;
        }
    }
    class Connection
    {
        TcpClient tcpClient;
        // The thread that will send information to the client
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string strResponse;
        private string strMain;
        // The constructor of the class takes in a TCP connection
        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            // The thread that accepts the client and awaits messages
            thrSender = new Thread(AcceptClient);
            // The thread calls the AcceptClient() method
            thrSender.Start();

        }

        public void CloseConnection()
        {
            // Close the currently open objects
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }

        // Occures when a new client is accepted
        private void AcceptClient()
        {
            srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
            swSender = new System.IO.StreamWriter(tcpClient.GetStream());
            try
            {
                string t = srReceiver.ReadLine();
                if (t.StartsWith("rcon|"))
                {
                    Console.WriteLine("New RCON session from: " + ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString());
                    string[] r = t.Split('|');
                    if (r.Length <= 1 || r[1] != config.RconPass)
                    {
                        Server.SendToUser(tcpClient, "Auth failed...", true);
                        Console.WriteLine("RCON Auth: Failed!");
                        CloseConnection();
                    }
                    else
                    {
                        Console.WriteLine("RCON Auth: Completed!");
                        Server.SendToUser(tcpClient, "RCON Auth: Completed!", true);
                        try
                        {
                            string rp = "";
                            while (true)
                            {
                                rp = srReceiver.ReadLine();
                                Console.WriteLine("RCON " + ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString() + ": " + rp);
                                Program.CommandProcessing(rp, tcpClient);
                            }
                        }
                        catch { }
                        Console.WriteLine("RCON Session: Ended - " + ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString());
                        try { CloseConnection(); }
                        catch { }
                    }
                }
                else
                {
                    strMain = Base64.Decrypt(t);

                    Server.AddUser(tcpClient, strMain);
                }
            }
            catch { CloseConnection(); }

            try
            {
                // Keep waiting for a message from the user
                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    // If it's invalid, remove the user
                    if (strResponse == null)
                    {
                        Server.RemoveUser(tcpClient);
                    }
                    else
                    {
                        // Otherwise send the message to all the other users
                        Server.WorkOnMessage(tcpClient, strResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Server.WriteErrorMessage("con:\n"+ex.ToString());
                // If anything went wrong with this user, disconnect him
                Server.RemoveUser(tcpClient);
            }
             
        }
    }
}
