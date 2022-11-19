using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day10 : DayBase, IDay
	{
		Joltages joltages;
		public Day10() : base(2020, 10)
		{
			joltages = new Joltages();
			joltages.JoltagesList = input.GetDataCached().SplitOnNewline().ToInt();
		}

		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Product: {result1}");

			long result2 = Problem2();
			Console.WriteLine($"P2: Product: {result2}");
		}
		public int Problem1()
		{
			joltages.GenerateStats();
			int result = joltages.OneJumps * joltages.ThreeJumps;
			return result;
		}
		public long Problem2()
		{
			long result = joltages.GetCominationsCount();
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
