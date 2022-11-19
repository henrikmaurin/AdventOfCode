using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day07 : DayBase, IDay
	{
		private List<string> data;
		public Day07() : base(2020, 7)
		{
			data = input.GetDataCached().SplitOnNewline();
		}

		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Contains shiny gold: {result1}");

			int result2 = Problem2();
			Console.WriteLine($"P2: Individual bags Containing shiny gold: {result2}");
		}
		public int Problem1()
		{
			BagRules rules = new BagRules();
			rules.ParseRules(data);

			int result = rules.Contains("shiny gold");

			return result;
		}

		public int Problem2()
		{
			BagRules rules = new BagRules();
			rules.ParseRules(data);

			int result = rules.TotalBags("shiny gold");

			return result;
		}

		public class BagRules
		{
			private Dictionary<string, BagRule> Rules { get; set; }
			public BagRules()
			{
				Rules = new Dictionary<string, BagRule>();
			}

			public void ParseRules(List<string> rules)
			{
				foreach (string rule in rules)
				{
					BagRule newRule = new BagRule(rule);
					Rules.Add(newRule.Color, newRule);
				}
			}

			public int Contains(string findColor)
			{
				int count = 0;
				foreach (string color in Colors)
				{
					if (ContainsBags(color, findColor) > 0)
						count++;
				}
				return count;
			}

			public int TotalBags(string bagColor, int depth = 0)
			{
				if (!Rules.ContainsKey(bagColor))
					return 0;

				BagRule bagRule = Rules[bagColor];

				//if (bagRule.ContainsRules.Count == 0)
				//	return 1;

				int count = 1;
				if (depth == 0)
					count = 0;

				foreach (Contains b in bagRule.ContainsRules)
				{
					count += TotalBags(b.Color, depth + 1) * b.Quantity;
				}
				return count;
			}

			public int ContainsBags(string bagColor, string findBagColor)
			{
				if (!Rules.ContainsKey(bagColor))
					return 0;

				BagRule bagRule = Rules[bagColor];
				if (bagRule.BagColorCount.ContainsKey(findBagColor))
					return bagRule.BagColorCount[findBagColor];
				else
				{
					int count = 0;
					foreach (Contains containedRule in bagRule.ContainsRules)
					{
						if (containedRule.Color == findBagColor)
							return containedRule.Quantity;

						count += ContainsBags(containedRule.Color, findBagColor);
					}
					bagRule.BagColorCount.Add(findBagColor, count);
					return count;
				}
			}

			public List<string> Colors { get => Rules.Select(c => c.Key).ToList(); }
		}

		public class BagRule
		{
			public BagRule(string rule)
			{
				Color = rule.Split("bags contain").First().Trim();
				BagColorCount = new Dictionary<string, int>();
				ContainsRules = new List<Contains>();
				foreach (string r in rule.Split("bags contain").ElementAtOrDefault(1).Split(","))
				{
					if (r.Trim() != "no other bags.")
					{
						string quantity = r.Trim().Split(" ").First();
						string color = r.Replace(quantity, "").Trim();
						color = color.Replace("bags", "").Replace("bag", "").Replace(".", "").Trim();
						color = color.Trim();

						Contains contains = new Contains
						{
							Quantity = quantity.ToInt(),
							Color = color
						};
						ContainsRules.Add(contains);
					}
				}

			}


			public Dictionary<string, int> BagColorCount;
			public string Color { get; set; }
			public List<Contains> ContainsRules { get; set; }


		}

		public class Contains
		{
			public string Color { get; set; }
			public int Quantity { get; set; }
		}
	}
}
