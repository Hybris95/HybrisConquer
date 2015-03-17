using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConquerServer_Basic.Main_Classes;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    public class Broadcast
    {
        static public void Handle(GameClient Hero, byte[] Data)
        {
            if (Data[4] == 3 && Hero.ConquerPoints >= 5)
            {
                Hero.ConquerPoints -= 5;
                byte Len = Data[13];
                string Msg = "";
                for (int i = 0; i < Len; i++)
                {
                    Msg += System.Convert.ToChar(Data[14 + i]);
                }
                Message.Send(Hero, Msg, "ALLUSERS", Hero.Entity.Name, Color.Teal, ChatType.Broadcast);
            }
        }
    }
}
