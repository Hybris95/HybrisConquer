using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    public class Trade
    {
        public const byte
        Request = 1,
        Close = 2,
        ShowTable = 3,
        HideTable = 5,
        AddItem = 6,
        SetMoney = 7,
        ShowMoney = 8,
        Accept = 10,
        RemoveItem = 11,
        ShowConquerPoints = 12,
        SetConquerPoints = 13;

        static void CancelTrade(GameClient C)
        {
            if (C.Trading)
            {
                GameClient Who = (GameClient)Kernel.GamePool[C.TradingWith];
                if (Who != null)
                {
                    Who.Send(PacketBuilder.Trade(C.TradingWith, 5));
                    Who.Trading = false;
                    Who.TradingWith = 0;
                    Who.TradeSide.Clear();
                    Who.TradingCPs = 0;
                    Who.TradingSilvers = 0;
                    Who.ClickedOK = false;
                    Who.Money = Who.Money;//update the Money
                    Who.ConquerPoints = Who.ConquerPoints;//update the ConquerPoints

                    Message.Send(Who, "Trading Failed!", Who.Entity.Name, "SYSTEM", (uint)Color.White, 2005);
                }
                C.Send(PacketBuilder.Trade(C.TradingWith, 5));
                C.Trading = false;
                C.TradingWith = 0;

                C.TradeSide = new System.Collections.ArrayList(20);
                C.TradingCPs = 0;
                C.TradingSilvers = 0;
                C.ClickedOK = false;
                C.ConquerPoints = C.ConquerPoints;//update the ConquerPoints
                C.Money = C.Money;//update the Money
                Message.Send(C, "Trading Failed!", C.Entity.Name, "SYSTEM", (uint)Color.White, 2005);

            }
        }
        static public void Handle(GameClient C, byte[] Data)
        {
            uint UID = BitConverter.ToUInt32(Data, 4);
            byte Type = Data[8];

            switch (Type)
            {
                #region Request Trade
                case 1:
                    {
                        GameClient Who = (GameClient)Kernel.GamePool[UID];
                        if (Who != null && !Who.Trading)
                        {
                            if (!C.Trading)
                            {
                                if (Who.Entity.UID != C.TradingWith)
                                {
                                    C.TradingWith = UID;
                                    if (Who.Entity.UID == C.TradingWith && Who.TradingWith == C.Entity.UID)
                                    {
                                        Who.Send(PacketBuilder.Trade(C.Entity.UID, 3));
                                        C.Send(PacketBuilder.Trade(Who.Entity.UID, 3));
                                        C.Trading = true;
                                        Who.Trading = true;
                                        break;
                                    }
                                    else
                                    {
                                        //C.AddSend(Packets.ChatMessage(C.MessageID, "SYSTEM", C.Name, "[Trade]Request for trading has been sent out.", 2005, 0));
                                        Message.Send(C, "Request for trading has been sent out.", C.Entity.Name, "SYSTEM", (uint)Color.White, 2005);

                                        Who.Send(PacketBuilder.Trade(C.Entity.UID, 1));
                                    }
                                }
                                if (Who.Entity.UID == C.TradingWith && Who.TradingWith == C.Entity.UID)
                                {
                                    Who.Send(PacketBuilder.Trade(C.Entity.UID, 3));
                                    C.Send(PacketBuilder.Trade(Who.Entity.UID, 3));
                                    C.Trading = true;
                                    Who.Trading = true;
                                }
                            }
                            else
                                //C.AddSend(Packets.ChatMessage(C.MessageID, "SYSTEM", C.Name, "[Trade]Close the current trade before you take another one.", 2005, 0));
                                Message.Send(C, "Please close your current trade before starting another one.", C.Entity.Name, "SYSTEM", (uint)Color.White, 2005);

                        }
                        else
                            //C.AddSend(Packets.ChatMessage(C.MessageID, "SYSTEM", C.Name, "[Trade]The target is trading with someone else.", 2005, 0));
                            Message.Send(C, "The target is trading with someone else.", C.Entity.Name, "SYSTEM", (uint)Color.White, 2005);

                        break;
                    }
                #endregion
                #region Close Trade
                case 2:
                    {
                        CancelTrade(C);
                        break;
                    }
                #endregion
                #region Trade Item
                case 6:
                    {
                        GameClient Who = (GameClient)Kernel.GamePool[C.TradingWith];
                        if (Who != null)
                        {
                            if (C.TradeSide.Count < 20)
                            {
                                if (Who.Inventory.Length + C.TradeSide.Count < 40)
                                {
                                    IConquerItem I = C.GetInventoryItem(UID);
                                    Who.Send(PacketBuilder.TradeItem(I));
                                    C.TradeSide.Add(I.UID);
                                }
                                else
                                {
                                    C.Send(PacketBuilder.Trade(UID, 11));
                                    Message.Send(C, "Your partner cannot hold anymore items.", C.Entity.Name, "SYSTEM", (uint)Color.White, 2005);

                                    Message.Send(Who, "The one your trading with cant add anymore items on the table because you have no room in your inventory.", Who.Entity.Name, "SYSTEM", (uint)Color.White, 2005);

                                }
                            }

                        }
                        break;
                    }
                #endregion
                #region Set Money
                case 7:
                    {
                        C.TradingSilvers = UID;
                        GameClient Who = (GameClient)Kernel.GamePool[C.TradingWith];
                        Who.Send(PacketBuilder.Trade(UID, 8));

                        break;
                    }
                #endregion
                #region Set CPs
                case 13:
                    {
                        C.TradingCPs = UID;
                        GameClient Who = (GameClient)Kernel.GamePool[C.TradingWith];
                        Who.Send(PacketBuilder.Trade(UID, 12));

                        break;
                    }
                #endregion
                #region Accept Trade
                case 10:
                    {
                        GameClient Who = (GameClient)Kernel.GamePool[C.TradingWith];
                        if (Who != null && Who.ClickedOK)
                        {
                            if (C.Money >= C.TradingSilvers && C.ConquerPoints >= C.TradingCPs && Who.Money >= Who.TradingSilvers && Who.ConquerPoints >= Who.TradingCPs)
                            {
                                Who.Send(PacketBuilder.Trade(C.TradingWith, 5));
                                C.Send(PacketBuilder.Trade(C.Entity.UID, 5));

                                Who.Money += C.TradingSilvers;
                                Who.Money -= Who.TradingSilvers;
                                C.Money += Who.TradingSilvers;
                                C.Money -= C.TradingSilvers;

                                Who.ConquerPoints += C.TradingCPs;
                                Who.ConquerPoints -= Who.TradingCPs;
                                C.ConquerPoints += Who.TradingCPs;
                                C.ConquerPoints -= C.TradingCPs;


                                foreach (uint Id in C.TradeSide)
                                {
                                    IConquerItem I = C.GetInventoryItem(Id);
                                    Who.AddInventory(I);
                                    C.RemoveInventory(I.UID);
                                }
                                foreach (uint Id in Who.TradeSide)
                                {
                                    IConquerItem I = Who.GetInventoryItem(Id);
                                    C.AddInventory(I);
                                    Who.RemoveInventory(I.UID);
                                }

                                Who.Trading = false;
                                Who.TradingWith = 0;
                                Who.TradeSide = new System.Collections.ArrayList(20);
                                Who.TradingCPs = 0;
                                Who.TradingSilvers = 0;
                                Who.ClickedOK = false;
                                Message.Send(Who, "Trading Succeeded!", Who.Entity.Name, "SYSTEM", (uint)Color.White, 2005);

                                C.Trading = false;
                                C.TradingWith = 0;
                                C.TradeSide = new System.Collections.ArrayList(20);
                                C.TradingCPs = 0;
                                C.TradingSilvers = 0;
                                C.ClickedOK = false;
                                Message.Send(C, "Trading succeeded!", C.Entity.Name, "SYSTEM", (uint)Color.White, 2005);

                            }
                            else
                                CancelTrade(C);

                        }
                        else
                        {
                            C.ClickedOK = true;
                            Who.Send(PacketBuilder.Trade(0, 10));
                        }
                        break;
                    }
                #endregion
            }
        }
    }
}