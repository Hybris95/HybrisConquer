using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic;

    enum ItemLootMode : uint
    {
        Drop = 0x1,
        Remove_Self = 0x2,
        Remove_Other = 0x3
    }
    class LootItemPacket : IClassPacket
    {
        byte[] Packet = new byte[22];

        public LootItemPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                PacketBuilder.WriteUInt16(22, Packet, 0);
                PacketBuilder.WriteUInt16(1101, Packet, 2);
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

        public uint ItemUID
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }

        public uint ItemID
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { PacketBuilder.WriteUInt32(value, Packet, 8); }
        }

        public ushort MapID
        {
            get { return BitConverter.ToUInt16(Packet, 12); }
            set { PacketBuilder.WriteUInt16(value, Packet, 12); }
        }

        public ushort X
        {
            get { return BitConverter.ToUInt16(Packet, 14); }
            set { PacketBuilder.WriteUInt16(value, Packet, 14); }
        }

        public ushort Y
        {
            get { return BitConverter.ToUInt16(Packet, 16); }
            set { PacketBuilder.WriteUInt16(value, Packet, 16); }
        }

        public uint ItemLootMode
        {
            get { return BitConverter.ToUInt32(Packet, 18); }
            set { PacketBuilder.WriteUInt32(value, Packet, 18); }
        }
    }