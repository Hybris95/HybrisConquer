using System;
using System.Drawing;
namespace DMapLoader
{
	public class DMap
	{
		private DmapMode Mode;
		private DMapTileAll[] tiles;
		private DMapPortal[] portals;
		private SceneFile[] scenes;
		public uint Width;
		public uint Height;
		public ushort MapId;
		public string FileName;
		public DMapPortal[] Portals
		{
			get
			{
				return this.portals;
			}
		}
		public SceneFile[] Scenes
		{
			get
			{
				return this.scenes;
			}
			set
			{
				this.scenes = value;
			}
		}
		public DMap(ushort MapID, string Filename)
		{
			this.MapId = MapID;
			this.FileName = Filename;
		}
		public void PopulateTiles(uint width, uint height)
		{
			this.Width = width;
			this.Height = height;
			this.tiles = new DMapTileAll[this.Width * this.Height];
		}
		public void PopulatePortals(uint amount)
		{
			this.portals = new DMapPortal[amount];
		}
		public void SetPortal(int Position, DMapPortal portal)
		{
			portal.MapID = (int)this.MapId;
			this.portals[Position] = portal;
		}
		public void SetWalk(ushort X, ushort Y, bool Walkable)
		{
			this.tiles[(int)((UIntPtr)((uint)X * this.Width + (uint)Y))].CanWalk = Walkable;
		}
		public void SetHeight(ushort X, ushort Y, ushort Height)
		{
			this.tiles[(int)((UIntPtr)((uint)X * this.Width + (uint)Y))].Height = (byte)Height;
		}
		public ushort GetHeight(int XCord, int YCord)
		{
			return (ushort)this.tiles[(int)checked((IntPtr)unchecked((long)XCord * (long)((ulong)this.Width) + (long)YCord))].Height;
		}
		public bool Check(int XCord, int YCord)
		{
			return this.tiles[(int)checked((IntPtr)unchecked((long)XCord * (long)((ulong)this.Width) + (long)YCord))].CanWalk;
		}
		public TileContent[,] Content(Point Start, Point End)
		{
			int num = Math.Abs(Start.X - End.X);
			int num2 = Math.Abs(Start.Y - End.Y);
			TileContent[,] array = new TileContent[num, num2];
			for (int i = Start.X; i < End.X; i++)
			{
				int y = Start.Y;
				while (i < End.Y)
				{
					array[i, y].FromTile(this.tiles[(int)checked((IntPtr)unchecked((long)i * (long)((ulong)this.Width) + (long)y))], DMapLoader.Content.None);
					i++;
				}
			}
			return array;
		}
	}
}
