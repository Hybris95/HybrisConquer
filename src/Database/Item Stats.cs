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

        private uint ItemID = 0;
        private uint _minAttack = 0;
        private uint _maxAttack = 0;
        private ushort _physicalDefence = 0;
        private ushort _mDefence = 0;
        private sbyte _dodge = 0;
        private uint _mAttack = 0;
        private ushort _hP = 0;
        private ushort _mP = 0;
        private uint _frequency = 0;
        private sbyte _attackRange = 0;
        private ushort _durability = 0;
        private ushort _dexterity = 0;

        public StanderdItemStats(uint ItemID)
        {
            this.ItemID = ItemID;
            InitItem();
        }

        private void InitItem()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select(tableName).Where("itemID", ItemID);
            MySqlReader r = new MySqlReader(cmd);
            while(r.Read())
            {
                _minAttack = r.ReadUInt32("MinAttack");
                _maxAttack = r.ReadUInt32("MaxAttack");
                _physicalDefence = r.ReadUInt16("PhysicalDefence");
                _mDefence = r.ReadUInt16("MDefence");
                _dodge = r.ReadSByte("Dodge");
                _mAttack = r.ReadUInt32("MAttack");
                _hP = r.ReadUInt16("HP");
                _mP = r.ReadUInt16("MP");
                _frequency = r.ReadUInt32("Frequency");
                _attackRange = r.ReadSByte("AttackRange");
                _durability = r.ReadUInt16("Durability");
                _dexterity = r.ReadUInt16("Dexterity");
            }
        }

        [Obsolete]
        public StanderdItemStats(uint ItemID, IniFile rdr) : this(ItemID) { }
        [Obsolete]
        public StanderdItemStats(uint ItemID, out IniFile rdr) : this(ItemID) { rdr = null; }

        static public ushort GetDura(uint uid)
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

        public uint MinAttack { get { return _minAttack; } }
        public uint MaxAttack { get { return _maxAttack; } }
        public ushort PhysicalDefence { get { return _physicalDefence; } }
        public ushort MDefence { get { return _mDefence; } }
        public sbyte Dodge { get { return _dodge; } }
        public uint MAttack { get { return _mAttack; } }
        public ushort HP { get { return _hP; } }
        public ushort MP { get { return _mP; } }
        public uint Frequency { get { return _frequency; } }
        public sbyte AttackRange { get { return _attackRange; } }
        public ushort Durability { get { return _durability; } }
        public ushort Dexterity { get { return _dexterity; } }
    }
}
