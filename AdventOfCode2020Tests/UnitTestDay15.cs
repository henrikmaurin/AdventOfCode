using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Days.Day15;

namespace Tests
{
	[TestClass]
	public class UnitTestDay15
	{
		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example 1")]
		public void Example1()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(0);
			game.AddNumber(3);
			game.AddNumber(6);

			int result = game.GetNthNumber(2020);

			Assert.AreEqual(436, result);
		}

		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example 2")]
		public void Example2()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(1);
			game.AddNumber(3);
			game.AddNumber(2);

			int result = game.GetNthNumber(2020);

			Assert.AreEqual(1, result);
		}

		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example 3")]
		public void Example3()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(2);
			game.AddNumber(1);
			game.AddNumber(3);

			int result = game.GetNthNumber(2020);

			Assert.AreEqual(10, result);
		}
		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example 4")]
		public void Example4()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(1);
			game.AddNumber(2);
			game.AddNumber(3);

			int result = game.GetNthNumber(2020);

			Assert.AreEqual(27, result);
		}
		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example 5")]
		public void Example5()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(2);
			game.AddNumber(3);
			game.AddNumber(1);

			int result = game.GetNthNumber(2020);

			Assert.AreEqual(78, result);
		}
		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example 6")]
		public void Example6()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(3);
			game.AddNumber(2);
			game.AddNumber(1);

			int result = game.GetNthNumber(2020);

			Assert.AreEqual(438, result);
		}
		[TestMethod("Day 15, Part 1")]
		[TestCategory("Example 7")]
		public void Example7()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(3);
			game.AddNumber(1);
			game.AddNumber(2);

			int result = game.GetNthNumber(2020);

			Assert.AreEqual(1836, result);
		}

		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example 1")]
		public void Example21()
		{
			ElfGame game = new ElfGame();
			List<int> numbers = new List<int> { 0, 3, 6 };

			//int result = game.GetNthNumber(30000000);
			int result = game.GetNthNumber2(new List<int> { 0, 3, 6 }, 30000000);

			Assert.AreEqual(175594, result);
		}

		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example 2")]
		public void Example22()
		{
			ElfGame game = new ElfGame();
		
			int result = game.GetNthNumber2(new List<int> { 1, 3, 2 }, 30000000);

			Assert.AreEqual(2578, result);
		}

		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example 3")]
		public void Example23()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(2);
			game.AddNumber(1);
			game.AddNumber(3);

			int result = game.GetNthNumber2(new List<int> { 2, 1, 3 }, 30000000);

			Assert.AreEqual(3544142, result);
		}
		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example 4")]
		public void Example24()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(1);
			game.AddNumber(2);
			game.AddNumber(3);

			int result = game.GetNthNumber2(new List<int> { 1, 2, 3 }, 30000000);

			Assert.AreEqual(261214, result);
		}
		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example 5")]
		public void Example25()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(2);
			game.AddNumber(3);
			game.AddNumber(1);

			int result = game.GetNthNumber2(new List<int> { 2, 3, 1 }, 30000000);

			Assert.AreEqual(6895259, result);
		}
		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example 6")]
		public void Example26()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(3);
			game.AddNumber(2);
			game.AddNumber(1);

			int result = game.GetNthNumber2(new List<int> { 3, 2, 1 }, 30000000);

			Assert.AreEqual(18, result);
		}
		[TestMethod("Day 15, Part 2")]
		[TestCategory("Example 7")]
		public void Example27()
		{
			ElfGame game = new ElfGame();
			game.AddNumber(3);
			game.AddNumber(1);
			game.AddNumber(2);

			int result = game.GetNthNumber2(new List<int> { 3, 1, 2 }, 30000000);

			Assert.AreEqual(362, result);
		}


	}
}
