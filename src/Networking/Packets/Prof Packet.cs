using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Interfaces;

namespace ConquerServer_Basic.Networking.Packets
{
    public class ProfPacket : IClassPacket, ISkill
    {
        byte[] Packet = new byte[16];

        public void Send(GameClient Hero)
        {
            Hero.Send(Packet);
        }
        public byte[] Serialize()
        {
            return Packet;
        }
        public void Deserialize(byte[] Packet)
        {
            this.Packet = Packet;
        }

        public ProfPacket(bool CreateInstance)
        {
            if (CreateInstance)
            {
                PacketBuilder.WriteUInt16(16, Packet, 0);
                PacketBuilder.WriteUInt16(1025, Packet, 2);
            }
        }

        public ushort ID
        {
            get { return BitConverter.ToUInt16(Packet, 4); }
            set { PacketBuilder.WriteUInt16(value, Packet, 4); }
        }
        public ushort Level
        {
            get { return BitConverter.ToUInt16(Packet, 8); }
            set { PacketBuilder.WriteUInt16(value, Packet, 8); }
        }
        public uint Experience
        {
            get { return BitConverter.ToUInt32(Packet, 12); }
            set { PacketBuilder.WriteUInt32(value, Packet, 12); }
        }
        static public bool Parse(string Prof, ProfPacket ProfData)
        {
            Console.WriteLine("Parse: {0}", Prof);
            ProfData = new ProfPacket(false);
            if (Prof == "")
                return false;
            ProfData = new ProfPacket(true); // init()
            try
            {
                string[] Info = Prof.Split(' ');
                ProfData.ID = ushort.Parse(Info[0]);
                ProfData.Level = ushort.Parse(Info[1]);
                ProfData.Experience = ushort.Parse(Info[2]);

                return true;
            }
            catch (Exception ex) /*(IndexOutOfRangeException, FormatException)*/
            {
                Console.WriteLine(ex);
                return false;
            }
        }

    }

}
