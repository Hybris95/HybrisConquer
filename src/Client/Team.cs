using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    // Thanks Ranny, but your implementation wasn't properly done
    // For instance some of the packet strings (messages)
    // Shouldn't nee to be sent manually :-)
    public class PlayerTeam
    {
        public bool ForbidTeam;
        public bool PickupGold;
        public bool PickupItems;

        private Dictionary<uint, GameClient> m_Team;
        private GameClient[] m_Teammates;

        public bool TeamLeader;
        public bool Active;
        public bool Full
        {
            get
            {
                if (Teammates != null)
                    return (m_Team.Count == 5);
                return false;
            }
        }
        public PlayerTeam()
        {
            m_Team = new Dictionary<uint, GameClient>(5);
            TeamLeader = false;
            Active = false;
        }
        public GameClient[] Teammates
        {
            get
            {
                return m_Teammates;
            }
        }
        public void Add(GameClient Teammate)
        {
            if (m_Team.ContainsKey(Teammate.Identifier))
                m_Team[Teammate.Identifier] = Teammate;
            else
                m_Team.ThreadSafeAdd<uint, GameClient>(Teammate.Identifier, Teammate);
            m_Teammates = m_Team.ThreadSafeValueArray<uint, GameClient>();
        }
        public void Remove(uint UID)
        {
            m_Team.ThreadSafeRemove<uint, GameClient>(UID);
            m_Teammates = m_Team.ThreadSafeValueArray<uint, GameClient>();
        }
        public bool IsTeammate(uint UID)
        {
            return m_Team.ContainsKey(UID);
        }
    }
}