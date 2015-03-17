using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;

namespace ConquerServer_Basic
{
    public class PlusItemStats
    {
        private static string tableName = "plusitemstats";
        private static uint GetBaseID(uint ID)
        {
            switch ((byte)(ID / 10000))
            {
                case 11:
                case 90:
                case 13:
                    {
                        ID = (uint)(
                                (((uint)(ID / 1000)) * 1000) + // [3] = 0
                                ((ID % 100) - (ID % 10)) // [5] = 0
                            );
                        break;
                    }
                case 12:
                case 15:
                case 16:
                case 50:
                    {
                        ID = (uint)(
                                ID - (ID % 10) // [5] = 0
                            );
                        break;
                    }
                default:
                    {
                        if (Kernel.IsItemType(ID, 421))
                        {
                            ID = (uint)(
                                ID - (ID % 10) // [5] = 0
                            );
                        }
                        else
                        {
                            byte head = (byte)(ID / 100000);
                            ID = (uint)(
                                    ((head * 100000) + (head * 10000) + (head * 1000)) + // [1] = [0], [2] = [0]
                                    ((ID % 1000) - (ID % 10)) // [5] = 0
                                );
                        }
                        break;
                    }
            }
            return ID;
        }

        public const string Section = "ItemInformation";

        public PlusItemStats(uint ItemID, byte Plus)
        {
            _itemID = ItemID;
            _baseID = GetBaseID(_itemID);
            _plus = Plus;
            InitItem();
        }

        [Obsolete]
        public PlusItemStats(uint ItemID, byte Plus, IniFile rdr) : this(ItemID, Plus) { }

        private void InitItem()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select(tableName).Where("itemID", _itemID).And("Plus", _plus);
            MySqlReader r = new MySqlReader(cmd);
            while(r.Read())
            {
                _minAttack = r.ReadUInt32("MinAttack");
                _maxAttack = r.ReadUInt32("MaxAttack");
                _mAttack = r.ReadUInt32("MAttack");
                _physicalDefence = r.ReadUInt16("PhysDefence");
                _dodge = r.ReadSByte("Dodge");
                _plusMDefence = r.ReadUInt16("MDefence");
                _hP = r.ReadUInt16("HP");
            }
        }

        private IniFile ini = null;
        private uint _itemID = 0;
        private uint _baseID = 0;
        private uint _plus = 0;

        private uint _minAttack = 0;
        private uint _maxAttack = 0;
        private uint _mAttack = 0;
        private ushort _physicalDefence = 0;
        private sbyte _dodge = 0;
        private ushort _plusMDefence = 0;
        private ushort _hP = 0;

        public uint MinAttack { get { return _minAttack; } }
        public uint MaxAttack { get { return _maxAttack; } }
        public uint MAttack { get { return _mAttack; } }
        public ushort PhysicalDefence { get { return _physicalDefence; } }
        public sbyte Dodge { get { return _dodge; } }
        public ushort PlusMDefence { get { return _plusMDefence; } }
        public ushort HP { get { return _hP; } }

        public void LoadAllItem()
        {
            
        }
    }
}
