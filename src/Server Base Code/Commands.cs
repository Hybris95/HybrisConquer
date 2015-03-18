using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Networking.Packets;
using System.IO;
using System.Windows.Forms;
using ConquerServer_Basic.Main_Classes;
using ConquerServer;
using ConquerServer_Basic.Networking.Packet_Handling;

namespace ConquerServer_Basic
{
    public class ConquerCommand
    {
        static public void Parse(GameClient Client, string From, string To, string Msg)
        {
            try
            {
                string[] Cmd = Msg.Split(' ');
                Cmd[0] = Cmd[0].ToLower();
                switch (Client.Staff)
                {
                    #region Staff Commands
                    case true: switch (Cmd[0])
                        {
                        case "@screen":
                                DataPacketHandling.ScreenColor(Client, uint.Parse(Cmd[1]));
                                break;
                            case "@dc":
                            case "@logoff":
                            case "@break":
                            case "@quit":
                                Client.LogOff(); break;
                            case "@level":
                                if (byte.Parse(Cmd[1]) > 130) Client.Send(new MessagePacket("[Command Error] Cannot command level higher than 130.", (uint)Color.White, (uint)ChatType.Top));
                                else Client.Level = byte.Parse(Cmd[1]); Misc.GetStats(Client); break;
                            case "@savechar":
                                Characters.SaveCharacter(Client);
                                break;
                            case "@job":
                                Client.Job = byte.Parse(Cmd[1]); break;
                            case "@mob":
                                Entity mob = null;
                            again:
                                uint uid = (uint)Kernel.Random.Next(400000, 500000);
                            switch (Kernel.eMonsters.TryGetValue(uid, out mob))
                            {
                                case true:
                                    Console.WriteLine("Mob found!");
                                    Client.Teleport(mob.MapID, mob.X, mob.Y);
                                    break;
                                case false:
                                    Console.WriteLine("Mob not found");
                                    goto again;
                            }
                            break;
                            case "@spawn":
                            StreamWriter SW = new StreamWriter(Application.StartupPath + @"\\MobSpawns.txt", true);
                            SW.WriteLine(Cmd[1] + " " + Cmd[2] + " " + Client.Entity.MapID.ToString() + " " + Cmd[3] + " " + Cmd[4] + " " + Cmd[5] + " " + Cmd[6]);
                            //Example
                            //@spawn mobid amount xstart ystart xend yend
                            SW.Flush();
                            SW.Close();
                            break;
                            case "@prof": Client.LearnProf(ushort.Parse(Cmd[1]), ushort.Parse(Cmd[2])); break;
                            case "@spell":
                            case "@skill": Client.LearnSpell(ushort.Parse(Cmd[1]), ushort.Parse(Cmd[2])); break;
                            case "@item": // @item id plus bless enchant s1 s2
                                {
                                    IConquerItem item = new ItemDataPacket(true);
                                    item.UID = ItemDataPacket.NextItemUID;
                                    item.ID = uint.Parse(Cmd[1]);
                                    StanderdItemStats itemStats = null;
                                    item.Durability = item.MaxDurability = Kernel.ItemsStats.TryGetValue(item.ID, out itemStats) ? itemStats.Durability : (ushort)0;
                                    if (Cmd.Length > 2)
                                    {
                                        item.Plus = byte.Parse(Cmd[2]);
                                        if (Cmd.Length > 3)
                                        {
                                            item.Bless = byte.Parse(Cmd[3]);
                                            if (Cmd.Length > 4)
                                            {
                                                item.Enchant = byte.Parse(Cmd[4]);
                                                if (Cmd.Length > 5)
                                                {
                                                    item.SocketOne = byte.Parse(Cmd[5]);
                                                    if (Cmd.Length > 6)
                                                    {
                                                        item.SocketTwo = byte.Parse(Cmd[6]);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    Client.AddInventory(item);
                                    break;
                                }
                            case "@mm": Client.Teleport(ushort.Parse(Cmd[1]), ushort.Parse(Cmd[2]), ushort.Parse(Cmd[3])); break;
                            case "@money": Client.Money = uint.Parse(Cmd[1]); break;
                            case "@cps": Client.ConquerPoints = uint.Parse(Cmd[1]); break;
                            case "@scroll":
                                switch (Cmd[1])
                                {
                                    case "tc":
                                        Client.Teleport(1002, 431, 379);
                                        break;
                                    case "pc":
                                        Client.Teleport(1011, 190, 271);
                                        break;
                                    case "am":
                                        Client.Teleport(1020, 567, 576);
                                        break;
                                    case "dc":
                                        Client.Teleport(1000, 500, 650);
                                        break;
                                    case "bi":
                                        Client.Teleport(1015, 723, 573);
                                        break;
                                    case "ma":
                                        Client.Teleport(1036, 200, 200);
                                        break;
                                    case "arena":
                                        Client.Teleport(1005, 52, 69);
                                        break;
                                }
                                break;
                        }
                        break;
                    #endregion
                    #region Player Commands
                    case false: switch (Cmd[0])
                        {

                            case "@dc":
                            case "@logoff":
                            case "@break":
                            case "@quit":
                                Client.LogOff(); break;
                            case "@mm": Client.Teleport(ushort.Parse(Cmd[1]), ushort.Parse(Cmd[2]), ushort.Parse(Cmd[3])); break;
                            case "@money": Client.Money = uint.Parse(Cmd[1]); break;
                            case "@cps": Client.ConquerPoints = uint.Parse(Cmd[1]); break;
                            case "@report":
                                string Suggestion = Cmd[1].Replace('~', ' ');
                                System.IO.StreamWriter WriteSuggestion = new System.IO.StreamWriter(System.Windows.Forms.Application.StartupPath + "/Suggestions.txt", true);
                                WriteSuggestion.WriteLine(Client.Entity.Name + "'s suggestion : " + Suggestion);
                                WriteSuggestion.Flush();
                                WriteSuggestion.Dispose();
                                Client.Send(new MessagePacket("Report submitted to staff.", (uint)Color.White, (uint)ChatType.Top));
                                break;
                            case "@savechar":
                                Characters.SaveCharacter(Client);
                                break;
                            case "@prof": Client.LearnProf(ushort.Parse(Cmd[1]), ushort.Parse(Cmd[2])); break;
                            case "@spell":
                            case "@skill": Client.LearnSpell(ushort.Parse(Cmd[1]), ushort.Parse(Cmd[2])); break;
                            case "@item": // @item id plus bless enchant s1 s2
                                {
                                    IConquerItem item = new ItemDataPacket(true);
                                    item.UID = ItemDataPacket.NextItemUID;
                                    item.ID = uint.Parse(Cmd[1]);
                                    StanderdItemStats itemStats = null;
                                    item.Durability = item.MaxDurability = Kernel.ItemsStats.TryGetValue(item.ID, out itemStats) ? itemStats.Durability : (ushort)0;
                                    if (Cmd.Length > 2)
                                    {
                                        item.Plus = byte.Parse(Cmd[2]);
                                        if (Cmd.Length > 3)
                                        {
                                            item.Bless = byte.Parse(Cmd[3]);
                                            if (Cmd.Length > 4)
                                            {
                                                item.Enchant = byte.Parse(Cmd[4]);
                                                if (Cmd.Length > 5)
                                                {
                                                    item.SocketOne = byte.Parse(Cmd[5]);
                                                    if (Cmd.Length > 6)
                                                    {
                                                        item.SocketTwo = byte.Parse(Cmd[6]);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    Client.AddInventory(item);
                                    break;
                                }
                            case "@heal":
                                Client.Entity.Hitpoints = Client.Entity.MaxHitpoints;
                                Sync.HP(Client);
                                break;
                            case "@job":
                                Client.Job = byte.Parse(Cmd[1]); break;
                            case "@level":
                                if (byte.Parse(Cmd[1]) > 130) Client.Send(new MessagePacket("[Command Error] Cannot command level higher than 130.", (uint)Color.White, (uint)ChatType.Top));
                                else Client.Level = byte.Parse(Cmd[1]); Misc.GetStats(Client); NewMath.Vitals(Client); break;
                            case "@scroll":
                                switch (Cmd[1].ToLower())
                                {
                                    case "tc":
                                        Client.Teleport(1002, 431, 379);
                                        break;
                                    case "pc":
                                        Client.Teleport(1011, 190, 271);
                                        break;
                                    case "am":
                                        Client.Teleport(1020, 567, 576);
                                        break;
                                    case "dc":
                                        Client.Teleport(1000, 500, 650);
                                        break;
                                    case "bi":
                                        Client.Teleport(1015, 723, 573);
                                        break;
                                    case "ma":
                                        Client.Teleport(1036, 200, 200);
                                        break;
                                    case "arena":
                                        Client.Teleport(1005, 52, 69);
                                        break;
                                }
                                break;
                            case "@agi":
                                if (Client.StatPoints >= ushort.Parse(Cmd[1]))
                                {
                                    Client.Agility += ushort.Parse(Cmd[1]);
                                    Client.StatPoints -= ushort.Parse(Cmd[1]);
                                }
                                else
                                    Client.Send(new MessagePacket("You do not have enough statpoints", (uint)Color.Blue, (uint)ChatType.Top));
                                break;
                            case "@str":
                                if (Client.StatPoints >= ushort.Parse(Cmd[1]))
                                {
                                    Client.Strength += ushort.Parse(Cmd[1]);
                                    Client.StatPoints -= ushort.Parse(Cmd[1]);
                                }
                                else
                                    Client.Send(new MessagePacket("You do not have enough statpoints", (uint)Color.Blue, (uint)ChatType.Top));
                                break;
                            case "@spi":
                                if (Client.StatPoints >= ushort.Parse(Cmd[1]))
                                {
                                    Client.Spirit += ushort.Parse(Cmd[1]);
                                    Client.StatPoints -= ushort.Parse(Cmd[1]);
                                }
                                else
                                    Client.Send(new MessagePacket("You do not have enough statpoints", (uint)Color.Blue, (uint)ChatType.Top));
                                break;
                            case "@vit":
                                if (Client.StatPoints >= ushort.Parse(Cmd[1]))
                                {
                                    Client.Vitality += ushort.Parse(Cmd[1]);
                                    Client.StatPoints -= ushort.Parse(Cmd[1]);
                                }
                                else
                                    Client.Send(new MessagePacket("You do not have enough statpoints", (uint)Color.Blue, (uint)ChatType.Top));
                                break;
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception e)
            {
                Client.Send(new MessagePacket("[Command Error] " + e.Message, (uint)Color.White, (uint)ChatType.Top));
            }
        }
        static public void ConsoleCommand()
        {
            string Command = Console.ReadLine();
            string[] Cmd = Command.Split(' ');
            switch (Cmd[0].ToLower())
            {
                case "listacc":
                {
                    List<string> accList = Accounts.GetAccList();
                    Console.WriteLine("Accounts List:");
                    foreach (string acc in accList)
                    {
                        Console.WriteLine(acc);
                    }
                    break;
                }
                case "newacc":
                {
                    string accountName = "";
                    string accountMail = "";
                    string accountStaff = "0";
                    if (Cmd.Length < 3)
                    {
                        ConsoleUsage();
                        break;
                    }
                    else if (Cmd.Length >= 3)
                    {
                        accountName = Cmd[1];
                        accountMail = Cmd[2];
                    }
                    else if (Cmd.Length >= 4)
                    {
                        accountStaff = Cmd[3];
                    }
                    try
                    {
                        Accounts.NewAcc(accountName, accountMail, sbyte.Parse(accountStaff));
                        Console.WriteLine("Account " + accountName + " (" + accountMail + ") created.");
                    }
                    catch (ArgumentException)
                    {
                        ConsoleUsage();
                        break;
                    }
                    catch (FormatException)
                    {
                        ConsoleUsage();
                        break;
                    }
                    break;
                }
                case "exit":
                {
                    Program.StopServer();
                    break;
                }
                case "help":
                {
                    ConsoleUsage();
                    break;
                }
                default:
                {
                    ConsoleUsage();
                    break;
                }
            }
        }
        static public void ConsoleUsage()
        {
            Console.WriteLine("-- Command Help --");
            Console.WriteLine("listacc - Shows the current list of accounts created on the server");
            Console.WriteLine("newacc <accountName> <mail> [isStaff] - Creates a new account");
            Console.WriteLine("help - Shows what you are currently reading");
            Console.WriteLine("exit - Stops the server");
        }
    }
}