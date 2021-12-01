using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
	[TestClass]
	public class UnitTestDay1
	{
		[TestMethod("Day 1, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			int goalvalue = 2020;
			List<int> values = new List<int> {
									1721,
									979,
									366,
									299,
									675,
									1456};

			int result = Day1.FindAndMultiplyX(values, goalvalue, 2);

			Assert.AreEqual(514579, result);
		}

		[TestMethod("Day 1, Part 1")]
		[TestCategory("Answer")]
		public void Answer1()
		{
			int answer = 1018336;
			Assert.AreEqual(answer, Day1.Problem1());
		}

		[TestMethod("Day 1, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			int goalvalue = 2020;
			List<int> values = new List<int> {
									1721,
									979,
									366,
									299,
									675,
									1456};

			int result = Day1.FindAndMultiplyX(values, goalvalue, 3);

			Assert.AreEqual(241861950, result);
		}

		[TestMethod("Day 1, Part 2")]
		[TestCategory("Answer")]
		public void Answer2()
		{
			long answer = 288756720;
			Assert.AreEqual(answer, Day1.Problem2());
		}

	}
}
