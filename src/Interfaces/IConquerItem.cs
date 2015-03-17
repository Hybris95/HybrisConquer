using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public interface IConquerItem
    {
        uint UID { get; set; }
        uint ID { get; set; }
        byte Bless { get; set; }
        byte Enchant { get; set; }
        byte Plus { get; set; }
        byte SocketOne { get; set; }
        byte SocketTwo { get; set; }
        ushort Position { get; set; }
        ushort Durability { get; set; }
        ushort MaxDurability { get; set; }
        void Send(GameClient Client);
    }
}
