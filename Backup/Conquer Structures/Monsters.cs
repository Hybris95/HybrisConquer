using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;

namespace ConquerServer_Basic.Conquer_Structures
{
    public class Monster
    {
        public uint ID;
        public string Name;
        public byte Type;
        public ushort Lookface;
        public uint Life;
        public uint AttackMin;
        public uint AttackMax;
        public uint Defence;
        public uint Dexterity;
        public uint Dodge;
        public ushort AttackRange;
        public ushort ViewRange;
        public uint EscapeLife;
        public uint MoveSpeed;
        public ushort Level;
        public byte AttackUser;
        public uint DropMoney;
        public uint DropItemType;
        public uint DropHP;
        public uint DropMP;
        public byte MagicType;
        public uint MagicDefence;
        public uint MagicHitRate;
        public byte AIType;
        public uint Defence2;
        public uint ExtraExp;
        static public void Load()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("monsters");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                Monster mob = new Monster();
                mob.ID = r.ReadUInt16("uid");
                mob.Name = r.ReadString("name");
                mob.Type = r.ReadByte("type");
                mob.Lookface = r.ReadUInt16("face");
                mob.Life = r.ReadUInt32("hitpoints");
                mob.AttackMax = r.ReadUInt32("maxattack");
                mob.AttackMin = r.ReadUInt32("minattack");
                mob.Defence = r.ReadUInt32("defence");
                mob.Dexterity = r.ReadUInt32("dexterity");
                mob.Dodge = r.ReadUInt32("dodge");
                mob.AttackRange = r.ReadUInt16("attackrange");
                mob.ViewRange = r.ReadUInt16("viewrange");
                mob.EscapeLife = r.ReadUInt32("escapelife");
                mob.Level = r.ReadUInt16("level");
                mob.AttackUser = r.ReadByte("attackuser");
                mob.DropMoney = r.ReadUInt32("dropmoney");
                mob.DropItemType = r.ReadUInt32("dropitemtype");
                mob.DropHP = r.ReadUInt32("drophp");
                mob.DropMP = r.ReadUInt32("dropmp");
                mob.MagicType = r.ReadByte("magictype");
                mob.MagicHitRate = r.ReadUInt32("magichitrate");
                mob.MagicDefence = r.ReadUInt32("magicdefence");
                mob.AIType = r.ReadByte("aitype");
                mob.Defence2 = r.ReadUInt32("defence2");
                mob.ExtraExp = r.ReadUInt32("extraexp");

                Kernel.Mobs.Add(mob.ID, mob);
            }
            Console.Write("Monsters Loaded [{0}]\n", Kernel.Mobs.Count);
        }
    }
}
