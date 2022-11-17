using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Day17;

namespace Tests
{
	[TestClass]
	public class UnitTestDay17
	{

		[TestMethod("Day 17, Part 1")]
		[TestCategory("Example 1")]
		public void Example1()
		{
			List<string> testData = new List<string>
			{
				".#.",
				"..#",
				"###"
			};

			Cubes cubes = new Cubes();
			cubes.Init(testData);


			int result = cubes.RunCycles(6);

			Assert.AreEqual(112, result);
		}

		[TestMethod("Day 17, Part 2")]
		[TestCategory("Example 1")]
		public void Example2()
		{
			List<string> testData = new List<string>
			{
				".#.",
				"..#",
				"###"
			};

			Cubes cubes = new Cubes();
			cubes.Init(testData);


			int result = cubes.RunCyclesHyperCube(6);

			Assert.AreEqual(848, result);
		}
	}
}
