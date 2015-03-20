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
                            if (Client.CanTeleport())
                            {
                                Client.Teleport(1002, 430, 380);
                                // TODO - Delete the item
                            }
                            return true;
                        }
                        case 13022:// DesertCityGate
                        {
                            if (Client.CanTeleport())
                            {
                                Client.Teleport(1000, 500, 650);
                                // TODO - Delete the item
                            }
                            return true;
                        }
                        case 13023:// ApeCityGate
                        {
                            if (Client.CanTeleport())
                            {
                                Client.Teleport(1020, 566, 565);
                                // TODO - Delete the item
                            }
                            return true;
                        }
                        case 13024:// CastleGate
                        {
                            if (Client.CanTeleport())
                            {
                                Client.Teleport(1011, 193, 266);
                                // TODO - Delete the item
                            }
                            return true;
                        }
                        case 13025:// BirdIslandGate
                        {
                            if (Client.CanTeleport())
                            {
                                Client.Teleport(1015, 717, 577);
                                // TODO - Delete the item
                            }
                            return true;
                        }
                        default:
                        {
                            Console.WriteLine("[UseItem()] ActionID : {0} is not managed!", itemStats.Action);
                            break;
                        }
                    }
                }
                else if (itemStats.Description == "SkillBook")
                {
                    ItemSkill itemSkill = null;
                    if (Kernel.ItemsSkills.TryGetValue(itemStats.ItemID, out itemSkill))
                    {
                        ushort skillID = itemSkill.SkillID;
                        ISkill skillLearnt = null;
                        if (itemSkill.SkillID != 0 && !Client.Spells.TryGetValue(itemSkill.SkillID, out skillLearnt))
                        {
                            // TODO - Check if the character can learn it (especially for spells)
                            if (Client.LearnSpell(itemSkill.SkillID, 0))
                            {
                                // TODO - Delete the item
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("[UseItem()] Unmanaged SkillBook : {0}", itemStats.ItemID);
                        return false;
                    }
                    return true;
                }
                else
                {
                    switch (Item.ID)
                    {
                        default:
                            {
                                Console.WriteLine("[UseItem()] Unmanaged Item : {0}", Item.ID);
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
                ushort EquipSlot = (ushort)Packet.dwParam;
                switch (EquipSlot)
                {
                    case 0:
                    {
                        UseItem(Client, EquipItem);
                        return;
                    }
                    default:
                    {
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
                                case 1:
                                    Client.Entity.HeadGear = EquipItem.ID;
                                    break;
                                /*case 2:
                                    Client.Entity.Necklace = EquipItem.ID;
                                    break;*/
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
                                /*case 7:
                                    Client.Entity.Talisman = EquiItem.ID;
                                    break;*/
                                /*case 8:
                                    Client.Entity.Boots = EquipItem.ID;
                                    break;*/
                                /*case 9:
                                    Client.Entity.Garment = EquipItem.ID;
                                    break;*/
                                default:
                                    Console.Error.WriteLine("[EquipGear()] Unmanaged EquipSlot[{0}] for ItemID[{1}]", EquipSlot, EquipItem.ID);
                                    break;
                            }
                            Client.RemoveInventory(EquipItem.UID);
                            NewMath.Vitals(Client);
                            Client.CalculateDamageBoost();
                            Client.SendStatMessage();
                            Client.SendScreen(Client.Entity.SpawnPacket, true);
                            Client.Screen.Reload(true, null);
                        }
                        break;
                    }
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
                switch (EquipSlot)
                {
                    /*case 1:
                        Client.Entity.HeadGear = 0;
                        break;*/
                    /*case 2:
                        Client.Entity.Necklace = 0;
                        break;*/
                    case 3:
                        Client.Entity.Armor = 0;
                        break;
                    case 4:
                        Client.Entity.LeftArm = 0;
                        break;
                    case 5:
                        Client.Entity.MainHand = 0;
                        break;
                    /*case 6:
                        Client.Entity.Ring = 0;
                        break;*/
                    /*case 7:
                        Client.Entity.Talisman = 0;
                        break;*/
                    /*case 8:
                        Client.Entity.Boots = 0;
                        break;*/
                    /*case 9:
                        Client.Entity.Garment = 0;
                        break;*/
                    default:
                        Console.WriteLine("[UnequipGear()] Unmanaged EquipSlot[{0}]", EquipSlot);
                        break;
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
