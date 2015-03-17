using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    /// <summary>
    /// You know, walking and running (can be used for monster-movement aswell).
    /// </summary>
    public class GroundMovementPacket : IClassPacket
    {
        private byte[] Packet;

        public GroundMovementPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                Packet = new byte[12];
                PacketBuilder.WriteUInt32(12, Packet, 0);
                PacketBuilder.WriteUInt32(1005, Packet, 2);
            }
        }
        public byte[] Serialize()
        {
            return Packet; 
        }
        public void Deserialize(byte[] Bytes)
        {
            Packet = Bytes;
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }
        public ConquerAngle Direction
        {
            get { return (ConquerAngle)(Packet[8] % 8); }
            set { Packet[5] = (byte)value; }
        }
        public bool IsRunning
        {
            get { return (Packet[9] == 1); }
            set { Packet[9] = (byte)(value ? 1 : 0); }
        }
    }
}
