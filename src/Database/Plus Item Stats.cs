using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public struct PlusItemStats
    {
        static public string GetBaseID(uint ID)
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
            return ID.ToString();
        }

        public const string Section = "ItemInformation";
        private IniFile ini;
        public PlusItemStats(uint ItemID, byte Plus)
        {
            ini = new IniFile(Misc.DatabasePath + "\\PItems\\" + GetBaseID(ItemID) + "[" + Plus.ToString() + "].ini");
        }
        public PlusItemStats(uint ItemID, byte Plus, IniFile rdr)
        {
            ini = rdr; 
            ini.FileName = Misc.DatabasePath + "\\PItems\\" + GetBaseID(ItemID) + "[" + Plus.ToString() + "].ini";
        }
        public uint MinAttack { get { return ini.ReadUInt32("ItemInformation", "MinAttack", 0); } }
        public uint MaxAttack { get { return ini.ReadUInt32("ItemInformation", "MaxAttack", 0); } }
        public uint MAttack { get { return ini.ReadUInt32("ItemInformation", "MAttack", 0); } }
        public ushort PhysicalDefence { get { return ini.ReadUInt16("ItemInformation", "PhysDefence", 0); } }
        public sbyte Dodge { get { return ini.ReadSByte("ItemInformation", "Dodge", 0); } }
        public ushort PlusMDefence { get { return ini.ReadUInt16("ItemInformation", "MDefence", 0); } }
        public ushort HP { get { return ini.ReadUInt16("ItemInformation", "HP", 0); } }
        public void LoadAllItem()
        {
            
        }
    }
}
