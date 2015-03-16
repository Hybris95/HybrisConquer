using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConquerServer_Basic.Main_Classes;
using ConquerServer_Basic.Networking.Packet_Handling;
using ConquerServer_Basic.Guilds;

namespace ConquerServer_Basic.Npc_Dialog.Twin_City
{
    class GuildDirector
    {
        public static void Npc(GameClient Hero, byte OptionID, string Input, NpcRequestPacket Packet)
        {
            switch (OptionID)
            {
                case 0:
                    {
                        NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 0",
                            "TEXT I am the Guild Director, I am in charge of all the guilds! What can I help you with?",
                            "OPTION1 Create a Guild.",
                            "OPTION5 Disband my Guild.",
                            "OPTION7 Donate Money.",
                            "OPTION9 Pass my Leadership.",
                            "OPTION3 Assign Deputy Leaders.",
                            "OPTION11 Remove Deputy Leaders.",
                            "OPTION13 Inquire about a Guild.",
                            "OPTION15 Others..."
                        });
                        break;
                    }
                case 15:
                    {
                        NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 0",
                            "TEXT I am the Guild Director, I am in charge of all the guilds! What can I help you with?",
                            "OPTION16 Enemy a Guild.",
                            "OPTION18 Reconcile with an Enemy.",
                            "OPTION20 Ally a Guild.",
                            "OPTION22 Break Ally with a Guild.",
                            "OPTION24 Guild List.",
                            "OPTION26 Online Members.",
                            "OPTION28 Expel Members.",
                            "OPTION30 Others..."
                        });
                        break;
                    }
                case 30:
                    {
                        NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 0",
                            "TEXT I am the Guild Director, I am in charge of all the guilds! What can I help you with?",
                            "OPTION31 Create Branch.",
                            "OPTION33 Assign Branch Manager.",
                            "OPTION35 Transfer Fund.",
                            "OPTION0 Others..."
                        });
                        break;
                    }
                case 1:
                    {
                        NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 0",
                            "TEXT Creating a guild costs 1,000,000 silvers and requires the creator to be level 90 or above.",
                            "INPUT2 16 Guild Name:",
                            "OPTION-1 Cancel."
                        });
                        break;
                    }
                case 2:
                    {
                        if (Hero.Entity.Level >= 90 && Hero.Money >= 1000000)
                        {
                            if (Hero.MyGuild == null)
                            {
                                string GuildName = Packet.Input;
                                foreach (Guild guild in Kernel.Guilds.Values)
                                {
                                    if (guild.Name == GuildName)
                                    {
                                        NpcProcessor.Dialog(Hero, new string[] {
                                        "AVATAR 0",
                                        "TEXT This guild already exists! Please pick a different name.",
                                        "OPTION1 Okay.",
                                        "OPTION-1 Cancel."
                                    });
                                        return;
                                    }
                                }

                                Hero.Money -= 1000000;
                                Sync.Money(Hero);

                                Guild NewGuild = new Guild();
                                NewGuild.Name = GuildName;
                                NewGuild.Leader = Hero.Entity.Name;
                                NewGuild.MemberCount = 1;
                                NewGuild.HoldingPole = false;
                                NewGuild.GwWins = 0;
                                NewGuild.Fund = 500000;
                                NewGuild.Bulletin = "Welcome to " + NewGuild.Name;
                                NewGuild.ID = (ushort)Kernel.Random.Next(1000, 9999);

                                while (Kernel.Guilds.ContainsKey(NewGuild.ID))
                                    NewGuild.ID = (ushort)Kernel.Random.Next(1000, 9999);

                                Kernel.Guilds.Add(NewGuild.ID, NewGuild);

                                Hero.Entity.GuildID = NewGuild.ID;
                                Hero.MyGuild = NewGuild;

                                Hero.GuildDonation = 500000;
                                Hero.GuildPosition = (byte)GuildPos.Leader;

                                Guild.SendGuildName(
                                    Hero,
                                    NewGuild);

                                Guild.SendGuildInfo(
                                    Hero,
                                    Hero.MyGuild);

                                Guild.SendGuildName(
                                    Hero,
                                    NewGuild);

                                Guild.SendBulletin(
                                    NewGuild);

                                Guild.SaveGuild(Hero.MyGuild);

                                Characters.SaveCharacter(Hero);

                                Message.Global(Hero.Entity.Name + " has set up " + GuildName + " successfully!", (uint)Color.White, MessagePacket.Talk);
                            }
                            else
                            {
                                NpcProcessor.Dialog(Hero, new string[] {
                                    "AVATAR 0",
                                    "TEXT You already belong to a guild!",
                                    "OPTION-1 I see.",
                                });
                            }
                        }
                        else
                        {
                            NpcProcessor.Dialog(Hero, new string[] {
                                "AVATAR 0",
                                "TEXT You are either lower than level 90 or do not have enough silvers to make a guild.",
                                "OPTION-1 I see.",
                            });
                        }
                        break;
                    }
                case 3:
                    {
                        NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 0",
                            "TEXT Write the name of the Player in your guild you wish to promote to Deputy Leader.",
                            "INPUT4 16 Character:",
                            "OPTION-1 Cancel."
                        });
                        break;
                    }
                case 4:
                    {
                        if (Hero.MyGuild != null && Hero.GuildPosition == (byte)GuildPos.Leader)
                        {
                            string CharName = Packet.Input;
                            GameClient Deputized = null;
                            bool found = false;
                            foreach (GameClient depClient in Kernel.Clients)
                            {
                                if (depClient.Entity.Name == CharName)
                                    if (depClient.MyGuild.ID == Hero.MyGuild.ID)
                                    {
                                        Deputized = depClient;
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        NpcProcessor.Dialog(Hero, new string[] {
                                            "AVATAR 0",
                                            "TEXT You do not have that Player in your guild.",
                                            "OPTION-1 I see."
                                        });
                                        return;
                                    }
                            }

                            if (found)
                            {
                                if (Deputized.GuildPosition == (byte)GuildPos.Normal)
                                {
                                    try
                                    {
                                        Deputized.GuildPosition = (byte)GuildPos.Deputy;
                                        Guild.MakeDeputy(Deputized);
                                        Guild.SendGuildInfo(Deputized, Deputized.MyGuild);
                                        Guild.SaveGuild(Deputized.MyGuild);
                                        Characters.SaveCharacter(Hero);
                                    }
                                    catch (Exception ex)
                                    { Console.WriteLine(ex); }
                                }
                                else
                                {
                                    NpcProcessor.Dialog(Hero, new string[] {
                                        "AVATAR 0",
                                        "TEXT You cannot make that Player a Deputy.",
                                        "OPTION-1 I see."
                                    });
                                }
                            }
                            else
                            {
                                NpcProcessor.Dialog(Hero, new string[] {
                                    "AVATAR 0",
                                    "TEXT Cannot find that player. They are either offline or don't exist.",
                                    "OPTION-1 I see."
                                });
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        if (Hero.MyGuild == null || Hero.GuildPosition != (byte)GuildPos.Leader)
                            return;
                        NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 0",
                            "TEXT Are you sure you wish to disband your guild? It will be forever lost in time if you do.",
                            "OPTION6 Yes.",
                            "OPTION-1 No."
                        });
                        break;
                    }
                case 6:
                    {
                        foreach (GameClient member in Kernel.Clients)
                        {
                            if (member.MyGuild != null && member.MyGuild.ID == Hero.MyGuild.ID)
                            {
                                member.GuildPosition = 0;
                                member.GuildDonation = 0;
                                member.MyGuild = null;
                                Characters.SaveCharacter(Hero);
                            }
                        }
                        foreach (string file in Directory.GetFiles(Misc.DatabasePath + @"/Accounts"))
                        {
                            IniFile ini = new IniFile(file);
                            ushort myGuildID = ini.ReadUInt16("Character", "GuildID", 0);
                            if (myGuildID == Hero.MyGuild.ID)
                            {
                                ini.Write("Character", "GuildID", 0);
                                ini.Write("Character", "GuildDonation", 0);
                                ini.Write("Character", "GuildPosition", 0);
                            }
                        }
                        Kernel.Guilds.Remove(Hero.MyGuild.ID);
                        File.Delete(Misc.DatabasePath + @"/Guilds/" + Hero.MyGuild.ID + ".ini");
                        Message.Global(Hero.Entity.Name + " disbanded his Guild " + Hero.MyGuild.Name + "!", (uint)Color.White, MessagePacket.Center);
                        Hero.MyGuild = null;
                        Hero.GuildDonation = 0;
                        Hero.GuildPosition = 0;
                        Characters.SaveCharacter(Hero);
                        DataPacketHandling.GetSurroundings(Hero);

                        break;
                    }
            }
        }

    }
}
