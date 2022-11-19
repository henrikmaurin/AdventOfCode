using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day05 : DayBase, IDay
	{
		private List<Seat> seats;
		public Day05() : base(2020, 5)
		{
			seats = input.GetDataCached().SplitOnNewline().Select(l => new Seat(l)).ToList();
		}

		public void Run()
		{
			int result1 = Problem1();
			Console.WriteLine($"P1: Highest ID: {result1}");

			int result2 = Problem2();
			Console.WriteLine($"P2: My Seat ID: {result2}");
		}

		public int Problem1()
		{
			int result = seats.Select(s => s.Id).Max();
			return result;
		}

		public int Problem2()
		{
			for (int row = 0; row <= 127; row++)
			{
				if (seats.Where(s => s.Row == row).Count() == 7)
				{
					for (int col = 0; col <= 7; col++)
					{
						if (!seats.Where(s => s.Column == col && s.Row == row).Any())
						{
							Seat seat = new Seat { Column = col, Row = row };
							return seat.Id;
						}
					}
				}
			}
			return -1;
		}

		public static Seat Decode(string code)
		{
			Seat seat = new Seat();

			seat.Row = 0;
			seat.Column = 0;
			for (int i = 0; i < 7; i++)
			{
				if (code[i] == 'B')
				{
					seat.Row += (int)Math.Pow(2, 6 - i);
				}
			}

			for (int i = 0; i < 3; i++)
			{
				if (code[i + 7] == 'R')
				{
					seat.Column += (int)Math.Pow(2, 2 - i);
				}
			}

			return seat;
		}

		public class Seat
		{
			public Seat()
			{

			}

			public Seat(string code)
			{
				Seat seat = Decode(code);
				this.Column = seat.Column;
				this.Row = seat.Row;
			}

			public override string ToString()
			{
				return $"Row: {Row}, Column: {Column}, Id: {Id}";
			}

			public int Row { get; set; }
			public int Column { get; set; }
			public int Id { get => Row * 8 + Column; }
		}
	}
}
