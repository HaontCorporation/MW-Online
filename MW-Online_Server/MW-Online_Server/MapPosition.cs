using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFSScript.Math;

namespace MW_Online_Server
{
    public class MapPosition
    {
        public Vector3 vec3pos;
        public string Name = null;
        public MapPosition(float posx, float posy, float posz, string name = null)
        {
            vec3pos = new Vector3(posx, posy, posz);
            Name = name;
        }
        public MapPosition(Vector3 vec3, string name = null)
        {
            vec3pos = vec3;
            Name = name;
        }
    }
}