using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public enum EntityFlag : byte
    {
        Monster = 2,
        Player = 1
    }

    public interface IBaseEntity
    {
        bool Dead { get; set; }
        uint Defence { get; set; }
        sbyte Dodge { get; set; }
        EntityFlag EntityFlag { get; set; }
        uint Hitpoints { get; set; }
        uint MagicAttack { get; set; }
        ushort MapID { get; set; }
        uint MaxAttack { get; set; }
        uint MaxHitpoints { get; set; }
        ushort MDefence { get; set; }
        uint MinAttack { get; set; }
        object Owner { get; set; }
        ushort PlusMDefence { get; set; }
        uint UID { get; set; }
        ushort X { get; set; }
        ushort Y { get; set; }
    }
}
