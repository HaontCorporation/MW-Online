using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NFSScript;
using NFSScript.MW;
using NFSScript.Core;
using System.Threading;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using NFSScript.Math;

namespace MW_Online
{
    public class MW_Online : Mod
    {
        
        static int mid = 0;
        public static bool StartWithGameplay = false;

        public static float lastYpos = 0;

        
        

        public static void LogCoords()
        {
            Llog("Debug", mid + " - " + Player.Position.x + " " + Player.Position.y + " " + Player.Position.z);
            Llog("Debug", mid + " - " + GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X));
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X, 20);
            Function.Call(MWFunctions.CAMERA_SHAKE);
            if (Game.isGameplayActive) UI.ShowTextScreenMessage("Go to console and view logs. ID: " + mid.ToString());
            mid++;
        }
        public static void ShowLoading(bool transparent = false)
        {
            if (!transparent) Game.LoadingScreenOn();
            else Game.FadeScreenOn();
        }
        public static void HideLoading()
        {
            Game.LoadingScreenOn();
            Thread.Sleep(500);
            Game.LoadingScreenOff();

        }
        
        public override void OnGameplayStart()
        {
            Thread.Sleep(5000);
            Sync.PauseSync = false;
            /*
            if (!StartWithGameplay) return;
            StartWithGameplay = false;
            Thread.Sleep(5000);
            MWOnlineDo();
            Thread.Sleep(5000);
            new Thread(MWOnlineSynchronize).Start();*/
            // TEMPORARY UNAVAILABLE!!! PLEASE, USE 'K' KEY IN GAME!
            //new Thread(MWOnlineSynchronize).Start();
            //UI.ShowDialogBox(UI.DIALOG_INFO, "GP");
        }

        public override void OnGameplayExit()
        {
            Sync.PauseSync = true;
        }
        
        public static void InitializeCarSlots()
        {
            Thread.Sleep(500);

            Thread.Sleep(500);

        }
        
        private void unPause()
        {
            Thread.Sleep(175);
            GameDialog.shown = false;
            if (Game.isGameplayActive) Game.Unpause();
            if (GameDialog.isInMn)
            {
                GameDialog.isInMn = false;
                Connection.SendToServer(GameDialog.MenuResponse + "#" + GameDialog.MenuIdx + "#" + GameDialog.MenuText[GameDialog.MenuIdx]);
            }
        }
        private static void startGame()
        {
            if (!Game.isGameplayActive)
            {
                UI.ShowDialogBox(UI.DIALOG_INFO, "Welcome to MW-Online!\nTo start game join in any race.");
                StartWithGameplay = true;
            }
            while (!Game.isGameplayActive) { }
            if (StartWithGameplay) Thread.Sleep(8000);
            StartWithGameplay = false;

            new Thread(Connection.StartMWOConnection).Start();

        }

        private static void showMm()
        {
            Connection.SendToServer("GiveMeMainMenuText");
        }

        // This method is called when a key is released.
        public override void OnKeyUp(Keys key)
        {
            if (key == Keys.Enter || key == Keys.Escape)
            {
                if (GameDialog.shown) new Thread(unPause).Start();
            }

            if (key == Keys.N && Game.isGameplayActive && Connection.Connected)
            {
                new Thread(showMm).Start();
            }

            if (GameDialog.isInMn)
            {
                if (key == Keys.Up) { GameDialog.MenuIdx--; if (GameDialog.MenuIdx < 0) { GameDialog.MenuIdx = GameDialog.MenuText.Length; } GameDialog.ShowMenu();  }
                if (key == Keys.Down) { GameDialog.MenuIdx++; if (GameDialog.MenuIdx >= GameDialog.MenuText.Length) { GameDialog.MenuIdx = 0; } GameDialog.ShowMenu(); }
                return;
            }

            if (key == Keys.O)
            {
                //HOTPOSITION: X, Y, Z Angle = 0xcfec Speed = 15

               // WriteMemory<unsigned char>(0x9B090C, 0x01, true);

                //PosC.Set(44 * 4 * 1, Player.Position.x, Player.Position.z);
                //MathFuncs.TeleportTo(new Vector3(GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + 44 * 4 * 1), GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Y + 44 * 4 * 1), GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + 44 * 4 * 1)));
            }

            if (key == Keys.P)
            {
                HideLoading();
            }
            if (key == Keys.J)
            {
                Player.Position = new NFSScript.Math.Vector3(Player.Position.x, Player.Position.y + 10, Player.Position.z);
            }
            /*
            if (key == Keys.U)
            {
                if (!StartWithGameplay)
                {
                    new Thread(startGame).Start();
                }


            }
             */

        }

        public void ReinitMWO()
        {
            try
            {
                Process.GetProcessesByName("MW-Online Launcher");
            }
            catch
            {
                MessageBox.Show("", "MW-Online", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
#if DEBUG
            Debugger.Launch();
#endif

            GameMemory.memory.WriteStringASCII((IntPtr)0x8918B0, "AIActionNone");
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Llog("MW-Online", "Loading settings from mwonline.ini...");
            StreamReader reader = new StreamReader("scripts\\mwonline.ini");
            string[] sp = reader.ReadToEnd().Split(';');
            string ipStr = sp[0].Split('=')[1];
            string portStr = sp[1].Split('=')[1];
            Connection.NickName = sp[2].Split('=')[1];
            reader.Close();
            reader.Dispose();
            Connection.connectIp = IPAddress.Parse(ipStr);
            Connection.connectPort = int.Parse(portStr);
            Llog("MW-Online", "Server IP: " + ipStr + ", port: " + portStr);


            Sync.PauseSync = true;
            InitConnection();
        }

        public static void InitConnection()
        {
            Connection.StartMWOConnection();
            Sync.StartSync();
        }
        
        public override void Main()
        {
            ReinitMWO();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StreamWriter writer = new StreamWriter("errorlog.log");
            string l = String.Format("{0}\r\n\r\n{1}", ((Exception)e.ExceptionObject).Message, ((Exception)e.ExceptionObject).StackTrace);
            writer.Write(l);
            writer.Flush();
            writer.Close();
            MessageBox.Show(null, "look error.log in game folder", "MW-Online", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        public static void Llog(object tag, object text)
        {
            Log.Print(tag.ToString(), text.ToString());
        }

        public override void Pre()
        {
            new Thread(gc_collect).Start();
        }
        private static void gc_collect()
        {
            while (true)
            {
                Thread.Sleep(500);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    
    }
}

