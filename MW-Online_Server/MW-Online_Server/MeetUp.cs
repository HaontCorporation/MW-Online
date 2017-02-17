using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MW_Online_Server
{
    class MeetUp
    {
        public static MapPosition[] meetUpCoordinates = {
            new MapPosition(-2987.48f, 189.1887f, -355.2282f, "NEAR THE OLD BRIDGE"),
        };
        public static MapPosition getRandomMeetUp()
        {
            MapPosition randomPos = meetUpCoordinates[new Random().Next(0, meetUpCoordinates.Length)];
            return randomPos;
        }
        public static void SendMeetUpInfo()
        {
            MapPosition meetUp = getRandomMeetUp();
            string meetUpPacket = String.Format("MeetUp#{0}#{1}#{2}#{3}", meetUp.vec3pos.x,
                                                                          meetUp.vec3pos.y,
                                                                          meetUp.vec3pos.z,
                                                                          meetUp.Name);
            Server.Broadcast(meetUpPacket);
        }
    }
}