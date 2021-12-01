using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Days.Day3;

namespace Tests
{
	[TestClass]
	public class UnitTestDay3
	{
		[TestMethod("Day 3, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			List<string> mapdata = new List<string>
			{
				"..##.......",
				"#...#...#..",
				".#....#..#.",
				"..#.#...#.#",
				".#...##..#.",
				"..#.##.....",
				".#.#.#....#",
				".#........#",
				"#.##...#...",
				"#...##....#",
				".#..#...#.#"
			};

			Map map = Map.MapFactory(mapdata);

			int result = Day3.CountTrees(3, 1, map);
			Assert.AreEqual(7, result);
		}

		[TestMethod("Day 3, Part 1")]
		[TestCategory("Answer")]
		public void Problem1()
		{
			int answer = 286;
			Assert.AreEqual(answer, Day3.Problem1());
		}

		[TestMethod("Day 3, Part 2")]
		[TestCategory("Example data")]
		public void Example2()
		{
			List<string> mapdata = new List<string>
			{
				"..##.......",
				"#...#...#..",
				".#....#..#.",
				"..#.#...#.#",
				".#...##..#.",
				"..#.##.....",
				".#.#.#....#",
				".#........#",
				"#.##...#...",
				"#...##....#",
				".#..#...#.#"
			};

			Map map = Map.MapFactory(mapdata);

			int result = Day3.CountTrees(1, 1, map);
			result *= Day3.CountTrees(3, 1, map);
			result *= Day3.CountTrees(5, 1, map);
			result *= Day3.CountTrees(7, 1, map);
			result *= Day3.CountTrees(1, 2, map);

			Assert.AreEqual(336, result);
		}

		[TestMethod("Day 3, Part 2")]
		[TestCategory("Answer")]
		public void Problem2()
		{
			long answer = 3638606400;
			Assert.AreEqual(answer, Day3.Problem2());
		}
	}


}
