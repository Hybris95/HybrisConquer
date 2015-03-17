using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConquerServer_Basic.Interfaces
{
    public interface IShop
    {
        uint ID { get; set; }
        string Name { get; set; }
        ushort Type { get; set; }
        ushort MoneyType { get; set; }
        ushort ItemAmount { get; set; }
        uint[] Items { get; set; }
    }

    public class Shop : IShop
    {
        private uint _ID;
        private string _Name;
        private ushort _Type;
        private ushort _MoneyType;
        private ushort _ItemAmount;
        private uint[] _Items;

        public uint ID { get { return this._ID; } set { this._ID = value; } }
        public string Name { get { return this._Name; } set { this._Name = value; } }
        public ushort Type { get { return this._Type; } set { this._Type = value; } }
        public ushort MoneyType { get { return this._MoneyType; } set { this._MoneyType = value; } }
        public ushort ItemAmount { get { return this._ItemAmount; } set { this._ItemAmount = value; } }
        public uint[] Items { get { return this._Items; } set { this._Items = value; } }
    }
}
