using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day11
	{
		public static int Problem1()
		{
			List<string> data = File.ReadAllLines("Data/Day11.txt").ToList();

			Ferry ferry = new Ferry();
			ferry.Setup(data);

			int loops = ferry.RunUntilStable();

			int result = ferry.Occupied;
			Console.WriteLine(result);

			return result;
		}

		public static int Problem2()
		{
			List<string> data = File.ReadAllLines("Data/Day11.txt").ToList();

			Ferry ferry = new Ferry();
			ferry.Setup(data);

			int loops = ferry.RunUntilStable2();

			int result = ferry.Occupied;
			Console.WriteLine(result);

			return result;
		}

		public class Ferry
		{
			private char[] _seatingArea;
			private int _sizeX;
			private int _sizeY;

			public void Setup(List<string> data)
			{
				_sizeX = data.ElementAt(0).Length;
				_sizeY = data.Count();

				List<char> temp = new List<char>();
				foreach (string row in data)
					temp.AddRange(row);
				_seatingArea = temp.ToArray();
			}

			public int RunUntilStable()
			{
				int rounds = 0;
				while (GenerateNext())
					rounds++;

				return rounds;
			}

			public int RunUntilStable2()
			{
				int rounds = 0;
				while (GenerateNext2())
					rounds++;

				return rounds;
			}

			public bool GenerateNext()
			{
				char[] nextArea = new char[_sizeX * _sizeY];
				bool changed = false;
				for (int y = 0; y < _sizeY; y++)
					for (int x = 0; x < _sizeX; x++)
					{
						char next = CheckVisible(x, y, 1, 4);
						char current = _seatingArea[y * _sizeX + x];
						if (next != current)
							changed = true;
						nextArea[y * _sizeX + x] = next;
					}

				_seatingArea = nextArea;
				return changed;
			}

			public bool GenerateNext2()
			{
				char[] nextArea = new char[_sizeX * _sizeY];
				bool changed = false;
				for (int y = 0; y < _sizeY; y++)
					for (int x = 0; x < _sizeX; x++)
					{
						char next = CheckVisible(x, y);
						char current = _seatingArea[y * _sizeX + x];
						if (next != current)
							changed = true;
						nextArea[y * _sizeX + x] = next;
					}

				_seatingArea = nextArea;
				return changed;
			}

			public int Occupied { get => _seatingArea.Where(w => w == '#').Count(); }

			private char CheckVisible(int x, int y, int maxLength = int.MaxValue, int threshold = 5)
			{
				char seat = _seatingArea[y * _sizeX + x];

				if (seat == '.')
					return '.';

				int free = 0;
				int occupied = 0;
				for (int dY = -1; dY <= 1; dY++)
				{

					for (int dX = -1; dX <= 1; dX++)
					{
						char sees = Ray(x, y, dX, dY, maxLength);
						if (sees == '#')
							occupied++;
						else
							free++;
					}

				}
				if (seat == 'L' && occupied == 0)
					return '#';
				if (seat == '#' && occupied >= threshold)
					return 'L';

				return seat;
			}

			private char Ray(int x, int y, int dx, int dy, int maxLength)
			{
				if (dx == 0 && dy == 0)
					return '.';
				x += dx;
				y += dy;
				while (x >= 0 && y >= 0 && x < _sizeX && y < _sizeY && maxLength-- > 0)
				{
					if (_seatingArea[y * _sizeX + x] != '.')
						return _seatingArea[y * _sizeX + x];
					x += dx;
					y += dy;
				}

				return '.';
			}

			public void PrettyPrint()
			{
				for (int y = 0; y < _sizeY; y++)
				{
					for (int x = 0; x < _sizeX; x++)
					{
						Console.Write(_seatingArea[y * _sizeX + x]);
					}
					Console.WriteLine();
				}

			}
		}
	}
}