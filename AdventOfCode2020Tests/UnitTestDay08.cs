﻿using AdventOfCode2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Day08;

namespace Tests
{
	[TestClass]
	public class UnitTestDay08
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
			int result = Day08.ModifyUntilExecuteToEnd(computer);

			Assert.AreEqual(8, result);
		}
	}
}
