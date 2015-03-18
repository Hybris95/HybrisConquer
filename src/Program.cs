/*
 * ***************************************
 *              CREDITS
 * ***************************************
 *  Originally created by Hybrid (InfamousNoone @ e*pvp), Copyright (C) 2007-2009,
 *  Hybrid Software, All rights reserved.
 *  
 *      - Yes I've been using that company name on my products since 2007 :-)
 *  Intentional use for learning purposes, however can be potentially used
 *  to develop a fully functional server if used wisely.
 *  
 * ***************************************
 *              SPECIAL THANKS
 * ***************************************
 * Rannny (punkmak2 @ e*pvp)
 * Saint (tao4229 @ e*pvp)
 * 
 * ***************************************
 *              DISCLAIMER
 * ***************************************
 * You are hereby free to rename, redistribute and modify this source
 * in any way, shape or forum. However original credits must still
 * remain to there original owners. Claiming to have "written your own source"
 * while using this as the base (assuming I find out your using this as the base)
 * you can so kindly expect me to do what I do best, wreck your server and
 * make sure it crashes and burns <3.
 * 
 * If you have a website for your server and a credits section, please
 * include a "special thanks" section underneath it, and include the names
 * provided in the "Special Thanks" section above and my own name; either
 * "Hybrid" or "Infamous Noone".
 * 
 * ***************************************
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Globalization;
using MySqlHandler;
using DMapLoader;
using ConquerServer_Basic.Conquer_Structures;
using System.Windows.Forms;
using System.Threading;
using ConquerServer_Basic.Networking.Packet_Handling;
using System.Net.Sockets.Encryptions;
using MySql.Data.MySqlClient;

namespace ConquerServer_Basic
{
    class Program
    {
        static public UInt32 WeaponSkillRate = 1;
        static public UInt32 ExperienceRate = 1;
        static public String Username;
        static public String Password;
        static public String Host;
        static public String GameIp;
        static public DMapServer DmapHandler = new DMapServer();

        #region AuthEvents
        static public void AuthConnect(HybridWinsockClient Sender, object param)
        {
            Sender.Wrapper = new AuthClient(Sender);
        }

        static public void AuthReceive(HybridWinsockClient Sender, byte[] param)
        {
            AuthClient Client = Sender.Wrapper as AuthClient;
            if (Sender.RecvSize == 52)
            {
                byte[] Recv = param;
                if (BitConverter.ToUInt16(Recv, 2) == 0x41B)
                {
                    byte i = 0;
                    Client.Username = Encoding.ASCII.GetString(Recv, 4, 16).Trim(new char[] { (char)0x0000 });
                    Client.Password = "";
                    while (i < 16)
                    {
                        Client.Password += Recv[i + 16].ToString("X2");
                        i = (byte)(i + 1);
                    }
                    Client.Identifier = Accounts.PullUID(Client.Username);
                    if (Accounts.CheckPass(Client.Username, Client.Password))
                    {
                        if (Kernel.GamePool.ContainsKey(Client.Identifier))
                        {
                            Client.Send(PacketBuilder.AuthResponse(22));
                            Sender.Disconnect();
                        }
                        else
                        {
                            Console.WriteLine("Account " + Client.Username + " logging in from IP: " + Sender.Connection.RemoteEndPoint.ToString().Split(':')[0]);

                            Kernel.AuthPool.Add(Client.Identifier, Client);

                            Client.AuthPhase = AuthPhases.AuthComplete;

                            if (Sender.Connection.RemoteEndPoint.ToString().Split(':')[0] != "127.0.0.1")
                                Sender.Send(PacketBuilder.AuthResponse(GameIp, Client.Identifier, 255, 5816));
                            else
                                Sender.Send(PacketBuilder.AuthResponse("192.168.1.1", Client.Identifier, 255, 5816));
                        }
                    }
                    else
                    {
                        Client.Send(PacketBuilder.AuthResponse(1));
                        Sender.Disconnect();
                    }
                }
            }
        }
        #endregion AuthEvents
        #region GameEvents
        public static void GameConnect(HybridWinsockClient Sender, object param)
        {
            Sender.Wrapper = new GameClient(Sender);
        }
        static public void GameDisconnect(HybridWinsockClient Sender, object param)
        {
            GameClient Client = Sender.Wrapper as GameClient;
            if (Client != null)
            {
                if (Client.AuthPhase >= AuthPhases.GameComplete)
                {
                    try
                    {
                        if (Client.Team != null)
                        {
                            TeamPacket Disband = new TeamPacket(true);
                            Disband.UID = Client.Identifier;
                            if (Client.Team.TeamLeader)
                            {
                                Disband.Type = TeamPacket.Dismiss;
                                Teams.DismissTeam(Disband, Client);
                            }
                            else
                            {
                                Disband.UID = TeamPacket.ExitTeam;
                                Teams.LeaveTeam(Disband, Client);
                            }
                        }

                        DataPacket remove = new DataPacket(true);
                        remove.UID = Client.Identifier;
                        remove.ID = DataPacket.RemoveEntity;
                    }
                    finally
                    {
                        Characters.SaveCharacter(Client);
                        Kernel.GamePool.ThreadSafeRemove<uint, GameClient>(Client.Identifier);

                    }
                }
            }
        }
        public static void GameReceive(HybridWinsockClient Sender, byte[] param)
        {
            ushort Size;
            GameClient Client = Sender.Wrapper as GameClient;
            byte[] Recv = param;
            for (int Counter = 0; Counter < Recv.Length; Counter += Size)
            {
                Size = BitConverter.ToUInt16(Recv, Counter);
                ushort Type = BitConverter.ToUInt16(Recv, Counter + 2);
                if (Size < Recv.Length)
                {
                    byte[] InitialPacket = new byte[Size];
                    Buffer.BlockCopy(Recv, Counter, InitialPacket, 0, Size);
                    PacketProcessor.Process(Client, InitialPacket, Type);
                }
                else
                {
                    PacketProcessor.Process(Client, Recv, Type);
                }
            }
        }
        #endregion GameEvents
        
        static private bool serverEnabled = false;
        static public void Main()
        {
            Console.Title = "Hybris95 Online 5017";
            #region OpeningDialogue
            Console.WriteLine();
            Console.WriteLine("            ╔═══════════════════════════════════════════════╗");
            Console.WriteLine("            ║                                               ║");
            Console.WriteLine("            ║            [Hybris95 Online 5017]             ║");
            Console.WriteLine("            ║                                               ║");
            Console.WriteLine("            ║                 << Credits >>                 ║");
            Console.WriteLine("            ║             Hybrid: Created base              ║");
            Console.WriteLine("            ║               Arco: Main coder                ║");
            Console.WriteLine("            ║           Hybris95: Modder                    ║");
            Console.WriteLine("            ║                                               ║");
            Console.WriteLine("            ╚═══════════════════════════════════════════════╝\n\n");
            #endregion
            Misc.LoadSettings();

            #region Dmap Handler
            DmapHandler.ConquerPath = Application.StartupPath;
            DmapHandler.Load();
            #endregion

            #region MySQL
            MySqlHandler.MySqlArgument arg = new MySqlHandler.MySqlArgument();
            arg.User = Username;
            arg.Password = Password;
            arg.Host = "localhost";
            arg.Database = Host;
            MySqlHandler.Connections.Open(arg);
            MySqlHandler.MySqlCommandHandler.TurnOn();
            string hiddenPassword = "";
            for (int i = 0; i < Password.Length; i++) { hiddenPassword += "*"; }
            Console.WriteLine("Username: {0}\nPassword: {1}\nDatabase: {2}\nMySQL information loaded.", Username, hiddenPassword, Host);
            #endregion
            Console.WriteLine();
            #region EnableServer
            serverEnabled = false;
            int trys = 0;
            int maxTrys = 3;
            while (trys < maxTrys)
            {
                try
                {
                    #region Database Loads
                    StanderdItemStats.Load();
                    Npc.Load();
                    Monster.Load();
                    MonsterSpawn.Load();
                    MonsterSpawn.SpawnMobs();
                    Database.Guilds.LoadGuilds();
                    Misc.LoadEmpire();
                    Misc.LoadPortals();
                    Misc.LoadSkills();
                    Misc.LoadRevivePoints();
                    Misc.LoadWepExp();
                    Misc.LoadExp();
                    Misc.LoadShops();
                    #endregion
                    Console.WriteLine();
                    #region Socket Init
                    Kernel.Socket(Kernel.AuthPort, (ushort)100, (byte)10);
                    Kernel.Socket(Kernel.GamePort, (ushort)1000, (byte)100);
                    #endregion
                    Console.WriteLine();
                    #region ConsoleCommand
                    trys = maxTrys;
                    serverEnabled = true;
                    while (serverEnabled)
                    {
                        Console.Write(">");
                        ConquerCommand.ConsoleCommand();
                    }
                    #endregion ConsoleCommand
                }
                catch (MySqlException)
                {
                    trys++;
                    Console.WriteLine("Please start MySQL server or make sure authentification is good and press a key (Try " + trys + "/" + maxTrys + ")");
                    Console.ReadKey();
                }
                finally
                {
                    #region Unload
                    // Force disconnect for all clients (in order to save correctly their character)
                    GameClient[] clients = Kernel.GamePool.Values.ToArray<GameClient>();
                    foreach (GameClient client in clients)
                    {
                        client.LogOff();
                    }

                    Kernel.CloseSocket(9958);
                    Kernel.CloseSocket(5816);

                    Misc.UnloadShops();
                    Misc.UnloadExp();
                    Misc.UnloadWepExp();
                    Misc.UnloadRevivePoints();
                    Misc.UnloadSkills();
                    Misc.UnloadPortals();
                    Misc.UnloadEmpire();
                    Database.Guilds.UnloadGuilds();
                    MonsterSpawn.DespawnMobs();
                    MonsterSpawn.Unload();
                    Monster.Unload();
                    Npc.Unload();
                    StanderdItemStats.Unload();
                    #endregion
                }
            }
            #endregion
        }

        public static void StopServer()
        {
            // TODO - Save everyone before shutting down
            serverEnabled = false;
        }

        private static string StrHexToAnsi(string StrHex)
        {
            string[] Data = StrHex.Split(new char[] { ' ' });
            string Ansi = "";
            foreach (string tmpHex in Data)
            {
                if (tmpHex != "")
                {
                    byte ByteData = byte.Parse(tmpHex, NumberStyles.HexNumber);
                    if ((ByteData >= 32) & (ByteData <= 126))
                    {
                        Ansi = Ansi + ((char)(ByteData)).ToString();
                    }
                    else
                    {
                        Ansi = Ansi + ".";
                    }
                }
            }
            return Ansi;
        }

        static public object Dump(byte[] Bytes)
        {
            string Hex = "";
            foreach (byte b in Bytes)
            {
                Hex = Hex + b.ToString("X2") + " ";
            }
            string Out = "";
            while (Hex.Length != 0)
            {
                int SubLength = 0;
                if (Hex.Length >= 48)
                {
                    SubLength = 48;
                }
                else
                {
                    SubLength = Hex.Length;
                }
                string SubString = Hex.Substring(0, SubLength);
                int Remove = SubString.Length;
                SubString = SubString.PadRight(60, ' ') + StrHexToAnsi(SubString);
                Hex = Hex.Remove(0, Remove);
                Out = Out + SubString + "\r\n";
            }
            return Out;
        }

        public class ConquerSocket : ServerSocket
        {
            protected override IPacketCipher MakeCrypto()
            {
                return new ConquerStanderedCipher();
            }

        }
    }
}