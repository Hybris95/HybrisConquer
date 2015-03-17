using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConquerServer_Basic.Guilds;
using System.Collections;

namespace ConquerServer_Basic.Database
{
    class Guilds
    {
        static public void LoadGuilds()
        {
            // TODO - Pass this on the database
            string guildsDirectory = Misc.DatabasePath + @"\Guilds\";
            if (Directory.Exists(guildsDirectory))
            {
                foreach (string file in Directory.GetFiles(guildsDirectory))
                {
                    IniFile read = new IniFile(file);
                    Guild guild = new Guild();
                    guild.ID = read.ReadUInt16("Guild", "ID", 0);
                    guild.Name = read.ReadString("Guild", "Name", "");
                    guild.Fund = read.ReadUInt32("Guild", "Fund", 0);
                    guild.GwWins = read.ReadUInt32("Guild", "GwWins", 0);
                    guild.HoldingPole = bool.Parse(read.ReadString("Guild", "HoldingPole", "False"));
                    guild.Leader = read.ReadString("Guild", "Leader", "Error");
                    guild.MemberCount = read.ReadUInt32("Guild", "MemberCount", 0);
                    guild.Bulletin = read.ReadString("Guild", "Bulletin", "Welcome to " + guild.Name);

                    ArrayList Members = new ArrayList();
                    try
                    {
                        string[] memlist = read.ReadString("Guild", "Members", "").Split(':');
                        foreach (string member in memlist)
                            Members.Add(uint.Parse(member));
                    }
                    catch { }
                    guild.Members = Members;

                    ArrayList Deps = new ArrayList();
                    try
                    {
                        string[] deplist = read.ReadString("Guild", "DeputyLeaders", "").Split(':');
                        foreach (string dep in deplist)
                            Deps.Add(uint.Parse(dep));
                    }
                    catch { }
                    guild.DeputyLeaders = Deps;

                    ArrayList allies = new ArrayList();
                    try
                    {
                        string[] allylist = read.ReadString("Guild", "Allies", "").Split(':');
                        foreach (string ally in allylist)
                            allies.Add(ushort.Parse(ally));
                    }
                    catch { }
                    guild.Allies = allies;

                    ArrayList enemies = new ArrayList();
                    try
                    {
                        string[] enemylist = read.ReadString("Guild", "Enemies", "").Split(':');
                        foreach (string enemy in enemylist)
                            enemies.Add(ushort.Parse(enemy));
                    }
                    catch { }
                    guild.Enemies = enemies;

                    Kernel.Guilds.Add(guild.ID, guild);
                }
            }
            else
            {
                Directory.CreateDirectory(guildsDirectory);
            }
            Console.WriteLine("Guilds Loaded [{0}]", Kernel.Guilds.Count);
        }
        static public void UnloadGuilds()
        {
            Kernel.Guilds.Clear();
            Console.WriteLine("Guilds Unloaded");
        }
    }
}
