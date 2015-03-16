using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packets
{
    public enum StringType : byte
    {
        GuildName = 0x3,
        Spouse = 0x6,
        Effect = 0xA,
        GuildList = 0xB,
        ViewEquipSpouse = 0x10,
        Sound = 0x14,
        GuildEnemies = 0x15,
        GuildAllies = 0x16
    }
    public class StringInfoPacket : IClassPacket
    {
        byte[] Packet = new byte[28];

        public StringInfoPacket(bool CreateInstance)
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

        public uint ID
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }

        public StringType Type
        {
            get { return (StringType)Packet[8]; }
            set { Packet[8] = (byte)value; }
        }

        public byte Number
        {
            get { return Packet[9]; }
            set { Packet[9] = value; }
        }

        public byte StringLength
        {
            get { return Packet[10]; }
            set { Packet[10] = value; }
        }

        public string String
        {
            get { return BitConverter.ToString(Packet, 11 + Packet[10]); }
            set { PacketBuilder.WriteStringWithLength(value, Packet, (ushort)(11 + Packet[10])); }
        }
    }

    unsafe class StringInfoPacket2
    {
        static public byte[] Packet(uint ID, StringType Type, string String)
        {
            byte[] Buffer = new byte[11 + String.Length];
            fixed (byte* Ptr = Buffer)
            {
                *((ushort*)(Ptr)) = (ushort)(11 + String.Length);
                *((ushort*)(Ptr + 2)) = 1015;
                *((uint*)(Ptr + 4)) = ID;
                Buffer[8] = (byte)Type;
                Buffer[9] = 1;
                Buffer[10] = (byte)String.Length;

                for (int i = 0; i < String.Length; i++)
                {
                    Buffer[11 + i] = Convert.ToByte(String[i]);
                }
            }
            return Buffer;
        }
    }
}
