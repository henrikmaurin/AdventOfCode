using AdventOfCode2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
	[TestClass]
	public class UnitTestDay10
	{
		[TestMethod("Day 10, Part 1")]
		[TestCategory("Example data 1")]
		public void Example1()
		{
			List<int> testData = new List<int>
			{
				16,
				10,
				15,
				5,
				1,
				11,
				7,
				19,
				6,
				12,
				4
			};

			Day10.Joltages joltages = new Day10.Joltages();
			joltages.JoltagesList = testData;
			joltages.GenerateStats();

			Assert.AreEqual(7, joltages.OneJumps);
			Assert.AreEqual(5, joltages.ThreeJumps);
		}

		[TestMethod("Day 10, Part 1")]
		[TestCategory("Example data 2")]
		public void Example2()
		{
			List<int> testData = new List<int>
			{
				28,33,18,42,31,14,46,20,48,
				47,24,23,49,45,19,38,39,11,
				1,32,25,35,8,17,7,9,4,2,34,
				10,3
			};

			Day10.Joltages joltages = new Day10.Joltages();
			joltages.JoltagesList = testData;
			joltages.GenerateStats();

			Assert.AreEqual(22, joltages.OneJumps);
			Assert.AreEqual(10, joltages.ThreeJumps);
		}

		[TestMethod("Day 10, Part 2")]
		[TestCategory("Example data 1")]
		public void Example3()
		{
			List<int> testData = new List<int>
			{
				16,
				10,
				15,
				5,
				1,
				11,
				7,
				19,
				6,
				12,
				4
			};

			Day10.Joltages joltages = new Day10.Joltages();
			joltages.JoltagesList = testData;
			long result = joltages.GetCominationsCount();

			Assert.AreEqual(8, result);
		}

		[TestMethod("Day 10, Part 2")]
		[TestCategory("Example data 2")]
		public void Example4()
		{
			List<int> testData = new List<int>
			{
				28,33,18,42,31,14,46,20,48,
				47,24,23,49,45,19,38,39,11,
				1,32,25,35,8,17,7,9,4,2,34,
				10,3
			};

			Day10.Joltages joltages = new Day10.Joltages();
			joltages.JoltagesList = testData;
			long result = joltages.GetCominationsCount();

			Assert.AreEqual(19208, result);
		}
	}
}
