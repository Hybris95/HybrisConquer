using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packets
{
    public class FlushExpType
    {
        public const ushort
            Proficiency = 0x0,
            Magic = 0x1;
    }
    public class FlushExpPacket : IClassPacket
    {
        byte[] Packet = new byte[11];

        public FlushExpPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                PacketBuilder.WriteUInt16(11, Packet, 0);
                PacketBuilder.WriteUInt16(1104, Packet, 2);
            }
        }

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

        public uint Experience
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }

        public ushort Type
        {
            get { return BitConverter.ToUInt16(Packet, 8); }
            set { PacketBuilder.WriteUInt16(value, Packet, 8); }
        }

        public byte Action
        {
            get { return Packet[10]; }
            set { Packet[10] = value; }
        }
    }
}
