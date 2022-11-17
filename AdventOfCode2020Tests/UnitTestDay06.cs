using AdventOfCode2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
	[TestClass]
	public class UnitTestDay06
	{
		[TestMethod]
		public void Example1()
		{
			List<string> data = new List<string>
			{
				"abc",
				"",
				"a",
				"b",
				"c",
				"",
				"ab",
				"ac",
				"",
				"a",
				"a",
				"a",
				"a",
				"",
				"b"
			};

			int result = Day06.Process(data);

			Assert.AreEqual(11, result);
		}

		[TestMethod]
		public void Example2()
		{
			List<string> data = new List<string>
			{
				"abc",
				"",
				"a",
				"b",
				"c",
				"",
				"ab",
				"ac",
				"",
				"a",
				"a",
				"a",
				"a",
				"",
				"b"
			};

			int result = Day06.Process2(data);

			Assert.AreEqual(6, result);
		}
	}
}
