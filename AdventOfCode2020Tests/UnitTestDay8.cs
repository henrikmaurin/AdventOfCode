using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Days.Day8;

namespace Tests
{
	[TestClass]
	public class UnitTestDay8
	{
		[TestMethod("Day 8, Part 1")]
		[TestCategory("Example data")]
		public void Part1()
		{
			List<string> program = new List<string>
			{
				"nop +0",
				"acc +1",
				"jmp +4",
				"acc +3",
				"jmp -3",
				"acc -99",
				"acc +1",
				"jmp -4",
				"acc +6"
			};

			Computer computer = new Computer();
			computer.Load(program);
			int result = computer.RunUntilRepeat();

			Assert.AreEqual(5, result);
		}


		[TestMethod("Day 8, Part 1")]
		[TestCategory("Answer")]
		public void Answer1()
		{
			int answer = 1548;
			Assert.AreEqual(answer, Day8.Problem1());
		}

		[TestMethod("Day 8, Part 2")]
		[TestCategory("Example data")]
		public void Part2()
		{
			List<string> program = new List<string>
			{
				"nop +0",
				"acc +1",
				"jmp +4",
				"acc +3",
				"jmp -3",
				"acc -99",
				"acc +1",
				"nop -4",
				"acc +6"
			};

			Computer computer = new Computer();
			computer.Load(program);
			int result = computer.RunUntilEnd();

			Assert.AreEqual(8, result);
		}

		[TestMethod("Day 8, Part 2")]
		[TestCategory("Example data, find instruction to modify;")]
		public void Part2_2()
		{
			List<string> program = new List<string>
			{
				"nop +0",
				"acc +1",
				"jmp +4",
				"acc +3",
				"jmp -3",
				"acc -99",
				"acc +1",
				"jmp -4",
				"acc +6"
			};

			Computer computer = new Computer();
			computer.Load(program);
			int result = Day8.ModifyUntilExecuteToEnd(computer);

			Assert.AreEqual(8, result);
		}

		[TestMethod("Day 8, Part 2")]
		[TestCategory("Answer")]
		public void Answer2()
		{
			int answer = 1375;
			Assert.AreEqual(answer, Day8.Problem2());
		}
	}
}
