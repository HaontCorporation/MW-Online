using NFSScript.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Online
{
    public static class SyncOld_P
    {
        public static Vector3 oldPos = new Vector3(0, 0, 0);
        public static Quaternion oldRot = new Quaternion(0, 0, 0, 0);

        public static void SetNewOld(Vector3 pos, Quaternion rot)
        {
            oldPos = pos; oldRot = rot;
        }
    }
}
