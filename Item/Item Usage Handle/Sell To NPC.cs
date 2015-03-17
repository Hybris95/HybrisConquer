using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;
using System.IO;

namespace ConquerServer_Basic.Item
{
    class SelltoNpc
    {
        static public void Handle(GameClient Hero, ItemUsagePacket cPacket)
        {
            uint ItemUID = cPacket.dwParam;
            uint ItemID = 0;
            string ItemName = string.Empty;
            uint Price = 0;

            IConquerItem SoldItem = new ItemDataPacket(true);
            SoldItem = Hero.GetInventoryItem(ItemUID);

            ItemID = SoldItem.ID;
            // TODO - Load Items from Database instead of Flat File
            if (File.Exists( System.Windows.Forms.Application.StartupPath+ @"/Items/" + ItemID + ".ini"))
            {
                IniFile ini = new IniFile(System.Windows.Forms.Application.StartupPath + @"/Items/" + ItemID + ".ini");
                Price = ini.ReadUInt32("ItemInformation", "ShopBuyPrice", 0);
                ItemName = ini.ReadString("ItemInformation", "ItemName", "Item");

                Price = (Price / 3);

                Hero.RemoveInventory(ItemUID);
                Hero.Money += Price;

                Message.Send(Hero, "Sold " + ItemName + " for " + Price + " silvers! Check your inventory!", 0x00FFFFFF, MessagePacket.TopLeft);
            }
            else
                Message.Send(Hero, "Item " + ItemID + "  does not exist in Misc.", 0x00FFFFFF, MessagePacket.TopLeft);
        }
    }

}
