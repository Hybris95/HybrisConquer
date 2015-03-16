using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public class DataPacket : IClassPacket
    {
        public const ushort
             SetLocation = 74,
             Hotkeys = 75,
             ConfirmFriends = 76,
             ConfirmProfincies = 77,
             ConfirmSpells = 78,
             ChangeDirection = 79,
             ChangeAction = 81,
             Portal = 85,
             EndXpList = 93,
             Revive = 94,
             ChangePkMode = 96,
             ConfirmGuild = 97,
             BEGIN_MINE = 99,
             EntitySpawn = 102,
             CompleteMapChange = 104,
             CorrectCords = 108,
             Shop = 111,
             OpenShop = 113,
             GetSurroundings = 114,
             RemoteCommands = 116,
             PickupCashEffect = 121,
             Dialog = 126,
             GuardJump = 129,
             CompleteLogin = 130,
             RemoveEntity = 132,
             Jump = 133,
             RemoveWeaponMesh = 135,
             RemoveWeaponMesh2 = 136,
             Avatar = 132,
             Pathfinding = 162;

        private byte[] Packet;

        public DataPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                this.Packet = new byte[25];
                PacketBuilder.WriteUInt16(25, this.Packet, 0);
                PacketBuilder.WriteUInt16(1010, this.Packet, 2);
                this.TimeStamp = (uint)Environment.TickCount;
            }
        }

        public void Deserialize(byte[] Bytes)
        {
            this.Packet = Bytes;
        }

        public byte[] Serialize()
        {
            return this.Packet;
        }

        public uint dwParam
        {
            get
            {
                return BitConverter.ToUInt32(this.Packet, 12);
            }
            set
            {
                PacketBuilder.WriteUInt32(value, this.Packet, 12);
            }
        }

        public ushort ID
        {
            get
            {
                return BitConverter.ToUInt16(this.Packet, 22);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.Packet, 22);
            }
        }

        public uint TimeStamp
        {
            get
            {
                return BitConverter.ToUInt32(this.Packet, 4);
            }
            set
            {
                PacketBuilder.WriteUInt32(value, this.Packet, 4);
            }
        }

        public uint UID
        {
            get
            {
                return BitConverter.ToUInt32(this.Packet, 8);
            }
            set
            {
                PacketBuilder.WriteUInt32(value, this.Packet, 8);
            }
        }

        public ushort wParam1
        {
            get
            {
                return BitConverter.ToUInt16(this.Packet, 16);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.Packet, 16);
            }
        }

        public ushort wParam2
        {
            get
            {
                return BitConverter.ToUInt16(this.Packet, 18);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.Packet, 18);
            }
        }

        public ushort wParam3
        {
            get
            {
                return BitConverter.ToUInt16(this.Packet, 20);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.Packet, 20);
            }
        }
    }
}