using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySqlHandler;
using ConquerServer_Basic.Interfaces;
using System.Collections;
using ConquerServer_Basic.Main_Classes;
using ConquerServer_Basic.Networking.Packets;
using ConquerServer_Basic.Attack_Handling;
using ConquerServer_Basic.Conquer_Structures;
using ConquerServer_Basic.Networking.Packet_Handling;

namespace ConquerServer_Basic
{
    public class Misc
    {
        static public string DatabasePath;
        public static void SaveEmpire()
        {
            FileStream FS = new FileStream("nobility.dat", FileMode.OpenOrCreate);
            BinaryWriter BW = new BinaryWriter(FS);

            for (int i = 0; i < Empire.EmpireBoard.Length; i++)
                Empire.EmpireBoard[i].WriteThis(BW);


            BW.Close();
            FS.Close();
        }

        public static void LoadEmpire()
        {
            try
            {
                if (System.IO.File.Exists("nobility.dat"))
                {
                    FileStream FS = new FileStream("nobility.dat", FileMode.Open);
                    BinaryReader BR = new BinaryReader(FS);

                    for (int i = 0; i < Empire.EmpireBoard.Length; i++)
                    {
                        Empire.EmpireBoard[i].ReadThis(BR);
                        Empire.empireBoard.Add(Empire.EmpireBoard[i].ID, Empire.EmpireBoard[i]);
                    }
                    BR.Close();
                    FS.Close();
                    Console.WriteLine("Empire Loaded [{0}]", Empire.EmpireBoard.Length);
                }
            }
            catch (Exception e) { Console.Write(e.ToString()); }
        }
        public static void UnloadEmpire()
        {
            Empire.empireBoard.Clear();
            Console.WriteLine("Empire Unloaded");
        }

        static public void LoadSettings()
        {
            using (StreamReader reader = new StreamReader("settings.txt"))
            {
                string[] Input = reader.ReadToEnd().Split('\n');
                for (int I = 0; I < Input.Length; I++)
                {
                    Input[I] = Input[I].Trim(); ;
                    string[] Line = Input[I].Split('=');
                    switch (Line[0])
                    {
                        case "IP":
                            Program.GameIp = Line[1];
                            break;
                        case "USERNAME":
                            Program.Username = Line[1];
                            break;
                        case "PASSWORD":
                            Program.Password = Line[1];
                            break;
                        case "DATABASE":
                            Program.Host = Line[1];
                            break;
                    }
                    DatabasePath = Application.StartupPath;
                }
            }
        }

        static public void LoadPortals()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("portals");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                IPortal portal = new Portal();
                portal.CurrentX = r.ReadUInt16("currentx");
                portal.CurrentY = r.ReadUInt16("currenty");
                portal.CurrentMapID = r.ReadUInt16("currentmapid");
                portal.DestinationX = r.ReadUInt16("destx");
                portal.DestinationY = r.ReadUInt16("desty");
                portal.DestinationMapID = r.ReadUInt16("destmapid");
                if (!Kernel.Portals.ContainsKey(portal.CurrentX.ToString() + portal.CurrentY.ToString() + portal.CurrentMapID.ToString()))
                    Kernel.Portals.Add(portal.CurrentX.ToString() + portal.CurrentY.ToString() + portal.CurrentMapID.ToString(), portal);
                else
                    Console.WriteLine(portal.CurrentX.ToString() + " " + portal.CurrentY.ToString() + " " + portal.CurrentMapID.ToString());
            }
            Console.WriteLine("Portals Loaded [{0}]", Kernel.Portals.Count);
        }
        static public void UnloadPortals()
        {
            Kernel.Portals.Clear();
            Console.WriteLine("Portals Unloaded");
        }

        static public void GetStats(GameClient Client)
        {
            string Job;
            if (Client.Job >= 10 && Client.Job <= 15)
                Job = "Trojan";
            else if (Client.Job >= 20 && Client.Job <= 25)
                Job = "Warrior";
            else if (Client.Job >= 40 && Client.Job <= 45)
                Job = "Archer";
            else
                Job = "Taoist";
            string Lvl = Math.Min(Client.Entity.Level, (ushort)120).ToString();
            IniFile rdr = new IniFile(Application.StartupPath + @"\Misc\" + Job + ".ini");

            Client.Spirit = rdr.ReadUInt16(Lvl, "Spirit", 0);
            Client.Strength = rdr.ReadUInt16(Lvl, "Strength", 0);
            Client.Agility = rdr.ReadUInt16(Lvl, "Agility", 0);
            Client.Vitality = rdr.ReadUInt16(Lvl, "Vitality", 0);
            if (Client.Entity.Reborn == 0)
            {
                switch (Client.Level)
                {
                    case 121: Client.StatPoints = 3; break;
                    case 122: Client.StatPoints = 6; break;
                    case 123: Client.StatPoints = 9; break;
                    case 124: Client.StatPoints = 12; break;
                    case 125: Client.StatPoints = 15; break;
                    case 126: Client.StatPoints = 18; break;
                    case 127: Client.StatPoints = 21; break;
                    case 128: Client.StatPoints = 24; break;
                    case 129: Client.StatPoints = 27; break;
                    case 130: Client.StatPoints = 30; break;
                    default: Client.StatPoints = 0; break;
                }
            }
            else
            {
                Client.StatPoints = (ushort)(Client.Spirit + Client.Strength + Client.Agility + Client.Vitality);
                Client.Spirit = Client.Strength = Client.Agility = Client.Vitality = 0;
            }
        }

        static public void LoadItemStats(GameClient Client, IConquerItem Item)
        {
            IniFile rdr;
            StanderdItemStats standerd = new StanderdItemStats(Item.ID, out rdr);

            Client.Entity.Defence += standerd.PhysicalDefence;
            Client.Entity.MDefence += standerd.MDefence;
            Client.Entity.Dodge += standerd.Dodge;

            Client.BaseMagicAttack += standerd.MAttack;
            Client.ItemHP += standerd.HP;
            Client.ItemMP += standerd.MP;

            if (Item.Position == ItemPosition.Right)
            {
                Client.AttackRange += standerd.AttackRange;
            }
            if (Item.Position == ItemPosition.Left)
            {
                Client.BaseMinAttack += (uint)(standerd.MinAttack * 0.5F);
                Client.BaseMaxAttack += (uint)(standerd.MaxAttack * 0.5F);
            }
            else
            {
                Client.BaseMinAttack += standerd.MinAttack;
                Client.BaseMaxAttack += standerd.MaxAttack;
            }

            if (Item.Plus != 0)
            {
                PlusItemStats plus = new PlusItemStats(Item.ID, Item.Plus, rdr);
                Client.BaseMinAttack += plus.MinAttack;
                Client.BaseMaxAttack += plus.MaxAttack;
                Client.BaseMagicAttack += plus.MAttack;
                Client.Entity.Defence += plus.PhysicalDefence;
                Client.Entity.PlusMDefence += plus.PlusMDefence;
                Client.ItemHP += plus.HP;
            }
            if (Item.Position != ItemPosition.Garment && // Ignore these stats on these slots
                Item.Position != ItemPosition.Bottle)
            {
                Client.ItemHP += Item.Enchant;
                Client.BlessPercent += Item.Bless;
                //GemAlgorithm(Client, Item->SocketOne, Item->SocketTwo, true);
            }
        }
        static public void UnloadItemStats(GameClient Client, IConquerItem Item)
        {
            IniFile rdr;
            StanderdItemStats standerd = new StanderdItemStats(Item.ID, out rdr);

            Client.Entity.Defence -= standerd.PhysicalDefence;
            Client.Entity.MDefence -= standerd.MDefence;
            Client.Entity.Dodge -= standerd.Dodge;

            Client.BaseMagicAttack -= standerd.MAttack;
            Client.ItemHP -= standerd.HP;
            Client.ItemMP -= standerd.MP;

            if (Item.Position == ItemPosition.Right)
            {
                Client.AttackRange = 0;
            }
            if (Item.Position == ItemPosition.Left)
            {
                Client.BaseMinAttack -= (uint)(standerd.MinAttack * 0.5F);
                Client.BaseMaxAttack -= (uint)(standerd.MaxAttack * 0.5F);
            }
            else
            {
                Client.BaseMinAttack -= standerd.MinAttack;
                Client.BaseMaxAttack -= standerd.MaxAttack;
            }

            if (Item.Plus != 0)
            {
                PlusItemStats plus = new PlusItemStats(Item.ID, Item.Plus, rdr);
                Client.BaseMinAttack -= plus.MinAttack;
                Client.BaseMaxAttack -= plus.MaxAttack;
                Client.BaseMagicAttack -= plus.MAttack;
                Client.Entity.Defence -= plus.PhysicalDefence;
                Client.Entity.PlusMDefence -= plus.PlusMDefence;
                Client.ItemHP -= plus.HP;
            }
            if (Item.Position != ItemPosition.Garment && // Ignore these stats on these slots
                Item.Position != ItemPosition.Bottle)
            {
                Client.ItemHP -= Item.Enchant;
                Client.BlessPercent -= Item.Bless;
                //GemAlgorithm(Client, Item->SocketOne, Item->SocketTwo, false);
            }
        }

        static public void LoadShops()
        {
            IniFile Shop = new IniFile(Application.StartupPath + "\\Misc\\Shop.ini");
            ushort ShopCount = Shop.ReadUInt16("Header", "Amount", 0);
            for (ushort i = 0; i < ShopCount; i++)
            {
                IShop new_shop = new Shop();
                new_shop.ID = Shop.ReadUInt32("Shop" + i.ToString(), "ID", 0);
                new_shop.Name = Shop.ReadString("Shop" + i.ToString(), "Name", "");
                new_shop.Type = Shop.ReadUInt16("Shop" + i.ToString(), "Type", 1);
                new_shop.MoneyType = Shop.ReadUInt16("Shop" + i.ToString(), "MoneyType", 0);
                uint ItemAmount = Shop.ReadUInt16("Shop" + i.ToString(), "ItemAmount", 0);
                new_shop.Items = new uint[ItemAmount];
                for (ushort e = 0; e < ItemAmount; e++)
                {
                    uint ItemID = Shop.ReadUInt32("Shop" + i.ToString(), "Item" + e.ToString(), 0);
                    new_shop.Items[e] = ItemID;
                }
                Kernel.Shops.Add(new_shop.ID, new_shop);
            }
            Console.WriteLine("Shops Loaded [{0}]", Kernel.Shops.Count);
        }
        static public void UnloadShops()
        {
            Kernel.Shops.Clear();
            Console.WriteLine("Shops Unloaded");
        }

        static public void LoadSkills()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("skills");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                Skill skill = new Skill();
                skill.ID = r.ReadUInt16("id");
                skill.Accuracy = r.ReadByte("accuracy");
                skill.BaseDamage = r.ReadInt32("basedamage");
                skill.Ground = r.ReadByte("ground");
                skill.Level = r.ReadByte("level");
                skill.Mana = r.ReadUInt16("mana");
                skill.MultiTarget = r.ReadByte("multitarget");
                skill.Offensive = r.ReadByte("offensive");
                skill.Range = r.ReadByte("range");
                skill.Stamina = r.ReadByte("stamina");
                skill.Type = (SpellType)r.ReadByte("Type");
                skill.Name = r.ReadString("name");

                Dictionary<ushort, Skill> skills = null;
                while (skills == null)
                {
                    if (Kernel.Skills.TryGetValue(skill.ID, out skills))
                    {
                        try
                        {
                            skills.Add(skill.Level, skill);
                        }
                        catch (ArgumentException) { }
                    }
                    else
                    {
                        Kernel.Skills.Add(skill.ID, new Dictionary<ushort, Skill>());
                    }
                }
            }
            Console.WriteLine("Skills Loaded [{0}]", Kernel.Skills.Count);
        }
        static public void UnloadSkills()
        {
            Kernel.Skills.Clear();
            Console.WriteLine("Skills Unloaded");
        }

        static public void LoadRevivePoints()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("revpoints");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                RevivePoint rPoint = new RevivePoint();
                rPoint.DieMap = r.ReadUInt16("DieMap");
                rPoint.RevMap = r.ReadUInt16("revmap");
                rPoint.RevX = r.ReadUInt16("revx");
                rPoint.RevY = r.ReadUInt16("revy");
                Kernel.RevivePoints.Add(rPoint.DieMap, rPoint);
            }
            Console.WriteLine("Revive Points Loaded [{0}]", Kernel.RevivePoints.Count);
        }
        static public void UnloadRevivePoints()
        {
            Kernel.RevivePoints.Clear();
            Console.WriteLine("Revive Points Unloaded");
        }

        static public void LoadExp()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("expreq");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                Kernel.LevelExpReq.Add(r.ReadByte("level"), r.ReadUInt64("experience"));
            }
            Console.WriteLine("Level experience requirements loaded [{0}]", Kernel.LevelExpReq.Count);
        }
        static public void UnloadExp()
        {
            Kernel.LevelExpReq.Clear();
            Console.WriteLine("Level experience requirements unloaded.");
        }

        static public void LoadWepExp()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("wepexpreq");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                Kernel.ProfExpReq.Add(r.ReadByte("level"), r.ReadUInt64("experience"));
            }
            Console.WriteLine("Weapon experience requirements loaded [{0}]", Kernel.ProfExpReq.Count);
        }
        static public void UnloadWepExp()
        {
            Kernel.ProfExpReq.Clear();
            Console.WriteLine("Weapon experience requirements unloaded.");
        }
    }
}
