using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public struct StanderdItemStats
    {
        private IniFile ini;
        public StanderdItemStats(uint ItemID)
        {
            ini = new IniFile(Misc.DatabasePath + "\\Items\\" + ItemID.ToString() + ".ini");
        }
        public StanderdItemStats(uint ItemID, IniFile rdr)
        {
            ini = rdr;
            ini.FileName = Misc.DatabasePath + "\\Items\\" + ItemID.ToString() + ".ini";
        }
        public StanderdItemStats(uint ItemID, out IniFile rdr)
        {
            ini = new IniFile(Misc.DatabasePath + "\\Items\\" + ItemID.ToString() + ".ini");
            rdr = ini;
        }
        static public ushort GetDura(uint uid)
        {
            IniFile IF = new IniFile(Misc.DatabasePath + "\\Items\\" + uid.ToString() + ".ini");
            ushort dura = IF.ReadUInt16("ItemInformation", "Durability", 0);
            return dura;
        }
        public uint MinAttack { get { return ini.ReadUInt32("ItemInformation", "MinPhysAtk", 0); } }
        public uint MaxAttack { get { return ini.ReadUInt32("ItemInformation", "MaxPhysAtk", 0); } }
        public ushort PhysicalDefence { get { return ini.ReadUInt16("ItemInformation", "PhysDefence", 0); } }
        public ushort MDefence { get { return ini.ReadUInt16("ItemInformation", "MDefence", 0); } }
        public sbyte Dodge { get { return ini.ReadSByte("ItemInformation", "Dodge", 0); } }
        public uint MAttack { get { return ini.ReadUInt32("ItemInformation", "MAttack", 0); } }
        public ushort HP { get { return ini.ReadUInt16("ItemInformation", "PotAddHP", 0); } }
        public ushort MP { get { return ini.ReadUInt16("ItemInformation", "PotAddMP", 0); } }
        public uint Frequency { get { return ini.ReadUInt32("ItemInformation", "Frequency", 0); } }
        public sbyte AttackRange { get { return ini.ReadSByte("ItemInformation", "Range", 0); } }
        public ushort Durability { get { return ini.ReadUInt16("ItemInformation", "Durability", 0); } }
        public ushort Dexerity { get { return ini.ReadUInt16("ItemInformation", "Dexerity", 0); } }
    }
}
