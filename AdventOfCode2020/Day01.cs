using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day01 : DayBase, IDay
	{
		private List<int> values;
		public Day01() : base(2020, 1)
		{
			values = input.GetDataCached().SplitOnNewline().ToInt();
		}

		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Product: {result1}");

			int result2 = Problem2();
			Console.WriteLine($"P2: Product: {result2}");
		}

		public int Problem1()
		{
			int goalValue = 2020;

			int result = FindAndMultiplyX(values, goalValue, 2);
			return result;
		}

		public int Problem2()
		{
			int goalValue = 2020;

			int result = FindAndMultiplyX(values, goalValue, 3);
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
