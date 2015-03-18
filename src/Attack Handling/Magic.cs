using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Networking.Packets;
using ConquerServer_Basic.Interfaces;
using ConquerServer_Basic.Main_Classes;

namespace ConquerServer_Basic.Attack_Handling
{
    public class Magic
    {
        static public void Attack(GameClient Hero)
        {
            AttackPacket atkPacket = new AttackPacket(true);
            atkPacket.Deserialize(Hero.Packet);

            ISkill spell;
            Dictionary<ushort, Skill> skills;
            Skill skill = new Skill();
            if (!Hero.Spells.TryGetValue(Hero.SkillID, out spell))
                return;
            if (!Kernel.Skills.TryGetValue(Hero.SkillID, out skills))
                return;
            foreach (KeyValuePair<ushort, Skill> DE in skills)
            {
                Skill info = DE.Value;
                if (info.ID == Hero.SkillID && info.Level == spell.Level)
                {
                    skill = info;
                    break;
                }
            }

            uint Damage = 0;
            Dictionary<Entity, uint> _Targets = new Dictionary<Entity, uint>();

            Hero.Entity.Action = ConquerAction.None;

            if (Hero.Mana < skill.Mana || Hero.Stamina < skill.Stamina)
            {
                return;
            }
            else
            {
                Hero.Mana -= skill.Mana;
                Hero.Stamina -= skill.Stamina;
                switch (Hero.Attacked.EntityFlag)
                {
                    case EntityFlag.Player:
                        {
                            Hero.Attacking = true;

                            switch (skill.Type)
                            {
                                case SpellType.Cure:
                                    {
                                        if (Hero.Attacked.Hitpoints != Hero.Attacked.MaxHitpoints)
                                        {
                                            Hero.Health((UInt16)skill.BaseDamage);
                                        }
                                        else { Hero.Send(new MessagePacket("Targets health is full", 0xFFFFFF, 2005)); }
                                        Sync.HP(Hero.Attacked.GetClient(Hero.Attacked));

                                        Damage = (uint)skill.BaseDamage;
                                        _Targets.Add(Hero.Attacked, Damage);

                                        break;
                                    }
                                case SpellType.Revive:
                                    {
                                        PacketProcessor.Revive(Hero.Attacked.GetClient(Hero.Attacked), true);

                                        _Targets.Add(Hero.Attacked, 0);

                                        break;
                                    }
                                case SpellType.Disguise:
                                    {
                                        // TODO - Transform the Hero with the corresponding model
                                        Console.WriteLine("{0}({1}) Type = {2}", skill.Name, skill.ID, skill.Type);
                                        _Targets.Add(Hero.Attacked, 0);
                                        break;
                                    }
                                case SpellType.Line:
                                    {
                                        // TODO - Code the line targets depending on the skill id/level
                                        Console.WriteLine("{0}({1}) Type = {2}", skill.Name, skill.ID, skill.Type);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("{0}({1}) Type = {2}", skill.Name, skill.ID, skill.Type);
                                    break;
                            }
                            break;
                        }
                }
            }
        }

       
    }

}
