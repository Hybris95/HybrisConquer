using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    public struct EmpireInfo
    {
        public string Name;
        public ulong Donation;
        public uint ID;
        public void WriteThis(System.IO.BinaryWriter BW)
        {
            if (Name == null)
                Name = "";
            BW.Write(Name.Length);
            BW.Write(Encoding.ASCII.GetBytes(Name));
            BW.Write(Donation);
            BW.Write(ID);
        }
        public void ReadThis(System.IO.BinaryReader BR)
        {
            try
            {
                //TextWriter TW = new StreamWriter(@"C:\Nobility.txt");
                Name = Encoding.ASCII.GetString(BR.ReadBytes(BR.ReadInt32()));
                Donation = BR.ReadUInt64();
                ID = BR.ReadUInt32();
                //TW.WriteLine(Name + ", " + Donation + ", " + ID);
                //TW.Close();
                //Console.WriteLine(Donation);
                //Console.WriteLine(ID);
            }
            catch
            {
                Name = "";
                Donation = 0;
                ID = 0;
            }
        }
    }
    public class Empire
    {
        public string Name;
        public uint UID;
        public int Gold;
        public Ranks Rank;
        public int Listing;

        public int Score { get { return Gold; } set { Gold = value; } }

        public string ListingString
        {
            get
            {
                return UID.ToString() + " 0 0 " + Name + " " + Gold.ToString() + " " + ((uint)Rank).ToString() + " " + Listing.ToString();
            }
        }
        public string LocalString
        {
            get
            {
                return UID.ToString() + " " + Gold.ToString() + " " + ((uint)Rank).ToString() + " " + Listing.ToString();
            }
        } 
        public static EmpireInfo[] EmpireBoard = new EmpireInfo[50];
        public static Dictionary<uint, EmpireInfo> empireBoard = new Dictionary<uint, EmpireInfo>();
        public class Entry
        {
            public List<uint> UIDs; public Dictionary<uint, EmpireInfo> EmpireInfos;
        }
        public enum Ranks : byte { Serf = 0, Knight = 1, Baron = 3, Earl = 5, Duke = 7, Prince = 9, King = 12 }
        public static void NewEmpire(GameClient Character)
        {
            try
            {
                if (Character.Staff)
                {
                    Character.NoblePosition = 0;
                    Character.NobilityDonation = 0;
                    Character.NobleRank = Ranks.King;
                    return;
                }
                if (Character.NobilityDonation >= 3000000 || empireBoard.ContainsKey(Character.Entity.UID))
                {
                    SortedDictionary<ulong, Entry> sortdict = new SortedDictionary<ulong, Entry>();
                    if (empireBoard.ContainsKey(Character.Entity.UID))
                        empireBoard.Remove(Character.Entity.UID);
                    EmpireInfo emp = new EmpireInfo() { ID = Character.Entity.UID, Donation = Character.NobilityDonation, Name = Character.Entity.Name };

                    empireBoard.Add(Character.Entity.UID, emp);
                    foreach (EmpireInfo info in empireBoard.Values)
                    {
                        if (sortdict.ContainsKey(info.Donation))
                        {
                            Entry entry = sortdict[info.Donation];
                            entry.EmpireInfos.Add(info.ID, info);
                            entry.UIDs.Add(info.ID);
                        }
                        else
                        {
                            Entry entry = new Entry(); entry.UIDs = new List<uint>(); entry.EmpireInfos = new Dictionary<uint, EmpireInfo>();
                            entry.EmpireInfos.Add(info.ID, info);
                            entry.UIDs.Add(info.ID);
                            sortdict.Add(info.Donation, entry);
                        }
                    }
                    EmpireBoard = new EmpireInfo[50];
                    int Place = 0;
                    foreach (KeyValuePair<ulong, Entry> entries in sortdict.Reverse())
                    {
                        foreach (uint e in entries.Value.UIDs)
                        {
                            GameClient c = null;
                            if (Kernel.GamePool.ContainsKey(e))
                            {
                                c = Kernel.GamePool[e] as GameClient;
                                int beforeplace = c.NoblePosition;
                                c.NoblePosition = Place;
                                Ranks rank = Ranks.Serf;
                                if (Place >= 50)
                                {
                                    if (c.NobilityDonation >= 30000000 && c.NobilityDonation <= 100000000)
                                        rank = Ranks.Knight;
                                    else if (c.NobilityDonation >= 100000000 && c.NobilityDonation <= 200000000)
                                        rank = Ranks.Baron;
                                    else if (c.NobilityDonation >= 200000000 && c.NobilityDonation <= 300000000)
                                        rank = Ranks.Earl;
                                }
                                else if (Place >= 15 && Place <= 50)
                                    rank = Ranks.Duke;
                                else if (Place >= 3 && Place <= 15)
                                    rank = Ranks.Prince;
                                else if (Place <= 3)
                                    rank = Ranks.King;

                                if (rank != c.NobleRank || c.Entity.UID == Character.Entity.UID)
                                    c.NobleRank = rank;
                                if (beforeplace != Place)
                                    c.NobleRank = rank;
                                if (Place < 50)
                                {
                                    EmpireBoard[Place].ID = c.Entity.UID;
                                    EmpireBoard[Place].Donation = c.NobilityDonation;
                                    EmpireBoard[Place].Name = c.Entity.Name;
                                }
                            }
                            else
                            {
                                if (Place < 50)
                                {
                                    EmpireBoard[Place] = entries.Value.EmpireInfos[e];
                                }
                            }
                            Place++;
                        }
                    }
                }
            }
            catch { }
        }

    }
}
