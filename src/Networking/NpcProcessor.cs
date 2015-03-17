using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Npc_Dialog;
using ConquerServer_Basic.Npc_Dialog.Market;
using ConquerServer_Basic.Npc_Dialog.Twin_City;

namespace ConquerServer_Basic
{
    public class NpcProcessor
    {
        static public int Dialog(GameClient Client, string[] dlg)
        {
            NpcReplyPacket Reply = new NpcReplyPacket();

            foreach (string parse_dlg in dlg)
            {
                if (parse_dlg.StartsWith("AVATAR"))
                {
                    string str_avatar = parse_dlg.Substring(7, parse_dlg.Length - 7);
                    Reply.Reset();
                    Reply.InteractType = NpcReplyPacket.Avatar;
                    Reply.wParam = ushort.Parse(str_avatar);
                    Client.Send(Reply);
                }
                else if (parse_dlg.StartsWith("TEXT"))
                {
                    string str_text = parse_dlg.Substring(5, parse_dlg.Length - 5);
                    Reply.Reset();
                    Reply.InteractType = NpcReplyPacket.Dialog;
                    Reply.Text = str_text;
                    Client.Send(Reply);
                }
                else if (parse_dlg.StartsWith("OPTION"))
                {
                    string str_op_num = parse_dlg.Substring(6, parse_dlg.IndexOf(' ') - 6);
                    string str_op_text = parse_dlg.Substring(6 + str_op_num.Length + 1, parse_dlg.Length - 6 - str_op_num.Length - 1);
                    Reply.Reset();
                    Reply.InteractType = NpcReplyPacket.Option;
                    Reply.OptionID = (byte)short.Parse(str_op_num);
                    Reply.Text = str_op_text;
                    Client.Send(Reply);
                }
                else if (parse_dlg.StartsWith("INPUT")) // INPUT[LINK] [LENGTH] [TEXT]
                {
                    string[] val = parse_dlg.Split(' ');
                    string str_op_num = val[0].Remove(0, 5);
                    string str_length = val[1];
                    string str_txt_len = parse_dlg.Substring(str_op_num.Length + 4 + str_length.Length + 3);
                    Reply.Reset();
                    Reply.InteractType = NpcReplyPacket.Input;
                    Reply.wParam = ushort.Parse(str_length);
                    Reply.OptionID = (byte)sbyte.Parse(str_op_num);
                    Client.Send(Reply);
                }
                else if (parse_dlg.StartsWith("NOP"))
                    continue;
                else
                    throw new ArgumentException("Failed to parse npc dialog statement `" + parse_dlg + "`");
            }
            Reply.Reset();
            Reply.InteractType = NpcReplyPacket.Finish;
            Reply.DontDisplay = false;
            Client.Send(Reply);
            return 0;
        }

        static public void Process(GameClient Client, byte OptionID, string Input, NpcRequestPacket Packet)
        {
            switch (Client.ActiveNpcID)
            {
                ///Twin City\\\
                case 10050: ConductressTC.Npc(Client, OptionID, Input); break;
                case 10003: GuildDirector.Npc(Client, OptionID, Input, Packet); break;

                ///Market\\\
                case 45: ConductressMarket.Npc(Client, OptionID, Input); break;

                ///Default\\\
                default: Default.Npc(Client, OptionID, Input); break;
            }
            Console.WriteLine(Client.ActiveNpcID);
        }
    }
}
