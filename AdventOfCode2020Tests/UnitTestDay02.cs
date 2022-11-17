using AdventOfCode2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
	[TestClass]
	public class UnitTestDay02
	{
		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data 1")]
		public void Part1_1()
		{
			string password = "1-3 a: abcde";
			bool result = Day02.ValidatePassword(password);

			Assert.IsTrue(result);
		}

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data 2")]
		public void Part1_2()
		{
			string password = "1-3 b: cdefg";
			bool result = Day02.ValidatePassword(password);

			Assert.IsFalse(result);
		}

		[TestMethod("Day 2, Part 1")]
		[TestCategory("Example data3")]
		public void Part1_3()
		{
			string password = "2-9 c: ccccccccc";
			bool result = Day02.ValidatePassword(password);

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

			int result = Day02.ValidatePasswords(passwords);
			Assert.AreEqual(2, result);
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data 1")]
		public void Part2_1()
		{
			string password = "1-3 a: abcde";
			bool result = Day02.ValidatePasswordNew(password);

			Assert.IsTrue(result);
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data 2")]
		public void Part2_2()
		{
			string password = "1-3 b: cdefg";
			bool result = Day02.ValidatePasswordNew(password);

			Assert.IsFalse(result);
		}

		[TestMethod("Day 2, Part 2")]
		[TestCategory("Example data 3")]
		public void Part2_3()
		{
			string password = "2-9 c: ccccccccc";
			bool result = Day02.ValidatePasswordNew(password);

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

			int result = Day02.ValidatePasswordsNew(passwords);
			Assert.AreEqual(1, result);
		}
	}
}
