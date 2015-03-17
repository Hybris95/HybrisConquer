using System;
namespace DMapLoader
{
	[Flags]
	public enum DmapMode
	{
		Flash = 1,
		CanPK = 2,
		NewbieMap = 4,
		BlackNameEvents = 8,
		None = 0
	}
}
