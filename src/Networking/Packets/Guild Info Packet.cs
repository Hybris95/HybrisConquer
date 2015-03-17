using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packets
{
    public class GuildInfoPacket : IClassPacket
    {
        byte[] Packet = new byte[46];

        public GuildInfoPacket()
        {
            PacketBuilder.WriteUInt16(46, Packet, 0);
            PacketBuilder.WriteUInt16(1106, Packet, 2);
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

        public uint GuildID
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }

        public uint Donation
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { PacketBuilder.WriteUInt32(value, Packet, 8); }
        }

        public uint Fund
        {
            get { return BitConverter.ToUInt32(Packet, 12); }
            set { PacketBuilder.WriteUInt32(value, Packet, 12); }
        }

        public uint MemberCount
        {
            get { return BitConverter.ToUInt32(Packet, 16); }
            set { PacketBuilder.WriteUInt32(value, Packet, 16); }
        }

        public byte Rank
        {
            get { return Packet[20]; }
            set { Packet[20] = value; }
        }

        public string LeaderName
        {
            get { return BitConverter.ToString(Packet, 21); }
            set { PacketBuilder.WriteStringWithLength(value, Packet, 21); }
        }

        public byte GuildPosition
        {
            get { return Packet[22 + Packet[21]]; }
            set { Packet[22 + Packet[21]] = value; }
        }

        public uint GuildNameLength
        {
            get { return BitConverter.ToUInt32(Packet, 23 + Packet[21]); }
            set { PacketBuilder.WriteUInt32(value, Packet, (ushort)(23 + Packet[21])); }
        }
    }

}
