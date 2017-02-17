using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using NFSScript.Math;
using System.Net;

namespace MW_Online_Server
{
    class PlayerInfo
    {
        public static List<int> usedIds = new List<int>();

        public int PlayerID;
        public string Nickname;
        public Vector3 Position;
        public Quaternion Rotation;
        public float SpeedX;
        public float SpeedY;
        public float Spin;
        public TcpClient TCP;
        public IPAddress IP;
        public PlayerInfo(int id, string nick, TcpClient con)
        {
            PlayerID = id;
            Nickname = nick;
            Position = new Vector3(0,0,0);
            Rotation = new Quaternion(0,0,0,0);
            SpeedX = 0;
            SpeedY = 0;
            Spin = 0;
            TCP = con;
            IP = ((IPEndPoint)con.Client.RemoteEndPoint).Address;
            usedIds.Add(PlayerID);
        }
    }
}
