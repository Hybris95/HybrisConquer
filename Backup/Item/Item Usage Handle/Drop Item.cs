using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;

namespace ConquerServer_Basic.Item.Item_Usage_Handle
{
    public class DropItem
    {
        static public void Handle(GameClient Hero, ItemUsagePacket cPacket)
        {
            uint ItemUID = cPacket.UID;

            IConquerItem DropItem = new ItemDataPacket(true);
            DropItem = Hero.GetInventoryItem(ItemUID);

            if (Hero.CountInventory(DropItem.ID) >= 1)
            {
                Hero.RemoveInventory(DropItem.UID);

                FloorItem dItem = new FloorItem();
                dItem.Item = DropItem;
                dItem.MapID = Hero.Entity.MapID;
                dItem.X = Hero.Entity.X;
                dItem.Y = Hero.Entity.Y;
                dItem.Money = 0;

                FloorItems.DroppedItems.Add(dItem.Item.UID, dItem);

                Message.Send(Hero, "Dropped an Item.", 0x00FFFFFF, MessagePacket.TopLeft);
            }
            else
                Message.Send(Hero, "You don't have that item to drop.", 0x00FFFFFF, MessagePacket.TopLeft);
        }

    }
}
