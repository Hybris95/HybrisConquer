using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Networking.Packets;


namespace ConquerServer_Basic
{
    public enum ConquerAction : byte
    {
        None = 0x00,
        Cool = 0xE6,
        Kneel = 0xD2,
        Sad = 0xAA,
        Happy = 0x96,
        Angry = 0xA0,
        Lie = 0x0E,
        Dance = 0x01,
        Wave = 0xBE,
        Bow = 0xC8,
        Sit = 0xFA,
        Jump = 0x64
    }

    public enum ConquerAngle : byte
    {
        SouthWest = 0,
        West = 1,
        NorthWest = 2,
        North = 3,
        NorthEast = 4,
        East = 5,
        SouthEast = 6,
        South = 7
    }

    public class Entity : IBaseEntity, IMapObject
    {
        private uint m_Defence;
        private sbyte m_Dodge;
        private EntityFlag m_EntityFlag;
        private uint m_MagicAttack;
        private ushort m_MapID;
        private uint m_MaxAttack;
        private uint m_MaxHitpoints;
        private ushort m_MDefence;
        private uint m_MinAttack;
        private object m_Owner;
        private ushort m_PlusMDefence;
        private uint m_Mesh;
        private uint m_Avatar;
        private uint m_OverlappingMesh;
        private MapObjectType m_MapObjectType;
        public byte[] SpawnPacket;

        public Entity(EntityFlag Flag, object AOwner)
        {
            this.m_EntityFlag = Flag;
            this.m_Owner = AOwner;
            this.SpawnPacket = new byte[102];
            PacketBuilder.WriteUInt16(102, this.SpawnPacket, 0);
            PacketBuilder.WriteUInt16(1014, this.SpawnPacket, 2);
            switch (m_EntityFlag)
            {
                case EntityFlag.Player: m_MapObjectType = MapObjectType.Player; break;
                case EntityFlag.Monster: m_MapObjectType = MapObjectType.Monster; break;
                //case EntityFlag.Pet: m_MapObjectType = MapObjectType.Pet; break;
                //case EntityFlag.GuildGate:
                //case EntityFlag.GuildPole:
                //case EntityFlag.TrainingGround: m_MapObjectType = MapObjectType.SOB; break;
                default: throw new ArgumentException("Flag");
            }
        }
        public void SendSpawn(GameClient Client)
        {
            SendSpawn(Client, false);
        }
        public void SendSpawn(GameClient Client, bool IgnoreScreen)
        {
            if (Client.Screen.Add(this) || IgnoreScreen)
            {
                Client.Send(SpawnPacket);
            }
        }

        public void Move(ConquerAngle Direction)
        {
            Facing = Direction;
            sbyte xi = 0, yi = 0;
            
                switch (Direction)
                {
                    case ConquerAngle.North: xi = -1; yi = -1; break;
                    case ConquerAngle.South: xi = 1; yi = 1; break;
                    case ConquerAngle.East: xi = 1; yi = -1; break;
                    case ConquerAngle.West: xi = -1; yi = 1; break;
                    case ConquerAngle.NorthWest: xi = -1; break;
                    case ConquerAngle.SouthWest: yi = 1; break;
                    case ConquerAngle.NorthEast: yi = -1; break;
                    case ConquerAngle.SouthEast: xi = 1; break;
                }
            
            X = (ushort)(X + xi);
            Y = (ushort)(Y + yi);
           
        }

        public MapObjectType MapObjType
        {
            get { return m_MapObjectType; }
            set { m_MapObjectType = value; }
        }

        public ConquerAngle Facing
        {
            get
            {
                return (ConquerAngle)this.SpawnPacket[59];
            }
            set
            {
                this.SpawnPacket[59] = (byte)value;
            }
        }

        public ConquerAction Action
        {
            get
            {
                return (ConquerAction)this.SpawnPacket[59];
            }
            set
            {
                this.SpawnPacket[59] = (byte)value;
            }
        }

        public bool Dead
        {
            get
            {
                return (this.Hitpoints <= 0);
            }
            set
            {
                if (value)
                {
                    this.Hitpoints = 0;
                    if (this.EntityFlag == EntityFlag.Player)
                    {
                        // todo
                    }
                }
                else
                {
                    this.Hitpoints = this.MaxHitpoints;
                    if (this.EntityFlag == EntityFlag.Player)
                    {
                        // todo
                    }
                }
            }
        }

        public uint Defence
        {
            get
            {
                return this.m_Defence;
            }
            set
            {
                this.m_Defence = value;
            }
        }

        public sbyte Dodge
        {
            get
            {
                return this.m_Dodge;
            }
            set
            {
                this.m_Dodge = value;
            }
        }

        public EntityFlag EntityFlag
        {
            get
            {
                return this.m_EntityFlag;
            }
            set
            {
                this.m_EntityFlag = value;
            }
        }
        public ushort GuildID
        {
            get
            {
                return BitConverter.ToUInt16(this.SpawnPacket, 20);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.SpawnPacket, 20);
            }
        }
        public uint Hitpoints
        {
            get
            {
                return BitConverter.ToUInt16(this.SpawnPacket, 48);
            }
            set
            {
                PacketBuilder.WriteUInt16((ushort)value, this.SpawnPacket, 48);
            }
        }

        public uint MagicAttack
        {
            get
            {
                return this.m_MagicAttack;
            }
            set
            {
                this.m_MagicAttack = value;
            }
        }

        public ushort MapID
        {
            get
            {
                return this.m_MapID;
            }
            set
            {
                this.m_MapID = value;
            }
        }

        public uint MaxAttack
        {
            get
            {
                return this.m_MaxAttack;
            }
            set
            {
                this.m_MaxAttack = value;
            }
        }

        public uint MaxHitpoints
        {
            get
            {
                return this.m_MaxHitpoints;
            }
            set
            {
                this.m_MaxHitpoints = value;
            }
        }

        public ushort MDefence
        {
            get
            {
                return this.m_MDefence;
            }
            set
            {
                this.m_MDefence = value;
            }
        }

        public uint MinAttack
        {
            get
            {
                return this.m_MinAttack;
            }
            set
            {
                this.m_MinAttack = value;
            }
        }

        public object Owner
        {
            get
            {
                return this.m_Owner;
            }
            set
            {
                this.m_Owner = value;
            }
        }

        public ushort PlusMDefence
        {
            get
            {
                return this.m_PlusMDefence;
            }
            set
            {
                this.m_PlusMDefence = value;
            }
        }

        public uint UID
        {
            get
            {
                return BitConverter.ToUInt32(this.SpawnPacket, 4);
            }
            set
            {
                PacketBuilder.WriteUInt32(value, this.SpawnPacket, 4);
            }
        }

        public ushort X
        {
            get
            {
                return BitConverter.ToUInt16(this.SpawnPacket, 52);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.SpawnPacket, 52);
            }
        }

        public ushort Y
        {
            get
            {
                return BitConverter.ToUInt16(this.SpawnPacket, 54);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.SpawnPacket, 54);
            }
        }

        public ushort Reborn
        {
            get
            {
                return BitConverter.ToUInt16(this.SpawnPacket, 60);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.SpawnPacket, 60);
            }
        }

        public uint StatusFlag
        {
            get
            {
                return BitConverter.ToUInt32(this.SpawnPacket, 12);
            }
            set
            {
                PacketBuilder.WriteUInt16((ushort)value, this.SpawnPacket, 12);
            }
        }

        public string Name
        {
            get
            {
                return Encoding.ASCII.GetString(this.SpawnPacket, 82, 16).Trim(new char[] { (char)0x0000 });
            }
            set
            {
                if (value.Length > 15)
                {
                    value = value.Substring(0, 15);
                }
                this.SpawnPacket[80] = 255;
                this.SpawnPacket[81] = (byte)value.Length;
                PacketBuilder.WriteString(value, this.SpawnPacket, 82);
            }
        }
        public uint HeadGear
        {
            get
            {
                return BitConverter.ToUInt32(this.SpawnPacket, 28);
            }
            set
            {
                PacketBuilder.WriteUInt32(value, this.SpawnPacket, 28);
            }
        }
        public uint Armor
        {
            get
            {
                return BitConverter.ToUInt32(this.SpawnPacket, 32);
            }
            set
            {
                PacketBuilder.WriteUInt32(value, this.SpawnPacket, 32);
            }
        }
        public uint MainHand
        {
            get
            {
                return BitConverter.ToUInt32(this.SpawnPacket, 36);
            }
            set
            {
                PacketBuilder.WriteUInt32(value, this.SpawnPacket, 36);
            }
        }
        public uint LeftArm
        {
            get
            {
                return BitConverter.ToUInt32(this.SpawnPacket, 40);
            }
            set
            {
                PacketBuilder.WriteUInt32(value, this.SpawnPacket, 40);
            }
        }


        public uint OverlappingMesh
        {
            get { return m_OverlappingMesh; }
            set
            {
                m_OverlappingMesh = value;
                PacketBuilder.WriteUInt32(((value * 10000000) + (m_Avatar * 10000) + m_Mesh), this.SpawnPacket, 8);
            }
        }
        public uint Mesh
        {
            get { return m_Mesh; }
            set
            {
                m_Mesh = value;
                PacketBuilder.WriteUInt32(((m_OverlappingMesh * 10000000) + (m_Avatar * 10000) + value), this.SpawnPacket, 8);
            }
        }
        public uint Avatar
        {
            get { return m_Avatar; }
            set
            {
                m_Avatar = value;
                PacketBuilder.WriteUInt32(((m_OverlappingMesh * 10000000) + (value * 10000) + m_Mesh), this.SpawnPacket, 8);
            }
        }

        public uint Model
        {
            get 
            { 
                return BitConverter.ToUInt32(this.SpawnPacket, 8);
            }
        }

        public ushort HairStyle
        {
            get
            {
                return BitConverter.ToUInt16(this.SpawnPacket, 56);
            }
            set
            {
                PacketBuilder.WriteUInt16(value, this.SpawnPacket, 56);
            }
        }

        public byte Level
        {
            get
            {
                return this.SpawnPacket[50];
            }
            set
            {
                this.SpawnPacket[50] = value;
            } 
        }

        public GameClient GetClient(Entity Entity)
        {
            foreach (GameClient Hero in Kernel.Clients)
            {
                if (Hero.Entity == Entity)
                    return Hero;
            }
            return null;
        }

        public void Dies(Entity Entity, byte[] Packet)
        {
            AttackPacket atkPacket = new AttackPacket(true);
            atkPacket.Deserialize(Packet);

            atkPacket.AttackType = (ushort)AttackType.Death;
            atkPacket.AttackedX = Entity.X;
            atkPacket.AttackedY = Entity.Y;

            foreach (GameClient Hero in Kernel.Clients)
            {
                if (Kernel.GetDistance(Hero.Entity.X, Hero.Entity.Y, atkPacket.AttackedX, atkPacket.AttackedY) < 24)
                {
                    atkPacket.Serialize();
                    atkPacket.Send(Hero);
                }
            }
            Entity.Dead = true;

            foreach (GameClient Hero in Kernel.Clients)
            {
                if (Hero.Entity.MapID == Entity.MapID)
                    if (Kernel.GetDistance(Hero.Entity.X, Hero.Entity.Y, Entity.X, Entity.Y) < 24)
                        Sync.MobFade(Hero, Entity);
            }
            DataPacket removeEntity = new DataPacket(true);
            removeEntity.UID = Entity.UID;
            removeEntity.wParam3 = DataPacket.RemoveEntity;
            removeEntity.Serialize();
        }

    }
}
