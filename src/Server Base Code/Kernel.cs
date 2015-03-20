using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using ConquerServer_Basic.Conquer_Structures;
using ConquerServer_Basic.Main_Classes;
using ConquerServer_Basic.Interfaces;
using ConquerServer_Basic.Guilds;


namespace ConquerServer_Basic
{
    public delegate int ConquerCallback(IBaseEntity sender, IBaseEntity caller);

    public class Kernel
    {
        static public Dictionary<ushort, Guild> Guilds = new Dictionary<ushort, Guild>();
        static public Dictionary<ushort, RevivePoint> RevivePoints = new Dictionary<ushort, RevivePoint>();
        static public Dictionary<uint, Entity> eMonsters = new Dictionary<uint, Entity>();
        static public Dictionary<uint, Monster> Mobs = new Dictionary<uint, Monster>();
        static public Dictionary<string, IPortal> Portals = new Dictionary<string, IPortal>();
        static public Dictionary<uint, MonsterSpawn> MobSpawns = new Dictionary<uint, MonsterSpawn>();
        static public Dictionary<uint, AuthClient> AuthPool = new Dictionary<uint, AuthClient>();
        static public Dictionary<uint, IShop> Shops = new Dictionary<uint, IShop>(); 
        static public Dictionary<byte, ulong> LevelExpReq = new Dictionary<byte, ulong>();
        static public Dictionary<ushort, Dictionary<ushort, Skill>> Skills = new Dictionary<ushort, Dictionary<ushort, Skill>>();
        static public Dictionary<byte, ulong> ProfExpReq = new Dictionary<byte, ulong>();
        static public Dictionary<uint, GameClient> GamePool = new Dictionary<uint, GameClient>();
        static public Dictionary<ushort, Dictionary<uint, INpc>> Npcs = new Dictionary<ushort, Dictionary<uint, INpc>>();
        static public Dictionary<uint, StanderdItemStats> ItemsStats = new Dictionary<uint, StanderdItemStats>();
        static public Dictionary<uint, ItemSkill> ItemsSkills = new Dictionary<uint, ItemSkill>();
        static public GameClient[] Clients = new GameClient[0];
        static public Random Random = new Random();

        static private Dictionary<ushort, ConquerSocket> Sockets = new Dictionary<ushort, ConquerSocket>(2);
        public const ushort AuthPort = 9959;
        public const ushort GamePort = 5816;

        static public void Socket(ushort port, ushort cbsize, byte backlog)
        {
            ConquerSocket Socket = new ConquerSocket();
            Socket.Port = port;
            Socket.ClientBufferSize = cbsize;
            Socket.Backlog = backlog;
            switch (port)
            {
                case Kernel.AuthPort:
                    Socket.OnClientConnect += new SocketEvent<HybridWinsockClient, object>(Program.AuthConnect);
                    Socket.OnClientReceive += new SocketEvent<HybridWinsockClient, byte[]>(Program.AuthReceive);
                    Console.WriteLine("Login server is online.");
                    break;
                case Kernel.GamePort:
                    Socket.OnClientConnect += new SocketEvent<HybridWinsockClient, object>(Program.GameConnect);
                    Socket.OnClientReceive += new SocketEvent<HybridWinsockClient, byte[]>(Program.GameReceive);
                    Socket.OnClientDisconnect += new SocketEvent<HybridWinsockClient, object>(Program.GameDisconnect);
                    Console.WriteLine("Game server is online.");
                    break;
            }
            Sockets.Add(port, Socket);
            Socket.Enable();
        }
        static public void CloseSocket(ushort port)
        {
            if (Sockets.ContainsKey(port))
            {
                ConquerSocket Socket = null;
                Sockets.TryGetValue(port, out Socket);
                Socket.Disable();
                Sockets.Remove(port);
                Console.WriteLine(port + " socket has been closed");
            }
        }
        static public void UpdateGameClients()
        {
            bool updated = false;
            while (!updated)
            {
                try
                {
                    Clients = GamePool.ThreadSafeValueArray<uint, GameClient>();
                    updated = true;
                }
                catch (IndexOutOfRangeException)
                {
                    Kernel.WriteLine("[Database SQL Failure] Failed to update game clients\r\n");
                }
            }
        }

        private static StringWriter DebugLog = new StringWriter();
        static public void WriteLine(string Line)
        {
            lock (DebugLog)
            {
                Console.WriteLine(Line);
                DebugLog.WriteLine(Line);
                File.WriteAllText(Misc.DatabasePath + "\\Debug.log", DebugLog.ToString());
            }
        }
        static public bool CanSee(int SeeX, int SeeY, int MyX, int MyY)
        {
            return (Math.Max(Math.Abs(SeeX - MyX), Math.Abs(SeeY - MyY)) <= 15);
        }
        static public short GetDistance(ushort X, ushort Y, ushort X2, ushort Y2)
        {
            return (short)Math.Max(Math.Abs(X - X2), Math.Abs(Y - Y2));
        }
        static public bool IsItemType(uint ID, ushort Type)
        {
            return ((int)(ID / 1000) == Type);
        }
    }
}
