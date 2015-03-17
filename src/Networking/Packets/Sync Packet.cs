using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic;

namespace ConquerServer
{
    public class SyncPacket : IClassPacket
    {
        public class Flags
        {
            public const uint
                None = 0x00,
                BlueName = 0x01,
                Poisoned = 0x02,
                XPSkills = 0x10,
                Ghost = 0x20,
                TeamLeader = 0x40,
                PurpleShield = 0x100,
                Stigma = 0x200,
                Dead = 0x400,
                RedName = 0x4000,
                BlackName = 0x8000,
                Superman = 0x40000,
                Invisible = 0x400000,
                Cyclone = 0x800000,
                Fly = 0x8000000,
                // qwFlags
                Curse = 0x01,
                HvnBless = 0x02,
                Nobility = 0x51;
        }

        public const uint
            Hitpoints = 0x00,
            MaxHitpoints = 0x01,
            Mana = 0x02,
            MaxMana = 0x03,
            Money = 0x04,
            Experience = 0x05,
            PKPoints = 0x06,
            Job = 0x07,
            None = 0xFFFFFFFF,
            qwRaiseFlag = 0x08,
            Stamina = 0x09,
            StatPoints = 0x0B,
            Reborn = 0x22,
            Mesh = 0x0C,
            Level = 0x0D,
            Spirit = 0x0E,
            Vitality = 0x0F,
            Strength = 0x10,
            Agility = 0x11,
            RaiseFlag = 0x1A,
            Hairstyle = 0x1B,
            ConquerPoints = 0x1E,
            XPCircle = 0x1F,
            DoubleExpTimer = 0x13,
            CursedTimer = 0x15,
            LuckyTimeTimer = 0x1D,
            HeavensBlessing = 0x12;

        private byte[] Buffer;

        const int SizeOf_Data = 12;
        public struct Data
        {
            public uint ID;
            public ulong Value;
            public uint Footer;

            static public Data Create(uint _ID, ulong _Value)
            {
                Data retn = new Data();
                retn.ID = _ID;
                retn.Value = (ulong)_Value;
                return retn;
            }
            static public Data Create(uint _ID, uint _Value)
            {
                Data retn = new Data();
                retn.ID = _ID;
                retn.Value = (uint)_Value;
                return retn;
            }
            static public Data Create(uint _ID, int _Value)
            {
                Data retn = new Data();
                retn.ID = _ID;
                retn.Value = (uint)_Value;
                return retn;
            }
            static public Data Create(uint _ID, ushort _Value)
            {
                Data retn = new Data();
                retn.ID = _ID;
                retn.Value = _Value;
                return retn;
            }
            static public Data Create(uint _ID, byte _Value)
            {
                Data retn = new Data();
                retn.ID = _ID;
                retn.Value = _Value;
                return retn;
            }
        }

        public SyncPacket(uint TotalUpdates)
        {
            Buffer = new byte[12 + (TotalUpdates * SizeOf_Data)];
            PacketBuilder.WriteUInt16((ushort)Buffer.Length, Buffer, 0);
            PacketBuilder.WriteUInt16(1017, Buffer, 2);
            PacketBuilder.WriteUInt32((uint)TotalUpdates, Buffer, 8);
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { PacketBuilder.WriteUInt32(value, Buffer, 4); }
        }
        public Data this[int UpdateNumber]
        {
            get
            {
                ushort offset = (ushort)(12 + (UpdateNumber * SizeOf_Data));
                Data data = new Data();
                data.ID = BitConverter.ToUInt32(Buffer, offset);
                data.Value = BitConverter.ToUInt32(Buffer, offset + 4);
                data.Footer = BitConverter.ToUInt32(Buffer, offset + 8);
                return data;
            }
            set
            {
                ushort offset = (ushort)(12 + (UpdateNumber * SizeOf_Data));
                PacketBuilder.WriteUInt32(value.ID, Buffer, offset);
                PacketBuilder.WriteUInt64(value.Value, Buffer, (ushort)(offset + 4));
                PacketBuilder.WriteUInt32(value.Footer, Buffer, (ushort)(offset + 8));
            }
        }

        public byte[] Serialize()
        {
            return Buffer;
        }
        public void Deserialize(byte[] Bytes)
        {
            Buffer = Bytes;
        }

        public void HitpointsAndMana(GameClient Hero, int StartIndex)
        {
            this[StartIndex] = Data.Create(Hitpoints, Hero.Entity.Hitpoints);
            this[StartIndex + 1] = Data.Create(Mana, Hero.Mana);
        }
        public void AllStats(GameClient Hero, int StartIndex)
        {
            this[StartIndex] = Data.Create(Spirit, Hero.Spirit);
            this[StartIndex + 1] = Data.Create(Vitality, Hero.Vitality);
            this[StartIndex + 2] = Data.Create(Strength, Hero.Strength);
            this[StartIndex + 3] = Data.Create(Agility, Hero.Agility);
            this[StartIndex + 4] = Data.Create(StatPoints, Hero.StatPoints);
        }
    }
}
