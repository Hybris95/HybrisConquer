using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Npc_Dialog.Twin_City
{
    class GeneralPeace
    {
        static public void Npc(GameClient Hero, byte OptionID, string Input)
        {
            switch (OptionID)
            {
                case 0:
                    NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 1",
                            "TEXT I can lead you to Desert City.",
                            "OPTION1 Ok.",
                            "OPTION-1 Just passing by."
                        });
                    break;
                case 1:
                    Hero.Teleport(1000, 968, 666);
                    break;
                default:
                    break;
            }
        }
    }
}
