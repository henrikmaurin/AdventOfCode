using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day09 : DayBase, IDay
	{
		List<long> data;

		public Day09() : base(2020, 9)
		{
			data = input.GetDataCached().SplitOnNewline().ToLong();
		}

		public void Run()
		{
			long result1 = Problem1();
			Console.WriteLine($"P1: Number: {result1}");

			long result2 = Problem2();
			Console.WriteLine($"P2: Encryption weakness: {result2}");
		}
		public long Problem1()
		{
			Sequence sequence = new Sequence(25);

			long result = sequence.FirstNonCompliant(data);

			return result;
		}

		public long Problem2()
		{
			Sequence sequence = new Sequence(25);

			long result = sequence.FindWeakness(sequence.FirstNonCompliant(data), data);

			return result;
		}

		public class Sequence
		{
			private int _size { get; set; }
			private List<long> list;

			public Sequence(int size = 25)
			{
				list = new List<long>();
				_size = size;
			}

			public bool Add(long value)
			{
				bool isCompliant = Validate(value);


				list.Add(value);
				if (list.Count > _size)
					list.RemoveAt(0);

				return isCompliant;
			}

			public long FindWeakness(long weaknessnumber, List<long> values)
			{
				long result = 0;
				int count = 0;
				while (result == 0 && count < values.Count)
				{
					result = AddAndFindWeakness(weaknessnumber, values[count++]);
				}
				return result;
			}

			public long AddAndFindWeakness(long weaknessnumber, long value)
			{
				long acc = 0;
				int count = 0;
				for (int i = list.Count - 1; i > 0; i--)
				{
					acc += list[i];
					count++;
					if (acc == weaknessnumber)
					{
						if (count == 1)
							i = 0;
						else
							return list.Skip(i).Min() + list.Skip(i).Max();
					}
					if (acc > weaknessnumber)
						i = 0;
				}
				Add(value);

				return 0;
			}

			public long FirstNonCompliant(List<long> values)
			{
				foreach (long value in values)
					if (!Add(value))
						return value;
				return 0;
			}

			public bool Validate(long value)
			{
				if (list.Count < _size)
					return true;

				for (int value1 = 0; value1 < _size - 1; value1++)
					for (int value2 = value1 + 1; value2 < _size; value2++)
						if (list[value1] + list[value2] == value)
							return true;

				return false;
			}
		}
	}
}
