using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConquerServer_Basic.Main_Classes
{
    public class Message
    {
        static public void Send(GameClient Hero, string Message, uint _Color, uint _ChatType)
        {
            MessagePacket msg = new MessagePacket(Message, _Color, _ChatType);
            Hero.Send(msg);
        }

        static public void Send(GameClient Hero, string Message, string To, uint _Color, uint _ChatType)
        {
            MessagePacket msg = new MessagePacket(Message, To, _Color, _ChatType);
            Hero.Send(msg);
        }

        static public void Send(GameClient Hero, string Message, string To, string From, uint _Color, uint _ChatType)
        {
            MessagePacket msg = new MessagePacket(Message, To, From, _Color, _ChatType); Hero.Send(msg);
        }

        static public void Global(string Message, uint _Color, uint _ChatType)
        {
            foreach (GameClient Hero in Kernel.Clients)
                Send(Hero, Message, _Color, _ChatType);
        }

        static public void Global(string Message, uint _Color, uint _ChatType, ushort MapID)
        {
            foreach (GameClient Hero in Kernel.Clients)
                if (Hero.Entity.MapID == MapID)
                    Send(Hero, Message, _Color, _ChatType);
        }
    }
}
