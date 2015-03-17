using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
namespace DMapLoader
{
	public class DMapServer
	{
		private Dictionary<int, DMap> _Maps;
		private Dictionary<int, DynamicMap> _DynamicMaps;
		private Dictionary<string, DMap> _GameMaps;
		private static string _ConquerPath = string.Empty;
		private bool _Dynamic;
		private bool _Height;
		private bool _Loaded;
		private bool _Convert;
		private bool _UseAlternate;
		private bool _Output;
		private DmapLoadMode _Mode;
		private string _sLoading = string.Empty;
		public Dictionary<int, DMap> Maps
		{
			get
			{
				return this._Maps;
			}
		}
		public Dictionary<int, DynamicMap> DynamicMaps
		{
			get
			{
				return this._DynamicMaps;
			}
			set
			{
				this._DynamicMaps = value;
			}
		}
		public string ConquerPath
		{
			get
			{
				return DMapServer._ConquerPath;
			}
			set
			{
				DMapServer._ConquerPath = value;
			}
		}
		public string CurrentlyLoading
		{
			get
			{
				return this._sLoading;
			}
			set
			{
				this._sLoading = value;
			}
		}
		public bool Loaded
		{
			get
			{
				return this._Loaded;
			}
		}
		public bool Output
		{
			set
			{
				this._Output = value;
			}
		}
		public void Load()
		{
			this._Mode = DmapLoadMode.Access;
			this.Start();
		}
		public void Load(bool Height)
		{
			this._Mode = DmapLoadMode.Height;
			this._Height = Height;
			this.Start();
		}
		public void Load(bool Height, bool Dynamic)
		{
			this._Mode = DmapLoadMode.All;
			this._Height = Height;
			this._Dynamic = Dynamic;
			this.Start();
		}
		public void AddDynamicMap(DynamicMap Map)
		{
			this._DynamicMaps.Add(Map.DynamicId, Map);
		}
		public void AddDynamicMap(int DynamicMapID, ushort StaticMapID)
		{
			DynamicMap dynamicMap = new DynamicMap();
			dynamicMap.MapId = StaticMapID;
			this._DynamicMaps.Add(DynamicMapID, dynamicMap);
		}
		public uint TranslateMap(uint MapID)
		{
			if (MapID < 10000u)
			{
				return MapID;
			}
			if (this._DynamicMaps.ContainsKey((int)MapID))
			{
				return (uint)this._DynamicMaps[(int)MapID].MapId;
			}
			return 1002u;
		}
		private void Start()
		{
			TextWriter @out;
			Monitor.Enter(@out = Console.Out);
			try
			{
				if (this._Output)
				{
					Console.Write("[DMapServer] Loading");
				}
				if (!Directory.Exists(DMapServer._ConquerPath + "\\map\\smap"))
				{
					Directory.CreateDirectory(DMapServer._ConquerPath + "\\map\\smap");
					this._Convert = true;
				}
				else
				{
					this._UseAlternate = true;
				}
				new Thread(new ThreadStart(this.LoadDmaps))
				{
					IsBackground = true
				}.Start();
				if (this._Output)
				{
					string currentlyLoading = this.CurrentlyLoading;
					while (!this._Loaded)
					{
						if (string.Compare(currentlyLoading, this.CurrentlyLoading) != 0)
						{
							currentlyLoading = this.CurrentlyLoading;
							Console.Write(" " + currentlyLoading + "              ");
							Console.SetCursorPosition(Console.CursorLeft - (this.CurrentlyLoading.Length + 15), Console.CursorTop);
						}
						Thread.Sleep(10);
					}
					Console.Write(" Complete              \n");
				}
			}
			finally
			{
				Monitor.Exit(@out);
			}
		}
		private void ConvertMap(DMap Map)
		{
			BitArray[] array = new BitArray[Map.Width * Map.Height / 8u + 1u];
			int num = 0;
			int num2 = 0;
			array[num] = new BitArray();
			ushort num3 = 0;
			while ((uint)num3 < Map.Height)
			{
				ushort num4 = 0;
				while ((uint)num4 < Map.Width)
				{
					switch (num2)
					{
					case 0:
						array[num].Set(BitValues.aFirst, Map.Check((int)num4, (int)num3));
						break;
					case 1:
						array[num].Set(BitValues.aSecond, Map.Check((int)num4, (int)num3));
						break;
					case 2:
						array[num].Set(BitValues.aThird, Map.Check((int)num4, (int)num3));
						break;
					case 3:
						array[num].Set(BitValues.aFourth, Map.Check((int)num4, (int)num3));
						break;
					case 4:
						array[num].Set(BitValues.aFifth, Map.Check((int)num4, (int)num3));
						break;
					case 5:
						array[num].Set(BitValues.aSixth, Map.Check((int)num4, (int)num3));
						break;
					case 6:
						array[num].Set(BitValues.aSeventh, Map.Check((int)num4, (int)num3));
						break;
					case 7:
						array[num].Set(BitValues.aEighth, Map.Check((int)num4, (int)num3));
						num2 = -1;
						num++;
						break;
					}
					if (num2 == -1 && array.Length != num)
					{
						array[num] = new BitArray();
					}
					num2++;
					num4 += 1;
				}
				num3 += 1;
			}
			BitArray[] array2 = new BitArray[Map.Width * Map.Height / 4u + 1u];
			num = 0;
			num2 = 0;
			array2[num] = new BitArray();
			ushort num5 = 0;
			while ((uint)num5 < Map.Height)
			{
				ushort num6 = 0;
				while ((uint)num6 < Map.Width)
				{
					int num7 = (int)(Map.GetHeight((int)num6, (int)num5) / 100);
					switch (num2)
					{
					case 0:
						if (num7 == 0)
						{
							array2[num].Set(BitValues.aFirst, false);
						}
						else
						{
							if (num7 == 1)
							{
								array2[num].Set(BitValues.aFirst, true);
							}
							else
							{
								if (num7 == 2)
								{
									array2[num].Set(BitValues.hFirst, true);
								}
							}
						}
						break;
					case 1:
						if (num7 == 0)
						{
							array2[num].Set(BitValues.aThird, false);
						}
						else
						{
							if (num7 == 1)
							{
								array2[num].Set(BitValues.aThird, true);
							}
							else
							{
								if (num7 == 2)
								{
									array2[num].Set(BitValues.hSecond, true);
								}
							}
						}
						break;
					case 2:
						if (num7 == 0)
						{
							array2[num].Set(BitValues.aFifth, false);
						}
						else
						{
							if (num7 == 1)
							{
								array2[num].Set(BitValues.aFifth, true);
							}
							else
							{
								if (num7 == 2)
								{
									array2[num].Set(BitValues.hThird, true);
								}
							}
						}
						break;
					case 3:
						if (num7 == 0)
						{
							array2[num].Set(BitValues.aSeventh, false);
						}
						else
						{
							if (num7 == 1)
							{
								array2[num].Set(BitValues.aSeventh, true);
							}
							else
							{
								if (num7 == 2)
								{
									array2[num].Set(BitValues.hFourth, true);
								}
							}
						}
						num2 = -1;
						num++;
						break;
					}
					if (num2 == -1 && array2.Length != num)
					{
						array2[num] = new BitArray();
					}
					num2++;
					num6 += 1;
				}
				num5 += 1;
			}
			ushort[] array3 = new ushort[Map.Portals.Length * 2];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = Map.Portals[i / 2].XCord;
				array3[i + 1] = Map.Portals[i / 2].YCord;
				i++;
			}
			using (FileStream fileStream = new FileStream(string.Concat(new object[]
			{
				DMapServer._ConquerPath,
				"\\map\\smap\\",
				Map.MapId,
				".smap"
			}), FileMode.Create))
			{
				BinaryWriter binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write((int)Map.MapId);
				binaryWriter.Write(array.Length);
				binaryWriter.Write((int)Map.Width);
				binaryWriter.Write((int)Map.Height);
				for (int j = 0; j < array.Length; j++)
				{
					binaryWriter.Write(array[j].Store);
				}
				binaryWriter.Seek(3, SeekOrigin.Current);
				for (int k = 0; k < array2.Length; k++)
				{
					binaryWriter.Write(array2[k].Store);
				}
				binaryWriter.Seek(4, SeekOrigin.Current);
				binaryWriter.Write(Map.Portals.Length);
				for (int l = 0; l < array3.Length; l++)
				{
					binaryWriter.Write(array3[l]);
				}
				binaryWriter.Close();
			}
		}
		private void LoadDmaps()
		{
			this.CurrentlyLoading = "GameMap.Dat";
			FileStream fileStream = new FileStream(DMapServer._ConquerPath + "\\ini\\GameMap.Dat", FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			int num = binaryReader.ReadInt32();
			this._Maps = new Dictionary<int, DMap>(num);
			this._GameMaps = new Dictionary<string, DMap>(num);
			for (int i = 0; i < num; i++)
			{
				if (this._Dynamic)
				{
					throw new NotImplementedException();
				}
				DMap dMap = new DMap((ushort)binaryReader.ReadUInt32(), Encoding.ASCII.GetString(binaryReader.ReadBytes((int)binaryReader.ReadUInt32())).Remove(0, 8));
				if (this._Maps.ContainsKey((int)dMap.MapId))
				{
					fileStream.Seek(4L, SeekOrigin.Current);
				}
				else
				{
					if (this._GameMaps.ContainsKey(dMap.FileName))
					{
						fileStream.Seek(4L, SeekOrigin.Current);
					}
					else
					{
						this._GameMaps.Add(dMap.FileName, dMap);
						this._Maps.Add((int)dMap.MapId, dMap);
						fileStream.Seek(4L, SeekOrigin.Current);
					}
				}
			}
			fileStream.Dispose();
			fileStream.Close();
			binaryReader.Close();
			if (this._UseAlternate)
			{
				this.PopulateCustomDmaps();
				return;
			}
			this.PopulateDmaps();
		}
		private void PopulateCustomDmaps()
		{
			foreach (DMap current in this._Maps.Values)
			{
				this.CurrentlyLoading = current.FileName;
				if (!File.Exists(string.Concat(new object[]
				{
					DMapServer._ConquerPath,
					"\\map\\smap\\",
					current.MapId,
					".smap"
				})))
				{
					Console.WriteLine(new FileNotFoundException().Message);
				}
				else
				{
					FileStream fileStream;
					try
					{
						fileStream = new FileStream(string.Concat(new object[]
						{
							DMapServer._ConquerPath,
							"\\map\\smap\\",
							current.MapId,
							".smap"
						}), FileMode.Open, FileAccess.Read);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						continue;
					}
					BinaryReader binaryReader = new BinaryReader(fileStream);
					fileStream.Seek(4L, SeekOrigin.Begin);
					int num = binaryReader.ReadInt32();
					int num2 = 0;
					BitArray[] array = new BitArray[num];
					num = 0;
					array[num] = new BitArray();
					current.PopulateTiles((uint)Convert.ToUInt16(binaryReader.ReadUInt32()), (uint)Convert.ToUInt16(binaryReader.ReadUInt32()));
					array[num].Store = binaryReader.ReadByte();
					ushort num3 = 0;
					while ((uint)num3 < current.Height)
					{
						ushort num4 = 0;
						while ((uint)num4 < current.Width)
						{
							switch (num2)
							{
							case 0:
								current.SetWalk(num4, num3, array[num].Check(BitValues.aFirst));
								break;
							case 1:
								current.SetWalk(num4, num3, array[num].Check(BitValues.aSecond));
								break;
							case 2:
								current.SetWalk(num4, num3, array[num].Check(BitValues.aThird));
								break;
							case 3:
								current.SetWalk(num4, num3, array[num].Check(BitValues.aFourth));
								break;
							case 4:
								current.SetWalk(num4, num3, array[num].Check(BitValues.aFifth));
								break;
							case 5:
								current.SetWalk(num4, num3, array[num].Check(BitValues.aSixth));
								break;
							case 6:
								current.SetWalk(num4, num3, array[num].Check(BitValues.aSeventh));
								break;
							case 7:
								current.SetWalk(num4, num3, array[num].Check(BitValues.aEighth));
								num2 = -1;
								num++;
								break;
							}
							if (num2 == -1 && array.Length > num)
							{
								array[num] = new BitArray();
								array[num].Store = binaryReader.ReadByte();
							}
							num2++;
							num4 += 1;
						}
						num3 += 1;
					}
					binaryReader.BaseStream.Seek(4L, SeekOrigin.Current);
					if (this._Height)
					{
						BitArray[] array2 = new BitArray[current.Width * current.Height / 4u];
						num = 0;
						num2 = 0;
						array2[num] = new BitArray();
						ushort num5 = 0;
						while ((uint)num5 < current.Height)
						{
							ushort num6 = 0;
							while ((uint)num6 < current.Width)
							{
								switch (num2)
								{
								case 0:
									if (array2[num].Check(BitValues.aFirst) && !array2[num].Check(BitValues.aSecond))
									{
										current.SetHeight(num6, num5, 1);
									}
									else
									{
										if (array2[num].Check(BitValues.hFirst))
										{
											current.SetHeight(num6, num5, 2);
										}
									}
									break;
								case 1:
									if (array2[num].Check(BitValues.aThird) && !array2[num].Check(BitValues.aFourth))
									{
										current.SetHeight(num6, num5, 1);
									}
									else
									{
										if (array2[num].Check(BitValues.hSecond))
										{
											current.SetHeight(num6, num5, 2);
										}
									}
									break;
								case 2:
									if (array2[num].Check(BitValues.aFifth) && !array2[num].Check(BitValues.aSixth))
									{
										current.SetHeight(num6, num5, 1);
									}
									else
									{
										if (array2[num].Check(BitValues.hThird))
										{
											current.SetHeight(num6, num5, 2);
										}
									}
									break;
								case 3:
									if (array2[num].Check(BitValues.aSeventh) && !array2[num].Check(BitValues.aEighth))
									{
										current.SetHeight(num6, num5, 1);
									}
									else
									{
										if (array2[num].Check(BitValues.hFourth))
										{
											current.SetHeight(num6, num5, 2);
										}
									}
									num2 = -1;
									num++;
									break;
								}
								if (num2 == -1 && array2.Length > num)
								{
									array2[num] = new BitArray();
									array2[num].Store = binaryReader.ReadByte();
								}
								num2++;
								num6 += 1;
							}
							num5 += 1;
						}
						binaryReader.BaseStream.Seek(5L, SeekOrigin.Current);
					}
					else
					{
						binaryReader.BaseStream.Seek((long)((ulong)(current.Width * current.Height / 4u + 4u)), SeekOrigin.Current);
					}
					int num7 = binaryReader.ReadInt32();
					current.PopulatePortals((uint)num7);
					ushort num8 = 0;
					while ((int)num8 < num7)
					{
						current.SetPortal((int)num8, new DMapPortal
						{
							XCord = binaryReader.ReadUInt16(),
							YCord = binaryReader.ReadUInt16()
						});
						num8 += 1;
					}
				}
			}
			Dictionary<int, DMap> dictionary = new Dictionary<int, DMap>();
			foreach (DMap current2 in this._Maps.Values)
			{
				if (current2.Height == 0u)
				{
					dictionary.Add((int)current2.MapId, current2);
				}
			}
			foreach (DMap current3 in dictionary.Values)
			{
				this._Maps.Remove((int)current3.MapId);
			}
			this._Loaded = true;
		}
		private void PopulateDmaps()
		{
			foreach (DMap current in this._Maps.Values)
			{
				this.CurrentlyLoading = current.FileName;
				if (!File.Exists(DMapServer._ConquerPath + "\\map\\map\\" + current.FileName))
				{
					Console.WriteLine(new FileNotFoundException().Message);
				}
				else
				{
					FileStream fileStream;
					try
					{
						fileStream = new FileStream(DMapServer._ConquerPath + "\\map\\map\\" + current.FileName, FileMode.Open, FileAccess.Read);
					}
					catch (FileNotFoundException ex)
					{
						Console.WriteLine(ex.Message);
						continue;
					}
					BinaryReader binaryReader = new BinaryReader(fileStream);
					fileStream.Seek(268L, SeekOrigin.Begin);
					current.PopulateTiles(binaryReader.ReadUInt32(), binaryReader.ReadUInt32());
					ushort num = 0;
					while ((uint)num < current.Height)
					{
						ushort num2 = 0;
						while ((uint)num2 < current.Width)
						{
							current.SetWalk(num2, num, !Convert.ToBoolean(binaryReader.ReadUInt16()));
							fileStream.Seek(2L, SeekOrigin.Current);
							if (this._Height)
							{
								current.SetHeight(num2, num, binaryReader.ReadUInt16());
							}
							else
							{
								fileStream.Seek(2L, SeekOrigin.Current);
							}
							num2 += 1;
						}
						fileStream.Seek(4L, SeekOrigin.Current);
						num += 1;
					}
					uint num3 = binaryReader.ReadUInt32();
					current.PopulatePortals(num3);
					ushort num4 = 0;
					while ((uint)num4 < num3)
					{
						current.SetPortal((int)num4, new DMapPortal
						{
							XCord = (ushort)binaryReader.ReadUInt32(),
							YCord = (ushort)binaryReader.ReadUInt32()
						});
						fileStream.Seek(4L, SeekOrigin.Current);
						num4 += 1;
					}
					this.LoadMapObjects(binaryReader, current);
					this.MergeSceneToTextureArea(current);
					fileStream.Dispose();
					fileStream.Close();
					binaryReader.Close();
					if (this._Convert)
					{
						this.ConvertMap(current);
					}
				}
			}
			this._Loaded = true;
		}
		private SceneFile CreateSceneFile(BinaryReader Reader)
		{
			SceneFile result = default(SceneFile);
			result.SceneFileName = DMapServer.NTString(Encoding.ASCII.GetString(Reader.ReadBytes(260)));
			result.Location = new Point(Reader.ReadInt32(), Reader.ReadInt32());
			using (BinaryReader binaryReader = new BinaryReader(new FileStream(DMapServer._ConquerPath + result.SceneFileName, FileMode.Open, FileAccess.Read)))
			{
				ScenePart[] array = new ScenePart[binaryReader.ReadInt32()];
				for (int i = 0; i < array.Length; i++)
				{
					binaryReader.BaseStream.Seek(332L, SeekOrigin.Current);
					array[i].Size = new Size(binaryReader.ReadInt32(), binaryReader.ReadInt32());
					binaryReader.BaseStream.Seek(4L, SeekOrigin.Current);
					array[i].StartPosition = new Point(binaryReader.ReadInt32(), binaryReader.ReadInt32());
					binaryReader.BaseStream.Seek(4L, SeekOrigin.Current);
					array[i].NoAccess = new bool[array[i].Size.Width, array[i].Size.Height];
					for (int j = 0; j < array[i].Size.Height; j++)
					{
						for (int k = 0; k < array[i].Size.Width; k++)
						{
							array[i].NoAccess[k, j] = (binaryReader.ReadInt32() == 0);
							binaryReader.BaseStream.Seek(8L, SeekOrigin.Current);
						}
					}
				}
				result.Parts = array;
			}
			return result;
		}
		private void LoadMapObjects(BinaryReader Reader, DMap Map)
		{
			int num = Reader.ReadInt32();
			List<SceneFile> list = new List<SceneFile>();
			for (int i = 0; i < num; i++)
			{
				int num2 = Reader.ReadInt32();
				int num3 = num2;
				if (num3 <= 4)
				{
					if (num3 != 1)
					{
						if (num3 == 4)
						{
							Reader.BaseStream.Seek(416L, SeekOrigin.Current);
						}
					}
					else
					{
						list.Add(this.CreateSceneFile(Reader));
					}
				}
				else
				{
					if (num3 != 10)
					{
						if (num3 == 15)
						{
							Reader.BaseStream.Seek(276L, SeekOrigin.Current);
						}
					}
					else
					{
						Reader.BaseStream.Seek(72L, SeekOrigin.Current);
					}
				}
			}
			Map.Scenes = list.ToArray();
		}
		private void MergeSceneToTextureArea(DMap Map)
		{
			for (int i = 0; i < Map.Scenes.Length; i++)
			{
				ScenePart[] parts = Map.Scenes[i].Parts;
				for (int j = 0; j < parts.Length; j++)
				{
					ScenePart scenePart = parts[j];
					int num = 0;
					while (true)
					{
						int arg_124_0 = num;
						Size size = scenePart.Size;
						if (arg_124_0 >= size.Width)
						{
							break;
						}
						int num2 = 0;
						while (true)
						{
							int arg_10A_0 = num2;
							Size size2 = scenePart.Size;
							if (arg_10A_0 >= size2.Height)
							{
								break;
							}
							Point point = default(Point);
							int arg_73_0 = Map.Scenes[i].Location.X;
							Point startPosition = scenePart.StartPosition;
							int arg_86_0 = arg_73_0 + startPosition.X + num;
							Size size3 = scenePart.Size;
							point.X = arg_86_0 - size3.Width;
							int arg_B8_0 = Map.Scenes[i].Location.Y;
							Point startPosition2 = scenePart.StartPosition;
							int arg_CB_0 = arg_B8_0 + startPosition2.Y + num2;
							Size size4 = scenePart.Size;
							point.Y = arg_CB_0 - size4.Height;
							Map.SetWalk((ushort)point.X, (ushort)point.Y, scenePart.NoAccess[num, num2]);
							num2++;
						}
						num++;
					}
				}
			}
		}
		private static string NTString(string value)
		{
			value = value.Remove(value.IndexOf("\0"));
			return value;
		}
	}
}
