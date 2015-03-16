using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic
{
    public class Screen
    {
        private Dictionary<uint, IMapObject> ScreenDictionary;
        private IMapObject[] m_Screen;
        private GameClient Client;

        public Screen(GameClient _Client)
        {
            Client = _Client;
            ScreenDictionary = new Dictionary<uint, IMapObject>(20);
            m_Screen = new IMapObject[0];
        }
        public IMapObject[] Objects
        {
            get
            {
                return m_Screen;
            }
        }
        public bool Add(IMapObject Base)
        {
            lock (ScreenDictionary)
            {
                if (!ScreenDictionary.ContainsKey(Base.UID))
                {
                    ScreenDictionary.Add(Base.UID, Base);
                    m_Screen = new IMapObject[ScreenDictionary.Count];
                    ScreenDictionary.Values.CopyTo(m_Screen, 0);
                    return true;
                }
            }
            return false;
        }
        public void Cleanup()
        {
            bool remove;
            foreach (IMapObject Base in m_Screen)
            {
                remove = false;
                if (Base.MapObjType == MapObjectType.Monster ||
                    Base.MapObjType == MapObjectType.SOB ||
                    Base.MapObjType == MapObjectType.Pet)
                {
                    remove = (Base as IBaseEntity).Dead ||
                        (Kernel.GetDistance(Client.Entity.X, Client.Entity.Y, Base.X, Base.Y) >= 16);
                }
                else if (Base.MapObjType == MapObjectType.Player)
                {
                    if (remove = (Kernel.GetDistance(Client.Entity.X, Client.Entity.Y, Base.X, Base.Y) >= 16))
                    {
                        GameClient pPlayer = Base.Owner as GameClient;
                        lock (pPlayer.Screen.ScreenDictionary)
                        {
                            pPlayer.Screen.ScreenDictionary.Remove(Client.Identifier);
                        }
                    }
                }
                else
                {
                    remove = (Kernel.GetDistance(Client.Entity.X, Client.Entity.Y, Base.X, Base.Y) >= 16);
                }

                if (remove)
                {
                    lock (ScreenDictionary)
                    {
                        ScreenDictionary.Remove(Base.UID);
                    }
                }
            }
        }
        public void FullWipe()
        {
            lock (ScreenDictionary)
            {
                ScreenDictionary.Clear();
                m_Screen = new IMapObject[0];
            }
        }
        public void Reload(bool Clear, ConquerCallback Callback)
        {
            lock (ScreenDictionary)
            {
                if (Clear)
                    ScreenDictionary.Clear();
                else
                    Cleanup();
                foreach (GameClient pClient in Kernel.Clients)
                {
                    if (pClient.Identifier != Client.Identifier)
                    {
                        if (pClient.Entity.MapID == Client.Entity.MapID)
                        {
                            if (Kernel.GetDistance(pClient.Entity.X, pClient.Entity.Y, Client.Entity.X, Client.Entity.Y) <= 16)
                            {
                                pClient.Entity.SendSpawn(Client);
                                if (Callback != null)
                                    Callback.Invoke(Client.Entity, pClient.Entity);
                            }
                        }
                    }
                }
                foreach (Entity mob in Kernel.eMonsters.Values)
                {
                    if (mob.MapID == Client.Entity.MapID)
                        if (Kernel.GetDistance(mob.X, mob.Y, Client.Entity.X, Client.Entity.Y) <= 24)
                        {
                            if (!mob.Dead)
                                mob.SendSpawn(Client);
                        }
                }
                Dictionary<uint, INpc> Npcs;
                if (Kernel.Npcs.TryGetValue(Client.Entity.MapID, out Npcs))
                {
                    foreach (KeyValuePair<uint, INpc> DE in Npcs)
                    {
                        INpc npc = DE.Value;
                        if (Kernel.GetDistance(npc.X, npc.Y, Client.Entity.X, Client.Entity.Y) <= 16)
                        {
                            npc.SendSpawn(Client);
                        }
                    }
                }
                m_Screen = new IMapObject[ScreenDictionary.Count];
                ScreenDictionary.Values.CopyTo(m_Screen, 0);
            }
        }
        public IMapObject FindObject(uint UID)
        {
            IMapObject res;
            ScreenDictionary.TryGetValue(UID, out res);
            return res;
        }
    }
}
