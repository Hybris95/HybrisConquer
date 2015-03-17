using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packets
{
    class StatPointsPacket : IClassPacket
    {
        byte[] Packet = new byte[8];

        public void Deserialize(byte[] Packet)
        {
            this.Packet = Packet;
        }
        public byte[] Serialize()
        {
            return Packet;
        }
        public void Send(GameClient Hero)
        {
            Hero.Send(Packet);
        }

        public StatPointsPacket()
        {
            PacketBuilder.WriteUInt16(8, Packet, 0);
            PacketBuilder.WriteUInt16(1024, Packet, 2);
        }

        public byte Strength
        {
            get { return Packet[4]; }
            set { Packet[4] = value; }
        }

        public byte Agility
        {
            get { return Packet[5]; }
            set { Packet[5] = value; }
        }

        public byte Vitality
        {
            get { return Packet[6]; }
            set { Packet[6] = value; }
        }

        public byte Spirit
        {
            get { return Packet[7]; }
            set { Packet[7] = value; }
        }
    }

}
