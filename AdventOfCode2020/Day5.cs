using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day5
	{
		public static int Problem1()
		{
			List<Seat> seats = File.ReadAllLines("Data/Day5.txt").Select(l => new Seat(l)).ToList();
			int result = seats.Select(s => s.Id).Max();
			Console.WriteLine(result);
			return result;
		}

		public static int Problem2()
		{
			List<Seat> seats = File.ReadAllLines("Data/Day5.txt").Select(l => new Seat(l)).ToList();

			for (int row = 0; row <= 127; row++)
			{
				if (seats.Where(s => s.Row == row).Count() == 7)
				{
					for (int col = 0; col <= 7; col++)
					{
						if (!seats.Where(s => s.Column == col && s.Row == row).Any())
						{
							Seat seat = new Seat { Column = col, Row = row };
							Console.WriteLine(seat.ToString());
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
