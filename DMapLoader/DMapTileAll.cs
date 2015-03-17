using System;
namespace DMapLoader
{
	public struct DMapTileAll
	{
		private bool canWalk;
		private byte height;
		public bool CanWalk
		{
			get
			{
				return this.canWalk;
			}
			set
			{
				this.canWalk = value;
			}
		}
		public byte Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}
	}
}
