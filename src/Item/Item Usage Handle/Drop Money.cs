using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;

namespace ConquerServer_Basic.Item.Item_Usage_Handle
{
    public class DropMoney
    {
        static public void Handle(GameClient Hero, ItemUsagePacket cPacket)
        {
            
            Console.WriteLine("cPacket.dwExtraInfo: {0}", cPacket.dwExtraInfo);
            Console.WriteLine("cPacket.dwParam: {0}", cPacket.dwParam);
            Console.WriteLine("cPacket.ID: {0}", cPacket.ID);
            Console.WriteLine("Amount Dropped: {0}", cPacket.UID);

            uint Amount = cPacket.UID;
            IConquerItem DroppedCash = new ItemDataPacket(true);
            if (Hero.Money >= Amount)
            {
                Hero.Money -= Amount;
                    FloorItem dItem = new FloorItem();
                dItem.Item = DroppedCash;
                dItem.Item.UID = ItemDataPacket.NextItemUID;
                dItem.MapID = Hero.Entity.MapID;
                dItem.X = (ushort)(Hero.Entity.X - Kernel.Random.Next(3) + Kernel.Random.Next(3));
                dItem.Y = (ushort)(Hero.Entity.Y - Kernel.Random.Next(3) + Kernel.Random.Next(3));
                dItem.Money = Amount;

                #region Cash Amounts
                if (dItem.Money < 10)
                    dItem.Item.ID = 1090000;
                else if (dItem.Money < 100)
                    dItem.Item.ID = 1090010;
                else if (dItem.Money < 1000)
                    dItem.Item.ID = 1090020;
                else if (dItem.Money < 3000)
                    dItem.Item.ID = 1091000;
                else if (dItem.Money < 10000)
                    dItem.Item.ID = 1091010;
                else
                    dItem.Item.ID = 1091020; 
                #endregion

                #region Timers
                dItem.OwnerOnly = new System.Timers.Timer();
                dItem.Dispose = new System.Timers.Timer();
                dItem.Dispose.Interval = 60000;
                dItem.Dispose.AutoReset = false;
                dItem.Dispose.Elapsed += delegate { dItem.Disappear(); };
                dItem.Dispose.Start(); 
                #endregion
                FloorItems.DroppedItems.Add(dItem.Item.UID, dItem);
                #region SendDropItemPacket
                NewMath.ToLocal(PacketBuilder.DropItem(dItem.Item.UID, dItem.Item.ID, dItem.X, dItem.Y), dItem.X, dItem.Y, dItem.MapID, 0, 0);
                #endregion
                Message.Send(Hero, "Dropped " + Amount + " silvers.", 0x00FFFFFF, MessagePacket.TopLeft);
            }
            else
                Message.Send(Hero, "You don't have " + Amount + " silvers to drop.", 0x00FFFFFF, MessagePacket.TopLeft);
        }

    }
}
