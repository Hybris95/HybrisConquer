using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    class NpcRequest
    {
        static public void HandleNpcRequest(GameClient Client, NpcRequestPacket Packet, bool First)
        {
            if (First)
                Client.ActiveNpcID = Packet.NpcID;
            NpcProcessor.Process(Client, Packet.OptionID, Packet.Input, Packet);
        }
        static public void ExitMarket(GameClient Hero, bool IsNpc)
        {
            if (Hero.Entity.MapID == 1036)
            {
                switch (Hero.PrevMap)
                {
                    case 1002:
                        {
                            if (IsNpc)
                                Hero.Teleport(1002, 430, 380);
                            else
                            {
                                Hero.Entity.MapID = 1002;
                                Hero.Entity.X = 430;
                                Hero.Entity.Y = 380;
                            }
                            break;
                        }
                    case 1011:
                        {
                            if (IsNpc)
                                Hero.Teleport(1011, 190, 271);
                            else
                            {
                                Hero.Entity.MapID = 1011;
                                Hero.Entity.X = 190;
                                Hero.Entity.Y = 271;
                            }
                            break;
                        }
                    case 1020:
                        {
                            if (IsNpc)
                                Hero.Teleport(1020, 567, 576);
                            else
                            {
                                Hero.Entity.MapID = 1020;
                                Hero.Entity.X = 567;
                                Hero.Entity.Y = 576;
                            }
                            break;
                        }
                    case 1000:
                        {
                            if (IsNpc)
                                Hero.Teleport(1000, 500, 650);
                            else
                            {
                                Hero.Entity.MapID = 1000;
                                Hero.Entity.X = 500;
                                Hero.Entity.Y = 650;
                            }
                            break;
                        }
                    case 1015:
                        {
                            if (IsNpc)
                                Hero.Teleport(1015, 723, 573);
                            else
                            {
                                Hero.Entity.MapID = 1015;
                                Hero.Entity.X = 723;
                                Hero.Entity.Y = 573;
                            }
                            break;
                        }
                    case 1005:
                        {
                            if (IsNpc)
                                Hero.Teleport(1005, 052, 069);
                            else
                            {
                                Hero.Entity.MapID = 1005;
                                Hero.Entity.X = 052;
                                Hero.Entity.Y = 069;
                            }
                            break;
                        }

                    default:
                        {
                            if (IsNpc)
                                Hero.Teleport(1002, 430, 380);
                            else
                            {
                                Hero.Entity.MapID = 1002;
                                Hero.Entity.X = 430;
                                Hero.Entity.Y = 380;
                            }
                            break;
                        }
                }
                Hero.PrevMap = Hero.Entity.MapID;
            }
        }

    }
}
