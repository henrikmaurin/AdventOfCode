using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day10
	{
		public static int Problem1()
		{
			Joltages joltages = new Joltages();
			joltages.JoltagesList = File.ReadAllLines("Data/Day10.txt").Select(v => v.ToInt()).ToList();

			joltages.GenerateStats();
			int result = joltages.OneJumps * joltages.ThreeJumps;
			Console.WriteLine(result);
			return result;
		}
		public static long Problem2()
		{
			Joltages joltages = new Joltages();
			joltages.JoltagesList = File.ReadAllLines("Data/Day10.txt").Select(v => v.ToInt()).ToList();

			long result = joltages.GetCominationsCount();
			Console.WriteLine(result);
			return result;
		}

		public class Joltages
		{
			public List<int> JoltagesList { get; set; }
			public int OneJumps { get; set; }
			public int ThreeJumps { get; set; }
			private Dictionary<int, long> numberAndVisits;

			public void GenerateStats()
			{
				OneJumps = 0;
				ThreeJumps = 0;
				int prevJoltage = 0;

				foreach (int joltage in JoltagesList.OrderBy(j => j))
				{
					switch (joltage - prevJoltage)
					{
						case 1:
							OneJumps++;
							break;
						case 3:
							ThreeJumps++;
							break;
					}
					prevJoltage = joltage;
				}
				ThreeJumps++;
			}

			public long GetCominationsCount()
			{
				long result = 1;
				numberAndVisits = new Dictionary<int, long>();
				JoltagesList.Add(0);

				return GetNumber(JoltagesList.Min());
			}

			public long GetNumber(int pos)
			{
				if (pos == JoltagesList.Max())
					return 1;

				if (!JoltagesList.Contains(pos))
					return 0;

				if (numberAndVisits.ContainsKey(pos))
				{
					return numberAndVisits[pos];
				}

				numberAndVisits.Add(pos, 0);

				for (int i = 1; i <= 3; i++)
				{
					numberAndVisits[pos] += GetNumber(pos + i);
				}

				return numberAndVisits[pos];
			}


		}
	}
}
