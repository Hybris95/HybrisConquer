using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public enum MapObjectType
    {
        Player = 1,
        Monster = 2,
        Pet = 3,
        Item = 4,
        Npc = 5,
        SOB = 6
    }

    public interface IMapObject
    {
        ushort X { get; }
        ushort Y { get; }
        ushort MapID { get; }
        uint UID { get; }
        object Owner { get; }
        MapObjectType MapObjType { get; }
        void SendSpawn(GameClient Client);
        void SendSpawn(GameClient Client, bool IgnoreScreen);
    }
}
