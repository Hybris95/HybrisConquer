using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public class ItemPosition
    {
        public const ushort
            Inventory = 0,
            Head = 1,
            Necklace = 2,
            Armor = 3,
            Right = 4,
            Left = 5,
            Ring = 6,
            Bottle = 7,
            Boots = 8,
            Garment = 9,
            Remove = 255;
    }

    public class ItemMode
    {
        public const ushort
            Default = 0x01,
            Trade = 0x02,

            View = 0x04;
    }

    public class ItemDataPacket : IClassPacket, IConquerItem
    {
        private const uint NextUID_Start = 100000000;
        private const uint NextUID_Reset = 900000000;
        private static uint m_NextUID = NextUID_Start;
        /// <summary>
        /// A self incrementing counter for item uids. When a new uid is required
        /// assign from this property.
        /// </summary>
        static public uint NextItemUID
        {
            get { m_NextUID++; return m_NextUID; }
            set
            {
                if (m_NextUID >= NextUID_Reset)
                    m_NextUID = NextUID_Start;
            }
        }
        private byte[] Packet;
        public ItemDataPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                Packet = new byte[32];
                PacketBuilder.WriteUInt16(32, this.Packet, 0);
                PacketBuilder.WriteUInt16(1008, this.Packet, 2);
                Mode = ItemMode.Default;
                Durability = 100;
                MaxDurability = 100;
            }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }
        public uint ID
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { PacketBuilder.WriteUInt32(value, Packet, 8); }
        }
        public ushort Arrows
        {
            get { return BitConverter.ToUInt16(Packet, 12); }
            set { PacketBuilder.WriteUInt16(value, Packet, 12); }
        }
        public ushort Durability
        {
            get { return BitConverter.ToUInt16(Packet, 12); }
            set { PacketBuilder.WriteUInt16(value, Packet, 12); }
        }
        public ushort MaxDurability
        {
            get { return BitConverter.ToUInt16(Packet, 14); }
            set { PacketBuilder.WriteUInt16(value, Packet, 14); }
        }
        public ushort Mode
        {
            get { return BitConverter.ToUInt16(Packet, 16); }
            set { PacketBuilder.WriteUInt16(value, Packet, 16); }
        }
        public ushort Position
        {
            get { return BitConverter.ToUInt16(Packet, 18); }
            set { PacketBuilder.WriteUInt16(value, Packet, 18); }
        }
        public byte SocketOne
        {
            get { return Packet[24]; }
            set { Packet[24] = value; }
        }
        public byte SocketTwo
        {
            get { return Packet[25]; }
            set { Packet[25] = value; }
        }
        public byte Plus
        {
            get { return Packet[28]; }
            set { Packet[28] = value; }
        }
        public byte Bless
        {
            get { return Packet[29]; }
            set { Packet[29] = value; }
        }
        public byte Enchant
        {
            get { return Packet[30]; }
            set { Packet[30] = value; }
        }

        public void Deserialize(byte[] Bytes)
        {
            Packet = Bytes;
        }
        public byte[] Serialize()
        {
            return this.Packet;
        }

        static public bool Parse(string Item, out ItemDataPacket ItemData)
        {
            ItemData = new ItemDataPacket(false);
            if (Item == "")
                return false;
            ItemData = new ItemDataPacket(true); // init()
            try
            {
                string[] Info = Item.Split('-');
                ItemData.ID = uint.Parse(Info[0]);
                ItemData.Plus = byte.Parse(Info[1]);
                ItemData.Bless = byte.Parse(Info[2]);
                ItemData.Enchant = byte.Parse(Info[3]);
                ItemData.SocketOne = byte.Parse(Info[4]);
                ItemData.SocketTwo = byte.Parse(Info[5]);
                ItemData.Durability = ushort.Parse(Info[6]);
                ItemData.MaxDurability = ushort.Parse(Info[7]);
                ItemData.UID = NextItemUID;
                return true;
            }
            catch /*(IndexOutOfRangeException, FormatException)*/
            {
                return false;
            }
        }
        public override string ToString()
        {
            return
                this.ID + " " +
                this.Plus + " " +
                this.Bless + " " +
                this.Enchant + " " +
                this.SocketOne + " " +
                this.SocketTwo + " " +
                this.Durability + " " +
                this.MaxDurability;
        }
        public override int GetHashCode()
        {
            return (int)UID;
        }
        public void Send(GameClient Client)
        {
            Client.Send(Packet);
        }
    }
}
