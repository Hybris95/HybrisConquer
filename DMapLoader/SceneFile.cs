using System;
using System.Drawing;
namespace DMapLoader
{
	public struct SceneFile
	{
		public string SceneFileName
		{
			get;
			set;
		}
		public Point Location
		{
			get;
			set;
		}
		public ScenePart[] Parts
		{
			get;
			set;
		}
	}
}
