using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer_Basic.Main_Classes;
using ConquerServer_Basic.Interfaces;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    class Equipping
    {
        static private bool UseItem(GameClient Client, IConquerItem Item)
        {
            switch (Item.ID)
            {
                case 725000:// Thunder (SkillBook)
                {
                    ISkill skillLearnt = null;
                    if (!Client.Spells.TryGetValue(1000, out skillLearnt))
                    {
                        // TODO - Check if the character can learn it (Tao or Reborn from Tao ?)
                        Client.LearnSpell(1000, 0);
                    }
                    return true;
                }
                case 725005:// FastBlade (SkillBook)
                {
                    ISkill skillLearnt = null;
                    if (!Client.Spells.TryGetValue(1045, out skillLearnt))
                    {
                        Client.LearnSpell(1045, 0);
                    }
                    return true;
                }
                case 725010:// ScentSword (SkillBook)
                {
                    ISkill skillLearnt = null;
                    if (!Client.Spells.TryGetValue(1046, out skillLearnt))
                    {
                        Client.LearnSpell(1046, 0);
                    }
                    return true;
                }
                default:
                    return false;
            }
            return false;
        }

        static public void EquipGear(GameClient Client, ItemUsagePacket Packet)
        {
            IConquerItem EquipItem = Client.GetInventoryItem(Packet.UID);
            if (EquipItem != null)
            {
                if (UseItem(Client, EquipItem))
                {
                    Client.RemoveInventory(EquipItem.UID);
                    return;
                }

                ushort EquipSlot = (ushort)Packet.dwParam;
                if (Client.Equipment.ContainsKey(EquipSlot))
                {
                    if (!Client.Unequip(EquipSlot))
                    {
                        throw new Exception("PacketProcessor::EquipGear() -> Failed to call Client.Unequip()");
                    }
                }
                if (!Client.Equip(EquipItem, EquipSlot))
                {
                    throw new Exception("PacketProcessor::EquipGear() -> Failed to call Client.Equip()");
                }
                else
                {
                    switch (EquipSlot)
                    {
                        case 9:
                            if (EquipItem.ID > 0)
                            {
                                Client.Entity.Armor = EquipItem.ID;
                            }
                            break;
                        case 0:// HeadGears and Others
                            if (EquipItem.ID > 0)
                            {
                                Client.Entity.HeadGear = EquipItem.ID;
                            }
                            break;
                        case 2:
                            if (Client.Entity.Armor == 0)
                            {
                                Client.Entity.Armor = EquipItem.ID;
                            }
                            break;
                        case 4:
                            if (EquipItem.ID > 0)
                            {
                                Client.Entity.LeftArm = EquipItem.ID;
                            }
                            break;
                        case 5:
                            if (EquipItem.ID > 0)
                            {
                                Client.Entity.MainHand = EquipItem.ID;
                            }
                            break;
                        default:
                            Console.Error.WriteLine("TODO - " + EquipItem.ID + " tryed to be equipped on slot : " + EquipSlot);
                            break;
                    }
                    Client.RemoveInventory(EquipItem.UID);
                    NewMath.Vitals(Client);
                    Client.CalculateDamageBoost();
                    Client.SendStatMessage();
                    Client.SendScreen(Client.Entity.SpawnPacket, true);
                    Client.Screen.Reload(true, null);
                }
            }
        }
        static public void UnequipGear(GameClient Client, ItemUsagePacket Packet)
        {
            ushort EquipSlot = (ushort)Packet.dwParam;
            if (Client.Equipment.ContainsKey(EquipSlot))
            {
                if (!Client.Unequip(EquipSlot))
                    throw new Exception("PacketProcessor::UnequipGear() -> Failed to call Client.Unequip()");
                if (EquipSlot == 9)
                {
                    for (int ID = 1; ID < Client.Equipment.Count; ID++)
                    {
                        if (Client.Equipment.ContainsKey((ushort)ID))
                        {
                            if (Client.Equipment[(ushort)ID].ID >= 130203 && Client.Equipment[(ushort)ID].ID <= 139999)
                            {
                                Client.Entity.Armor = Client.Equipment[(ushort)ID].ID;
                            }
                        }
                    }
                }
                if (EquipSlot == 3)
                {
                    for (int ID = 1; ID < Client.Equipment.Count; ID++)
                    {
                        if (Client.Equipment.ContainsKey((ushort)ID))
                        {
                            if (Client.Equipment[(ushort)ID].ID <= 137300 && Client.Equipment[(ushort)ID].ID >= 137970)
                            {
                                Client.Entity.Armor = Client.Equipment[(ushort)ID].ID;
                            }
                        }
                    }
                }
                if (EquipSlot == 5)
                {
                    {
                        Client.Entity.MainHand = 0;
                    }
                }
                if (EquipSlot == 4)
                {
                    {
                        Client.Entity.LeftArm = 0;
                    }
                }
                if (EquipSlot == 0)
                {
                    {
                        Client.Entity.HeadGear = 0;
                    }
                }
                Client.SendScreen(Packet, true);
                NewMath.Vitals(Client);
                Client.CalculateDamageBoost();
                Client.SendStatMessage();
                Client.SendScreen(Client.Entity.SpawnPacket, true);
                Client.Screen.Reload(true, null);
            }
        }
    }
}
