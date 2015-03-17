using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;

namespace ConquerServer_Basic.Item
{
    class FloorItem
    {
        public IConquerItem Item;
        public ushort MapID;
        public ushort X;
        public ushort Y;
        public uint Money;
        public System.Timers.Timer OwnerOnly;
        public System.Timers.Timer Dispose;
        public void Stop()
        {
            Dispose.Stop();
            Dispose.Dispose();
            OwnerOnly.Stop();
            OwnerOnly.Dispose();
        }

        public void Disappear()
        {
            if (FloorItems.DroppedItems.ContainsKey(Item.UID))
            {
                try
                {
                    FloorItems.DroppedItems.Remove(Item.UID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            NewMath.ToLocal(PacketBuilder.RemoveItemDropEffect(Item.UID, Item.ID, X, Y), X, Y, MapID, 0, 0);
            NewMath.ToLocal(PacketBuilder.RemoveItemDrop(Item.UID), X, Y, MapID, 0, 0);
            Stop();
        }
    }

    class FloorItems
    {
        static public Dictionary<uint, FloorItem> DroppedItems = new Dictionary<uint, FloorItem>();
    }
}
