using System;
using System.Drawing;
namespace DMapLoader
{
	public struct ScenePart
	{
		public string Animation;
		public string PartFile;
		public Point Offset;
		public int aniInterval;
		public Size Size;
		public int Thickness;
		public Point StartPosition;
		public bool[,] NoAccess;
	}
}
