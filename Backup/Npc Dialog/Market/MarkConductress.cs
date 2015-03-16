using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Networking.Packet_Handling;

namespace ConquerServer_Basic.Npc_Dialog.Market
{
    class MarkConductress
    {
        static public void Npc(GameClient Hero, byte OptionID, string Input)
        {
            switch (OptionID)
            {
                case 0:
                    {
                        NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 1",
                            "TEXT Do you want to leave the Market? I can teleport you for free.",
                            "OPTION1 Yeah, thanks.",
                            "OPTION-1 No, I'll stay here."
                        });
                        break;
                    }
                case 1:
                    {
                        NpcRequest.ExitMarket(Hero, true);
                        break;
                    }
            }
        }

    }
}
