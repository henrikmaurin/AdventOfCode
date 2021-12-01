using AdventOfCode2020.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static AdventOfCode2020.Days.Day7;

namespace Tests
{
	[TestClass]
	public class UnitTestDay7
	{
		[TestMethod]
		public void Example1()
		{
			List<string> testdata = new List<string>
			{
				"light red bags contain 1 bright white bag, 2 muted yellow bags.",
				"dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
				"bright white bags contain 1 shiny gold bag.",
				"muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
				"shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
				"dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
				"vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
				"faded blue bags contain no other bags.",
				"dotted black bags contain no other bags."
			};

			string toFind = "shiny gold";

			BagRules bagRules = new BagRules();
			bagRules.ParseRules(testdata);

			int count = bagRules.Contains(toFind);

			Assert.AreEqual(4, count);
		}

		[TestMethod]
		public void Example2()
		{
			List<string> testdata = new List<string>
			{
				"shiny gold bags contain 2 dark red bags.",
				"dark red bags contain 2 dark orange bags.",
				"dark orange bags contain 2 dark yellow bags.",
				"dark yellow bags contain 2 dark green bags.",
				"dark green bags contain 2 dark blue bags.",
				"dark blue bags contain 2 dark violet bags.",
				"dark violet bags contain no other bags."
			};

			string startBag = "shiny gold";

			BagRules bagRules = new BagRules();
			bagRules.ParseRules(testdata);

			int count = bagRules.TotalBags(startBag);

			Assert.AreEqual(126, count);
		}
		[TestMethod]
		public void Promblem1()
		{
			Assert.AreEqual(278, Day7.Problem1());
		}

		[TestMethod]
		public void Promblem2()
		{
			Assert.AreEqual(45157, Day7.Problem2());
		}
	}
}
