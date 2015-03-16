using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConquerServer;

namespace ConquerServer_Basic.Networking.Packet_Handling
{
    class Teams
    {
        static public void AcceptInviteToJoinTeam(TeamPacket team, GameClient client)
        {
            if (client.Team == null && !client.Entity.Dead)
            {
                GameClient Leader;
                if (Kernel.GamePool.TryGetValue(team.UID, out Leader))
                {
                    if (Leader.Team != null)
                    {
                        if (Leader.Team.Full || Leader.Team.ForbidTeam)
                            return;

                        client.Team = new PlayerTeam();

                        AddToTeamPacket AddYou = new AddToTeamPacket();
                        AddToTeamPacket AddMe = new AddToTeamPacket();
                        AddMe.Name = client.Entity.Name;
                        AddMe.MaxHitpoints = (ushort)client.Entity.MaxHitpoints;
                        AddMe.Hitpoints = (ushort)client.Entity.Hitpoints;
                        AddMe.Mesh = client.Entity.Model;
                        AddMe.UID = client.Entity.UID;
                        foreach (GameClient Teammate in Leader.Team.Teammates)
                        {
                            if (Teammate != null)
                            {
                                Teammate.Send(AddMe);
                                client.Team.Add(Teammate);
                                AddYou.Name = Teammate.Entity.Name;
                                AddYou.MaxHitpoints = (ushort)Teammate.Entity.MaxHitpoints;
                                AddYou.Hitpoints = (ushort)Teammate.Entity.Hitpoints;
                                AddYou.Mesh = Teammate.Entity.Model;
                                AddYou.UID = Teammate.Entity.UID;
                                client.Send(AddYou);
                                if (Teammate.Entity.UID != Leader.Entity.UID)
                                    Teammate.Team.Add(client);
                            }
                        }
                        Leader.Team.Add(client);
                        client.Team.Add(client);
                        client.Team.Active = true;
                        client.Team.TeamLeader = false;
                        client.Send(AddMe);
                    }
                }
            }
        }
        static public void SendInviteToJoinTeam(TeamPacket team, GameClient client)
        {
            if (client.Team != null)
            {
                if (!client.Team.Full && client.Team.TeamLeader)
                {
                    GameClient Invitee;
                    if (Kernel.GamePool.TryGetValue(team.UID, out Invitee))
                    {
                        if (Invitee.Team == null)
                        {
                            team.UID = client.Entity.UID;
                            Invitee.Send(team);
                        }
                        else
                        {
                            client.Send(new MessagePacket(Invitee.Entity.Name + " is already in a team.", (uint)(uint)Color.White, (uint)(uint)ChatType.Top));
                        }
                    }
                }
            }
        }
        static public void AcceptRequestToJoinTeam(TeamPacket team, GameClient client)
        {
            if (client.Team != null && !client.Entity.Dead)
            {
                if (!client.Team.Full && client.Team.TeamLeader && !client.Team.ForbidTeam)
                {
                    GameClient NewTeammate;
                    if (Kernel.GamePool.TryGetValue(team.UID, out NewTeammate))
                    {
                        if (NewTeammate.Team != null)
                            return;

                        NewTeammate.Team = new PlayerTeam();

                        AddToTeamPacket AddMe = new AddToTeamPacket();
                        AddToTeamPacket AddYou = new AddToTeamPacket();
                        AddYou.Name = NewTeammate.Entity.Name;
                        AddYou.MaxHitpoints = (ushort)NewTeammate.Entity.MaxHitpoints;
                        AddYou.Hitpoints = (ushort)NewTeammate.Entity.Hitpoints;
                        AddYou.Mesh = NewTeammate.Entity.Model;
                        AddYou.UID = NewTeammate.Entity.UID;
                        //lock (client.Team.Teammates)
                        {
                            foreach (GameClient Teammate in client.Team.Teammates)
                            {
                                if (Teammate != null)
                                {
                                    Teammate.Send(AddYou);
                                    NewTeammate.Team.Add(Teammate);
                                    AddMe.Name = Teammate.Entity.Name;
                                    AddMe.MaxHitpoints = (ushort)Teammate.Entity.MaxHitpoints;
                                    AddMe.Hitpoints = (ushort)Teammate.Entity.Hitpoints;
                                    AddMe.Mesh = Teammate.Entity.Model;
                                    AddMe.UID = Teammate.Entity.UID;
                                    NewTeammate.Send(AddMe);
                                    if (Teammate.Entity.UID != client.Entity.UID)
                                        Teammate.Team.Add(NewTeammate);
                                }
                            }

                            client.Team.Add(NewTeammate);
                            NewTeammate.Team.Add(NewTeammate);
                            NewTeammate.Team.Active = true;
                            NewTeammate.Team.TeamLeader = false;
                            client.Send(AddYou);
                            NewTeammate.Send(AddYou);
                        }
                    }
                }
            }
        }
        static public void SendRequestJoinToTeam(TeamPacket team, GameClient client)
        {
            if (client.Team == null && !client.Entity.Dead)
            {
                GameClient Leader;
                if (Kernel.GamePool.TryGetValue(team.UID, out Leader))
                {
                    if (Leader.Team != null)
                    {
                        if (Leader.Team.TeamLeader && !Leader.Team.Full)
                        {
                            team.UID = client.Entity.UID;
                            Leader.Send(team);
                        }
                        else
                        {
                            client.Send(new MessagePacket(Leader.Entity.Name + "'s team is already full.", (uint)(uint)Color.White, (uint)(uint)ChatType.Top));
                        }
                    }
                    else
                    {
                        client.Send(new MessagePacket(Leader.Entity.Name + " doesn't have a team.", (uint)(uint)Color.White, (uint)(uint)ChatType.Top));
                    }
                }
            }
        }
        static public void LeaveTeam(TeamPacket team, GameClient client)
        {
            if (client.Team != null)
            {
                if (!client.Team.TeamLeader)
                {
                    lock (client.Team.Teammates)
                    {
                        foreach (GameClient Teammate in client.Team.Teammates)
                        {
                            if (Teammate != null)
                            {
                                if (Teammate.Entity.UID != client.Entity.UID)
                                {
                                    Teammate.Send(team);
                                    Teammate.Team.Remove(client.Entity.UID);
                                }
                            }
                        }
                    }
                    client.Send(team);
                    client.Team = null;
                }
            }
        }
        static public void KickFromTeam(TeamPacket team, GameClient client)
        {
            if (client.Team != null)
            {
                if (client.Team.TeamLeader)
                {
                    GameClient Teammate; // The guy we're kicking out
                    if (Kernel.GamePool.TryGetValue(team.UID, out Teammate))
                    {
                        if (Teammate.Team != null)
                        {
                            if (Teammate.Team.IsTeammate(client.Entity.UID))
                            {
                                LeaveTeam(team, Teammate);
                            }
                        }
                    }
                }
            }
        }
        static public void DismissTeam(TeamPacket team, GameClient client)
        {
            if (client.Team != null)
            {
                if (!client.Entity.Dead && client.Team.TeamLeader)
                {
                    lock (client.Team.Teammates)
                    {
                        foreach (GameClient Teammate in client.Team.Teammates)
                        {
                            if (Teammate != null)
                            {
                                if (Teammate.Entity.UID != client.Entity.UID)
                                {
                                    Teammate.Send(team);
                                    Teammate.Team = null;
                                }
                            }
                        }
                    }
                    client.Send(team);
                    client.Team = null;

                    client.Entity.StatusFlag &= ~SyncPacket.Flags.TeamLeader;

                    SyncPacket status = new SyncPacket(1);
                    status.UID = client.Identifier;
                    status[0] = SyncPacket.Data.Create(SyncPacket.RaiseFlag, client.Entity.StatusFlag);
                    client.SendScreen(status, true);
                }
            }
        }
        static public void CreateTeam(TeamPacket team, GameClient client)
        {
            if (!client.Entity.Dead && client.Team == null)
            {
                client.Team = new PlayerTeam();
                client.Team.Active = true;
                client.Team.TeamLeader = true;
                client.Team.Add(client);
                client.Send(team);
                client.Entity.StatusFlag |= SyncPacket.Flags.TeamLeader;
                SyncPacket status = new SyncPacket(1);
                status.UID = client.Identifier;
                status[0] = SyncPacket.Data.Create(SyncPacket.RaiseFlag, client.Entity.StatusFlag);
                client.SendScreen(status, true);
            }
        }
    }
}
