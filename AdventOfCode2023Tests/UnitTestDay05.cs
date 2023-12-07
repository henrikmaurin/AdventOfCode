using AdventOfCode2023;
using Common;

using static AdventOfCode2023.Day05;

namespace Tests
{
	[TestClass]
	public class UnitTestDay05
	{
		private string data;
		private string[][] testdata;

		private Seeds SeedInstructions;
		private NecessityMappings NecessityMapper;

		[TestInitialize]
		public void Init()
		{
			data = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";
			testdata = data.GroupByEmptyLine();
			SeedInstructions = new Seeds(testdata[0][0]);
			NecessityMapper = new NecessityMappings(testdata[1..]);		
		}


		[TestMethod("Day 5, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			long result = GardeningElf.PlantSeeds(SeedInstructions, NecessityMapper);

			Assert.AreEqual(35, result);
		}

		[TestMethod("Day 5, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			long result = GardeningElf.PlantRangesOfSeeds(SeedInstructions, NecessityMapper);

			Assert.AreEqual(46, result);
		}
	}
}
