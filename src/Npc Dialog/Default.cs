using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Npc_Dialog
{
    class Default
    {
        static public void Npc(GameClient Hero, byte OptionID, string Input)
        {
            switch (OptionID)
            {
                default:
                    NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 30",
                            "TEXT Sorry, this NPC (" + Hero.ActiveNpcID + ") is not implemented yet!",
                            "OPTION-1 I see."
                        });
                    break;
            }
        }
    }
}
