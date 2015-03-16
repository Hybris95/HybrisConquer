using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer;
using ConquerServer_Basic.Main_Classes;
using ConquerServer_Basic.Networking.Packets;
using ConquerServer_Basic.Attack_Handling;
using ConquerServer_Basic.Item;
using ConquerServer_Basic.Networking.Packet_Handling;
using ConquerServer_Basic.Item.Item_Usage_Handle;
using ConquerServer_Basic.Guilds;

namespace ConquerServer_Basic
{
    public class PacketProcessor
    {
        static public void Process(GameClient Client, byte[] Packet, ushort Type)
        {
            try
            {
                switch (Type)
                {
                    #region MessagePacket
                    case 1004:
                        {
                            MessagePacket cPacket = new MessagePacket();
                            cPacket.Deserialize(Packet);

                            if (!cPacket.Message.StartsWith("@"))
                            {
                                switch (cPacket.ChatType)
                                {
                                    default:
                                        Client.SendScreen(Packet, false);
                                        break;
                                }
                            }
                            else
                            {
                                ConquerCommand.Parse(Client, cPacket._From, cPacket._To, cPacket.Message);
                            }
                            break;
                        } 
                    #endregion

                    #region GroundMovement
                    case 1005:
                        {
                            GroundMovementPacket cPacket = new GroundMovementPacket(false);
                            cPacket.Deserialize(Packet);
                            PlayerMovement.PlayerGroundMovment(Client, cPacket);
                            break;
                        } 
                    #endregion

                    #region GameConnect
                    case 1052:
                        {
                            Gameconnect.GameConnect(Client, Packet);
                            break;
                        } 
                    #endregion

                    #region Broadcast
                    case 2050:
                        Broadcast.Handle(Client, Packet);
                        break;
                    #endregion

                    #region Create Character
                    case 1001:
                        try
                        {
                            CreateCharacter.Handle(Client, Packet);
                        }
                        finally
                        {
                            Kernel.GamePool.ThreadSafeRemove(Client.Identifier);
                            Client.Socket.Disconnect();
                        }
                        break;
                    #endregion

                    #region Compose
                    case 2036:
                        Composition.Handle(Client, Packet);
                        break;
                    #endregion

                    #region ItemLoot
                    case 1101:
                        uint UID = (uint)((Packet[7] << 24) + (Packet[6] << 16) + (Packet[5] << 8) + Packet[4]);
                        PickupMoney.Handle(Client, UID);
                        break;
                    #endregion

                    #region ItemUsage
                    case 1009:
                        {
                            ItemUsagePacket cPacket = new ItemUsagePacket(false);
                            cPacket.Deserialize(Packet);
                            switch (cPacket.ID)
                            {
                                case ItemUsagePacket.Ping:
                                    Client.Send(Packet); // Just echo it back.
                                    break;
                                case ItemUsagePacket.UnequipItem:
                                    Equipping.UnequipGear(Client, cPacket);
                                    break;
                                case ItemUsagePacket.EquipItem:
                                    Equipping.EquipGear(Client, cPacket);
                                    break;
                                case ItemUsagePacket.BuyFromNPC:
                                    BuyFromNPC.Handle(Client, cPacket);
                                    break;
                                case ItemUsagePacket.SellToNPC:
                                    SelltoNpc.Handle(Client, cPacket);
                                    break;
                                case ItemUsagePacket.MeteorUpgrade:
                                    MeteorUpgrade.Handle(Client, cPacket);
                                    break;
                                case ItemUsagePacket.DragonBallUpgrade:
                                    DragonballUpgrade.Handle(Client, cPacket);
                                    break;
                                case ItemUsagePacket.DropMoney:
                                    DropMoney.Handle(Client, cPacket); // todo
                                    break;
                                case ItemUsagePacket.DropItem:
                                    DropItem.Handle(Client, cPacket); // todo
                                    break;
                            }
                            break;
                        } 
                    #endregion

                    #region DataPacket
                    case 1010:
                        {
                            DataPacket cPacket = new DataPacket(false);
                            cPacket.Deserialize(Packet);
                            switch (cPacket.ID)
                            {
                                case DataPacket.SetLocation:
                                    DataPacketHandling.AppendLocation(Client, cPacket);
                                    break;

                                case DataPacket.ChangeDirection:
                                    DataPacketHandling.ChangeDirection(Client, cPacket);
                                    break;

                                case DataPacket.GetSurroundings:
                                    DataPacketHandling.GetSurroundings(Client);
                                    break;

                                case DataPacket.ChangeAction:
                                    DataPacketHandling.ChangeAction(Client, cPacket);
                                    break;

                                case DataPacket.Hotkeys:
                                    if (Client.AuthPhase == AuthPhases.GameServer)
                                    {
                                        Client.AuthPhase = AuthPhases.GameComplete;
                                        foreach (IConquerItem Item in Client.Inventory)
                                        {
                                            Item.Send(Client);
                                        }
                                        Client.Send(Packet);
                                    }
                                    break;

                                case DataPacket.ChangePkMode:
                                    DataPacketHandling.ChangePKMode(Client, cPacket);
                                    break;

                                case DataPacket.ConfirmFriends: // ' Not implemented yet, echo when its done
                                    Client.Send(Packet);
                                    break;

                                case DataPacket.Portal:
                                    DataPacketHandling.Portal(Client, cPacket);
                                    break;
                                case DataPacket.ConfirmSpells: // ' Not implemented, echo when its done
                                    Client.Send(Packet);
                                    break;

                                case DataPacket.ConfirmProfincies: // ' Not implemented, echo when its done
                                    Client.Send(Packet);
                                    break;

                                case DataPacket.ConfirmGuild: // ' Not implemented, echo when its done
                                    Client.Send(Packet);
                                    break;

                                case DataPacket.CompleteLogin:
                                    DataPacketHandling.CompleteLogin(Client);
                                    break;

                                case DataPacket.Jump:
                                    PlayerMovement.PlayerJump(Client, cPacket);
                                    break;
                            }
                            break;
                        } 
                    #endregion

                    #region NpcRequest
                    case 2031:
                    case 2032:
                        {
                            NpcRequestPacket cPacket = new NpcRequestPacket();
                            cPacket.Deserialize(Packet);
                            if (cPacket.OptionID != NpcRequestPacket.BreakOnCase)
                            {
                                NpcRequest.HandleNpcRequest(Client, cPacket, (Type == 2031));
                            }
                            break;
                        } 
                    #endregion

                    #region Nobility
                    case 2064:
                        {
                            uint type = BitConverter.ToUInt32(Packet, 4);
                            switch (type)
                            {
                                #region Donating
                                case 1:
                                    uint Donation = BitConverter.ToUInt32(Packet, 8);
                                    if (!Client.Staff)
                                    {
                                        if (Donation <= Client.Money)
                                        {
                                            Client.Money -= Donation;
                                            Client.NobilityDonation += Donation;
                                            Empire.NewEmpire(Client);
                                        }
                                        else
                                        {
                                            if (Donation / 50000 <= Client.ConquerPoints)
                                            {
                                                Client.ConquerPoints -= (uint)(Donation / 50000);
                                                Client.NobilityDonation += Donation;
                                                Empire.NewEmpire(Client);
                                            }
                                        }
                                        Client.SendScreen(PacketBuilder.Nobility(Client), true);
                                    }
                                    else { Message.Send(Client, "Sorry, staff is not allowed to donate to nobility.", Color.Teal, ChatType.Top); }
                                    break; 
                                #endregion
                                #region Open
                                case 2:
                                    uint Page = BitConverter.ToUInt32(Packet, 8);
                                    //Client.Send(PacketBuilder.DonateOpen());
                                    Client.Send(PacketBuilder.SendTopDonaters(Page));
                                    break; 
                                #endregion
                                #region Sort by Rank
                                case 4:
                                    Console.WriteLine("Open2");
                                    break; 
                                #endregion
                                    
                            }
                        }
                        break;
                    #endregion

                    #region Statpoints
                    case 1024:
                        {
                            if (Client.StatPoints > 0)
                            {
                                StatPointsPacket cPacket = new StatPointsPacket();
                                cPacket.Deserialize(Packet);
                                Client.Strength += (ushort)cPacket.Strength;
                                Client.Vitality += (ushort)cPacket.Vitality;
                                Client.Spirit += (ushort)cPacket.Spirit;
                                Client.Agility += (ushort)cPacket.Agility;
                                Client.StatPoints--;
                                Sync.Stats(Client);
                            }
                            break;
                        }
                    #endregion

                    #region Attack
                    case 1022:
                        {
                            AttackPacket Atk = new AttackPacket(true);
                            switch (Atk.AttackType)
                            {
                               // case 8:
                                //case 9: Marriage.Handle(Hero, Packet); break;
                                default:
                                    if (Client.Entity.MapID != 1036)
                                        AttackHandler.Handle(Client, Packet); break;
                            }
                            break;
                        }
                    #endregion

                    #region Trading
                    case 1056:
                        Trade.Handle(Client, Packet);
                        break;
                    #endregion

                    #region TeamPacket
                    case 1023: // Thanks tao & ranny & my hco source
                        {
                            TeamPacket cPacket = new TeamPacket(false);
                            cPacket.Deserialize(Packet);
                            switch (cPacket.Type)
                            {
                                case TeamPacket.Create: Teams.CreateTeam(cPacket, Client); break;
                                case TeamPacket.AcceptJoinRequest: Teams.AcceptRequestToJoinTeam(cPacket, Client); break;
                                case TeamPacket.AcceptInvitation: Teams.AcceptInviteToJoinTeam(cPacket, Client); break;
                                case TeamPacket.InviteRequest: Teams.SendInviteToJoinTeam(cPacket, Client); break;
                                case TeamPacket.JoinRequest: Teams.SendRequestJoinToTeam(cPacket, Client); break;
                                case TeamPacket.ExitTeam: Teams.LeaveTeam(cPacket, Client); break;
                                case TeamPacket.Dismiss: Teams.DismissTeam(cPacket, Client); break;
                                case TeamPacket.Kick: Teams.KickFromTeam(cPacket, Client); break;
                            }
                            break;
                        } 
                    #endregion

                    #region Guilds
                    case 1015:
                        {
                            StringInfoPacket cPacket = new StringInfoPacket(true);
                            cPacket.Deserialize(Packet);

                            switch (cPacket.Type)
                            {
                                case StringType.GuildList:
                                    Guild.SendGuildList(Client, Client.MyGuild);
                                    break;
                            }

                            break;
                        }
                    case 1107:
                        {
                            GuildPacket cPacket = new GuildPacket(true);
                            cPacket.Deserialize(Packet);

                            switch (cPacket.Type)
                            {
                                case GuildInfoType.Quit:
                                    {
                                        if (Client.MyGuild != null)
                                        {
                                            if (Client.Money >= 10000)
                                            {
                                                Client.Money -= 10000;
                                                Client.MyGuild.MemberCount--;
                                                if (Client.GuildPosition == 50)
                                                    Client.MyGuild.Members.Remove(Client.Entity.UID);
                                                else
                                                    Client.MyGuild.DeputyLeaders.Remove(Client.Entity.UID);

                                                Client.GuildPosition = 0;
                                                Client.GuildDonation = 0;
                                                Guild.SaveGuild(Client.MyGuild);
                                                Client.MyGuild = null;
                                                Characters.SaveCharacter(Client);
                                            }
                                        }
                                        break;
                                    }
                                case GuildInfoType.Join:
                                    {
                                        if (Client.MyGuild == null)
                                        {
                                            GameClient JoinClient = null;
                                            if (Kernel.GamePool.TryGetValue(cPacket.Value, out JoinClient))
                                                if (JoinClient.MyGuild != null && JoinClient.GuildPosition >= 90)
                                                {
                                                    GuildPacket gPacket = new GuildPacket(true);
                                                    gPacket.Type = GuildInfoType.Join;
                                                    gPacket.Value = Client.Entity.UID;
                                                    gPacket.Send(JoinClient);
                                                }
                                        }
                                        break;
                                    }
                                case GuildInfoType.Invite:
                                    {
                                        try
                                        {
                                            GameClient JoinClient = null;
                                            if (Kernel.GamePool.TryGetValue(cPacket.Value, out JoinClient))
                                                if (JoinClient.MyGuild == null)
                                                {
                                                    JoinClient.Entity.GuildID = Client.Entity.GuildID;
                                                    JoinClient.MyGuild = Client.MyGuild;
                                                    JoinClient.GuildDonation = 0;
                                                    JoinClient.GuildPosition = (byte)GuildPos.Normal;

                                                    Client.MyGuild.Members.Add(JoinClient.Entity.UID);
                                                    Client.MyGuild.MemberCount++;

                                                    Guild.SaveGuild(Client.MyGuild);
                                                    Characters.SaveCharacter(JoinClient);

                                                    Guild.SendGuildName(JoinClient, JoinClient.MyGuild);
                                                    Guild.SendGuildInfo(JoinClient, JoinClient.MyGuild);
                                                    Guild.SendBulletin(Client.MyGuild);
                                                }
                                        }
                                        catch (Exception ex)
                                        { Console.WriteLine(ex); }
                                        break;
                                    }
                                case GuildInfoType.Donate:
                                    {
                                        if (Client.MyGuild != null)
                                        {
                                            if (Client.Money >= cPacket.Value)
                                            {
                                                Client.Money -= cPacket.Value;
                                                Client.GuildDonation += cPacket.Value;
                                                Client.MyGuild.Fund += cPacket.Value;
                                                Sync.Money(Client);
                                                Guild.SendGuildInfo(Client, Client.MyGuild);
                                                Guild.SaveGuild(Client.MyGuild);
                                                Characters.SaveCharacter(Client);
                                                Message.Global(Client.Entity.Name + " donated " + cPacket.Value + " silvers to " + Client.MyGuild.Name + ".", (uint)Color.White, MessagePacket.TopLeft);
                                            }
                                        }
                                        break;
                                    }
                                case GuildInfoType.Status:
                                    {
                                        if (Client.MyGuild != null)
                                        {
                                            Guild.SendGuildName(Client, Client.MyGuild);
                                            Guild.SendGuildInfo(Client, Client.MyGuild);
                                            Guild.SendBulletin(Client.MyGuild);
                                            /*DataPacket dPacket = new DataPacket(true);
                                            dPacket.UID = Client.Entity.UID;
                                            dPacket.ID = DataPacket.ConfirmGuild;
                                            Client.Send(dPacket);*/
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    #endregion
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[PacketProcessor -> Dropped Client]\r\nReason:\r\n" + e.ToString());
                Client.Socket.Disconnect();
            }
        }

        static public void Revive(GameClient Hero, bool OnSpot)
        {
            if (!Hero.Entity.Dead)
                return;

            Hero.Entity.Dead = false;

            if (!OnSpot)
            {
                RevivePoint rPoint;
                if (Kernel.RevivePoints.TryGetValue(Hero.Entity.MapID, out rPoint))
                    Hero.Teleport(rPoint.RevMap, rPoint.RevX, rPoint.RevY);
                else
                    Hero.Teleport(1002, 430, 380);
            }
            else
                Hero.Teleport(Hero.Entity.MapID, Hero.Entity.X, Hero.Entity.Y);

            Hero.Entity.Hitpoints = Hero.Entity.MaxHitpoints;
            Hero.Stamina = 100;

            Hero.LoadEquipment();

            Sync.HP(Hero);
            Sync.Stamina(Hero);
            Sync.Revive(Hero);

            DataPacketHandling.GetSurroundings(Hero);

            Screen screen = new Screen(Hero);
            screen.Reload(true, null);
        }

    }
}
