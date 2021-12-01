using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Days.Day11;

namespace Tests
{
	[TestClass]
	public class UnitTestDay11
	{
		[TestMethod("Day 11, Part 1")]
		[TestCategory("Example data 1")]
		public void Example1()
		{
			List<string> testData = new List<string>
			{
				"L.LL.LL.LL",
				"LLLLLLL.LL",
				"L.L.L..L..",
				"LLLL.LL.LL",
				"L.LL.LL.LL",
				"L.LLLLL.LL",
				"..L.L.....",
				"LLLLLLLLLL",
				"L.LLLLLL.L",
				"L.LLLLL.LL"
			};

			Ferry erry = new Ferry();
			erry.Setup(testData);
			int rounds = erry.RunUntilStable();

			Assert.AreEqual(5, rounds);
			Assert.AreEqual(37, erry.Occupied);

		}

		[TestMethod("Day 11, Part2")]
		[TestCategory("Example data 1")]
		public void Example2()
		{
			List<string> testData = new List<string>
			{
				"L.LL.LL.LL",
				"LLLLLLL.LL",
				"L.L.L..L..",
				"LLLL.LL.LL",
				"L.LL.LL.LL",
				"L.LLLLL.LL",
				"..L.L.....",
				"LLLLLLLLLL",
				"L.LLLLLL.L",
				"L.LLLLL.LL"
			};

			Ferry ferry = new Ferry();
			ferry.Setup(testData);
			int rounds = ferry.RunUntilStable2();

			Assert.AreEqual(6, rounds);
			Assert.AreEqual(26, ferry.Occupied);

		}
	}
}
