using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packets
{
    public class WeatherPacket : IClassPacket
    {
        byte[] Packet = new byte[20];

        public WeatherPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                PacketBuilder.WriteUInt16(20, Packet, 0);
                PacketBuilder.WriteUInt16(0x3f8, Packet, 2);
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

        public byte WeatherType
        {
            get { return Packet[4]; }
            set { Packet[4] = value; }
        }

        public uint Intensity // Number increasing with intensity. Valid range is 0-999 
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { PacketBuilder.WriteUInt32(value, Packet, 8); }
        }

        public uint Direction // Angle in degrees, starting from ?unknown? (0-359) 
        {
            get { return BitConverter.ToUInt32(Packet, 12); }
            set { PacketBuilder.WriteUInt32(value, Packet, 12); }
        }

        public uint Appearance // The color or style of the particular weather type. For example, WEATHER_LEAFY would have different kinds of leaves. Valid Range (1-5)
        {
            get { return BitConverter.ToUInt32(Packet, 16); }
            set { PacketBuilder.WriteUInt32(value, Packet, 16); }
        }
    }
}
