using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlHandler;

namespace ConquerServer_Basic
{
    class Accounts
    {
        static public bool CheckPass(string ClientName, string Password)
        {
            string DbPass = "";
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("Accounts").Where("Username", ClientName);
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                DbPass = r.ReadString("password");
            }
            if (DbPass == "")
            {
                MySqlCommand cmd2 = new MySqlCommand(MySqlCommandType.UPDATE);
                cmd2.Update("Accounts");
                cmd2.Set("Password", Password);
                cmd2.Where("Username", ClientName);
                cmd2.Execute();
                return true;
            }
            if (DbPass == Password)
                return true;
            else
                return false;
        }
        static public uint PullUID(string Username)
        {
            uint UID = 0;
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("accounts").Where("Username", Username);
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                UID = r.ReadUInt32("EntityID");
            }
            return UID;
        }
        static public void NewAcc(string acc, string email, sbyte staff)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.INSERT);
            cmd.Insert("accounts");
            cmd.Insert("username", acc);
            cmd.Insert("email", email);
            cmd.Execute();
            MySqlCommand cmd2 = new MySqlCommand(MySqlCommandType.INSERT);
            cmd2.Insert("characters");
            cmd2.Insert("account", acc);
            cmd2.Insert("staff", staff);
            cmd2.Execute();
        }
        static public List<string> GetAccList()
        {
            List<string> accList = new List<string>();
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("accounts");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                accList.Add(r.ReadString("Username"));
            }
            return accList;
        }
    }
}
