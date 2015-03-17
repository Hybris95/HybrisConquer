﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;
using System.IO;
using ConquerServer_Basic.Interfaces;
using ConquerServer_Basic.Networking.Packets;
using ConquerServer_Basic.Guilds;
using ConquerServer_Basic.Networking.Packet_Handling;

namespace ConquerServer_Basic
{
    class Characters
    {
        static byte Color = (byte)Kernel.Random.Next(4, 9);
        static ushort HairStyle = (ushort)(Color * 100 + 10 + (byte)Kernel.Random.Next(4, 9));
       
        static public Boolean LoadCharacter(GameClient Client)
        {
            bool res = false;
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("characters").Where("entityid", Client.Identifier);
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                res = true;
                Client.Entity.UID = r.ReadUInt32("entityid");
                Client.Entity.Name = r.ReadString("name");
                if (Client.Entity.Name == "")
                {
                    Console.WriteLine(Client.Username + " has no character. Creating character!");
                    Client.Send(new MessagePacket("NEW_ROLE", "ALLUSERS", 0xFFFFFF, MessagePacket.Dialog));
                }
                Client.Money = r.ReadUInt32("money");
                Client.Entity.Mesh = r.ReadUInt32("model");
                Client.Entity.Avatar = r.ReadUInt16("avatar");
                Client.ConquerPoints = r.ReadUInt32("conquerpoints");
                Client.Job = r.ReadByte("class");
                Client.Spouse = r.ReadString("spouse");
                Client.Entity.Reborn = r.ReadByte("reborncount");
                Client.Entity.Level = r.ReadByte("level");
                Client.Experience = r.ReadUInt64("experience");
                Client.PkPoints = r.ReadUInt16("pkpoints");
                Client.Entity.HairStyle = r.ReadUInt16("hairstyle");
                Client.Entity.MapID = r.ReadUInt16("MapID");
                Client.Entity.X = r.ReadUInt16("X");
                Client.Entity.Y = r.ReadUInt16("Y");
                if (Client.Entity.Reborn > 0)
                {
                    Client.StatPoints = r.ReadUInt16("statpoints");
                    Client.Strength = r.ReadUInt16("strength");
                    Client.Agility = r.ReadUInt16("dexterity");
                    Client.Spirit = r.ReadUInt16("spirit");
                    Client.Vitality = r.ReadUInt16("vitality");
                }
                else
                {
                    Misc.GetStats(Client);
                }
                Client.Staff = r.ReadBoolean("staff");
                Client.Entity.GuildID = r.ReadUInt16("guildid");
                Client.GuildDonation = r.ReadUInt32("guilddonation");
                Client.GuildPosition = r.ReadByte("guildposition");
                foreach (Guild guild in Kernel.Guilds.Values)
                    if (guild.ID == Client.Entity.GuildID)
                        Client.MyGuild = guild;
                Client.NobilityDonation = r.ReadUInt32("NobDonation");
                //Client.NoblePosition = r.ReadInt32("NobPosition");
                Client.NobleRank = (Empire.Ranks)r.ReadByte("NobRank");
                Client.AtkTimer = new System.Timers.Timer();
                Client.AtkTimer.Interval = Math.Max(100, 2000 - (Client.Agility * 10));
                Client.AtkTimer.Elapsed += new System.Timers.ElapsedEventHandler(Client.AtkTimer_Elapsed);
                Client.AtkTimer.Start();
                LoadProfs(Client); LoadInventory(Client); LoadEquips(Client); LoadSkills(Client);
            }

            return res;

        }
        static public void SaveCharacter(GameClient Client)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("characters");
            cmd.Set("Money", Client.Money);
            cmd.Set("Model", Client.Entity.Mesh);
            cmd.Set("Conquerpoints", Client.ConquerPoints);
            cmd.Set("Class", Client.Job);
            cmd.Set("Strength", Client.Strength);
            cmd.Set("Dexterity", Client.Agility);
            cmd.Set("Spirit", Client.Spirit);
            cmd.Set("avatar", Client.Entity.Avatar);
            cmd.Set("Vitality", Client.Vitality);
            cmd.Set("statpoints", Client.StatPoints);
            cmd.Set("level", Client.Entity.Level);
            cmd.Set("hairstyle", Client.Entity.HairStyle);
            cmd.Set("Mapid", Client.Entity.MapID);
            cmd.Set("x", Client.Entity.X);
            cmd.Set("y", Client.Entity.Y);
            cmd.Set("reborncount", Client.Entity.Reborn);
            cmd.Set("pkpoints", Client.PkPoints);
            cmd.Set("experience", Client.Experience);
            cmd.Set("spouse", Client.Spouse);
            cmd.Set("guildid", Client.Entity.GuildID);
            cmd.Set("guilddonation", Client.GuildDonation);
            cmd.Set("guildposition", Client.GuildPosition);
            cmd.Set("nobposition", Client.NoblePosition);
            cmd.Set("nobrank", (byte)Client.NobleRank);
            cmd.Set("nobdonation", Client.NobilityDonation);
            cmd.Where("entityid", Client.Entity.UID);
            cmd.Execute();
            SaveEquips(Client);
            SaveInventory(Client);
            SaveProfs(Client);
            SaveSkills(Client);

        }
        static public void CreateCharacter(GameClient Client, string CharName, uint Class, uint Model, int Avatar)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("Characters");
            cmd.Set("name", CharName);
            cmd.Set("class", Class);
            cmd.Set("avatar", Avatar);
            cmd.Set("model", Model);
            cmd.Set("hairstyle", HairStyle);
            cmd.Where("entityid", Client.Identifier);
            cmd.Execute();
        }
        
        static public void LoadInventory(GameClient Hero)
        {
            // TODO - Load Inventory from database instead of Flat File
            IniFile rdr = new IniFile(Misc.DatabasePath + @"\Inventory\" + Hero.Username + ".ini");
            sbyte count = rdr.ReadSByte("Inventory", "Count", 0);
            for (sbyte i = 0; i < count; i++)
            {
                string[] Item = (rdr.ReadString("Inventory", "Item[" + i.ToString() + "]", String.Empty)).Split(' ');
                ItemDataPacket LoadedItem = new ItemDataPacket(true);

                LoadedItem.ID = uint.Parse(Item[0]);
                LoadedItem.Plus = byte.Parse(Item[1]);
                LoadedItem.Bless = byte.Parse(Item[2]);
                LoadedItem.Enchant = byte.Parse(Item[3]);
                LoadedItem.SocketOne = byte.Parse(Item[4]);
                LoadedItem.SocketTwo = byte.Parse(Item[5]);
                LoadedItem.Durability = ushort.Parse(Item[6]);
                LoadedItem.MaxDurability = ushort.Parse(Item[7]);
                LoadedItem.UID = ItemDataPacket.NextItemUID;
                Hero.AddInventory(LoadedItem);
            }
        }
        static public void SaveInventory(GameClient Hero)
        {
            // TODO - Save the inventory in database instead of flat file
            if (File.Exists(Misc.DatabasePath + @"\Inventory\" + Hero.Username + ".ini"))
                File.Delete(Misc.DatabasePath + @"\Inventory\" + Hero.Username + ".ini");
            IniFile wrtr = new IniFile(Misc.DatabasePath + @"\Inventory\" + Hero.Username + ".ini");
            // I use a foreach loop because the Inventory
            // variables length can change at any time technically
            // seeing as it's in sync with the dictionary, but not
            // neccisarly with the function calling it
            // this is rare -- but I've had it happen.
            sbyte i = 0;
            foreach (IConquerItem Item in Hero.Inventory)
            {
                string Save = Item.ID
                    + " " + Item.Plus
                    + " " + Item.Bless
                    + " " + Item.Enchant
                    + " " + Item.SocketOne
                    + " " + Item.SocketTwo
                    + " " + Item.Durability
                + " " + Item.MaxDurability;
                wrtr.Write("Inventory", "Item[" + i.ToString() + "]", Save);
                i++;
            }
            wrtr.Write("Inventory", "Count", i.ToString());
        }

        static public void LoadProfs(GameClient Hero)
        {
            // TODO - Load Profs in Database instead of Flat File
            IniFile rdr = new IniFile(Misc.DatabasePath + @"\Profs\" + Hero.Username + ".ini");
            sbyte count = rdr.ReadSByte("Prof", "Count", 0);
            for (sbyte i = 0; i < count; i++)
            {
                string[] Skill = (rdr.ReadString("Prof", "Prof[" + i.ToString() + "]", String.Empty)).Split(' ');
                ProfPacket Profi = new ProfPacket(true);
                Profi.ID = ushort.Parse(Skill[0]);
                Profi.Level = ushort.Parse(Skill[1]);
                Profi.Experience = uint.Parse(Skill[2]);
                Hero.LearnProf(Profi);
            }
        }
        static public void SaveProfs(GameClient Hero)
        {
            // TODO - Save Profs in Database instead of Flat File
            if (File.Exists(Misc.DatabasePath + @"\Profs\" + Hero.Username + ".ini"))
                File.Delete(Misc.DatabasePath + @"\Profs\" + Hero.Username + ".ini");
            IniFile wrtr = new IniFile(Misc.DatabasePath + @"\Profs\" + Hero.Username + ".ini");

            sbyte i = 0;
            foreach (ISkill skill in Hero.Profs.Values)
            {
                wrtr.Write("Prof", "Prof[" + i + "]", skill.ID + " " + skill.Level + " " + skill.Experience);
                i++;
            }
            wrtr.Write("Prof", "Count", i.ToString());
        }

        static public void LoadSkills(GameClient Hero)
        {
            // TODO - Load Skills from Database instead of Flat File
            IniFile rdr = new IniFile(Misc.DatabasePath + @"\Skills\" + Hero.Username + ".ini");
            sbyte count = rdr.ReadSByte("Skill", "Count", 0);
            for (sbyte i = 0; i < count; i++)
            {
                string[] Skill = (rdr.ReadString("Skill", "Skill[" + i.ToString() + "]", String.Empty)).Split(' ');
                ISkill LoadedSkill = new SpellPacket(true);
                LoadedSkill.ID = ushort.Parse(Skill[0]);
                LoadedSkill.Level = ushort.Parse(Skill[1]);
                LoadedSkill.Experience = uint.Parse(Skill[2]);
                Hero.LearnSpell(LoadedSkill);
            }
        }
        static public void SaveSkills(GameClient Hero)
        {
            // TODO - Save Skills in Database instead of Flat File
            if (File.Exists(Misc.DatabasePath + @"\Skills\" + Hero.Username + ".ini"))
                File.Delete(Misc.DatabasePath + @"\Skills\" + Hero.Username + ".ini");
            IniFile wrtr = new IniFile(Misc.DatabasePath + @"\Skills\" + Hero.Username + ".ini");

            sbyte i = 0;
            foreach (ISkill skill in Hero.Spells.Values)
            {
                wrtr.Write("Skill", "Skill[" + i + "]", skill.ID + " " + skill.Level + " " + skill.Experience);
                i++;
            }
            wrtr.Write("Skill", "Count", i.ToString());
        }

        static public void LoadEquips(GameClient Client)
        {
            IniFile rdr = new IniFile(Misc.DatabasePath + @"\Equips\" + Client.Username + ".ini");
            for (sbyte i = 0; i < 9; i++)
            {
                string[] Item = (rdr.ReadString("Equips", "Item[" + i.ToString() + "]", String.Empty)).Split(' ');
                if (Item.Length < 2) continue;
                ItemDataPacket LoadedItem = new ItemDataPacket(true);
                LoadedItem.ID = uint.Parse(Item[0]);
                LoadedItem.Plus = byte.Parse(Item[1]);
                LoadedItem.Enchant = byte.Parse(Item[2]);
                LoadedItem.Bless = byte.Parse(Item[3]);
                LoadedItem.SocketOne = byte.Parse(Item[4]);
                LoadedItem.SocketTwo = byte.Parse(Item[5]);
                LoadedItem.Durability = ushort.Parse(Item[6]);
                LoadedItem.MaxDurability = ushort.Parse(Item[7]);
                LoadedItem.UID = ItemDataPacket.NextItemUID;
                Client.Equip(LoadedItem, (ushort)(i + 1));
                if (i == 8)
                {
                    if (LoadedItem.ID > 0)
                    {
                        Client.Entity.Armor = LoadedItem.ID;
                    }
                    else if (LoadedItem.ID == 0) { Client.Entity.Armor = 0; }
                }
                if (i == 0)
                {
                    if (LoadedItem.ID > 0)
                    {
                        Client.Entity.HeadGear = LoadedItem.ID;
                    }
                }
                if (i == 2)
                {
                    if (Client.Entity.Armor == 0)
                    { Client.Entity.Armor = LoadedItem.ID; }
                }
                if (i == 3)
                {
                    if (LoadedItem.ID > 0)
                    {
                        Client.Entity.LeftArm = LoadedItem.ID;
                    }
                }
                if (i == 4)
                {
                    if (LoadedItem.ID > 0)
                    {
                        Client.Entity.MainHand = LoadedItem.ID;
                    }
                }
            }
        }
        static public void SaveEquips(GameClient Client)
        {
            IniFile wrtr = new IniFile(Misc.DatabasePath + @"\Equips\" + Client.Username + ".ini");
            lock (Client.Equipment)
            {
                sbyte i = 0;
                foreach (KeyValuePair<ushort, IConquerItem> DE in Client.Equipment)
                {
                    wrtr.Write("Equips", "Item[" + (DE.Key - 1).ToString() + "]", DE.Value.ToString());
                    i++;
                }
            }
        }
        #region UpdateCharacter
        static public void UpdateCharacter(string Value, string Column, GameClient Client)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("Characters");
            cmd.Set(Column, Value);
            cmd.Where("entityid", Client.Entity.UID);
            cmd.Execute();
        }
        static public void UpdateCharacter(byte Value, string Column, GameClient Client)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("Characters");
            cmd.Set(Column, Value);
            cmd.Where("entityid", Client.Entity.UID);
            cmd.Execute();
        }
        static public void UpdateCharacter(int Value, string Column, GameClient Client)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("Characters");
            cmd.Set(Column, Value);
            cmd.Where("entityid", Client.Entity.UID);
            cmd.Execute();
        }
        static public void UpdateCharacter(ulong Value, string Column, GameClient Client)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("Characters");
            cmd.Set(Column, Value);
            cmd.Where("entityid", Client.Entity.UID);
            cmd.Execute();
        }
        static public void UpdateCharacter(ushort Value, string Column, GameClient Client)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("Characters");
            cmd.Set(Column, Value);
            cmd.Where("entityid", Client.Entity.UID);
            cmd.Execute();
        }
        static public void UpdateCharacter(uint Value, string Column, GameClient Client)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("Characters");
            cmd.Set(Column, Value);
            cmd.Where("entityid", Client.Entity.UID);
            cmd.Execute();
        }
        #endregion
    }
}
