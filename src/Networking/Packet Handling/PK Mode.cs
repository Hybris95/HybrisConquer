using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    class PlayerMovement
    {
        static public void PlayerGroundMovment(GameClient Client, GroundMovementPacket Packet)
        {
            Client.SendScreen(Packet, true);
            Client.Entity.Move(Packet.Direction);
            Client.Screen.Reload(false, null);
        }
        static public void PlayerJump(GameClient Client, DataPacket Packet)
        {
            ushort new_X = (ushort)(Packet.dwParam & 0xFFFF);
            ushort new_Y = (ushort)(Packet.dwParam >> 16);

            if (Kernel.GetDistance(new_X, new_Y, Client.Entity.X, Client.Entity.Y) <= 16)
            {
                if (Client.Attacking)
                    Client.Attacking = false;

                Client.Entity.Action = ConquerAction.Jump;
                Client.SendScreen(Packet, true);

                Client.Entity.Facing = (ConquerAngle)Packet.wParam3;
                Client.Entity.X = new_X;
                Client.Entity.Y = new_Y;
                Client.Screen.Reload(false, null);
            }
            else
            {
                throw new Exception("PacketProcessor::PlayerJump() -> Failed To Assert `Kernel.GetDistance(new_X, new_Y, ClientX, ClientY) <= 16`");
            }
        }
    }
}
