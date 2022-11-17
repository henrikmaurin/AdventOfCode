using AdventOfCode2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Day03;

namespace Tests
{
	[TestClass]
	public class UnitTestDay03
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

			int result = Day03.CountTrees(3, 1, map);
			Assert.AreEqual(7, result);
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

			int result = Day03.CountTrees(1, 1, map);
			result *= Day03.CountTrees(3, 1, map);
			result *= Day03.CountTrees(5, 1, map);
			result *= Day03.CountTrees(7, 1, map);
			result *= Day03.CountTrees(1, 2, map);

			Assert.AreEqual(336, result);
		}
	}


}
