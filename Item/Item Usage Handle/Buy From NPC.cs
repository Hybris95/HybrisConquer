using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;
using System.IO;

namespace ConquerServer_Basic.Item
{
    class BuyFromNPC
    {
        static public void Handle(GameClient Hero, ItemUsagePacket cPacket)
        {
            uint Item = cPacket.dwParam;
            uint Amount = cPacket.dwExtraInfo;
            uint ShopID = cPacket.UID;
            uint Price = 0;
            uint MoneyType = 1;
            string ItemName = string.Empty;

            Amount = Math.Max(1, Amount);

            switch (ShopID)
            {
                case 432:
                case 433:
                case 2888:
                    MoneyType = 2;
                    break;
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + @"/Items/" + Item + ".ini"))
            {
                IniFile ini = new IniFile(System.Windows.Forms.Application.StartupPath + @"/Items/" + Item + ".ini");
                switch (MoneyType)
                {
                    case 1:
                        {
                            Price = ini.ReadUInt32("ItemInformation", "ShopBuyPrice", 0);
                            ItemName = ini.ReadString("ItemInformation", "ItemName", "Item");

                            if (Hero.Money >= Price)
                            {
                                Hero.Money -= Price;
                                IConquerItem BoughtItem = new ItemDataPacket(true);
                                BoughtItem.ID = Item;
                                BoughtItem.UID = ItemDataPacket.NextItemUID;

                                Hero.AddInventory(BoughtItem, Convert.ToByte(Amount));

                                Message.Send(Hero, "Bought " + Amount + " " + ItemName + "! Check your inventory!", 0x00FFFFFF, MessagePacket.TopLeft);
                            }
                            else
                                Message.Send(Hero, "Not enough silvers to purchase " + Amount + " " + ItemName + "", 0x00FFFFFF, MessagePacket.TopLeft);
                            break;
                        }
                    case 2:
                        {
                            Price = ini.ReadUInt32("ItemInformation", "ShopCPPrice", 0);
                            ItemName = ini.ReadString("ItemInformation", "ItemName", "Item");

                            if (Hero.ConquerPoints >= Price)
                            {
                                Hero.ConquerPoints -= Price;
                                IConquerItem BoughtItem = new ItemDataPacket(true);
                                BoughtItem.ID = Item;
                                BoughtItem.UID = ItemDataPacket.NextItemUID;

                                Hero.AddInventory(BoughtItem, Convert.ToByte(Amount));

                                Message.Send(Hero, "Bought " + Amount + " " + ItemName + "! Check your inventory!", 0x00FFFFFF, MessagePacket.TopLeft);
                            }
                            else
                                Message.Send(Hero, "Not enough CPs to purchase " + Amount + " " + ItemName + "", 0x00FFFFFF, MessagePacket.TopLeft);
                            break;
                        }
                }
            }
            else
                Message.Send(Hero, "Item " + Item + "  does not exist in Misc.", 0x00FFFFFF, MessagePacket.TopLeft);
        }

    }
}
