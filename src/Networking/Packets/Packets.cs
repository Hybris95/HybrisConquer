using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Networking.Packet_Handling;

namespace ConquerServer_Basic
{
    public interface IClassPacket
    {
        void Deserialize(byte[] Bytes);
        byte[] Serialize();
    }

    public class PacketBuilder
    {
        static public byte[] AuthResponse(string ServerIP, uint Key1, uint Key2, ushort Port)
        {
            byte[] Packet = new byte[33];
            WriteUInt16(32, Packet, 0);
            WriteUInt16(1055, Packet, 2);
            WriteUInt32(Key2, Packet, 4);
            WriteUInt32(Key1, Packet, 8);
            WriteString(ServerIP, Packet, 12);
            WriteUInt16(Port, Packet, 28);
            return Packet;
        }
        static public byte[] CharacterInfo(GameClient Client)
        {
            byte[] Packet = new byte[((70 + Client.Spouse.Length) + Client.Entity.Name.Length) + 1];
            WriteUInt16((ushort)Packet.Length, Packet, 0);
            WriteUInt16(1006, Packet, 2);
            WriteUInt32(Client.Entity.UID, Packet, 4);
            WriteUInt32(Client.Entity.Model, Packet, 8);
            WriteUInt16(Client.Entity.HairStyle, Packet, 12);
            WriteUInt32((uint)Client.Money, Packet, 14);
            WriteUInt32((uint)Client.ConquerPoints, Packet, 18);
            WriteUInt64((uint)Client.Experience, Packet, 22);
            WriteUInt16(Client.Strength, Packet, 46);
            WriteUInt16(Client.Agility, Packet, 48);
            WriteUInt16(Client.Vitality, Packet, 50);
            WriteUInt16(Client.Spirit, Packet, 52);
            WriteUInt16(Client.StatPoints, Packet, 54);
            WriteUInt16((ushort)Client.Entity.Hitpoints, Packet, 56);
            WriteUInt16(Client.Mana, Packet, 58);
            WriteUInt16(Client.PkPoints, Packet, 60);
            Packet[62] = Client.Entity.Level;
            Packet[63] = Client.Job;
            Packet[64] = 5;
            Packet[65] = (byte)Client.Entity.Reborn;
            Packet[66] = 1;
            Packet[67] = 2;
            WriteStringWithLength(Client.Entity.Name, Packet, 68); 
            WriteStringWithLength(Client.Spouse, Packet, (ushort)(69 + Packet[68]));
            return Packet;
        }
        static public byte[] Nobility(GameClient Client)
        {
            string nobility_str = Client.Entity.UID.ToString() + " "
                + Client.NobilityDonation.ToString() + " "
                + ((byte)Client.NobleRank).ToString() + " "
                + Client.NoblePosition.ToString();
            byte[] RPacket = new byte[25 + nobility_str.Length];
            PacketBuilder.WriteUInt16((ushort)RPacket.Length, RPacket, 0);
            PacketBuilder.WriteUInt16(0x810, RPacket, 2);
            PacketBuilder.WriteUInt32(0x03, RPacket, 4);//0x03 = Icon;
            PacketBuilder.WriteUInt32(Client.Entity.UID, RPacket, 8);
            RPacket[16] = 0x01;//String Count
            PacketBuilder.WriteStringWithLength(nobility_str, RPacket, 17);
            return RPacket;
        }

        public static byte[] SendTopDonaters(uint Page)
        {
            string Str = "";
            for (int i = (int)(Page * 10); i < Page * 10 + 10; i++)
            {
                if (Empire.EmpireBoard[i].Donation != 0)
                {
                    int PotGet = 7;
                    if (i < 15) PotGet = 9;
                    if (i < 3) PotGet = 12;

                    string nStr = Empire.EmpireBoard[i].ID + " 0 0 " + Empire.EmpireBoard[i].Name + " " + Empire.EmpireBoard[i].Donation + " " + PotGet + " " + i;
                    nStr = Convert.ToChar((byte)nStr.Length) + nStr;
                    Str += nStr;
                }
            }
            byte[] Packet = new byte[25 + Str.Length];
            WriteUInt16((ushort)(Packet.Length), Packet, 0);//length
            WriteUInt16(2064, Packet, 2);
            WriteUInt32(2, Packet, 4);
            WriteUInt16((ushort)Page, Packet, 8);
            WriteUInt32(5, Packet, 10);

            Packet[16] = 10;
            WriteString(Str, Packet, 17);
            Console.WriteLine("SendTopDonaters(uint Page)");
            return Packet;
        }

        static public byte[] DropItem(uint UID, uint GID, ushort X, ushort Y)
        {
            byte[] Packet = new byte[20];
            PacketBuilder.WriteUInt16(20, Packet, 0);
            PacketBuilder.WriteUInt16(1101, Packet, 2);
            PacketBuilder.WriteUInt32(UID, Packet, 4);
            PacketBuilder.WriteUInt32(GID, Packet, 8);
            PacketBuilder.WriteUInt16(X, Packet, 12);
            PacketBuilder.WriteUInt16(Y, Packet, 14);
            PacketBuilder.WriteUInt32(1, Packet, 16);
            return Packet;
        }
        //public static byte[] DonateOpen()
        //{
        //    byte[] Packet = new byte[25];
        //    WriteUInt16((ushort)(Packet.Length), Packet, 0);//length
        //    WriteUInt16(2064, Packet, 2);
        //    WriteUInt32(4, Packet, 4);
        //    WriteUInt32(12, Packet, 8);
        //    Console.WriteLine("DonateOpen()");
        //    return Packet;
        //}
        static public byte[] RemoveItemDropEffect(uint UID, uint GID, ushort X, ushort Y)
        {
            byte[] Packet = new byte[20];
            PacketBuilder.WriteUInt16(20, Packet, 0);
            PacketBuilder.WriteUInt16(1101, Packet, 2);
            PacketBuilder.WriteUInt32(UID, Packet, 4);
            PacketBuilder.WriteUInt32(GID, Packet, 8);
            PacketBuilder.WriteUInt16(X, Packet, 12);
            PacketBuilder.WriteUInt16(Y, Packet, 14);
            PacketBuilder.WriteUInt32(3, Packet, 16);
            return Packet;
        }
        static public byte[] RemoveItemDrop(uint uid)
        {
            byte[] Packet = new byte[20];
            PacketBuilder.WriteUInt16(20, Packet, 0);
            PacketBuilder.WriteUInt16(1101, Packet, 2);
            PacketBuilder.WriteUInt32(uid, Packet, 4);
            PacketBuilder.WriteUInt32(0, Packet, 8);
            PacketBuilder.WriteUInt32(0, Packet, 12);
            PacketBuilder.WriteUInt16(3, Packet, 14);
            PacketBuilder.WriteUInt16(2, Packet, 16);
            return Packet;
        }
        static public byte[] Trade(uint UID, byte Type)
        {
            byte[] Packet = new byte[12];
            PacketBuilder.WriteUInt16(12, Packet, 0);
            PacketBuilder.WriteUInt16(1056, Packet, 2);
            WriteUInt32(UID, Packet, 4);
            WriteUInt32(Type, Packet, 8);

            return Packet;
        }
        static public byte[] TradeItem(IConquerItem I)
        {
            byte[] Packet = new byte[32];
            PacketBuilder.WriteUInt16(32, Packet, 0);
            PacketBuilder.WriteUInt16(1008, Packet, 2);
            WriteUInt32(I.UID, Packet, 4);
            WriteUInt32(I.ID, Packet, 8);
            WriteUInt16(I.Durability, Packet, 12);
            WriteUInt16(I.MaxDurability, Packet, 14);
            WriteUInt16(0x02, Packet, 16);
            WriteUInt16(I.Position, Packet, 18);
            Packet[24] = I.SocketOne;
            Packet[25] = I.SocketTwo;
            Packet[28] = I.Plus;
            Packet[29] = I.Bless;
            Packet[30] = I.Enchant;
            return Packet;
        }
        static public byte[] AuthResponse(uint type)
        {
            byte[] Packet = new byte[32];
            PacketBuilder.WriteUInt16(32, Packet, 0);
            PacketBuilder.WriteUInt16(1055, Packet, 2);
            PacketBuilder.WriteUInt32(0, Packet, 4);
            PacketBuilder.WriteUInt32(type, Packet, 8);//Note to self*****22 for banned account, 1 for wrong pass, 20 for full server
            return Packet;
        }
        static public void ZeroFill(byte[] Buffer, ushort Offset, ushort Count)
        {
            for (ushort i = 0; i < Count; i++)
                Buffer[i + Offset] = 0x00;
        }
        static public void WriteStringWithLength(string Arg, byte[] Buffer, ushort Offset)
        {
            Buffer[Offset] = (byte)Arg.Length;
            Offset++;
            ushort i = 0;
            while (i < Arg.Length)
            {
                Buffer[(ushort)(i + Offset)] = (byte)Arg[i];
                i = (ushort)(i + 1);
            }
        }
        static public void WriteString(string Arg, byte[] Buffer, ushort Offset)
        {
            ushort i = 0;
            while (i < Arg.Length)
            {
                Buffer[(ushort)(i + Offset)] = (byte)Arg[i];
                i = (ushort)(i + 1);
            }
        }
        static public void WriteUInt16(ushort Arg, byte[] Buffer, ushort Offset)
        {
            Buffer[Offset] = (byte)(Arg);
            Buffer[Offset + 1] = (byte)(Arg >> 8);
        }
        static public void WriteUInt32(uint Arg, byte[] Buffer, ushort Offset)
        {
            Buffer[Offset] = (byte)(Arg);
            Buffer[Offset + 1] = (byte)(Arg >> 8);
            Buffer[Offset + 2] = (byte)(Arg >> 16);
            Buffer[Offset + 3] = (byte)(Arg >> 24);
        }
        static public void WriteUInt64(ulong Arg, byte[] Buffer, ushort Offset)
        {
            Buffer[Offset] = (byte)(Arg);
            Buffer[Offset + 1] = (byte)(Arg >> 8);
            Buffer[Offset + 2] = (byte)(Arg >> 16);
            Buffer[Offset + 3] = (byte)(Arg >> 24);
            Buffer[Offset + 4] = (byte)(Arg >> 32);
            Buffer[Offset + 5] = (byte)(Arg >> 40);
            Buffer[Offset + 6] = (byte)(Arg >> 48);
            Buffer[Offset + 7] = (byte)(Arg >> 56);
        }
    }
}
