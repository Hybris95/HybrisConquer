using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;
using System.IO;
using DMapLoader;
using System.Windows.Forms;

namespace ConquerServer_Basic.Conquer_Structures
{
    public class MonsterSpawn
    {
        public uint SpawnID;
        public ushort MapID;
        public ushort X;
        public ushort Y;
        public ushort Xc;
        public ushort Yc;
        public uint MaxMobs;
        public ushort RestSecs;
        public uint SpawnAmount;
        public uint MobID;
        static public void Ltoad()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("mobspawns");
            int count =0;
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                MonsterSpawn spawn = new MonsterSpawn();
                spawn.SpawnID = r.ReadUInt32("spawnid");//cq_gen.ReadUInt32("cq_generator", "id", 0);
                spawn.MapID = r.ReadUInt16("mapid");
                spawn.X = r.ReadUInt16("x");
                spawn.Y = r.ReadUInt16("y");
                spawn.Xc = r.ReadUInt16("xc");
                spawn.Yc = r.ReadUInt16("yc");
                spawn.MaxMobs = r.ReadUInt32("maxmobs");
                spawn.RestSecs = r.ReadUInt16("rest");
                spawn.SpawnAmount = r.ReadUInt32("spawncount");
                spawn.MobID = r.ReadUInt32("mobid");
                Kernel.MobSpawns.Add(spawn.SpawnID, spawn);
                count++;
            }
            Console.Write("Spawns Loaded [{0}]\n", count);
        }
        static public void SpawnMobs()
        {
            foreach (MonsterSpawn spawn in Kernel.MobSpawns.Values)
            {
                Monster ThisMob = new Monster();
                foreach (Monster mob in Kernel.Mobs.Values)
                {
                    if (mob.ID == spawn.MobID)
                    {
                        ThisMob = mob;
                        break;
                    }
                }

                for (uint i = 0; i < spawn.SpawnAmount; i++)
                {
                    Entity newMob = new Entity(EntityFlag.Monster, null);
                    newMob.Action = ConquerAction.None;
                    newMob.Dead = false;
                    newMob.Defence = (ushort)ThisMob.Defence;
                    newMob.Dodge = (sbyte)ThisMob.Dodge;
                    newMob.Facing = (ConquerAngle)Kernel.Random.Next(0, 9);
                    newMob.Hitpoints = ThisMob.Life;
                    newMob.MaxHitpoints = ThisMob.Life;
                    newMob.Level = (byte)ThisMob.Level;
                    newMob.MagicAttack = ThisMob.AttackMin;
                    newMob.MaxAttack = ThisMob.AttackMax;
                    newMob.MinAttack = ThisMob.AttackMin;
                    newMob.MDefence = (ushort)ThisMob.MagicDefence;
                    newMob.Mesh = ThisMob.Lookface;
                    newMob.Name = ThisMob.Name;
                    newMob.UID = (uint)Kernel.Random.Next(400000, 500000);
                    while (Kernel.eMonsters.ContainsKey(newMob.UID))
                        newMob.UID = (uint)Kernel.Random.Next(400000, 500000);
                    newMob.MapID = spawn.MapID;
                    newMob.X = (ushort)Kernel.Random.Next(
                        Math.Min(spawn.X, spawn.Xc),
                        Math.Max(spawn.X, spawn.Xc));
                    newMob.Y = (ushort)Kernel.Random.Next(
                        Math.Min(spawn.Y, spawn.Yc),
                        Math.Max(spawn.Y, spawn.Yc));
                    Kernel.eMonsters.Add(newMob.UID, newMob);
                }

            }

            Console.WriteLine("Monsters Spawned [{0}]", Kernel.eMonsters.Count);
        }
        static public void DespawnMobs()
        {
            Kernel.eMonsters.Clear();
            Console.WriteLine("Monsters Despawned");
        }


        static public void Load()
        {
            string filePath = Application.StartupPath + @"\MobSpawns.txt";
            // TODO - Pass this in the database
            if (File.Exists(filePath))
            {
                string[] FSpawns = File.ReadAllLines(filePath);
                ushort mobspawn = 1;
                foreach (string Spawn in FSpawns)
                {
                    if (Spawn[0] == '*') return;
                    string[] SpawnInfo = Spawn.Split(' ');
                    MonsterSpawn spawn = new MonsterSpawn();
                    spawn.SpawnID = mobspawn;
                    spawn.MobID = uint.Parse(SpawnInfo[0]);
                    spawn.SpawnAmount = uint.Parse(SpawnInfo[1]);
                    spawn.MapID = ushort.Parse(SpawnInfo[2]);
                    spawn.X = ushort.Parse(SpawnInfo[3]);
                    spawn.Y = ushort.Parse(SpawnInfo[4]);
                    spawn.Xc = ushort.Parse(SpawnInfo[5]);
                    spawn.Yc = ushort.Parse(SpawnInfo[6]);
                    Kernel.MobSpawns.Add(spawn.SpawnID, spawn);
                    mobspawn++;
                }
                Console.WriteLine("Spawns Loaded [{0}]", mobspawn - 1);
            }
            else
            {
                File.AppendAllText(filePath, "*");
                Console.WriteLine("Default Spawns File Created");
            }
        }
        static public void Unload()
        {
            Kernel.MobSpawns.Clear();
            Console.WriteLine("Spawns Unloaded");
        }
    }
}
