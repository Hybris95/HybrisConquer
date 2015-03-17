using System;
namespace DMapLoader
{
	internal class BitArray
	{
		public byte Store;
		public void Set(BitValues Location, bool Value)
		{
			if (Value)
			{
				this.Store |= (byte)Location;
			}
		}
		public bool Check(BitValues Location)
		{
			return (this.Store & (byte)Location) == (byte)Location;
		}
	}
}
