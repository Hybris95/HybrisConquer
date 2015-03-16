using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets.Encryptions;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    class Gameconnect
    {
        static public void GameConnect(GameClient Client, byte[] Packet)
        {
            Client.Identifier = BitConverter.ToUInt32(Packet, 8);
            uint Key2 = BitConverter.ToUInt32(Packet, 4);
            (Client.Socket.Crypto as ConquerStanderedCipher).SetKeys(Client.Identifier, Key2);
            AuthClient authData;

            if (Kernel.AuthPool.TryGetValue(Client.Identifier, out authData))
            {
                Client.Username = authData.Username;
                Client.Password = authData.Password;
                Kernel.AuthPool.Remove(authData.Identifier);

                if (Characters.LoadCharacter(Client))
                {
                    Kernel.GamePool.ThreadSafeAdd<uint, GameClient>(Client.Identifier, Client);
                    Kernel.UpdateGameClients();
                    Client.Send(PacketBuilder.CharacterInfo(Client));
                    Client.Send(new MessagePacket("ANSWER_OK", "ALLUSERS", Color.White, MessagePacket.Dialog)); return;
                }
                Client.Socket.Disconnect();
            }
        }
    }
}
