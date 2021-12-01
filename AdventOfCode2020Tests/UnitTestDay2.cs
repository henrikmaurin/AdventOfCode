using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
	[TestClass]
	public class UnitTestDay2
	{
		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data 1")]
		public void Part1_1()
		{
			string password = "1-3 a: abcde";
			bool result = Day2.ValidatePassword(password);

			Assert.IsTrue(result);
		}

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data 2")]
		public void Part1_2()
		{
			string password = "1-3 b: cdefg";
			bool result = Day2.ValidatePassword(password);

			Assert.IsFalse(result);
		}

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data3")]
		public void Part1_3()
		{
			string password = "2-9 c: ccccccccc";
			bool result = Day2.ValidatePassword(password);

			Assert.IsTrue(result);
		}

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data Count")]
		public void Part1_Count()
		{
			List<string> passwords = new List<string>
				{
					"1-3 a: abcde",
					"1-3 b: cdefg",
					"2-9 c: ccccccccc"
				};

			int result = Day2.ValidatePasswords(passwords);
			Assert.AreEqual(2, result);
		}

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Answer")]
		public void Answer1()
		{
			int answer = 625;
			Assert.AreEqual(answer, Day2.Problem1());
		}


		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data 1")]
		public void Part2_1()
		{
			string password = "1-3 a: abcde";
			bool result = Day2.ValidatePasswordNew(password);

			Assert.IsTrue(result);
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data 2")]
		public void Part2_2()
		{
			string password = "1-3 b: cdefg";
			bool result = Day2.ValidatePasswordNew(password);

			Assert.IsFalse(result);
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data 3")]
		public void Part2_3()
		{
			string password = "2-9 c: ccccccccc";
			bool result = Day2.ValidatePasswordNew(password);

			Assert.IsFalse(result);
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data Count")]
		public void Part2_Count()
		{
			List<string> passwords = new List<string>
				{
					"1-3 a: abcde",
					"1-3 b: cdefg",
					"2-9 c: ccccccccc"
				};

			int result = Day2.ValidatePasswordsNew(passwords);
			Assert.AreEqual(1, result);
		}

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Answer")]
		public void Problem2()
		{
			int answer = 391;
			Assert.AreEqual(answer, Day2.Problem2());
		}
	}
}
