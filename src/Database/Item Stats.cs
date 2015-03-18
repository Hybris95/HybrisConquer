using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;

namespace ConquerServer_Basic
{
    public class StanderdItemStats
    {
        private static string tableName = "itemstats";
        static public void Load()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select(tableName);
            MySqlReader r = new MySqlReader(cmd);
            int count = 0;
            while (r.Read())
            {
                uint ItemID = r.ReadUInt32("ItemID");
                StanderdItemStats itemstat = null;
                if (!Kernel.ItemsStats.TryGetValue(ItemID, out itemstat))
                {
                    itemstat = new StanderdItemStats(ItemID);
                    itemstat.ItemName = r.ReadString("ItemName");
                    itemstat.ReqJob = r.ReadUInt16("ReqJob");
                    itemstat.ReqProfLvl = r.ReadUInt16("ReqProfLvl");
                    itemstat.ReqLvl = r.ReadUInt16("ReqLvl");
                    itemstat.ReqSex = r.ReadUInt16("ReqSex");
                    itemstat.ReqStr = r.ReadUInt16("ReqStr");
                    itemstat.ReqAgi = r.ReadUInt16("ReqAgi");
                    itemstat.ReqVit = r.ReadUInt16("ReqVit");
                    itemstat.ReqSpi = r.ReadUInt16("ReqSpi");
                    itemstat.Tradability = r.ReadSByte("Tradability");
                    itemstat.Weight = r.ReadInt32("Weight");
                    itemstat.ShopBuyPrice = r.ReadUInt32("ShopBuyPrice");
                    itemstat.Action = r.ReadUInt16("Action");
                    itemstat.MaxPhysAtk = r.ReadUInt32("MaxPhysAtk");
                    itemstat.MinPhysAtk = r.ReadUInt32("MinPhysAtk");
                    itemstat.PhysDefence = r.ReadUInt32("PhysDefence");
                    itemstat.Dexerity = r.ReadUInt16("Dexerity");
                    itemstat.Dodge = r.ReadSByte("Dodge");
                    itemstat.PotAddHP = r.ReadUInt32("PotAddHP");
                    itemstat.PotAddMP = r.ReadUInt32("PotAddMP");
                    itemstat.Durability = r.ReadUInt16("Durability");
                    itemstat.Arrows = r.ReadUInt32("Arrows");
                    itemstat.Identity = r.ReadInt32("Identity");
                    itemstat.Gem1 = r.ReadUInt32("Gem1");
                    itemstat.Gem2 = r.ReadUInt32("Gem2");
                    itemstat.Magic1 = r.ReadInt32("Magic1");
                    itemstat.Magic2 = r.ReadInt32("Magic2");
                    itemstat.Magic3 = r.ReadInt32("Magic3");
                    itemstat.MAttack = r.ReadUInt32("MAttack");
                    itemstat.MDefence = r.ReadUInt16("MDefence");
                    itemstat.Range = r.ReadUInt16("Range");
                    itemstat.Frequency = r.ReadUInt32("Frequency");
                    itemstat.Unknown1 = r.ReadInt32("Unknown1");
                    itemstat.Unknown2 = r.ReadInt32("Unknown2");
                    itemstat.Unknown3 = r.ReadInt32("Unknown3");
                    itemstat.ShopCPPrice = r.ReadUInt32("ShopCPPrice");
                    itemstat.Description = r.ReadString("Description");
                    itemstat.Properties = r.ReadString("Properties");
                    Kernel.ItemsStats.Add(itemstat.ItemID, itemstat);
                    count++;
                }
            }
            Console.WriteLine("ItemsStats Loaded [{0}]", count);
        }
        static public void Unload()
        {
            Kernel.ItemsStats.Clear();
            Console.WriteLine("ItemsStats Unloaded.");
        }

        public uint ItemID { get; private set; }
        public string ItemName { get; private set; }
        public ushort ReqJob { get; private set; }
        public ushort ReqProfLvl { get; private set; }
        public ushort ReqLvl { get; private set; }
        public ushort ReqSex { get; private set; }
        public ushort ReqStr { get; private set; }
        public ushort ReqAgi { get; private set; }
        public ushort ReqVit { get; private set; }
        public ushort ReqSpi { get; private set; }
        public sbyte Tradability { get; private set; }
        public int Weight { get; private set; }
        public uint ShopBuyPrice { get; private set; }
        public ushort Action { get; private set; }
        public uint MaxPhysAtk { get; private set; }
        public uint MinPhysAtk { get; private set; }
        public uint PhysDefence { get; private set; }
        public ushort Dexerity { get; private set; }
        public sbyte Dodge { get; private set; }
        public uint PotAddHP { get; private set; }
        public uint PotAddMP { get; private set; }
        public ushort Durability { get; private set; }
        public uint Arrows { get; private set; }
        public int Identity { get; private set; }
        public uint Gem1 { get; private set; }
        public uint Gem2 { get; private set; }
        public int Magic1 { get; private set; }
        public int Magic2 { get; private set; }
        public int Magic3 { get; private set; }
        public uint MAttack { get; private set; }
        public ushort MDefence { get; private set; }
        public ushort Range { get; private set; }
        public uint Frequency { get; private set; }
        public int Unknown1 { get; private set; }
        public int Unknown2 { get; private set; }
        public int Unknown3 { get; private set; }
        public uint ShopCPPrice { get; private set; }
        public string Description { get; private set; }
        public string Properties { get; private set; }

        private StanderdItemStats(uint ItemID)
        {
            this.ItemID = ItemID;
        }

        [Obsolete]
        static private ushort GetDura(uint uid)
        {
            ushort dura = 0;
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select(tableName).Where("itemID", uid);
            MySqlReader r = new MySqlReader(cmd);
            if (r.Read())
            {
                dura = r.ReadUInt16("Durability");
            }
            return dura;
        }
    }
}
