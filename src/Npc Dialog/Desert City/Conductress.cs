using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Npc_Dialog
{
    class ConductressDC
    {
        static public void Npc(GameClient Hero, byte OptionID, string Input)
        {
            switch (OptionID)
            {
                case 0:
                    {
                        NpcProcessor.Dialog(Hero, new string[] {
                            "AVATAR 1",
                            "TEXT Where are you heading for? I can teleport you for a price of 100 silver.",
                            "OPTION1 Twin City.",
                            "OPTION2 Mystic Castle.",
                            "OPTION3 Market.",
                            "OPTION-1 Just passing by."
                        });
                        break;
                    }
                default:
                    {
                        if (Hero.Money >= 100)
                        {
                            Hero.Money -= 100;
                            Hero.PrevMap = Hero.Entity.MapID;
                            switch (OptionID)
                            {
                                case 1:
                                    Hero.Teleport(1000, 968, 666);
                                    break;
                                case 2:
                                    Hero.Teleport(1000, 085, 323);
                                    break;
                                case 3:
                                    Hero.Teleport(1036, 211, 196);
                                    break;
                            }
                        }
                        else
                        {
                            NpcProcessor.Dialog(Hero, new string[] {
                                "AVATAR 1",
                                "TEXT You don't have enough silvers.",
                                "OPTION-1 I see."
                            });
                        }
                        break;
                    }
            }
        }
    }

}
