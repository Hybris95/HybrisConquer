using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer;

namespace ConquerServer_Basic
{
    class Sync
    {
        static public void LevelUp(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(11);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Agility, Hero.Agility);
            sync[1] = SyncPacket.Data.Create(SyncPacket.Strength, Hero.Strength);
            sync[2] = SyncPacket.Data.Create(SyncPacket.Vitality, Hero.Vitality);
            sync[3] = SyncPacket.Data.Create(SyncPacket.Spirit, Hero.Spirit);
            sync[4] = SyncPacket.Data.Create(SyncPacket.StatPoints, Hero.StatPoints);
            sync[5] = SyncPacket.Data.Create(SyncPacket.Level, Hero.Entity.Level);
            sync[6] = SyncPacket.Data.Create(SyncPacket.MaxHitpoints, Hero.Entity.MaxHitpoints);
            sync[7] = SyncPacket.Data.Create(SyncPacket.MaxMana, Hero.MaxMana);
            sync[8] = SyncPacket.Data.Create(SyncPacket.Hitpoints, Hero.Entity.Hitpoints);
            sync[9] = SyncPacket.Data.Create(SyncPacket.Mana, Hero.Mana);
            sync[10] = SyncPacket.Data.Create(SyncPacket.Experience, (ulong)Hero.Experience);
            Hero.Send(sync);
        }
        static public void PkP(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.PKPoints, Hero.PkPoints);
            Hero.Send(sync);
        }
        static public void Test(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(4);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Level, Hero.Entity.Level);
            sync[1] = SyncPacket.Data.Create(SyncPacket.Reborn, Hero.Entity.Reborn);
            sync[2] = SyncPacket.Data.Create(SyncPacket.Money, Hero.Money);
            sync[3] = SyncPacket.Data.Create(SyncPacket.ConquerPoints, Hero.ConquerPoints);
            Hero.Send(sync);
        }

        static public void Experience(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Experience, (ulong)Hero.Experience);
            Hero.Send(sync);
        }

        static public void MobFade(GameClient Hero, Entity Mob)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Mob.UID;
            sync[0] = SyncPacket.Data.Create(SyncPacket.RaiseFlag, 2080);
            Hero.SendScreen(sync, true);
        }

        static public void Money(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Money, Hero.Money);
            Hero.Send(sync);
        }
        static public void Currency(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(2);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.ConquerPoints, Hero.ConquerPoints);
            sync[0] = SyncPacket.Data.Create(SyncPacket.Money, Hero.Money);
            Hero.Send(sync);
        }
        static public void CPs(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.ConquerPoints, Hero.ConquerPoints);
            //Hero.SendScreen(sync, true);
            Hero.Send(sync);
        }

        static public void Stats(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(6);
            sync.UID = Hero.Identifier;

            sync[0] = SyncPacket.Data.Create(SyncPacket.Agility, Hero.Agility);
            sync[1] = SyncPacket.Data.Create(SyncPacket.Strength, Hero.Strength);
            sync[2] = SyncPacket.Data.Create(SyncPacket.Vitality, Hero.Vitality);
            sync[3] = SyncPacket.Data.Create(SyncPacket.Spirit, Hero.Spirit);
            sync[4] = SyncPacket.Data.Create(SyncPacket.StatPoints, Hero.StatPoints);
            sync[5] = SyncPacket.Data.Create(SyncPacket.Reborn, Hero.Entity.Reborn);
            Hero.Send(sync);
        }

        static public void Level(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(2);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Level, Hero.Entity.Level);
            sync[1] = SyncPacket.Data.Create(SyncPacket.Experience, Hero.Experience);

            Hero.Send(sync);
        }

        static public void HP(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Hitpoints, Hero.Entity.Hitpoints);
            Hero.Send(sync);
        }

        static public void MaxHP(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.MaxHitpoints, Hero.Entity.MaxHitpoints);
            Hero.Send(sync);
        }

        static public void MP(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(2);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.MaxMana, Hero.MaxMana);
            sync[1] = SyncPacket.Data.Create(SyncPacket.Mana, Hero.Mana);
            Hero.Send(sync);
        }

        static public void Hairstyle(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Hairstyle, Hero.Entity.HairStyle);
            Hero.Send(sync);
        }

        static public void Job(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Job, Hero.Job);
            Hero.SendScreen(sync, true);
        }

        static public void Stamina(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Stamina, Hero.Stamina);
            Hero.SendScreen(sync, true);
        }

        static public void Mesh(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Mesh, Hero.Entity.Model);
            Hero.SendScreen(sync, true);
        }
        static public void Avatar(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(1);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Mesh, Hero.Entity.Model);
            Hero.SendScreen(sync, true);
        }
        
        static public void Revive(GameClient Hero)
        {
            SyncPacket sync = new SyncPacket(2);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Mesh, Hero.Entity.Model);
            sync[1] = SyncPacket.Data.Create(SyncPacket.RaiseFlag, SyncPacket.Flags.None);
            Hero.SendScreen(sync, true);
        }
        static public void Death(GameClient Hero, uint Mesh)
        {
            SyncPacket sync = new SyncPacket(2);
            sync.UID = Hero.Identifier;
            sync[0] = SyncPacket.Data.Create(SyncPacket.Mesh, Mesh);
            sync[1] = SyncPacket.Data.Create(SyncPacket.RaiseFlag, SyncPacket.Flags.Dead);
            Hero.SendScreen(sync, true);
        }
    }

}
