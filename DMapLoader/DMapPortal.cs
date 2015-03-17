using System;
namespace DMapLoader
{
	public struct DMapPortal
	{
		private ushort xCord;
		private ushort yCord;
		public int MapID;
		public ushort XCord
		{
			get
			{
				return this.xCord;
			}
			set
			{
				this.xCord = value;
			}
		}
		public ushort YCord
		{
			get
			{
				return this.yCord;
			}
			set
			{
				this.yCord = value;
			}
		}
	}
}
