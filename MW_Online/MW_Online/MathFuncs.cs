using NFSScript.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Online
{
    public static class MathFuncs
    {
        public static bool PlayerToPoint(float radi, Vector3 pos1, Vector3 pos2)// aw god 
        {
            float posx, posy, posz, oldposx, oldposy, oldposz, tempposx, tempposy, tempposz;
            oldposx = pos1.x; oldposy = pos1.y; oldposz = pos1.z;
            posx = pos2.x; posy = pos2.y; posz = pos2.z;
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
}
