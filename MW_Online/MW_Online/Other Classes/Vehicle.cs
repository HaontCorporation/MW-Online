using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFSScript;
using NFSScript.Math;
using NFSScript.Core;
using NFSScript.MW;

namespace MW_Online
{
    public class Vehicle
    {
        private static int Offset = 44 * 4;

        public static Vector3 GetPosition(int VehicleID)
        {
            if (VehicleID >= 1)
            {
                float x = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + Offset * VehicleID);
                float y = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Y + Offset * VehicleID);
                float z = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + Offset * VehicleID);
                return new Vector3(x, y, z);
            }
            return new Vector3();
        }
        public static void SetPosition(int VehicleID, Vector3 position)
        {
            float x = position.x;
            float y = position.y;
            float z = position.z;
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + Offset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Y + Offset * VehicleID, y);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + Offset * VehicleID, z);
        }
        public static void SetPosition(int VehicleID, float x, float z)
        {
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + Offset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + Offset * VehicleID, z);
        }
        public static void SetPosition(int VehicleID, float x, float y, float z)
        {
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + Offset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Y + Offset * VehicleID, y);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + Offset * VehicleID, z);
        }
        public static Quaternion GetRotation(int VehicleID)
        {
            float x = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_X_ROT + Offset * VehicleID);
            float y = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Y_ROT + Offset * VehicleID);
            float z = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Z_ROT + Offset * VehicleID);
            float w = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_W_ROT + Offset * VehicleID);
            return new Quaternion(x, y, z, w);
        }
        public static void SetRotation(int VehicleID, Quaternion rotation)
        {
            float x = rotation.x;
            float y = rotation.y;
            float z = rotation.z;
            float w = rotation.w;
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_X_ROT + Offset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Y_ROT + Offset * VehicleID, y);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Z_ROT + Offset * VehicleID, z);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_W_ROT + Offset * VehicleID, w);
        }
        public static void SetSpeed(int VehicleID, float x, float y)//
        {
            GameMemory.memory.WriteFloat((IntPtr)0x9386F0 + Offset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)0x9386E8 + Offset * VehicleID, y);
        }
        public static float[] GetSpeedXY(int VehicleID)
        {
            float[] spd = { 
                              GameMemory.memory.ReadFloat((IntPtr)0x9386F0 + Offset * VehicleID),
                              GameMemory.memory.ReadFloat((IntPtr)0x9386E8 + Offset * VehicleID) 
                          };
            return spd;
        } // example: GetSpeed()[0] = x
        public static float GetSpeed(int VehicleID)
        {
            return GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_SPEED + Offset * VehicleID);
        } // default Car.GetSpeed
        public static float GetSpin(int VehicleID)
        {
            return GameMemory.memory.ReadFloat((IntPtr)NewAddresses.SPIN + Offset * VehicleID);
        }
        public static void SetSpin(int VehicleID, float r)
        {
            GameMemory.memory.WriteFloat((IntPtr)NewAddresses.SPIN + Offset * VehicleID, r);
        }


    }
}
