using System;
namespace DMapLoader
{
	[Flags]
	internal enum BitValues
	{
		aFirst = 1,
		aSecond = 2,
		aThird = 4,
		aFourth = 8,
		aFifth = 16,
		aSixth = 32,
		aSeventh = 64,
		aEighth = 128,
		hFirst = 3,
		hSecond = 12,
		hThird = 48,
		hFourth = 192,
		None = 0
	}
}
