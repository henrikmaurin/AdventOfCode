using AdventOfCode2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
	[TestClass]
	public class UnitTestDay01
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

			int result = Day01.FindAndMultiplyX(values, goalvalue, 2);

			Assert.AreEqual(514579, result);
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

			int result = Day01.FindAndMultiplyX(values, goalvalue, 3);

			Assert.AreEqual(241861950, result);
		}
	}
}
