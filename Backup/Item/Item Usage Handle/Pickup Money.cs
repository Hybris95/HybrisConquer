using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;

namespace ConquerServer_Basic.Item.Item_Usage_Handle
{
    public class PickupMoney
    {
        static public void Handle(GameClient Hero, uint uid)
        {
            FloorItem dItem = FloorItems.DroppedItems[uid];
            if (dItem.Money > 0)
            {
                Hero.Money += dItem.Money;
                Message.Send(Hero, "Picked up " + dItem.Money + " silvers.", 0x00FFFFFF, MessagePacket.TopLeft);
                NewMath.ToLocal(PacketBuilder.RemoveItemDrop(dItem.Item.UID),Hero.Entity.X, Hero.Entity.Y, Hero.Entity.MapID, 0,0);
                DataPacket DP = new DataPacket(true);
                DP.UID = Hero.Entity.UID;
                DP.ID = DataPacket.PickupCashEffect;
                DP.dwParam = dItem.Money;
                Hero.Send(DP);
            }
        }
    }
}
