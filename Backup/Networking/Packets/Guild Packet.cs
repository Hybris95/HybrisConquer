using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packets
{
    public enum GuildInfoType : byte
    {
        Join = 0x1,
        Invite = 0x2,
        Quit = 0x3,
        Info = 0x6,
        Allied = 0x7,
        Neutral = 0x8,
        Enemied = 0x9,
        Donate = 0xB,
        Status = 0xC,
        Leave = 0x13
    }
    class GuildPacket : IClassPacket
    {
        byte[] Packet = new byte[12];

        public GuildPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                PacketBuilder.WriteUInt16(12, Packet, 0);
                PacketBuilder.WriteUInt16(1107, Packet, 2);
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

        public GuildInfoType Type
        {
            get { return (GuildInfoType)Packet[4]; }
            set { Packet[4] = (byte)value; }
        }

        public uint Value
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { PacketBuilder.WriteUInt32(value, Packet, 8); }
        }
    }

}
