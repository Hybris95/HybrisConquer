using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    class CreateCharacter
    {
        static bool ValidName(string name)
        {
            foreach (char ch in name)
            {
                if (!
                    ((ch >= 48 && ch <= 57) ||
                    (ch >= 65 && ch <= 90) ||
                    (ch >= 97 && ch <= 122))
                    )
                    return false;
            }
            return true;
        }
        static public void Handle(GameClient Hero, byte[] Data)
        {
            string CharName = "";
            for (int i = 0; i < 16; i++)
                if (Data[20 + i] != 0)
                    CharName += Convert.ToChar(Data[20 + i]);

            ushort Body = BitConverter.ToUInt16(Data, 52);
            ushort Job = Data[54];

            int Avatar=0;
            if (ValidName(CharName))
            {
                MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
                cmd.Select("Characters").Where("Name", CharName);
                MySqlReader r = new MySqlReader(cmd);
                if (r.Read())
                    Hero.Send(new MessagePacket("Character name has been taken! Please choose a new one!", "ALLUSERS", "SYSTEM", 0xFFFFFF, 2100));
                else
                    switch (Body)
                    {
                        case 1003:
                        case 1004:
                        Avatar = Kernel.Random.Next(1, 102); break;
                        case 2001:
                        case 2002: Avatar = Kernel.Random.Next(201, 290); break;
                    }
                Characters.CreateCharacter(Hero, CharName, Job, Body, Avatar);
            }
            else
            {
                Hero.Send(new MessagePacket("Invalid character name!", "ALLUSERS", "SYSTEM", 0xFFFFFF, 2100));
            }
        }
    }

}
