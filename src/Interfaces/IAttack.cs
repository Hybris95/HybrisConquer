using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Interfaces
{
    public interface IAttack
    {
        uint AttackedUID { get; set; }
        ushort AttackedX { get; set; }
        ushort AttackedY { get; set; }
        uint AttackerUID { get; set; }
        ushort AttackType { get; set; }
        uint Damage { get; set; }
    }
}
