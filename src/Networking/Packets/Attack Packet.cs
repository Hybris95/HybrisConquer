using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Interfaces;

namespace ConquerServer_Basic.Networking.Packets
{
    class AttackPacket : IAttack, IClassPacket
    {
        byte[] Packet = new byte[28];

        public AttackPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                PacketBuilder.WriteUInt16(28, Packet, 0);
                PacketBuilder.WriteUInt16(1022, Packet, 2);
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

        public uint TimeStamp
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }

        public uint AttackerUID
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { PacketBuilder.WriteUInt32(value, Packet, 8); }
        }

        public uint AttackedUID
        {
            get { return BitConverter.ToUInt32(Packet, 12); }
            set { PacketBuilder.WriteUInt32(value, Packet, 12); }
        }

        public ushort AttackedX
        {
            get { return BitConverter.ToUInt16(Packet, 16); }
            set { PacketBuilder.WriteUInt16(value, Packet, 16); }
        }

        public ushort AttackedY
        {
            get { return BitConverter.ToUInt16(Packet, 18); }
            set { PacketBuilder.WriteUInt16(value, Packet, 18); }
        }

        public ushort AttackType
        {
            get { return BitConverter.ToUInt16(Packet, 20); }
            set { PacketBuilder.WriteUInt16(value, Packet, 20); }
        }

        public ushort Blank
        {
            get { return BitConverter.ToUInt16(Packet, 22); }
            set { PacketBuilder.WriteUInt16(value, Packet, 22); }
        }

        public uint Damage
        {
            get { return BitConverter.ToUInt32(Packet, 24); }
            set { PacketBuilder.WriteUInt32(value, Packet, 24); }
        }
    }

}
