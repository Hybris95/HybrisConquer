using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public class AddToTeamPacket : IClassPacket
    {
        private byte[] Packet;
        public AddToTeamPacket()
        {
            Packet = new byte[36];
            PacketBuilder.WriteUInt16(36, Packet, 0);
            PacketBuilder.WriteUInt16(1026, Packet, 2);
        }
        public string Name
        {
            set
            {
                Packet[5] = 0x01;
                PacketBuilder.WriteString(value, Packet, 8);
                PacketBuilder.ZeroFill(Packet, (ushort)(8 + value.Length), (ushort)(16 - value.Length));
            }
            get
            {
                return Encoding.ASCII.GetString(Packet, 8, 16).Trim('\0');
            }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Packet, 24); }
            set { PacketBuilder.WriteUInt32(value, Packet, 24); }
        }
        public uint Mesh
        {
            get { return BitConverter.ToUInt32(Packet, 28); }
            set { PacketBuilder.WriteUInt32(value, Packet, 28); }
        }
        public ushort MaxHitpoints
        {
            get { return BitConverter.ToUInt16(Packet, 32); }
            set {PacketBuilder.WriteUInt16(value, Packet, 32); }
        }
        public ushort Hitpoints
        {
            get { return BitConverter.ToUInt16(Packet, 34); }
            set {PacketBuilder.WriteUInt16(value, Packet, 34); }
        }

        public byte[] Serialize()
        {
            return Packet;
        }
        public void Deserialize(byte[] Bytes)
        {
            throw new NotImplementedException();
        }
    }
}
