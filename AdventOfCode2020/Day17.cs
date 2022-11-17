using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day17 : DayBase, IDay
	{
		private List<string> data;
		public Day17() : base(2020, 17)
		{
			data = input.GetDataCached().SplitOnNewline();
		}

		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Cubes: {result1}");

			int result2 = Problem2();
			Console.WriteLine($"P2: Cubes left: {result2}");
		}
		public int Problem1()
		{
			Cubes cubes = new Cubes();
			cubes.Init(data);
			int result = cubes.RunCycles(6);

			return result;
		}

		public int Problem2()
		{
			Cubes cubes = new Cubes();
			cubes.Init(data);
			int result = cubes.RunCyclesHyperCube(6);

			return result;
		}

		public class Cubes
		{
			private HashSet<ConwayCube> _currentConwayCubes;
			private HashSet<HyperCube> _currentHyperCubes;

			public void Init(List<string> data)
			{
				_currentConwayCubes = new HashSet<ConwayCube>(new ConwayCube());
				_currentHyperCubes = new HashSet<HyperCube>(new HyperCube());

				int z = 0;
				int x = 0;
				int y = 0;
				int w = 0;

				foreach (string row in data)
				{
					x = 0;
					foreach (char c in row)
					{
						if (c == '#')
						{
							_currentConwayCubes.Add(new ConwayCube { X = x, Y = y, Z = z });
							_currentHyperCubes.Add(new HyperCube { X = x, Y = y, Z = z, W = w });
						}
						x++;
					}
					y++;
				}
			}

			public int RunCycles(int count)
			{
				for (int i = 0; i < count; i++)
					RunCycle();

				return _currentConwayCubes.Count;
			}

			public int RunCyclesHyperCube(int count)
			{
				for (int i = 0; i < count; i++)
					RunCycleHyperCube();

				return _currentHyperCubes.Count;
			}


			public void RunCycle()
			{
				HashSet<ConwayCube> newSet = new HashSet<ConwayCube>(new ConwayCube());
				HashSet<ConwayCube> calculated = new HashSet<ConwayCube>(new ConwayCube());

				foreach (ConwayCube currentCube in _currentConwayCubes)
				{
					if (!calculated.Contains(currentCube))
					{
						calculated.Add(currentCube);

						if (ApplyRule(currentCube))
							newSet.Add(currentCube);
					}

					foreach (ConwayCube neighbor in GetNeighbors(currentCube))
					{
						{
							if (!calculated.Contains(neighbor))
								calculated.Add(neighbor);

							if (ApplyRule(neighbor))
								newSet.Add(neighbor);
						}
					}
				}

				_currentConwayCubes = newSet;
			}
			public void RunCycleHyperCube()
			{
				HashSet<HyperCube> newSet = new HashSet<HyperCube>(new HyperCube());
				HashSet<HyperCube> calculated = new HashSet<HyperCube>(new HyperCube());

				foreach (HyperCube currentCube in _currentHyperCubes)
				{
					if (!calculated.Contains(currentCube))
					{
						calculated.Add(currentCube);

						if (ApplyRule(currentCube))
							newSet.Add(currentCube);
					}

					foreach (HyperCube neighbor in GetNeighbors(currentCube))
					{
						{
							if (!calculated.Contains(neighbor))
								calculated.Add(neighbor);

							if (ApplyRule(neighbor))
								newSet.Add(neighbor);
						}
					}
				}

				_currentHyperCubes = newSet;
			}


			public HashSet<ConwayCube> GetNeighbors(ConwayCube currentCube)
			{
				HashSet<ConwayCube> conwayCubes = new HashSet<ConwayCube>();
				for (int x1 = currentCube.X - 1; x1 <= currentCube.X + 1; x1++)
					for (int y1 = currentCube.Y - 1; y1 <= currentCube.Y + 1; y1++)
						for (int z1 = currentCube.Z - 1; z1 <= currentCube.Z + 1; z1++)
							if (x1 != currentCube.X || y1 != currentCube.Y || z1 != currentCube.Z)
								conwayCubes.Add(new ConwayCube { X = x1, Y = y1, Z = z1 });

				return conwayCubes;
			}

			public HashSet<HyperCube> GetNeighbors(HyperCube currentCube)
			{
				HashSet<HyperCube> conwayCubes = new HashSet<HyperCube>();
				for (int x1 = currentCube.X - 1; x1 <= currentCube.X + 1; x1++)
					for (int y1 = currentCube.Y - 1; y1 <= currentCube.Y + 1; y1++)
						for (int z1 = currentCube.Z - 1; z1 <= currentCube.Z + 1; z1++)
							for (int w1 = currentCube.W - 1; w1 <= currentCube.W + 1; w1++)
								if (x1 != currentCube.X || y1 != currentCube.Y || z1 != currentCube.Z || w1 != currentCube.W)
									conwayCubes.Add(new HyperCube { X = x1, Y = y1, Z = z1, W = w1 });

				return conwayCubes;
			}

			public bool ApplyRule(ConwayCube currentCube)
			{
				bool currentstat = _currentConwayCubes.Contains(currentCube);

				int activeNeighbors = _currentConwayCubes.Where(c => c.X >= currentCube.X - 1 && c.X <= currentCube.X + 1)
					.Where(c => c.Y >= currentCube.Y - 1 && c.Y <= currentCube.Y + 1)
					.Where(c => c.Z >= currentCube.Z - 1 && c.Z <= currentCube.Z + 1)
					.Where(c => c.X != currentCube.X || c.Y != currentCube.Y || c.Z != currentCube.Z)
					.Count();

				if (currentstat == true)
					if (activeNeighbors == 2 || activeNeighbors == 3)
						return true;
					else
						return false;
				else
					if (activeNeighbors == 3)
					return true;


				return false;
			}

			public bool ApplyRule(HyperCube currentCube)
			{
				bool currentstat = _currentHyperCubes.Contains(currentCube);

				int activeNeighbors = _currentHyperCubes.Where(c => c.X >= currentCube.X - 1 && c.X <= currentCube.X + 1)
					.Where(c => c.Y >= currentCube.Y - 1 && c.Y <= currentCube.Y + 1)
					.Where(c => c.Z >= currentCube.Z - 1 && c.Z <= currentCube.Z + 1)
					.Where(c => c.W >= currentCube.W - 1 && c.W <= currentCube.W + 1)
					.Where(c => c.X != currentCube.X || c.Y != currentCube.Y || c.Z != currentCube.Z || c.W != currentCube.W)
					.Count();

				if (currentstat == true)
					if (activeNeighbors == 2 || activeNeighbors == 3)
						return true;
					else
						return false;
				else
					if (activeNeighbors == 3)
					return true;


				return false;
			}
		}

		public class ConwayCube : IEqualityComparer<ConwayCube>
		{
			public int X { get; set; }
			public int Y { get; set; }
			public int Z { get; set; }

			public bool Equals([AllowNull] ConwayCube obj1, [AllowNull] ConwayCube obj2)
			{
				return (obj1?.X == obj2?.X && obj1?.Y == obj2?.Y && obj1?.Z == obj2?.Z);
			}

			public int GetHashCode([DisallowNull] ConwayCube obj)
			{
				int hash = 137 * obj.X + 149 * obj.Y + 163 * obj.Z;
				return hash;
			}

			public override string ToString()
			{
				return $"X:{X}, Y:{Y}, Z:{Z}";
			}
		}

		public class HyperCube : ConwayCube, IEqualityComparer<HyperCube>
		{
			public int W { get; set; }

			public bool Equals([AllowNull] HyperCube obj1, [AllowNull] HyperCube obj2)
			{
				return (obj1?.X == obj2?.X && obj1?.Y == obj2?.Y && obj1?.Z == obj2?.Z && obj1?.W == obj2?.W);
			}

			public int GetHashCode([DisallowNull] HyperCube obj)
			{
				int hash = 137 * obj.X + 149 * obj.Y + 163 * obj.Z + obj.W * 173;
				return hash;
			}

			public override string ToString()
			{
				return $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";
			}
		}
	}
}
