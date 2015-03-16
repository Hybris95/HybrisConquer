using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public class TeamPacket : IClassPacket
    {
        public const ushort
             Create = 0,
             JoinRequest = 1,
             ExitTeam = 2,
             AcceptInvitation = 3,
             InviteRequest = 4,
             AcceptJoinRequest = 5,
             Dismiss = 6,
             Kick = 7,
             ForbidJoining = 8,
             UnforbidJoining = 9,
             LootMoneyOff = 10,
             LootMoneyOn = 11,
             LootItemsOff = 12,
             LootItemsOn = 13;


        private byte[] Packet;

        public TeamPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                Packet = new byte[12];
                PacketBuilder.WriteUInt16(12, Packet, 0);
                PacketBuilder.WriteUInt16(1023, Packet, 2);
            }
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { PacketBuilder.WriteUInt32(value, Packet, 8); }
        }
        public byte[] Serialize()
        {
            return Packet;
        }
        public void Deserialize(byte[] Bytes)
        {
            Packet = Bytes;
        }
    }
}