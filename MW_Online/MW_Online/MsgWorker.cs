using NFSScript;
using NFSScript.Core;
using NFSScript.Math;
using NFSScript.MW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MW_Online
{
    public static class MsgWorker
    {
        public static StreamWriter writer = new StreamWriter("superlog.txt");
        public static void StartWorker()
        {
            new Thread(msgWork).Start();
        }
        private static void msgWork()
        {
            try
            {
                Connection.srReceiver = new StreamReader(Connection.tcpServer.GetStream());
                Log.Print("MW-Online", "Accepting messages from server!");
                while (true)
                {
                    try
                    {
                        string msg = Privatcom_Basic_Server.Base64.Decrypt(Connection.srReceiver.ReadLine()).Replace("~n~", "\n");

                        writer.WriteLine(msg);
                        writer.Flush(); 
                        //Llog("MW-Online", "From server: " + msg);
                        if (msg.StartsWith("SetAI#"))
                        {
                            string[] g = msg.Split('#');
                            int mem = int.Parse(g[1]);
                            string t = g[2];
                        }
                        if (msg.StartsWith("ShowUpdateForm"))
                        {
                            new UpdateForm().ShowDialog();
                        }
                        if (msg.StartsWith("SetPos#"))
                        {
                            string[] s = msg.Split('#');
                            // x1#y2#z3
                            float x = float.Parse(s[1]);
                            float y = float.Parse(s[2]);
                            float z = float.Parse(s[3]);



                            StreamWriter wr = new StreamWriter("TRACKS\\HotPositionL2RA.HOT");
                            wr.Write(String.Format("HOTPOSITION: {0}, {1}, {2} Angle = 0xcfec Speed = {3}", x, y, z, 0));
                            wr.Flush();
                            wr.Close();
                            wr.Dispose();

                            GameMemory.memory.WriteInt32((IntPtr)0x9B090C, 0x01);
                        }
                        if (msg.StartsWith("ShowListDialog#"))
                        {
                            string[] s = msg.Split('#');
                            // s[1] = response
                            // s[2] = line~n~line2~n~line3 = lines
                            GameDialog.MenuResponse = s[1];
                            GameDialog.MenuIdx = 0;
                            GameDialog.MenuText = s[2].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            GameDialog.ShowMenu();

                        }
                        if (msg.StartsWith("Disconnect"))
                        {
                            Connection.CloseConnection();
                            GameDialog.ShowStableDialog(0, "Disconnected...");

                        }
                        if (msg.StartsWith("ShowDialog#"))
                        {
                            string[] s = msg.Split('#');
                            // s[0] = ShowDialog
                            // s[1] = Type. Ex: 1
                            // s[2] = Text.
                            int type = int.Parse(s[1]);
                            string text = s[2];
                            GameDialog.ShowStableDialog(type, text);
                        }
                        if (msg.StartsWith("ShowText#"))
                        {
                            string[] s = msg.Split('#');
                            // s[0] = ShowTtext
                            // s[1] = Text
                            string text = s[1];
                            UI.ShowTextScreenMessage(text);
                        }
                        if (msg.StartsWith("PlayerUpdate#") && !Sync.PauseSync)
                        {
                            writer.WriteLine(msg);
                            writer.Flush();
                            string[] s = msg.Split('#');
                            // PlayerUpdate0#id1#posx2#posy3#posz4#rotx5#roty6#rotz7
                            int player_id = int.Parse(s[1]);
                            float posx = float.Parse(s[2]);
                            float posy = float.Parse(s[3]);
                            float posz = float.Parse(s[4]);
                            float rotx = float.Parse(s[5]);
                            float roty = float.Parse(s[6]);
                            float rotz = float.Parse(s[7]);
                            float rotw = float.Parse(s[8]);
                            float speedx = float.Parse(s[9]);
                            float speedy = float.Parse(s[10]);
                            float spn = float.Parse(s[11]);
                            if (!float.IsNaN(posx) && !float.IsNaN(posy) && !float.IsNaN(posz) && !float.IsNaN(rotx) && !float.IsNaN(roty) && !float.IsNaN(rotz) && !float.IsNaN(speedx) && !float.IsNaN(speedy) && !float.IsNaN(spn))
                            {
                                //Llog("MW-Online", "Updating player ID: " + player_id);
                                // Llog("MW-Online", String.Format("PosX: {0}, PosY = {1}, PosZ = {2}", posx, posy, posz));
                                //Llog("MW-Online", String.Format("RotX: {0}, RotY = {1}, RotZ = {2}", rotx, roty, rotz));


                               // PosC.Set(44 * 4 * player_id, posx, posz);
                                GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + 44 * 4 * player_id, posx);
                                //GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Y + 44 * 4 * player_id, 0);
                                GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + 44 * 4 * player_id, posz);
                                //GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_SPEED + 44 * 4 * player_id, spd); 
                                GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_X_ROT + 44 * 4 * player_id, rotx);
                                GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Y_ROT + 44 * 4 * player_id, roty);
                                GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Z_ROT + 44 * 4 * player_id, rotz);
                                GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_W_ROT + 44 * 4 * player_id, rotw);
                                GameMemory.memory.WriteFloat((IntPtr)0x9386F0 + 44 * 4 * player_id, speedx);
                                GameMemory.memory.WriteFloat((IntPtr)0x9386E8 + 44 * 4 * player_id, speedy);
                                GameMemory.memory.WriteFloat((IntPtr)NewAddresses.SPIN + 44 * 4 * player_id, spn);
                            }
                        }
                        if(msg.StartsWith("MeetUp#"))
                        {
                            string[] s = msg.Split('#');
                            UI.ShowTextScreenMessage(String.Format("MEET UP^^{0}^Drive there and evade the pursuit!", s[4]));
                        }
                        if (msg.StartsWith("911Call")) //Server wants to play the pursuit :D
                        {
                            Function.Call(MWFunctions.SHOW_RACE_COUNTDOWN);
                            Thread.Sleep(4000);
                            Function.Call(MWFunctions.FORCE_PURSUIT_START, 50, 5);
                        }
                    }
                    catch (Exception ex) { string m = String.Format("Connection error: {0}", ex.Message); Log.Print("MW-Online", m); GameDialog.ShowStableDialog(0, m); Connection.CloseConnection(); break; }
                }
            }
            catch { }
        }
        
    }
}
