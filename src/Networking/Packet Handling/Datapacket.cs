using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer;
using ConquerServer_Basic.Main_Classes;
using ConquerServer_Basic.Guilds;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    class DataPacketHandling
    {
        static public void Portal(GameClient Hero, DataPacket Packet)
        {
            ushort portal_X = (ushort)(Packet.dwParam & 0xFFFF);
            ushort portal_Y = (ushort)(Packet.dwParam >> 16);

            string portal_ID = portal_X.ToString() + portal_Y.ToString() + Hero.Entity.MapID.ToString();

            if (Kernel.Portals.ContainsKey(portal_ID))
            {
                Console.WriteLine("Portal {0}, {1}, {2}", portal_X, portal_Y, Hero.Entity.MapID);

                if (Kernel.GetDistance(portal_X, portal_Y, Hero.Entity.X, Hero.Entity.Y) <= 1)
                {
                    Hero.Teleport(Kernel.Portals[portal_ID].DestinationMapID, Kernel.Portals[portal_ID].DestinationX, Kernel.Portals[portal_ID].DestinationY);
                    Hero.Send(Packet);

                    Hero.Screen.Reload(true,
                    delegate(IBaseEntity Sender, IBaseEntity Caller)
                    {
                        if (Caller.EntityFlag == EntityFlag.Player &&
                            Sender.EntityFlag == EntityFlag.Player)
                        {
                            GameClient __Client = Caller.Owner as GameClient;
                            __Client.Send(Hero.Entity.SpawnPacket);
                        }
                        return 0;
                    }
                );
                }
                else
                    Hero.Socket.Disconnect();
                // INVALID_PORTAL (No Such Portal)
            }
            else
            {
                Console.WriteLine("Invailed Portal {0}, {1}, {2}", portal_X, portal_Y, Hero.Entity.MapID);
                Hero.Entity.MapID = 1002;
                Hero.Entity.X = 430;
                Hero.Entity.Y = 380;
                Message.Send(Hero, "You have entered a non-existant portal and have been teleported back to Twin City.", 0x00FFFFFF, MessagePacket.Center);
                Hero.Socket.Disconnect(); // INVALID_PORTAL (No Such Portal)
            }
        }
        static public void ScreenColor(GameClient Client, uint Color)
        {
            DataPacket Packet = new DataPacket(true);
            Packet.UID = Client.Entity.UID;
            Packet.ID = DataPacket.CompleteMapChange;
            Packet.dwParam = Color;
            Client.Send(Packet);
        }
        static public void Pathfinding(GameClient Client, ushort X, ushort Y)
        {
            DataPacket General = new DataPacket(true);
            General.UID = Client.Entity.UID;
            General.wParam1 = X;
            General.wParam2 = Y; // Value 3
            General.ID = DataPacket.Pathfinding; // Packet Type
            Client.Send(General);
        }
        static public void ChangePKMode(GameClient Client, DataPacket Packet)
        {
            Client.PKMode = (PKMode)(Packet.dwParam);
            Client.SendScreen(Packet, true);
        }
        static public void ChangeAction(GameClient Client, DataPacket Packet)
        {
            Client.Entity.Action = (ConquerAction)(Packet.dwParam);
            Client.SendScreen(Packet, false);
        }
        static public void ChangeDirection(GameClient Client, DataPacket Packet)
        {
            Client.Entity.Facing = (ConquerAngle)(Packet.wParam3 % 8);
            Client.SendScreen(Packet, false);
        }
        static public void CompleteLogin(GameClient Client)
        {
            if (Kernel.Guilds.Count > 0)
                foreach (Guild guild in Kernel.Guilds.Values)
                {
                    Guild.SendGuildName(Client, guild);
                }
            if (Client.Entity.GuildID != 0)
            {
                Client.MyGuild = Guild.GetGuild(Client.Entity.GuildID);
                if (Client.MyGuild != null)
                {
                    Guild.SendGuildInfo(Client, Client.MyGuild);
                    Guild.SendBulletin(Client.MyGuild);
                    Guild.SendGuildName(Client, Client.MyGuild);
                }
            }
            else
                Client.MyGuild = null;

            Client.Send(PacketBuilder.Nobility(Client));
            Empire.NewEmpire(Client);
            Client.LoadEquipment();
            Client.CalculateMaxHP(Client);
            Client.CalculateDamageBoost();
            
            Client.Stamina = 100;
            //Client.Entity.Hitpoints = Math.Min(Client.Entity.Hitpoints, Client.Entity.MaxHitpoints);
            Client.Entity.Hitpoints = Client.Entity.MaxHitpoints;
            // ^ temp because reviving isn't added if someones dead, lol

            if (Client.Entity.Dead)
            {
                // revive
            }
            else
            {
                SyncPacket sync = new SyncPacket(4);
                sync.UID = Client.Identifier;
                sync.HitpointsAndMana(Client, 0);
                sync[2] = SyncPacket.Data.Create(SyncPacket.Stamina, Client.Stamina);
                sync[3] = SyncPacket.Data.Create(SyncPacket.RaiseFlag, Client.Entity.StatusFlag);
                Client.Send(sync);
            }
            Client.AuthPhase = AuthPhases.FullLogin;
            Client.Send(new MessagePacket("Welcome to Arco Online. We are currently in closed alpha testing.", Client.Entity.Name, "SYSTEM", Color.White, ChatType.Talk));
            Client.Send(new MessagePacket("Please refer to the commands.txt document you recieved upon downloading the client.", Client.Entity.Name, "SYSTEM", Color.White, ChatType.Talk));
            Client.Send(new MessagePacket("Please report anything to us that you find wrong. Have fun.", Client.Entity.Name, "SYSTEM", Color.White, ChatType.Talk));
        }
        static public void AppendLocation(GameClient Client, DataPacket Packet)
        {
            Packet.UID = Client.Entity.UID;
            Packet.dwParam = Client.Entity.MapID;
            Packet.wParam1 = Client.Entity.X;
            Packet.wParam2 = Client.Entity.Y;
            Client.Send(Packet);
        }
        static public void GetSurroundings(GameClient Hero)
        {
            // Spawn people who I can see to me,
            // and spawn me to them also (callback)
            Hero.Screen.Reload(true,
                    delegate(IBaseEntity Sender, IBaseEntity Caller)
                    {
                        if (Caller.EntityFlag == EntityFlag.Player &&
                            Sender.EntityFlag == EntityFlag.Player)
                        {
                            GameClient __Client = Caller.Owner as GameClient;
                            __Client.Send(Hero.Entity.SpawnPacket);
                        }
                        else if (Caller.EntityFlag == EntityFlag.Player &&
                                 Sender.EntityFlag == EntityFlag.Monster)
                        {
                            GameClient __Client = Caller.Owner as GameClient;
                            __Client.Send(Hero.Entity.SpawnPacket);
                        }
                        return 0;
                    }
                );
        }

    }
}
