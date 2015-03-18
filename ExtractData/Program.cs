using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConquerServer_Basic;

namespace ExtractData
{
    class Program
    {
        private static string itemsPath = AppDomain.CurrentDomain.BaseDirectory + @"\Items\";
        private static string miscPath = AppDomain.CurrentDomain.BaseDirectory + @"\Misc\";
        private static string sectionItemInformation = "ItemInformation";

        static void Main(string[] args)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            
            string[] items = Directory.GetFiles(itemsPath, "*.ini");
            foreach (string item in items)
            {
                IniFile iniFile = new IniFile(item);
                uint ItemID = iniFile.ReadUInt32(sectionItemInformation, "ItemID", 0);
                string ItemName = iniFile.ReadString(sectionItemInformation, "ItemName", String.Empty);
                ushort ReqJob = iniFile.ReadUInt16(sectionItemInformation, "ReqJob", 0);
                ushort ReqProfLvl = iniFile.ReadUInt16(sectionItemInformation, "ReqProfLvl", 0);
                ushort ReqLvl = iniFile.ReadUInt16(sectionItemInformation, "ReqLvl", 0);
                ushort ReqSex = iniFile.ReadUInt16(sectionItemInformation, "ReqSex", 0);
                ushort ReqStr = iniFile.ReadUInt16(sectionItemInformation, "ReqStr", 0);
                ushort ReqAgi = iniFile.ReadUInt16(sectionItemInformation, "ReqAgi", 0);
                ushort ReqVit = iniFile.ReadUInt16(sectionItemInformation, "ReqVit", 0);
                ushort ReqSpi = iniFile.ReadUInt16(sectionItemInformation, "ReqSpi", 0);
                sbyte Tradability = iniFile.ReadSByte(sectionItemInformation, "Tradability", 0);
                int Weight = iniFile.ReadInt32(sectionItemInformation, "Weight", 0);
                uint ShopBuyPrice = iniFile.ReadUInt32(sectionItemInformation, "ShopBuyPrice", 0);
                ushort Action = iniFile.ReadUInt16(sectionItemInformation, "Action", 0);
                uint MaxPhysAtk = iniFile.ReadUInt32(sectionItemInformation, "MaxPhysAtk", 0);
                uint MinPhysAtk = iniFile.ReadUInt32(sectionItemInformation, "MinPhysAtk", 0);
                uint PhysDefence = iniFile.ReadUInt32(sectionItemInformation, "PhysDefence", 0);
                ushort Dexerity = iniFile.ReadUInt16(sectionItemInformation, "Dexerity", 0);
                sbyte Dodge = iniFile.ReadSByte(sectionItemInformation, "Dodge", 0);
                uint PotAddHP = iniFile.ReadUInt32(sectionItemInformation, "PotAddHP", 0);
                uint PotAddMP = iniFile.ReadUInt32(sectionItemInformation, "PotAddMP", 0);
                ushort Durability = iniFile.ReadUInt16(sectionItemInformation, "Durability", 0);
                uint Arrows = iniFile.ReadUInt32(sectionItemInformation, "Arrows", 0);
                int Identity = iniFile.ReadInt32(sectionItemInformation, "Identity", 0);
                uint Gem1 = iniFile.ReadUInt32(sectionItemInformation, "Gem1", 0);
                uint Gem2 = iniFile.ReadUInt32(sectionItemInformation, "Gem2", 0);
                int Magic1 = iniFile.ReadInt32(sectionItemInformation, "Magic1", 0);
                int Magic2 = iniFile.ReadInt32(sectionItemInformation, "Magic2", 0);
                int Magic3 = iniFile.ReadInt32(sectionItemInformation, "Magic3", 0);
                uint MAttack = iniFile.ReadUInt32(sectionItemInformation, "MAttack", 0);
                ushort MDefence = iniFile.ReadUInt16(sectionItemInformation, "MDefence", 0);
                ushort Range = iniFile.ReadUInt16(sectionItemInformation, "Range", 0);
                uint Frequency = iniFile.ReadUInt32(sectionItemInformation, "Frequency", 0);
                int Unknown1 = iniFile.ReadInt32(sectionItemInformation, "Unknown1", 0);
                int Unknown2 = iniFile.ReadInt32(sectionItemInformation, "Unknown2", 0);
                int Unknown3 = iniFile.ReadInt32(sectionItemInformation, "Unknown3", 0);
                uint ShopCPPrice = iniFile.ReadUInt32(sectionItemInformation, "ShopCPPrice", 0);
                string Description = iniFile.ReadString(sectionItemInformation, "Description", String.Empty);
                string Properties = iniFile.ReadString(sectionItemInformation, "Properties", String.Empty);

                sqlBuilder.AppendFormat("INSERT INTO `itemstats` VALUES ({0}, '{1}', {2}, {3}, {4}, ", ItemID, ItemName.Replace("'", "''"), ReqJob, ReqProfLvl, ReqLvl);
                sqlBuilder.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, ", ReqSex, ReqStr, ReqAgi, ReqVit, ReqSpi, Tradability, Weight, ShopBuyPrice);
                sqlBuilder.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, ", Action, MaxPhysAtk, MinPhysAtk, PhysDefence, Dexerity, Dodge, PotAddHP, PotAddMP);
                sqlBuilder.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, ", Durability, Arrows, Identity, Gem1, Gem2, Magic1, Magic2, Magic3);
                sqlBuilder.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, ", MAttack, MDefence, Range, Frequency, Unknown1, Unknown2, Unknown3, ShopCPPrice);
                sqlBuilder.AppendFormat("'{0}', '{1}');{2}", Description.Replace("'", "''"), Properties.Replace("'", "''"), Environment.NewLine);
                Console.WriteLine("{0} has been added.", ItemID);
            }

            using(FileStream fS = new FileStream("itemsSQL.sql", FileMode.Create))
            {
                using(StreamWriter sW = new StreamWriter(fS))
                {
                    sW.Write(sqlBuilder.ToString());
                    sW.Flush();
                }
            }

            Console.WriteLine("Press a Key to exit the tool");
            Console.ReadKey();
        }
    }
}
