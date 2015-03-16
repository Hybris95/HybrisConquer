using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Interfaces
{
    public interface ISkill
    {
        ushort ID { get; set; }
        ushort Level { get; set; }
        uint Experience { get; set; }

        void Send(GameClient Hero);
    }
}
