using NFSScript;
using NFSScript.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MW_Online
{
    class PosC
    {
        private static float oldX = 0;
        private static float oldZ = 0;

        private static float newX = 0;
        private static float newZ = 0;

        private static int addr;

        public static void Set(int adr, float x, float z)
        {
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + addr, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + addr, z);
            // todo
            //Log.Print("Set", "1");
            /*
            addr = adr;
            newX = x; newZ = z;
            oldX = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + adr);
            oldZ = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + adr);
            new Thread(cX).Start();
            new Thread(cZ).Start();
             */
        }
        private static void cX()
        {
            float x = oldX;
            while (x < newX)
            {
                try
                {
                    x += 0.007f;
                    GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + addr, x);
                }
                catch { }
               // Log.Print("x<newX", "oldX: " + oldX + " x: " + x + "newX: " + newX);
            }
            while (x > newX)
            {
                try
                {
                    x -= 0.007f;
                    GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + addr, x);
                }
                catch { }
               // Log.Print("x>newX", "oldX: " + oldX + " x: " + x + "newX: " + newX);
            }
        }
        private static void cZ()
        {
            float Z = oldZ;
            while (Z < newZ)
            {
                try
                {
                    Z += 0.007f;
                    GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + addr, Z);
                }
                catch { }
               // Log.Print("z<newZ", "oldZ: " + oldZ + " z: " + Z + "newZ: " + newZ);
            }
            while (Z > newZ)
            {
                try
                {
                    Z -= 0.007f;
                    GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + addr, Z);
                }
                catch { }
               // Log.Print("z>newZ", "oldZ: " + oldZ + " z: " + Z + "newZ: " + newZ);
            }
        }
    }
}
