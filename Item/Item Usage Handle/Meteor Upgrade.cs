using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;

namespace ConquerServer_Basic.Item.Item_Usage_Handle
{
    public class MeteorUpgrade
    {
        static public void Handle(GameClient Hero, ItemUsagePacket cPacket)
        {
            uint ItemUID = cPacket.UID;
            uint MetUID = cPacket.dwParam;

            IConquerItem ItemUp = new ItemDataPacket(true);
            ItemUp = Hero.GetInventoryItem(ItemUID);

            int CheckUpgrade = int.Parse(ItemUp.ID.ToString().Remove(2, ItemUp.ID.ToString().Length - 2));
            if (CheckUpgrade == 90 || CheckUpgrade == 11 || CheckUpgrade == 12 || CheckUpgrade == 13 || CheckUpgrade == 15 || CheckUpgrade == 16 || CheckUpgrade == 4 || CheckUpgrade == 5)
            {
                Hero.RemoveInventory(MetUID);

                byte ItemQuality = byte.Parse(ItemUp.ID.ToString()[5].ToString());
                byte ItemIdentify = byte.Parse(ItemUp.ID.ToString()[4].ToString());
                string ItemFour = ItemUp.ID.ToString().Remove(4);
                Random Rand = new Random();
                bool Chance = false;

                switch (ItemQuality)
                {
                    case 3:
                    case 4:
                    case 5:
                        Chance = ((double)Rand.Next(1, 1000000)) / 10000 >= 100 - 90;
                        break;
                    case 6:
                        Chance = ((double)Rand.Next(1, 1000000)) / 10000 >= 100 - 75;
                        break;
                    case 7:
                        Chance = ((double)Rand.Next(1, 1000000)) / 10000 >= 100 - 60;
                        break;
                    case 8:
                        Chance = ((double)Rand.Next(1, 1000000)) / 10000 >= 100 - 50;
                        break;
                    case 9:
                        Chance = ((double)Rand.Next(1, 1000000)) / 10000 >= 100 - 45;
                        break;
                }

                Console.WriteLine(Chance);

                if (Chance)
                {
                    Hero.RemoveInventory(ItemUID);

                    ItemIdentify = Convert.ToByte(ItemIdentify + 1);

                    ItemUp.ID = uint.Parse(ItemFour + ItemIdentify.ToString() + ItemQuality.ToString());
                    ItemUp.UID = ItemDataPacket.NextItemUID;

                    bool SocketChance = ((double)Rand.Next(1, 1000000)) / 10000 >= 100 - 2;
                    if (SocketChance)
                    {

                        if (ItemUp.SocketOne == 0)
                        {
                            ItemUp.SocketOne = 255;
                            Message.Send(Hero, "Your item gained its first socket!", 0x00FFFFFF, MessagePacket.TopLeft);
                        }
                        else if (ItemUp.SocketTwo == 0)
                        {
                            ItemUp.SocketTwo = 255;
                            Message.Send(Hero, "Your item gained its second socket!", 0x00FFFFFF, MessagePacket.TopLeft);
                        }
                    }
                    else
                        Message.Send(Hero, "Upgrading successful!", 0x00FFFFFF, MessagePacket.TopLeft);

                    Hero.AddInventory(ItemUp);
                }
                else
                    Message.Send(Hero, "Upgrading failed!", 0x00FFFFFF, MessagePacket.TopLeft);
            }
        }

    }
}
