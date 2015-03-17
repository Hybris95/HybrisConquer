using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;
using System.Collections;
using ConquerServer_Basic.Networking.Packets;

namespace ConquerServer_Basic.Guilds
{
    public enum GuildPos : byte
    {
        Leader = 100,
        Deputy = 90,
        Normal = 50,
        None = 1
    }
    public class Guild
    {
        public ushort ID;
        public string Name;
        public string Leader;
        public uint MemberCount;
        public uint Fund;
        public uint GwWins;
        public bool HoldingPole;
        public string Bulletin;
        public ArrayList Members;
        public ArrayList DeputyLeaders;
        public ArrayList Allies;
        public ArrayList Enemies;

        static public void SendGuildInfo(
            GameClient Hero,
            Guild Guild)
        {
            GuildInfoPacket guildinfo = new GuildInfoPacket();
            guildinfo.Donation = Hero.GuildDonation;
            guildinfo.Fund = Guild.Fund;
            guildinfo.GuildID = Guild.ID;
            guildinfo.LeaderName = Guild.Leader;
            guildinfo.MemberCount = Guild.MemberCount;
            guildinfo.Rank = Hero.GuildPosition;
            guildinfo.GuildPosition = Hero.GuildPosition;
            guildinfo.GuildNameLength = (ushort)Guild.Name.Length;
            guildinfo.Send(Hero);
        }

        static public void SendGuildInfo(
            GameClient You,
            GameClient Seen,
            Guild Guild)
        {
            GuildInfoPacket guildinfo = new GuildInfoPacket();
            guildinfo.Donation = Seen.GuildDonation;
            guildinfo.Fund = Guild.Fund;
            guildinfo.GuildID = Guild.ID;
            guildinfo.LeaderName = Guild.Leader;
            guildinfo.MemberCount = Guild.MemberCount;
            guildinfo.Rank = Seen.GuildPosition;
            guildinfo.GuildPosition = Seen.GuildPosition;
            guildinfo.GuildNameLength = (ushort)Guild.Name.Length;
            guildinfo.Send(You);
        }

        static public void SendGuildName(
            GameClient Hero,
            Guild Guild)
        {
            Hero.Send(StringInfoPacket2.Packet(Guild.ID, StringType.GuildName, Guild.Name));
        }

        static public void SendGuildList(
            GameClient Hero,
            Guild Guild)
        {
            StringInfoPacket Packet = new StringInfoPacket(true);
            Packet.ID = Guild.ID;
            Packet.Type = StringType.GuildList;
            Packet.Number = 90;
            Packet.String = Guild.Name;
            Packet.StringLength = (byte)Guild.Name.Length;
            Packet.Send(Hero);
        }

        static public void SendBulletin(
            Guild Guild)
        {
            foreach (GameClient Hero in Kernel.Clients)
                if (Hero.Entity.GuildID == Guild.ID)
                    Message.Send(Hero, Guild.Bulletin, Hero.Entity.Name, (uint)Color.White, (uint)ChatType.GuildBulletin);
        }

        static public Guild GetGuild(ushort GuildID)
        {
            foreach (Guild guild in Kernel.Guilds.Values)
                if (guild.ID == GuildID)
                    return guild;
            return null;
        }

        static public void MakeDeputy(GameClient Hero)
        {
            Guild TheGuild = GetGuild(Hero.MyGuild.ID);
            TheGuild.Members.Remove(Hero.Entity.UID);
            TheGuild.DeputyLeaders.Add(Hero.Entity.UID);
        }

        static public void SaveGuild(Guild guild)
        {
            IniFile ini = new IniFile(Misc.DatabasePath + @"/Guilds/" + guild.ID + ".ini");
            ini.Write("Guild", "ID", guild.ID);
            ini.Write("Guild", "Fund", guild.Fund);
            ini.Write("Guild", "GwWins", guild.GwWins);
            ini.Write("Guild", "HoldingPole", guild.HoldingPole);
            ini.Write("Guild", "Leader", guild.Leader);
            ini.Write("Guild", "MemberCount", guild.MemberCount);
            ini.Write("Guild", "Name", guild.Name);
            ini.Write("Guild", "Bulletin", guild.Bulletin);

            string Members = ":";
            try
            {
                foreach (uint member in guild.Members)
                    Members += member + ":";
            }
            catch (Exception Ex)
            { Console.WriteLine(Ex); }
            ini.Write("Guild", "Members", Members);

            string Deps = ":";
            try
            {
                foreach (uint dep in guild.DeputyLeaders)
                    Deps += dep + ":";
            }
            catch (Exception Ex)
            { Console.WriteLine(Ex); }
            ini.Write("Guild", "DeputyLeaders", Deps);

            string allies = ":";
            try
            {
                foreach (ushort ally in guild.Allies)
                    allies += ally + ":";
            }
            catch (Exception Ex)
            { Console.WriteLine(Ex); }
            ini.Write("Guild", "Allies", allies);

            string enemies = ":";
            try
            {
                foreach (ushort enemy in guild.Allies)
                    enemies += enemy + ":";
            }
            catch (Exception Ex)
            { Console.WriteLine(Ex); }
            ini.Write("Guild", "Enemies", enemies);
        }
    }
}
