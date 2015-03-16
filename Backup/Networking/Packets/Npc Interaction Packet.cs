using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public class NpcReplyPacket : IClassPacket
    {
        public const byte
            Dialog = 1,
            Option = 2,
            Input = 3,
            Avatar = 4,
            Finish = 100;

        const int base_Size = 14;
        private byte[] Packet;

        public NpcReplyPacket()
        {
            Packet = new byte[base_Size];
            PacketBuilder.WriteUInt16((ushort)base_Size, Packet, 0);
            PacketBuilder.WriteUInt16(2032, Packet, 2);
            TimeStamp = (uint)Environment.TickCount;
        }
        public void Reset()
        {
            OptionID = unchecked((byte)-1);
            DontDisplay = true;
            Text = "";
        }

        public byte[] Serialize()
        {
            return Packet;
        }
        public void Deserialize(byte[] Bytes)
        {
            Packet = Bytes;
        }

        public uint TimeStamp
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }
        /// <summary>
        /// This should be the max length of the input string if the interact type is
        /// `Input`. This should be the avatar number if the interact type is
        /// `Avatar`. Otherwise, if it is neither of these two, it should be 0.
        /// </summary>
        public ushort wParam
        {
            get { return BitConverter.ToUInt16(Packet, 8); }
            set { PacketBuilder.WriteUInt16(value, Packet, 8); }
        }
        public byte OptionID
        {
            get { return Packet[10]; }
            set { Packet[10] = value; }
        }
        public byte InteractType
        {
            get { return Packet[11]; }
            set { Packet[11] = value; }
        }
        /// <summary>
        /// This should be set to true when your sending the packet with the
        /// interaction type `Finish`, otherwise false
        /// </summary>
        public bool DontDisplay
        {
            get { return (Packet[12] == 1); }
            set { Packet[12] = (byte)(value ? 1 : 0); }
        }
        public string Text
        {
            get { return Encoding.ASCII.GetString(Packet, 14, Packet[13]); }
            set
            {
                int realloc = value.Length + base_Size;
                if (realloc != Packet.Length)
                {
                    byte[] new_Packet = new byte[realloc];
                    Buffer.BlockCopy(Packet, 0, new_Packet, 0, base_Size);
                    Packet = new_Packet;
                }
                PacketBuilder.WriteUInt16((ushort)realloc, Packet, 0);
                PacketBuilder.WriteStringWithLength(value, Packet, 13);
            }
        }
    }
}
