using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Day16;

namespace Tests
{
	[TestClass]
	public class UnitTestDay16
	{
		[TestMethod("Day 16, Part 1")]
		[TestCategory("Example 1")]
		public void Example1()
		{
			List<string> testData = new List<string>
			{
				"class: 1-3 or 5-7",
				"row: 6-11 or 33-44",
				"seat: 13-40 or 45-50",
				"",
				"your ticket:",
				"7,1,14",
				"",
				"nearby tickets:",
				"7,3,47",
				"40,4,50",
				"55,2,20",
				"38,6,12"
			};

			TicketValidator validator = new TicketValidator();
			validator.ParseData(testData);
			int result = validator.ValidateAllNumbers();

			Assert.AreEqual(71, result);
		}


		[TestMethod("Day 16, Part 2")]
		[TestCategory("Example 1")]
		public void Example2()
		{
			List<string> testData = new List<string>
			{
				"class: 1-3 or 5-7",
				"row: 6-11 or 33-44",
				"seat: 13-40 or 45-50",
				"",
				"your ticket:",
				"7,1,14",
				"",
				"nearby tickets:",
				"7,3,47",
				"40,4,50",
				"55,2,20",
				"38,6,12"
			};

			TicketValidator validator = new TicketValidator();
			validator.ParseData(testData);
			validator.DecodeColumns();


			Assert.AreEqual("row", validator.GetRuleForColumn(0).Name);
			Assert.AreEqual("class", validator.GetRuleForColumn(1).Name);
			Assert.AreEqual("seat", validator.GetRuleForColumn(2).Name);
		}
	}
}
