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
            StanderdItemStats itemStats = null;
            if (Kernel.ItemsStats.TryGetValue(Item.ID, out itemStats))
            {
                if (itemStats.Action != 0)
                {
                    switch (itemStats.Action)
                    {
                        case 13021:// TwinCityGate
                        {
                            // TODO - Can the character get teleported ?
                            Client.Teleport(1002, 430, 380);
                            return true;
                        }
                        case 13022:// DesertCityGate
                        {
                            // TODO - Can the character get teleported ?
                            Client.Teleport(1000, 500, 650);
                            return true;
                        }
                        case 13023:// ApeCityGate
                        {
                            // TODO - Can the character get teleported ?
                            Client.Teleport(1020, 566, 565);
                            return true;
                        }
                        case 13024:// CastleGate
                        {
                            // TODO - Can the character get teleported ?
                            Client.Teleport(1011, 193, 266);
                            return true;
                        }
                        case 13025:// BirdIslandGate
                        {
                            // TODO - Can the character get teleported ?
                            Client.Teleport(1015, 717, 577);
                            return true;
                        }
                        default:
                        {
                            Console.WriteLine("ActionID : {0} is not managed!", itemStats.Action);
                            break;
                        }
                    }
                }
                else if (itemStats.Description == "SkillBook")
                {
                    // TODO - Make a Dictionary (or a database table) to store those associations
                    ushort skillID = 0;
                    switch (itemStats.ItemID)
                    {
                        case 725000:// Thunder
                        {
                            skillID = 1000; break;
                        }
                        case 725001:// Fire
                        {
                            skillID = 1001; break;
                        }
                        case 725002:// Tornado
                        {
                            skillID = 1002; break;
                        }
                        case 725003:// Cure
                        {
                            skillID = 1005; break;
                        }
                        case 725004:// Lightning
                        {
                            skillID = 1010; break;
                        }
                        case 725005:// FastBlade
                        {
                            skillID = 1045; break;
                        }
                        case 725010:// ScentSword
                        {
                            skillID = 1046; break;
                        }
                        case 725011:// WideStrike
                        {
                            skillID = 1250; break;
                        }
                        case 725012:// SpeedGun
                        {
                            skillID = 1260; break;
                        }
                        case 725013:// Penetration
                        {
                            skillID = 1290; break;
                        }
                        case 725014:// Halt
                        {
                            skillID = 1300; break;
                        }
                        case 725015:// DivineHare
                        {
                            skillID = 1350; break;
                        }
                        case 725016:// NightDevil
                        {
                            skillID = 1360; break;
                        }
                        case 725018:// Dance2
                        {
                            skillID = 1380; break;
                        }
                        case 725019:// Dance3
                        {
                            skillID = 1385; break;
                        }
                        case 725020:// Dance4
                        {
                            skillID = 1390; break;
                        }
                        case 725021:// Dance5
                        {
                            skillID = 1395; break;
                        }
                        case 725022:// Dance6
                        {
                            skillID = 1400; break;
                        }
                        case 725023:// Dance7
                        {
                            skillID = 1405; break;
                        }
                        case 725024:// Dance8
                        {
                            skillID = 1410; break;
                        }
                        case 725025:// FlyingMoon
                        {
                            skillID = 1320; break;
                        }
                        case 725026:// Snow
                        {
                            skillID = 5010; break;
                        }
                        case 725027:// StrandedMonster
                        {
                            skillID = 5020; break;
                        }
                        case 725028:// SpeedLightning
                        {
                            skillID = 5001; break;
                        }
                        case 725029:// Phoenix
                        {
                            skillID = 5030; break;
                        }
                        case 725030:// Boom
                        {
                            skillID = 5040; break;
                        }
                        case 725031:// Boreas
                        {
                            skillID = 5050; break;
                        }
                        case 725040:// Seizer
                        {
                            skillID = 7000; break;
                        }
                        case 725041:// Earthquake
                        {
                            skillID = 7010; break;
                        }
                        case 725042:// Rage
                        {
                            skillID = 7020; break;
                        }
                        case 725043:// Celestial
                        {
                            skillID = 7030; break;
                        }
                        case 725044:// Roamer
                        {
                            skillID = 7040; break;
                        }
                        default:
                        {
                            Console.WriteLine("Unmanaged SkillBook : {0}", itemStats.ItemID);
                            break;
                        }

                    }
                    ISkill skillLearnt = null;
                    if (skillID != 0 && !Client.Spells.TryGetValue(skillID, out skillLearnt))
                    {
                        // TODO - Check if the character can learn it (especially for spells)
                        Client.LearnSpell(skillID, 0);
                    }
                    return true;
                }
                else
                {
                    switch (Item.ID)
                    {
                        default:
                            {
                                // TODO - Say what item can't be used (if not an equipement)
                                break;
                            }
                    }
                }
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
                    if (EquipItem.ID > 0)
                    {
                        switch (EquipSlot)
                        {
                            case 0:// HeadGears and Others
                                Client.Entity.HeadGear = EquipItem.ID;
                                break;
                            case 3:
                                Client.Entity.Armor = EquipItem.ID;
                                break;
                            case 4:
                                Client.Entity.LeftArm = EquipItem.ID;
                                break;
                            case 5:
                                Client.Entity.MainHand = EquipItem.ID;
                                break;
                            /*case 6:
                                Client.Entity.Ring = EquipItem.ID;
                                break;*/
                            /*case 8:
                                Client.Entity.Boots = EquipItem.ID;
                                break;*/
                            case 9:
                                Client.Entity.Armor = EquipItem.ID;
                                break;
                            default:
                                Console.Error.WriteLine("TODO - " + EquipItem.ID + " tryed to be equipped on slot : " + EquipSlot);
                                break;
                        }
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
