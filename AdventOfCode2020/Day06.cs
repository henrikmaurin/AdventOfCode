using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day06 : DayBase, IDay
	{
		private List<string> data;
		public Day06() : base(2020, 6)
		{
			data = input.GetDataCached().SplitOnNewline(false);
		}

		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Sum: {result1}");

			int result2 = Problem2();
			Console.WriteLine($"P2: Sum: {result2}");
		}
		public int Problem1()
		{
			int result = Process(data);

			return result;
		}

		public int Problem2()
		{
			int result = Process2(data);

			return result;
		}

		public static int Process(List<string> answers)
		{
			int sum = 0;
			Declaration declaration = null;
			foreach (string line in answers)
			{
				if (declaration == null)
					declaration = new Declaration();

				if (string.IsNullOrWhiteSpace(line))
				{
					sum += declaration.Sum();
					declaration = null;
				}
				else
				{
					declaration.Fill(line);
				}
			}
			if (declaration != null)
				sum += declaration.Sum();

			return sum;
		}

		public static int Process2(List<string> answers)
		{
			int sum = 0;
			Declaration declaration = null;
			foreach (string line in answers)
			{
				if (declaration == null)
					declaration = new Declaration();

				if (string.IsNullOrWhiteSpace(line))
				{
					sum += declaration.SumAllYes();
					declaration = null;
				}
				else
				{
					declaration.Fill(line, Declaration.Mode.Add);
				}
			}
			if (declaration != null)
				sum += declaration.SumAllYes();

			return sum;
		}

		public class Declaration
		{
			public Declaration()
			{
				Answers = new int[26];
				Passengers = 0;
			}

			public void Set(char question)
			{
				Answers['z' - question] = 1;
			}

			public void Add(char question)
			{
				Answers['z' - question] += 1;
			}

			public void Fill(string answers, Mode mode = Mode.Set)
			{
				Passengers++;
				foreach (char answer in answers)
					if (mode == Mode.Set)
						Set(answer);
					else
						Add(answer);
			}

			public enum Mode
			{
				Set = 0,
				Add = 1
			}

			public int Sum() { return Answers.Sum(); }
			public int SumAllYes() { return Answers.Where(a => a == Passengers).Count(); }

			private int[] Answers { get; set; }
			private int Passengers { get; set; }
		}
	}
}
