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
    public class RacerVehicle
    {
        public static RacerVehicle MainCar;
        public static List<RacerVehicle> CarSlot = new List<RacerVehicle>();


        public static int maximumVehicles = 3;
        private static Dictionary<RacerVehicle, int> p_veh = new Dictionary<RacerVehicle, int>();
        private static int TrafOffset = 44 * 4;

        private int VehicleID = -1;

        public RacerVehicle()
        {
            int id = GetFreeVehId();
            VehicleID = id;
            UI.ShowTextScreenMessage(id.ToString());
            Function.Call(MWFunctions.ADD_RACER, TrafOffset * VehicleID);
            if (id >= 1)
            {
                p_veh.Add(this, id);
            }
            else
            {
                //
            }
        }
        public RacerVehicle(int id)
        {
            VehicleID = id;
            if (id >= 0)
            {
                p_veh.Add(this, id);
            }
            else
            {
                // 
            }
        }
        public int ID()
        {
            return VehicleID;
        }
        public Vector3 GetPosition()
        {
            if (VehicleID >= 1)
            {
                float x = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + TrafOffset * VehicleID);
                float y = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Y + TrafOffset * VehicleID);
                float z = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + TrafOffset * VehicleID);
                return new Vector3(x, y, z);
            }
            return new Vector3();
        }
        public void SetPosition(Vector3 position)
        {
            if (VehicleID <= -1) return;
            float x = position.x;
            float y = position.y;
            float z = position.z;
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + TrafOffset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Y + TrafOffset * VehicleID, y);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + TrafOffset * VehicleID, z);
        }
        public void SetPosition(float x, float z)
        {
            if (VehicleID <= -1) return;
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + TrafOffset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + TrafOffset * VehicleID, z);
        }
        public void SetPosition(float x, float y, float z)
        {
            if (VehicleID <= -1) return;
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_X + TrafOffset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Y + TrafOffset * VehicleID, y);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_POS_Z + TrafOffset * VehicleID, z);
        }
        public Quaternion GetRotation()
        {
            if (VehicleID <= -1) return new Quaternion();
            float x = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_X_ROT + TrafOffset * VehicleID);
            float y = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Y_ROT + TrafOffset * VehicleID);
            float z = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Z_ROT + TrafOffset * VehicleID);
            float w = GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_W_ROT + TrafOffset * VehicleID);
            return new Quaternion(x, y, z, w);
        }
        public void SetRotation(Quaternion rotation)
        {
            if (VehicleID <= -1) return;
            float x = rotation.x;
            float y = rotation.y;
            float z = rotation.z;
            float w = rotation.w;
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_X_ROT + TrafOffset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Y_ROT + TrafOffset * VehicleID, y);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_Z_ROT + TrafOffset * VehicleID, z);
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_W_ROT + TrafOffset * VehicleID, w);
        }
        public void SetSpeed(float speed)//-
        {
            if (VehicleID <= -1) return;
            GameMemory.memory.WriteFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_SPEED + TrafOffset * VehicleID, speed);
        }
        public void SetSpeed(float x, float y)//
        {
            if (VehicleID <= -1) return;
            GameMemory.memory.WriteFloat((IntPtr)0x9386F0 + TrafOffset * VehicleID, x);
            GameMemory.memory.WriteFloat((IntPtr)0x9386E8 + TrafOffset * VehicleID, y);
        }
        public float[] GetSpeedXY()
        {
            if (VehicleID <= -1) return new float[] { 0f, 0f };
            float[] spd = { 
                              GameMemory.memory.ReadFloat((IntPtr)0x9386F0 + TrafOffset * VehicleID),
                              GameMemory.memory.ReadFloat((IntPtr)0x9386E8 + TrafOffset * VehicleID) 
                          };
            return spd;
        } // example: GetSpeed()[0] = x
        public float GetSpeed()
        {
            if(VehicleID <= -1)return 0;
            return GameMemory.memory.ReadFloat((IntPtr)MWAddresses.PlayerAddrs.STATIC_PLAYER_SPEED + TrafOffset * VehicleID);
        } // default Car.GetSpeed
        public float GetWheelRot()
        {
            if (VehicleID <= -1) return 0;
            return GameMemory.memory.ReadFloat((IntPtr)NewAddresses.POVOROT + TrafOffset * VehicleID);
        }
        public void SetWheelRot(float r)
        {
        }


        private static int GetFreeVehId()
        {
            int k = 1;
            while (k != maximumVehicles)
            {
                if (!p_veh.ContainsValue(k)) return k;
                k++;
            }
            return -1;
        }
    }
}
