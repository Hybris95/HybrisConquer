using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Interfaces;

namespace ConquerServer_Basic.Networking.Packets
{
    public class SpellPacket : ISkill, IClassPacket
    {
        byte[] Packet = new byte[12];

        public SpellPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                PacketBuilder.WriteUInt16(12, Packet, 0);
                PacketBuilder.WriteUInt16(1103, Packet, 2);
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
        public ushort ID
        {
            get { return BitConverter.ToUInt16(Packet, 8); }
            set { PacketBuilder.WriteUInt16(value, Packet, 8); }
        }
        public ushort Level
        {
            get { return BitConverter.ToUInt16(Packet, 10); }
            set { PacketBuilder.WriteUInt16(value, Packet, 10); }
        }
        public uint Experience
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }
    }
}
