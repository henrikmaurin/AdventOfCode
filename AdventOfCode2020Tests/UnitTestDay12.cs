using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Day12;

namespace Tests
{
	[TestClass]
	public class UnitTestDay12
	{
		[TestMethod("Day 12, Part 1")]
		[TestCategory("Example data")]
		public void Example1()
		{
			List<string> exampleData = new List<string> {
				"F10",
				"N3",
				"F7",
				"R90",
				"F11"
				};

			Navigation nav = new Navigation();
			long result = nav.RunInstructions(exampleData);

			Assert.AreEqual(25, result);
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example data")]
		public void Example2()
		{
			List<string> exampleData = new List<string> {
				"F10",
				"N3",
				"F7",
				"R90",
				"F11"
				};

			Navigation nav = new Navigation();
			long result = nav.RunInstructions2(exampleData);

			Assert.AreEqual(286, result);
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example Rotate R90")]
		public void RotateRight90()
		{
			Navigation nav = new Navigation();
			nav.SetWaypoint(2, -1);

			nav.DecodeWaypoints("R90");
			Assert.AreEqual(1, nav.CurrentWaypoint.X);
			Assert.AreEqual(2, nav.CurrentWaypoint.Y);
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example Rotate R180")]
		public void RotateRight180()
		{
			Navigation nav = new Navigation();
			nav.SetWaypoint(2, -1);

			nav.DecodeWaypoints("R180");
			Assert.AreEqual(-2, nav.CurrentWaypoint.X);
			Assert.AreEqual(1, nav.CurrentWaypoint.Y);
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example Rotate R270")]
		public void RotateRight270()
		{
			Navigation nav = new Navigation();
			nav.SetWaypoint(2, -1);

			nav.DecodeWaypoints("R270");
			Assert.AreEqual(-1, nav.CurrentWaypoint.X);
			Assert.AreEqual(-2, nav.CurrentWaypoint.Y);
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example Rotate L90")]
		public void RotateLeft90()
		{
			Navigation nav = new Navigation();
			nav.SetWaypoint(2, -1);

			nav.DecodeWaypoints("L90");
			Assert.AreEqual(-1, nav.CurrentWaypoint.X);
			Assert.AreEqual(-2, nav.CurrentWaypoint.Y);
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example Rotate L180")]
		public void RotateLeft180()
		{
			Navigation nav = new Navigation();
			nav.SetWaypoint(2, -1);

			nav.DecodeWaypoints("L180");
			Assert.AreEqual(-2, nav.CurrentWaypoint.X);
			Assert.AreEqual(1, nav.CurrentWaypoint.Y);
		}

		[TestMethod("Day 12, Part 2")]
		[TestCategory("Example Rotate L270")]
		public void RotateLeft270()
		{
			Navigation nav = new Navigation();
			nav.SetWaypoint(2, -1);

			nav.DecodeWaypoints("L270");
			Assert.AreEqual(1, nav.CurrentWaypoint.X);
			Assert.AreEqual(2, nav.CurrentWaypoint.Y);
		}

	}
}
