using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    // There are no set fields because this packet is never sent to the client
    // therefore the Serialize() method is not implemented
    public class NpcRequestPacket : IClassPacket
    {
        // If this case is sent by the client, it will not be processed
        public const byte
            BreakOnCase = unchecked((byte)-1);

        private byte[] Packet;
        public void Deserialize(byte[] Bytes)
        {
            Packet = Bytes;
        }
        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Only in the 2031 packet is this field not 0
        /// If this is the 2032 packet, then don't use this field!
        /// </summary>
        public uint NpcID
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
        }
        public byte OptionID
        {
            get { return Packet[10]; }
        }
        public string Input
        {
            get { return Encoding.ASCII.GetString(Packet, 14, Packet[13]); }
        }
    }
}
