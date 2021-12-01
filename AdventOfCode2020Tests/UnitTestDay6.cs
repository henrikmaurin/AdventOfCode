using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
	[TestClass]
	public class UnitTestDay6
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

			int result = Day6.Process(data);

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

			int result = Day6.Process2(data);

			Assert.AreEqual(6, result);
		}

		[TestMethod]
		public void Promblem1()
		{
			Assert.AreEqual(7120, Day6.Problem1());
		}

		[TestMethod]
		public void Promblem2()
		{
			Assert.AreEqual(3570, Day6.Problem2());
		}



	}
}
