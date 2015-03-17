using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public class ItemUsagePacket : IClassPacket
    {
        // special thanks to punkmak2 (e*pvp)
        public const ushort
            BuyFromNPC = 1,
            SellToNPC = 2,
            RemoveInventory = 3,
            DropItem = 3,
            EquipItem = 4,
            UnequipItem = 6,
            ViewWarehouse = 9,
            WarehouseDeposit = 10,
            WarehouseWithdraw = 11,
            DropMoney = 12,
            DragonBallUpgrade = 19,
            MeteorUpgrade = 20,
            Ping = 27;

        private byte[] Packet;

        public ItemUsagePacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                Packet = new byte[24];
                PacketBuilder.WriteUInt16(24, this.Packet, 0);
                PacketBuilder.WriteUInt16(1009, this.Packet, 2);
            }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }
        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { PacketBuilder.WriteUInt32(value, Packet, 8); }
        }
        public uint ID
        {
            get { return BitConverter.ToUInt32(Packet, 12); }
            set { PacketBuilder.WriteUInt32(value, Packet, 12); }
        }
        public uint TimeStamp
        {
            get { return BitConverter.ToUInt32(Packet, 16); }
            set { PacketBuilder.WriteUInt32(value, Packet, 16); }
        }
        public uint dwExtraInfo
        {
            get { return BitConverter.ToUInt32(Packet, 20); }
            set { PacketBuilder.WriteUInt32(value, Packet, 20); }
        }

        public void Deserialize(byte[] Bytes)
        {
            Packet = Bytes;
        }
        public byte[] Serialize()
        {
            return this.Packet;
        }
    }
}