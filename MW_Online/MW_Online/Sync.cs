using NFSScript;
using NFSScript.Core;
using NFSScript.MW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MW_Online
{
    public static class Sync
    {
        public static bool PauseSync = false;
        public static void StartSync()
        {
            new Thread(MWOnlineSynchronize).Start();
        }
        private static void MWOnlineSynchronize()
        {
            Thread.Sleep(1500);
            Log.Print("MW-Online", "Syncing with server!");
            while (Connection.Connected)
            {
                while (PauseSync) { }
                try
                {
                    if (!MathFuncs.PlayerToPoint(0.05f, Player.Position, SyncOld_P.oldPos))
                    {
                        Connection.SendToServer(String.Format("PlayerPacket#{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}#{9}",
                        Player.Position.x,//1
                        Player.Position.y,//2
                        Player.Position.z,//3
                        Player.Rotation.x,//4
                        Player.Rotation.y,//5
                        Player.Rotation.z,//6
                        Player.Rotation.w,//7
                        GameMemory.memory.ReadFloat((IntPtr)0x9386F0)/*8*/,
                        GameMemory.memory.ReadFloat((IntPtr)0x9386E8)/*9*/,
                        GameMemory.memory.ReadFloat((IntPtr)NewAddresses.SPIN)
                        ));
                    }
                    SyncOld_P.SetNewOld(Player.Position, Player.Rotation);
                    Thread.Sleep(10);// 50 ms is more stable than 100 because its game, not only app
                }

                catch (Exception ex) { string m = String.Format("Sync error: {0}", ex.Message); Log.Print("MW-Online", m); GameDialog.ShowStableDialog(0, m); Connection.CloseConnection(); break; }
            }
        }
    }
}
