using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day1
	{
		public static int Problem1()
		{
			List<int> values = File.ReadAllLines("Data/Day1.txt").Select(v => int.Parse(v)).ToList();
			int goalValue = 2020;

			int result = FindAndMultiplyX(values, goalValue, 2);
			Console.WriteLine(result);
			return result;
		}

		public static int Problem2()
		{
			List<int> values = File.ReadAllLines("Data/Day1.txt").Select(v => int.Parse(v)).ToList();
			int goalValue = 2020;

			int result = FindAndMultiplyX(values, goalValue, 3);
			Console.WriteLine(result);
			return result;
		}

		public static int FindAndMultiplyX(List<int> values, int goalvalue, int depth, int withValue = 0)
		{
			if (depth > values.Count - 1)
				return 0;

			for (int i = 0; i <= values.Count - depth; i++)
			{
				if (depth == 1)
				{
					if (withValue + values[i] == goalvalue)
						return values[i];
				}
				else
				{
					int val = FindAndMultiplyX(values.Skip(1).ToList(), goalvalue, depth - 1, withValue + values[i]);
					if (val > 0)
						return val * values[i];
				}
			}
			return 0;
		}
	}
}
