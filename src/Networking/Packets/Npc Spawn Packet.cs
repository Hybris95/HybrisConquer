using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;

namespace ConquerServer_Basic
{
    public class Npc : IClassPacket, INpc, IMapObject
    {
        private byte[] Packet;
        private ushort m_MapID;

        public Npc()
        {
            Packet = new byte[20];
            PacketBuilder.WriteUInt16(20, Packet, 0);
            PacketBuilder.WriteUInt16(2030, Packet, 2);
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { PacketBuilder.WriteUInt32(value, Packet, 4); }
        }
        public ushort X
        {
            get { return BitConverter.ToUInt16(Packet, 8); }
            set { PacketBuilder.WriteUInt16(value, Packet, 8); }
        }
        public ushort Y
        {
            get { return BitConverter.ToUInt16(Packet, 10); }
            set { PacketBuilder.WriteUInt16(value, Packet, 10); }
        }
        public ushort Type
        {
            get { return BitConverter.ToUInt16(Packet, 12); }
            set { PacketBuilder.WriteUInt16(value, Packet, 12); }
        }
        public ConquerAngle Facing
        {
            get { return (ConquerAngle)Packet[14]; }
            set { Packet[14] = (byte)value; }
        }
        public uint StatusFlag
        {
            get { return BitConverter.ToUInt32(Packet, 16); }
            set { PacketBuilder.WriteUInt32(value, Packet, 16); }
        }
        public ushort MapID { get { return m_MapID; } set { m_MapID = value; } }
        public MapObjectType MapObjType { get { return MapObjectType.Npc; } }
        public object Owner { get { return this; } }
        public void SendSpawn(GameClient Client, bool Ignore)
        {
            if (Client.Screen.Add(this) || Ignore)
            {
                Client.Send(Packet);
            }
        }
        public void SendSpawn(GameClient Client)
        {
            SendSpawn(Client, false);
        }

        public byte[] Serialize()
        {
            return Packet;
        }
        public void Deserialize(byte[] Bytes)
        {
            Packet = Bytes;
        }
        static public void Load()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("npc");
            MySqlReader r = new MySqlReader(cmd);
            int count = 0;
            while (r.Read())
            {
                INpc npc = new Npc();
                npc.UID = r.ReadUInt32("uid");
                npc.X = r.ReadUInt16("x");
                npc.Y = r.ReadUInt16("y");
                npc.MapID = r.ReadUInt16("mapid");
                npc.Type = r.ReadUInt16("type");
                npc.Facing = (ConquerAngle)r.ReadUInt16("facing");
                npc.StatusFlag = r.ReadUInt32("statusflag");
                count++;
                Dictionary<uint, INpc> npcs = null;
                while (npcs == null)
                {
                    if (Kernel.Npcs.TryGetValue(npc.MapID, out npcs))
                    {
                        // Add a npc to the existing list of npcs in the given MapID
                        try
                        {
                            npcs.Add(npc.UID, npc);
                        }
                        catch (ArgumentException) { }
                    }
                    else
                    {
                        // Create an empty npc list for the given MapID
                        Kernel.Npcs.Add(npc.MapID, new Dictionary<uint, INpc>());
                    }
                }
            }
            Console.WriteLine("Npcs Loaded [{0}]", count);
        }
        static public void Unload()
        {
            Kernel.Npcs.Clear();
            Console.WriteLine("Npcs Unloaded");
        }

    }
}
