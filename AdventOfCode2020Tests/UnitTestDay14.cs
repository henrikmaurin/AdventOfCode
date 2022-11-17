using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Day14;

namespace Tests
{
	[TestClass]
	public class UnitTestDay14
	{

		[TestMethod("Day 14, Part 1")]
		[TestCategory("Example 1")]
		public void Example1()
		{
			List<string> instructions = new List<string>
			{
				"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
				"mem[8] = 11",
				"mem[7] = 101",
				"mem[8] = 0"
			};

			PortComputer pc = new PortComputer();
			pc.ParseInstuctions(instructions);

			ulong result = pc.Sum();

			Assert.AreEqual((ulong)165, result);
		}

		[TestMethod("Day 14, Part 2")]
		[TestCategory("Example 1")]
		public void Example2()
		{
			List<string> instructions = new List<string>
			{
				"mask = 000000000000000000000000000000X1001X",
				"mem[42] = 100",
				"mask = 00000000000000000000000000000000X0XX",
				"mem[26] = 1"
			};

			PortComputer pc = new PortComputer();
			pc.ParseInstuctions2(instructions);

			ulong result = pc.Sum();

			Assert.AreEqual((ulong)208, result);
		}

	}
}
