using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day3
	{
		public static int Problem1()
		{
			List<string> mapData = File.ReadAllLines("Data/Day3.txt").ToList();
			Map map = Map.MapFactory(mapData);
			int result = CountTrees(3, 1, map);

			Console.WriteLine(result);
			return result;
		}

		public static long Problem2()
		{
			List<string> mapData = File.ReadAllLines("Data/Day3.txt").ToList();
			Map map = Map.MapFactory(mapData);

			long result = CountTrees(1, 1, map);
			result *= CountTrees(3, 1, map);
			result *= CountTrees(5, 1, map);
			result *= CountTrees(7, 1, map);
			result *= CountTrees(1, 2, map);

			Console.WriteLine(result);
			return result;
		}


		public static int CountTrees(int x, int y, Map map)
		{
			if (map == null)
				return -1;

			int xpos = 0;
			int ypos = 0;
			int trees = 0;

			while (ypos < map.MapSizeY - 1)
			{
				xpos += x;
				ypos += y;
				if (map[xpos, ypos] == '#')
					trees++;
			}

			return trees;
		}

		public class Map
		{
			public Map(int sizeX, int sizeY)
			{
				MapSizeX = sizeX;
				MapSizeY = sizeY;
				Data = new char[MapSizeX, MapSizeY];
			}

			public static Map MapFactory(List<string> mapData)
			{
				Map map = new Map(mapData[0].Count(), mapData.Count());

				map.Data = new char[map.MapSizeX, map.MapSizeY];
				int x = 0;
				int y = 0;
				foreach (string mapLine in mapData)
				{
					foreach (char mapChar in mapLine)
					{
						map.Data[x, y] = mapChar;
						x++;
					}
					y++;
					x = 0;
				}
				return map;
			}

			private char GetAt(int x, int y)
			{
				return Data[x % MapSizeX, y];
			}

			public char this[int x, int y] { get => GetAt(x, y); }

			public char[,] Data;
			public int MapSizeX;
			public int MapSizeY;
		}
	}
}
