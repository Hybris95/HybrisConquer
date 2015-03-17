using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;


namespace ConquerServer_Basic
{
    public enum AuthPhases : byte
    {
        LoggedOut = 0,
        AuthServer = 1,
        AuthComplete = 2,
        GameServer = 3,
        GameComplete = 4,
        FullLogin = 5
    }

    public class AuthClient
    {
        public uint Identifier;
        public string Password;
        public HybridWinsockClient Socket;
        public string Username;
        public AuthPhases AuthPhase;

        public AuthClient(HybridWinsockClient _Socket)
        {
            this.Socket = _Socket;
            this.AuthPhase = AuthPhases.AuthServer;
        }

        public void Send(IClassPacket Packet)
        {
            lock (this)
            {
                this.Socket.Send(Packet.Serialize());
            }
        }

        public void Send(byte[] Packet)
        {
            lock (this)
            {
                this.Socket.Send(Packet);
            }
        }
    }
}
