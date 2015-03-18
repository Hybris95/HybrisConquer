using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Networking.Packets;
using ConquerServer_Basic.Networking.Packet_Handling;


namespace ConquerServer_Basic.Attack_Handling
{
    public class Physical
    {
        static public void Attack(GameClient Hero)
        {
            AttackPacket atkPacket = new AttackPacket(true);
            atkPacket.Deserialize(Hero.Packet);

            if (Hero.Attacked.Dead)
                return;
            if (Hero.Attacked.MapID != Hero.Entity.MapID)
                return;
            if (Kernel.GetDistance(Hero.Entity.X, Hero.Entity.Y, Hero.Attacked.X, Hero.Attacked.Y) > 6 && Hero.AtkType != AttackType.Archer)
                return;

            switch (Hero.Attacked.EntityFlag)
            {
                case EntityFlag.Monster:
                    {
                        Hero.Attacking = true;

                        uint Damage = (uint)Kernel.Random.Next(
                            (int)Hero.Entity.MinAttack,
                            (int)Hero.Entity.MaxAttack);
                        Damage -= Hero.Attacked.Defence;

                        Damage = Math.Max(1, Damage);
                        atkPacket.Damage = Damage;

                        foreach (GameClient _hero in Kernel.Clients)
                        {
                            if (Kernel.GetDistance(_hero.Entity.X, _hero.Entity.Y, atkPacket.AttackedX, atkPacket.AttackedY) < 24)
                            {
                                atkPacket.Serialize();
                                atkPacket.Send(_hero);
                            }
                        }

                        foreach (Entity mob in Kernel.eMonsters.Values)
                        {
                            if (mob.UID == atkPacket.AttackedUID)
                            {
                                if (Damage >= mob.Hitpoints)
                                {
                                    DataPacketHandling.GetSurroundings(Hero);
                                    mob.Dies(mob, Hero.Packet);
                                    Hero.Attacking = false;
                                    Hero.Attacked = null;
                                    Hero.GainExp((ulong)(mob.Hitpoints + Damage) / 10, true);
                                    Hero.GainProfExp((uint)mob.Hitpoints + Damage, true);
                                    DataPacketHandling.GetSurroundings(Hero);
                                }
                                else
                                {
                                    mob.Hitpoints -= Damage;
                                    Hero.GainExp(Damage / 10, true);
                                    Hero.GainProfExp(Damage, true);
                                }
                                break;
                            }
                        }

                        break;
                    }
                case EntityFlag.Player:
                    {
                        Hero.Attacking = true;

                        GameClient AttackedClient = Hero.Attacked.GetClient(Hero.Attacked);

                        uint Damage = (uint)Kernel.Random.Next(
                            (int)Hero.Entity.MinAttack,
                            (int)Hero.Entity.MaxAttack);
                        Damage -= Hero.Attacked.Defence;

                        double RbDmgPct = 0;
                        switch (Hero.Attacked.Reborn)
                        {
                            case 1:
                                RbDmgPct += 0.05;
                                break;
                            case 2:
                                RbDmgPct += 0.1;
                                break;
                        }
                        Damage = (uint)(Damage - (Damage * RbDmgPct));

                        double BlessedPct = 0;
                        foreach (short gem in AttackedClient.Gems)
                        {
                            switch (gem)
                            {
                                case 71:
                                    BlessedPct += 0.02;
                                    break;
                                case 72:
                                    BlessedPct += 0.04;
                                    break;
                                case 73:
                                    BlessedPct += 0.06;
                                    break;
                            }
                        }
                        Damage = (uint)(Damage - (Damage * BlessedPct));

                        Damage = Math.Max(1, Damage);

                        atkPacket.Damage = Damage;

                        foreach (GameClient _Hero in Kernel.Clients)
                        {
                            if (Kernel.GetDistance(_Hero.Entity.X, _Hero.Entity.Y, atkPacket.AttackedX, atkPacket.AttackedY) < 24)
                            {
                                atkPacket.Serialize();
                                atkPacket.Send(_Hero);
                            }
                        }

                        foreach (GameClient hero in Kernel.Clients)
                        {
                            if (hero.Entity.UID == atkPacket.AttackedUID)
                            {
                                if (Damage >= hero.Attacked.Hitpoints)
                                {
                                    DataPacketHandling.GetSurroundings(Hero);
                                    hero.Dies(hero, hero.Packet);
                                    hero.Attacking = false;
                                }
                                else
                                {
                                    hero.Attacked.Hitpoints -= Damage;
                                    Sync.HP(hero);
                                }
                                break;
                            }
                        }

                        break;
                    }
            }
        }
    }

}
