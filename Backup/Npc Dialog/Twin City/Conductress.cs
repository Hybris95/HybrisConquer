using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Npc_Dialog
{
    class Conductress
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
                            "OPTION1 Phoenix Castle.",
                            "OPTION2 Desert City.",
                            "OPTION3 Ape Mountain.",
                            "OPTION4 Bird Island.",
                            "OPTION5 Mine Cave.",
                            "OPTION6 Market.",
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
                                    Hero.Teleport(1002, 958, 555);
                                    break;
                                case 2:
                                    Hero.Teleport(1002, 069, 473);
                                    break;
                                case 3:
                                    Hero.Teleport(1002, 555, 957);
                                    break;
                                case 4:
                                    Hero.Teleport(1002, 232, 190);
                                    break;
                                case 5:
                                    Hero.Teleport(1002, 053, 399);
                                    break;
                                case 6:
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
