using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day16 : DayBase, IDay
	{
		private List<string> data;
		public Day16() : base(2020, 16)
		{
			data = input.GetDataCached().SplitOnNewline();
		}

		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Error rate: {result1}");

			long result2 = Problem2();
			Console.WriteLine($"P2: Product: {result2}");
		}
		public long Problem2()
		{
			TicketValidator validator = new TicketValidator();

			validator.ParseData(data);
			validator.DecodeColumns();

			List<Rule> departureRules = validator.Rules.Where(r => r.Name.StartsWith("departure")).ToList();

			long result = 1;
			foreach (Rule rule in departureRules)
				result *= validator.MyTicket._values.ElementAt(rule.Column);

			return result;
		}

		public int Problem1()
		{
			TicketValidator validator = new TicketValidator();

			validator.ParseData(data);
			int result = validator.ValidateAllNumbers();
			return result;
		}


		public class TicketValidator
		{
			private ICollection<Ticket> _tickets;
			private ICollection<Rule> _rules;
			private Ticket _myTicket;

			public void ParseData(List<string> data)
			{
				string mode = "rules";

				_rules = new List<Rule>();
				_tickets = new List<Ticket>();

				foreach (string row in data)
				{
					if (row.Trim() == "your ticket:")
					{
						mode = "your ticket";
						continue;
					}
					else if (row.Trim() == "nearby tickets:")
					{
						mode = "nearby tickets";
						continue;
					}


					if (!string.IsNullOrEmpty(row))
					{
						switch (mode)
						{
							case "rules":
								_rules.Add(Rule.CreateFromDescription(row));
								break;
							case "your ticket":
								_myTicket = Ticket.CreateFromData(row);
								break;
							case "nearby tickets":
								_tickets.Add(Ticket.CreateFromData(row));
								break;
						}
					}

				}
			}

			public int ValidateAllNumbers()
			{
				int sum = 0;
				List<Ticket> valid = new List<Ticket>();
				bool isValid = false;
				foreach (Ticket ticket in _tickets)
				{
					isValid = true;
					foreach (int value in ticket._values)
					{
						bool hasMatch = false;
						foreach (Rule rule in _rules)
						{
							if (rule.ValidateAgainstAll(value))
								hasMatch = true;
						}
						if (!hasMatch)
						{
							isValid = false;
							sum += value;
						}

					}
					if (isValid)
						valid.Add(ticket);
				}
				_tickets = valid;
				return sum;
			}

			/*
			public void ValidateAllTickets()
			{
				List<Ticket> valid = new List<Ticket>();
				foreach (Ticket ticket in _tickets)
				{
					bool isValid = false;
					foreach (int value in ticket._values)
					{
						foreach (Rule rule in _rules)
						{
							if (rule.ValidateAgainstAll(value))
								isValid = true;
						}

					}

					if (isValid)
						valid.Add(ticket);
				}
				_tickets = valid;
			}
			*/

			public void DecodeColumns()
			{
				ValidateAllNumbers();
				foreach (Rule rule in _rules)
					rule.InitPossibleColumns(_myTicket._values.Count);


				foreach (Rule rule in _rules)
				{
					foreach (Ticket ticket in _tickets)
					{
						for (int ticketField = 0; ticketField < ticket._values.Count; ticketField++)
							if (!rule.ValidateAgainstAll(ticket._values[ticketField]))
							{
								rule.RemovePossibleColumn(ticketField);
								if (rule.Column != -1)
									RemoveColumnFromAll(rule.Column, rule);
							}
					}
				}
			}

			public void RemoveColumnFromAll(int column, Rule rule)
			{
				foreach (Rule r in _rules.Where(ru => ru != rule).Where(ru => ru.Column == -1))
				{
					r.RemovePossibleColumn(column);
					if (r.PossibleColumns.Count == 0)
						throw new Exception("Error");

					if (r.Column != -1)
						RemoveColumnFromAll(r.Column, r);
				}
			}

			public Rule GetRuleForColumn(int i)
			{
				return _rules.Where(r => r.Column == i).SingleOrDefault();
			}

			public List<Rule> Rules { get => _rules.ToList(); }
			public Ticket MyTicket { get => _myTicket; }
		}


		public class Ticket
		{
			public List<int> _values;

			public static Ticket CreateFromData(string data)
			{
				Ticket newTicket = new Ticket();
				newTicket._values = data.Split(",").Select(d => d.ToInt()).ToList();

				return newTicket;
			}
		}

		public class Rule
		{
			public string Name { get; set; }
			public ICollection<Range> ValidRanges { get; set; }
			public List<int> PossibleColumns { get; set; }
			public int Column { get => PossibleColumns?.Count == 1 ? PossibleColumns.First() : -1; }

			public void InitPossibleColumns(int columncount)
			{
				PossibleColumns = new List<int>();
				for (int i = 0; i < columncount; i++)
					PossibleColumns.Add(i);
			}

			public void RemovePossibleColumn(int column)
			{
				PossibleColumns.Remove(column);
			}

			public bool ValidateAgainstAll(int value)
			{
				bool hasMatch = false;
				foreach (Range range in ValidRanges)
				{
					if (range.Match(value))
						hasMatch = true;
				}
				return hasMatch;
			}

			public static Rule CreateFromDescription(string description)
			{
				Rule newRule = new Rule();
				newRule.Name = description.Split(":").First();
				newRule.ValidRanges = new List<Range>();
				foreach (string range in description.Split(":").Last().Split("or"))
				{
					newRule.ValidRanges.Add(Range.Create(range));
				}

				return newRule;
			}
		}

		public class Range
		{
			public int Lower { get; set; }
			public int Upper { get; set; }
			public bool Match(int value)
			{
				return (value >= Lower && value <= Upper);
			}
			public static Range Create(string range)
			{
				Range newRange = new Range();
				newRange.Lower = range.Split("-").First().ToInt();
				newRange.Upper = range.Split("-").Last().ToInt();

				return newRange;
			}

		}

	}
}
