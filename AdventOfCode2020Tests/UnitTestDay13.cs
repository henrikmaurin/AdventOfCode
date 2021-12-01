using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Days.Day13;

namespace Tests
{
	[TestClass]
	public class UnitTestDay13
	{

		[TestMethod("Day 13, Part 1")]
		[TestCategory("Example 1")]
		public void Example1()
		{
			int earliest = 939;

			List<int> Ids = new List<int> { 7, 13, 59, 31, 19 };

			BusLines buslines = new BusLines();

			int Id = buslines.FindEarliestDepartureId(earliest, Ids);
			Assert.AreEqual(59, Id);

			long earliestDeparture = buslines.FindEarliestDeparture(earliest, Id);
			Assert.AreEqual(944, earliestDeparture);
		}

		[TestMethod("Day 13, Part 1")]
		[TestCategory("Example 2")]
		public void Example2()
		{
			int earliest = 939;

			List<string> IdStrings = new List<string> { "7", "13", "x", "x", "59", "x", "31", "19" };

			BusLines buslines = new BusLines();
			long result = buslines.ParseAndGetAnswer(earliest, IdStrings);

			Assert.AreEqual(295, result);
		}


		[TestMethod("Day 13, Part 2")]
		[TestCategory("Example 1")]
		public void Example3()
		{

			List<string> IdStrings = new List<string> { "17", "x", "13", "19" };

			BusLines buslines = new BusLines();
			long result = buslines.FindSequence2(IdStrings);

			Assert.AreEqual(3417, result);
		}

		[TestMethod("Day 13, Part 2")]
		[TestCategory("Example 2")]
		public void Example4()
		{
			List<string> IdStrings = new List<string> { "67", "7", "59", "61" };

			BusLines buslines = new BusLines();
			long result = buslines.FindSequence2(IdStrings);

			Assert.AreEqual(754018, result);
		}

		[TestMethod("Day 13, Part 2")]
		[TestCategory("Example 3")]
		public void Example5()
		{
			List<string> IdStrings = new List<string> { "67", "x", "7", "59", "61" };

			BusLines buslines = new BusLines();
			long result = buslines.FindSequence2(IdStrings);

			Assert.AreEqual(779210, result);
		}

		[TestMethod("Day 13, Part 2")]
		[TestCategory("Example 4")]
		public void Example6()
		{
			List<string> IdStrings = new List<string> { "67", "7", "x", "59", "61" };

			BusLines buslines = new BusLines();
			long result = buslines.FindSequence2(IdStrings);

			Assert.AreEqual(1261476, result);
		}

		[TestMethod("Day 13, Part 2")]
		[TestCategory("Example 4")]
		public void Example7()
		{
			List<string> IdStrings = new List<string> { "1789", "37", "47", "1889" };

			BusLines buslines = new BusLines();
			long result = buslines.FindSequence2(IdStrings);

			Assert.AreEqual(1202161486, result);
		}

		[TestMethod("Day 13, Part 2")]
		[TestCategory("Example 5")]
		public void Example8()
		{
			List<string> IdStrings = new List<string> { "7", "13", "x", "x", "59", "x", "31", "19" };

			BusLines buslines = new BusLines();
			long result = buslines.FindSequence2(IdStrings);

			Assert.AreEqual(1068781, result);
		}

	}


}
