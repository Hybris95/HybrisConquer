using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public interface INpc
    {
        uint UID { get; set; }
        ushort X { get; set; }
        ushort Y { get; set; }
        ushort Type { get; set; }
        ConquerAngle Facing { get; set; }
        uint StatusFlag { get; set; }
        ushort MapID { get; set; }
        void SendSpawn(GameClient Client);
    }
}
