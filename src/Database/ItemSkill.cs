using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;

namespace ConquerServer_Basic
{
    public class ItemSkill
    {
        private ItemSkill(uint itemID, ushort skillID)
        {
            ItemID = itemID;
            SkillID = skillID;
        }
        public uint ItemID { get; private set; }
        public ushort SkillID { get; private set; }

        private static string tableName = "itemskills";
        static public void Load()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select(tableName);
            MySqlReader r = new MySqlReader(cmd);
            int count = 0;
            while (r.Read())
            {
                uint ItemID = r.ReadUInt32("itemID");
                ItemSkill itemskill = null;
                if (!Kernel.ItemsSkills.TryGetValue(ItemID, out itemskill))
                {
                    ushort SkillID = r.ReadUInt16("skillID");
                    itemskill = new ItemSkill(ItemID, SkillID);
                    Kernel.ItemsSkills.Add(itemskill.ItemID, itemskill);
                    count++;
                }
            }
            Console.WriteLine("ItemsSkills Loaded [{0}]", count);
        }
        static public void Unload()
        {
            Kernel.ItemsSkills.Clear();
            Console.WriteLine("ItemsSkills Unloaded.");
        }
    }
}
