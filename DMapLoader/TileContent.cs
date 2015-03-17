using System;
using System.Drawing;
namespace DMapLoader
{
	public class TileContent
	{
		private Content _tileContent;
		private Point _point;
		private DMapTileAll _tile;
		private int _distance;
		private int _score;
		private bool _isPath;
		public Point Cordinates
		{
			get
			{
				return this._point;
			}
			set
			{
				this._point = value;
			}
		}
		public Content TileContents
		{
			get
			{
				return this._tileContent;
			}
			set
			{
				this._tileContent = value;
			}
		}
		public int Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				this._distance = value;
			}
		}
		public int Score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}
		public bool IsPath
		{
			get
			{
				return this._isPath;
			}
			set
			{
				this._isPath = value;
			}
		}
		public void FromTile(DMapTileAll Tile, Content Force = Content.None)
		{
			if (Force != Content.None)
			{
				this._tileContent = Force;
				return;
			}
			if (!Tile.CanWalk)
			{
				this._tileContent = Content.Impassable;
			}
		}
	}
}
