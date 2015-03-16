#pragma warning disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using ConquerServer_Basic.Networking.Packets;
using ConquerServer_Basic.Interfaces;
using ConquerServer_Basic.Attack_Handling;
using System.Collections;
using ConquerServer_Basic.Networking.Packet_Handling;
using ConquerServer_Basic.Main_Classes;
using ConquerServer_Basic.Guilds;


namespace ConquerServer_Basic
{
    public enum PKMode : byte
    {
        PK = 0,
        Peace = 1,
        Team = 2,
        Capture = 3
    }

    public class GameClient : AuthClient
    {
        public PKMode PKMode;
        public Entity Attacked;
        public UInt16 SkillID;
        public AttackType AtkType;
        public Byte[] Packet;
        public Boolean Attacking = false;
        public System.Timers.Timer AtkTimer;
        public void AtkTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Attacking)
            {
                AttackHandler.Handle(this, Packet);
            }
        }

        public ushort BaseHP
        {
            get { return (ushort)Math.Floor((double)Entity.Hitpoints); }
        }
        public ushort BaseMP
        {
            get { return (ushort)Math.Floor((double)Mana); }
        }

        private UInt32 _conquerpoints;
        public UInt32 ConquerPoints
        {
            get { return _conquerpoints; }
            set
            {
                _conquerpoints = value;
                Characters.UpdateCharacter(value, "ConquerPoints", this); 
                Sync.CPs(this);
            }
        }

        private UInt32 _money;
        public UInt32 Money
        {
            get { return _money; }
            set
            {
                _money = value;
                Characters.UpdateCharacter(value, "money", this);
                Sync.Money(this);
            }
        }

        private UInt64 _experience;
        public UInt64 Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                Characters.UpdateCharacter(value, "experience", this);
                Sync.Experience(this);
            }
        }

        private UInt16 _agility;
        public UInt16 Agility
        {
            get { return _agility; }
            set
            {
                _agility = value;
                Characters.UpdateCharacter(value, "dexterity", this);
                Sync.Stats(this);
            }
        }

        private UInt16 _statpoints;
        public UInt16 StatPoints
        {
            get { return _statpoints; }
            set
            {
                _statpoints = value;
                Characters.UpdateCharacter(value, "statpoints", this);
                Sync.Stats(this);
            }
        }

        private UInt16 _strength;
        public UInt16 Strength
        {
            get { return _strength; }
            set
            {
                _strength = value;
                Characters.UpdateCharacter(value, "strength", this);
                Sync.Stats(this);
            }
        }

        private UInt16 _vitality;
        public UInt16 Vitality
        {
            get { return _vitality; }
            set
            {
                _vitality = value;
                Characters.UpdateCharacter(value, "vitality", this);
                Sync.Stats(this);
            }
        }

        private UInt16 _spirit;
        public UInt16 Spirit
        {
            get { return _spirit; }
            set
            {
                _spirit = value;
                Characters.UpdateCharacter(value, "spirit", this);
                Sync.Stats(this);
            }
        }

        private UInt16 _mana;
        public UInt16 Mana
        {
            get { return _mana; }
            set
            {
                _mana = value;
                Sync.MP(this);
            }
        }

        private UInt16 _pkp;
        public UInt16 PkPoints
        {
            get { return _pkp; }
            set
            {
                _pkp = value;
                Characters.UpdateCharacter(value, "pkpoints", this);
                Sync.PkP(this);
            }
        }

        private UInt16 _maxmana;
        public UInt16 MaxMana
        {
            get { return _maxmana; }
            set
            {
                _maxmana = value;
                Sync.MP(this);
            }
        }

        private Byte _job;
        public Byte Job
        {
            get { return _job; }
            set
            {
                _job = value;
                Characters.UpdateCharacter(value, "class", this);
                Sync.Job(this);
            }
        }

        private String _spouse;
        public String Spouse
        {
            get { return _spouse; }
            set { _spouse = value; }
        }

        private Byte _stamina;
        public Byte Stamina
        {
            get { return _stamina; }
            set
            {
                _stamina = value;
                Sync.Stamina(this);
            }
        }

        public byte Level
        {
            get { return this.Entity.Level; }
            set { this.Entity.Level = value; Characters.UpdateCharacter(value, "level", this); Sync.Level(this); }
        }

        private ushort _prevmap;
        public ushort PrevMap
        {
            get { return _prevmap; }
            set
            {
                _prevmap = value;
                Characters.UpdateCharacter(value, "prevmap", this);
            }
        }
        
        public ushort MapID
        {
            get { return this.Entity.MapID; }
            set { this.Entity.MapID = value; Characters.UpdateCharacter(value, "mapid", this); }
        }
        public ushort X
        {
            get { return this.Entity.X; }
            set { this.Entity.X = value; Characters.UpdateCharacter(value, "x", this); }
        }
        public ushort Y
        {
            get { return this.Entity.Y; }
            set { this.Entity.Y = value; Characters.UpdateCharacter(value, "y", this); }
        }


        public enum Ranks : byte { Serf = 0, Knight = 1, Baron = 3, Earl = 5, Duke = 7, Prince = 9, King = 12 }

        private uint _nobilitydonaton;
        public uint NobilityDonation
        {
            get { return _nobilitydonaton; }
            set { _nobilitydonaton = value; Characters.UpdateCharacter(value, "NobDonation", this); }
        }
        private Empire.Ranks _noblerank;
        public Empire.Ranks NobleRank
        {
            get { return _noblerank; }
            set { _noblerank = value; Characters.UpdateCharacter((byte)value, "NobRank", this); }
        }
        private int _nobleposition;
        public int NoblePosition
        {
            get { return _nobleposition; }
            set { _nobleposition = value; Characters.UpdateCharacter(value, "NobPosition", this); }
        }
        public bool Staff;

        public PlayerTeam Team; // null if no team is active
        public Entity Entity;
        public Screen Screen;
        private Dictionary<UInt32, IConquerItem> m_Inventory;
        public IConquerItem[] Inventory;
        public Guild MyGuild;
        public uint GuildDonation;
        public byte GuildPosition;
        public Dictionary<UInt16, IConquerItem> Equipment;
        public Dictionary<ushort, ISkill> Profs, Spells;
        

        // These should only be modified in Misc.(Un)LoadItemStats ----
        public UInt32 BaseMinAttack;
        public UInt32 BaseMaxAttack;
        public UInt32 BaseMagicAttack;
        public UInt16 ItemHP;
        public UInt16 ItemMP;
        // -----

        public Byte BlessPercent;
        public SByte AttackRange;
        public UInt32 AttackSpeed;
        public Int16[] Gems;


        //Trade <<
        public uint TradingWith = 0;
        public ArrayList TradeSide = new ArrayList(20);
        public uint TradingSilvers = 0;
        public uint TradingCPs = 0;
        public bool Trading = false;
        public bool ClickedOK = false;
        // >>

        #region Job Bools
        public Boolean Trojan
        {
            get { return (this.Job > 9 && this.Job < 16); }
        }
        public Boolean Warrior
        {
            get { return (this.Job > 19 && this.Job < 26); }
        }
        public Boolean Archer
        {
            get { return (this.Job > 39 && this.Job < 46); }
        }
        public Boolean Water
        {
            get { return (this.Job > 131 && this.Job < 136); }
        }
        public Boolean Fire
        {
            get { return (this.Job > 141 && this.Job < 146); }
        }
        public Boolean Tao
        {
            get { return (this.Job > 99 && this.Job < 102); }
        }
        #endregion
        public UInt32 ActiveNpcID;

        public GameClient(HybridWinsockClient _Socket)
            : base(_Socket)
        {
            this.Entity = new Entity(EntityFlag.Player, this);
            this.AuthPhase = AuthPhases.GameServer;
            this.Screen = new Screen(this);
            this.m_Inventory = new Dictionary<UInt32, IConquerItem>(40);
            this.Inventory = new IConquerItem[0];
            this.Equipment = new Dictionary<UInt16, IConquerItem>(9);
            this.Gems = new Int16[8];

            this.Profs = new Dictionary<ushort, ISkill>();
            this.Spells = new Dictionary<ushort, ISkill>();
        }

        public void LogOff()
        {
            try
            {
                if (this.Team != null)
                {
                    TeamPacket Disband = new TeamPacket(true);
                    Disband.UID = this.Identifier;
                    if (this.Team.TeamLeader)
                    {
                        Disband.Type = TeamPacket.Dismiss;
                        Teams.DismissTeam(Disband, this);
                    }
                    else
                    {
                        Disband.Type = TeamPacket.ExitTeam;
                        Teams.LeaveTeam(Disband, this);
                    }
                }
                if (this.Entity.MapID == 1036)
                    this.Teleport(1002, 438, 377);
                if (this.Entity.Dead)
                    PacketProcessor.Revive(this, false);
                DataPacket dp = new DataPacket(true);
                dp.UID = this.Identifier;
                dp.ID = DataPacket.RemoveEntity;
                foreach (GameClient C in Kernel.GamePool.Values)
                { C.Send(dp); }
            }
            finally
            {
                Characters.SaveCharacter(this);
                Kernel.GamePool.ThreadSafeRemove(this.Identifier);
                this.Socket.Disconnect();
                Console.WriteLine(this.Entity.Name + " has safely logged off from IP: " + this.Socket.Connection.RemoteEndPoint.ToString().Split(':')[0]);
            }
        }
        private ushort StatHP;
        public void CalculateHPBonus()
        {
            switch (this.Job)
            {
                case 11: this.Entity.MaxHitpoints = (int)(this.StatHP * 1.05F); break;
                case 12: this.Entity.MaxHitpoints = (int)(this.StatHP * 1.08F); break;
                case 13: this.Entity.MaxHitpoints = (int)(this.StatHP * 1.10F); break;
                case 14: this.Entity.MaxHitpoints = (int)(this.StatHP * 1.12F); break;
                case 15: this.Entity.MaxHitpoints = (int)(this.StatHP * 1.15F); break;
                default: this.Entity.MaxHitpoints = (int)this.StatHP; break;
            }
            this.Entity.MaxHitpoints += this.ItemHP;
            this.Entity.Hitpoints = Math.Min(this.Entity.Hitpoints, this.Entity.MaxHitpoints);
        }
        public void CalculateMaxHP(GameClient Hero)
        {
            double HP = (Hero.Vitality * 24) + ((Hero.Strength + Hero.Agility + Hero.Spirit) * 3);
            switch (Hero.Job)
            {
                case 11:
                    HP *= 1.05;
                    break;
                case 12:
                    HP *= 1.08;
                    break;
                case 13:
                    HP *= 1.10;
                    break;
                case 14:
                    HP *= 1.12;
                    break;
                case 15:
                    HP *= 1.15;
                    break;
                default:
                    HP *= 1;
                    break;
            }
            HP += ItemHP;
            Hero.Entity.MaxHitpoints = (int)HP;
            Sync.MaxHP(this);
        }
        public void Health(ushort Amount)
        {
            if (Entity.Hitpoints + Amount > Entity.MaxHitpoints)
                Entity.Hitpoints = Entity.MaxHitpoints;
            else
                Entity.Hitpoints += Amount;
        }
        public void CalculateDamageBoost()
        {
            this.Entity.MaxAttack = (UInt32)((this.Strength + this.BaseMaxAttack) * (1 + (this.Gems[1] * 0.01))); // dg
            this.Entity.MinAttack = (UInt32)((this.Strength + this.BaseMinAttack) * (1 + (this.Gems[1] * 0.01))); // dg
            this.Entity.MagicAttack = (UInt32)(this.BaseMagicAttack * (1 + (this.Gems[0] * 0.01))); // pg
        }


        public void GainExp(ulong ExpGain, bool UseExpRate)
        {
            if (UseExpRate)
                Experience += (ulong)(ExpGain * Program.ExperienceRate);
            else
                Experience += ExpGain;

            ulong ReqExp = 0;
            Kernel.LevelExpReq.TryGetValue(Entity.Level, out ReqExp);
            if (Experience >= ReqExp)
            {
                while (Experience >= ReqExp && Entity.Level < 137)
                {
                    ReqExp = 0;
                    Kernel.LevelExpReq.TryGetValue(Entity.Level, out ReqExp);
                    Experience -= ReqExp;
                    Entity.Level++;
                }
                Misc.GetStats(this);
                NewMath.Vitals(this);
                Entity.Hitpoints = Entity.MaxHitpoints;

                Sync.LevelUp(this);
            }
            else
                Sync.Experience(this);
        }

        public void LoadEquipment()
        {
            lock (this.Equipment)
            {
                foreach (KeyValuePair<UInt16, IConquerItem> DE in Equipment)
                {
                    LoadItemStats(DE.Value);
                    Send(DE.Value as IClassPacket);
                }
            }
        }
        public void LoadItemStats(IConquerItem Item)
        {
            IniFile rdr;
            StanderdItemStats standerd = new StanderdItemStats(Item.ID, out rdr);

            this.BaseMinAttack += standerd.MinAttack;
            this.BaseMaxAttack += standerd.MaxAttack;

            this.Entity.Defence += standerd.PhysicalDefence;
            this.Entity.MDefence += standerd.MDefence;
            this.Entity.Dodge += standerd.Dodge;

            this.BaseMagicAttack += standerd.MAttack;
            this.ItemHP += standerd.HP;
            this.ItemMP += standerd.MP;
            if (Item.Position == ItemPosition.Right)
            {
                this.AttackSpeed = standerd.Frequency;
                this.AttackRange += standerd.AttackRange;
            }

            if (Item.Plus != 0)
            {
                PlusItemStats plus = new PlusItemStats(Item.ID, Item.Plus, rdr);
                this.BaseMinAttack += plus.MinAttack;
                this.BaseMaxAttack += plus.MaxAttack;
                this.BaseMagicAttack += plus.MAttack;
                this.Entity.Defence += plus.PhysicalDefence;
                this.Entity.Dodge += plus.Dodge;
                this.Entity.PlusMDefence += plus.PlusMDefence;
                this.ItemHP += plus.HP;
            }
            if (Item.Position != ItemPosition.Garment && // Ignore these stats on these slots
                Item.Position != ItemPosition.Bottle)
            {
                this.ItemHP += Item.Enchant;
                this.BlessPercent += Item.Bless;
                //GemAlgorithm(this, Item.SocketOne, Item.SocketTwo, true);
            }
        }

        public void Teleport(UInt16 _MapID, UInt16 _X, UInt16 _Y)
        {
            DataPacket Packet = new DataPacket(true);
            Packet.ID = DataPacket.RemoveEntity;
            Packet.UID = this.Entity.UID;
            this.SendScreen(Packet, false);

            this.MapID = PrevMap;
            this.MapID = _MapID;
            this.X = _X;
            this.Y = _Y;

            Packet.ID = DataPacket.SetLocation;
            Packet.dwParam = this.Entity.MapID;
            Packet.wParam1 = this.Entity.X;
            Packet.wParam2 = this.Entity.Y;
            this.Send(Packet);
        }

        public void SendScreen(Byte[] Msg, Boolean SendSelf)
        {
            foreach (IMapObject obj in Screen.Objects)
            {
                if (obj.MapObjType == MapObjectType.Player)
                {
                    (obj.Owner as GameClient).Send(Msg);
                }
            }
            if (SendSelf)
                Send(Msg);
        }
        public void SendScreen(IClassPacket CMsg, Boolean SendSelf)
        {
            SendScreen(CMsg.Serialize(), SendSelf);
        }

        #region Item Functions
        private void CreateInventoryInstance()
        {
            IConquerItem[] temp = new IConquerItem[m_Inventory.Count];
            m_Inventory.Values.CopyTo(temp, 0);
            Inventory = temp;
        }
        public void AddInv(IConquerItem Item)
        {
            lock (m_Inventory)
            {
                if (m_Inventory.Count < 40)
                {
                    Item = new ItemDataPacket(true);
                    Item.Position = ItemPosition.Inventory;
                    m_Inventory.Add(Item.UID, Item);

                    Item.Send(this);
                    CreateInventoryInstance();

                }
            }
        }
        public Boolean AddInventory(IConquerItem Item)
        {
            lock (m_Inventory)
            {
                if (m_Inventory.Count < 40)
                {
                    Item.Position = ItemPosition.Inventory;
                    m_Inventory.Add(Item.UID, Item);

                    Item.Send(this);
                    CreateInventoryInstance();

                    return true;
                }
            }
            return false;
        }
        public Boolean AddInventory(IConquerItem Item, byte Amount)
        {
            lock (m_Inventory)
            {
                if (m_Inventory.Count < (40 - Amount))
                {
                    for (byte i = 0; i < Amount; i++)
                    {
                        Item.UID = ItemDataPacket.NextItemUID;
                        Item.Position = ItemPosition.Inventory;
                        m_Inventory.Add(Item.UID, Item);
                        Item.Send(this);
                        CreateInventoryInstance();
                    }
                    return true;
                }
            }
            return false;
        }
        public void RemoveInventory(UInt32 UID)
        {
            lock (m_Inventory)
            {
                m_Inventory.Remove(UID);
                ItemUsagePacket Remove = new ItemUsagePacket(true);
                Remove.ID = ItemUsagePacket.RemoveInventory;
                Remove.UID = UID;

                this.Send(Remove);
                CreateInventoryInstance();
            }
        }
        public SByte CountInventory(UInt32 ItemID)
        {
            SByte Count = 0;
            foreach (IConquerItem item in Inventory)
                if (item.ID == ItemID)
                    Count++;
            return Count;
        }
        public IConquerItem GetInventoryItem(UInt32 UID)
        {
            IConquerItem item;
            m_Inventory.TryGetValue(UID, out item);
            return item;
        }
        // This method should ONLY be called if you need
        // the instance to the items, not the count, if you want
        // the count for an itemID, you should call CountInventory()
        public IEnumerable<IConquerItem> SearchInventory(UInt32 ItemID)
        {
            foreach (IConquerItem item in Inventory)
            {
                if (item.ID == ItemID)
                    yield return item;
            }
        } 
        #endregion

        #region Equipment Functions
        public void SendStatMessage()
        {
            MessagePacket Msg = new MessagePacket(" Attack: {0}~{1} MagicAttack: {2} Defence: {3} MDefence: {4} Dodge: {5}",
                (uint)Color.White, (uint)ChatType.Center);
            Msg.Message = String.Format(Msg.Message,
                new object[] { this.Entity.MinAttack, this.Entity.MaxAttack, this.Entity.MagicAttack, this.Entity.Defence, (this.Entity.MDefence + this.Entity.PlusMDefence), this.Entity.Dodge });
            this.Send(Msg);
        }
        public void RemoveEquip(byte EquipSlot)
        {
            IConquerItem Removed;
            if (Equipment.TryGetValue(EquipSlot, out Removed))
            {
                Equipment.Remove(EquipSlot);

                ItemUsagePacket UnEquipPack = new ItemUsagePacket(true);
                UnEquipPack.UID = Removed.UID;
                UnEquipPack.ID = ItemUsagePacket.UnequipItem;
                Send(UnEquipPack);

                ushort swap = Removed.Position;
                Removed.Position = EquipSlot;
                Misc.UnloadItemStats(this, Removed);
                Removed.Position = swap;

            }
        }
        public Boolean Unequip(UInt16 EquipSlot)
        {
            IConquerItem Removed;
            if (Equipment.TryGetValue(EquipSlot, out Removed))
            {
                if (AddInventory(Removed))
                {
                    Equipment.Remove(EquipSlot);

                    ItemUsagePacket UnEquipPack = new ItemUsagePacket(true);
                    UnEquipPack.UID = Removed.UID;
                    UnEquipPack.ID = ItemUsagePacket.UnequipItem;
                    Send(UnEquipPack);

                    UInt16 swap = Removed.Position;
                    Removed.Position = EquipSlot;
                    Misc.UnloadItemStats(this, Removed);
                    Removed.Position = swap;
                    return true;
                }
            }
            return false;
        }
        public Boolean Equip(IConquerItem Item, UInt16 EquipSlot)
        {
            if (!Equipment.ContainsKey(EquipSlot))
            {
                Item.Position = EquipSlot;
                // TODO - Can the item be equipped ?
                // TODO - If not, don't add the item and return "false"
                Equipment.Add(EquipSlot, Item);
                Item.Send(this);
                Misc.LoadItemStats(this, Item);
                return true;
            }
            return false;
        } 
        #endregion

        public void Dies(GameClient DeadClient, byte[] Packet)
        {
            AttackPacket atkPacket = new AttackPacket(true);
            atkPacket.Deserialize(Packet);

            atkPacket.AttackType = (ushort)AttackType.Death;
            atkPacket.AttackedX = DeadClient.Entity.X;
            atkPacket.AttackedY = DeadClient.Entity.Y;

            foreach (GameClient Hero in Kernel.Clients)
            {
                if (Kernel.GetDistance(Hero.Entity.X, Hero.Entity.Y, atkPacket.AttackedX, atkPacket.AttackedY) < 24)
                {
                    atkPacket.Serialize();
                    atkPacket.Send(Hero);
                }
            }

            DeadClient.Entity.Dead = true;

            uint ghostModel = 0;
            switch (DeadClient.Entity.Model)
            {
                case 1003:
                case 1004:
                    ghostModel = 15099;
                    break;
                case 2001:
                case 2002:
                    ghostModel = 15199;
                    break;
                default:
                    ghostModel = 15099;
                    break;
            }

            Sync.HP(DeadClient);
            Sync.Death(DeadClient, ghostModel);
        }

        #region Proficiency Functions
        public bool LearnProf(ISkill Prof)
        {
            if (Profs.ContainsKey(Prof.ID))
            {
                Profs[Prof.ID] = Prof;
                Prof.Send(this);
                return false;
            }
            else
            {
                this.Profs.Add(Prof.ID, Prof);
                Prof.Send(this);
                return true;
            }
        }
        public bool LearnProf(ushort ID, ushort Level)
        {
            ISkill Prof = new ProfPacket(true);
            Prof.ID = ID; Prof.Level = Level;
            return LearnProf(Prof);
        }
        public bool LearnProf(ushort ID, ushort Level, uint Exp)
        {
            ISkill Prof = new ProfPacket(true);
            Prof.ID = ID; Prof.Level = Level; Prof.Experience = Exp;
            return LearnProf(Prof);
        }
        public bool LearnSpell(ISkill Spell)
        {
            if (this.Spells.ContainsKey(Spell.ID))
            {
                Spells[Spell.ID] = Spell;
                Spell.Send(this);
                return false;
            }
            else
            {
                Spells.Add(Spell.ID, Spell);
                Spell.Send(this);
                return true;
            }
        }
        public bool LearnSpell(ushort ID, ushort Level)
        {
            ISkill Spell = new SpellPacket(true);
            Spell.ID = ID;
            Spell.Level = Level;
            return LearnSpell(Spell);
        }
        public void GainProfExp(ISkill Prof, uint ProfGain, bool UseProfRate)
        {
            if (UseProfRate)
                Prof.Experience += ProfGain * Program.WeaponSkillRate;
            else
                Prof.Experience += ProfGain;

            ulong ReqProfExp = 0;
            Kernel.ProfExpReq.TryGetValue((byte)Prof.Level, out ReqProfExp);
            if (Prof.Experience >= ReqProfExp)
            {
                while (Prof.Experience >= ReqProfExp && Prof.Level < 20)
                {
                    ReqProfExp = 0;
                    Kernel.ProfExpReq.TryGetValue((byte)Prof.Level, out ReqProfExp);
                    Prof.Experience -= (uint)ReqProfExp;
                    Prof.Level++;
                }
            }

            Prof.Send(this);
        }
        public void GainProfExp(uint ProfGain, bool UseProfRate)
        {
            ISkill Prof = new ProfPacket(true);
            IConquerItem Equip = null;
            if (Equipment.TryGetValue(ItemPosition.Left, out Equip))
            {
                Prof.ID = ushort.Parse(Equip.ID.ToString().Remove(3));
                Console.WriteLine("Prof ID: {0}", Prof.ID);
                if (Profs.ContainsKey(Prof.ID))
                    Profs[Prof.ID] = Prof;
                else
                    this.Profs.Add(Prof.ID, Prof);
                GainProfExp(Prof, ProfGain, UseProfRate);
            }
            if (Equipment.TryGetValue(ItemPosition.Right, out Equip))
            {
                Prof.ID = ushort.Parse(Equip.ID.ToString().Remove(3));
                Console.WriteLine("Prof ID: {0}", Prof.ID);
                if (Profs.ContainsKey(Prof.ID))
                    Profs[Prof.ID] = Prof;
                else
                    this.Profs.Add(Prof.ID, Prof);
                GainProfExp(Prof, ProfGain, UseProfRate);
            }
        }
        #endregion
    }
}
