using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Networking.Packets;


namespace ConquerServer_Basic.Attack_Handling
{
    public class AttackHandler
    {
        static public void Handle(GameClient Hero, byte[] Packet)
        {
            AttackPacket cPacket = new AttackPacket(true);
            cPacket.Deserialize(Packet);
            
            switch (cPacket.AttackType)
            {
                case (ushort)AttackType.Magic:
                {
                    ushort SkillId = Convert.ToUInt16(((long)Packet[24] & 0xFF) | (((long)Packet[25] & 0xFF) << 8));
                    SkillId ^= (ushort)0x915d;
                    SkillId ^= (ushort)Hero.Entity.UID;
                    SkillId = (ushort)(SkillId << 0x3 | SkillId >> 0xd);
                    SkillId -= 0xeb42;

                    long x = (Packet[16] & 0xFF) | ((Packet[17] & 0xFF) << 8);
                    long y = (Packet[18] & 0xFF) | ((Packet[19] & 0xFF) << 8);

                    x = x ^ (uint)(Hero.Entity.UID & 0xffff) ^ 0x2ed6;
                    x = ((x << 1) | ((x & 0x8000) >> 15)) & 0xffff;
                    x |= 0xffff0000;
                    x -= 0xffff22ee;

                    y = y ^ (uint)(Hero.Entity.UID & 0xffff) ^ 0xb99b;
                    y = ((y << 5) | ((y & 0xF800) >> 11)) & 0xffff;
                    y |= 0xffff0000;
                    y -= 0xffff8922;

                    uint Target = ((uint)Packet[12] & 0xFF) | (((uint)Packet[13] & 0xFF) << 8) | (((uint)Packet[14] & 0xFF) << 16) | (((uint)Packet[15] & 0xFF) << 24);
                    Target = ((((Target & 0xffffe000) >> 13) | ((Target & 0x1fff) << 19)) ^ 0x5F2D2463 ^ Hero.Entity.UID) - 0x746F4AE6;

                    cPacket.AttackedUID = Target;
                    cPacket.AttackedX = (ushort)x;
                    cPacket.AttackedY = (ushort)y;
                    Hero.SkillID = SkillId;

                    // TODO - Optimize here if there is no target
                    foreach (GameClient Attacked in Kernel.Clients)
                    {
                        if (Attacked.Entity.UID == Target)
                        {
                            Hero.Attacked = Attacked.Entity;
                            break;
                        }
                    }
                    Hero.AtkType = (AttackType)cPacket.AttackType;
                    Hero.Packet = Packet;
                    Magic.Attack(Hero);
                    break;
                }
                case (ushort)AttackType.Archer:
                case (ushort)AttackType.Physical:
                {
                    if (cPacket.AttackType == (ushort)EntityFlag.Monster)
                    {
                        // TODO - Optimize here if there is no target
                        foreach (Entity Attacked in Kernel.eMonsters.Values)
                        {
                            if (Attacked.UID == cPacket.AttackedUID)
                            {
                                Hero.Attacked = Attacked;
                            }
                        }
                        Hero.AtkType = (AttackType)cPacket.AttackType;
                        Hero.Packet = Packet;
                        Physical.Attack(Hero);
                    }
                    else
                    {
                        // TODO - Optimize here if there is no target
                        foreach (GameClient Attacked in Kernel.Clients)
                        {
                            if (Attacked.Entity.UID == cPacket.AttackedUID)
                            {
                                Hero.Attacked = Attacked.Entity;
                                break;
                            }
                        }
                        Hero.AtkType = (AttackType)cPacket.AttackType;
                        Hero.Packet = Packet;
                        Physical.Attack(Hero);
                    }
                    break;
                }
                default:
                    Console.WriteLine("[AttackHandler.Handle()] Unmanaged AttackType : {0}", cPacket.AttackType);
                    break;
            }
        }
    }

}
