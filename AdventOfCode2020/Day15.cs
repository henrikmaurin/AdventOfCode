using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
	public class Day15
	{
		public static int Problem1()
		{
			List<int> input = File.ReadAllText("Data/Day15.txt").Split(",").Select(i => i.ToInt()).ToList();


			ElfGame game = new ElfGame();

			foreach (int number in input)
				game.AddNumber(number);

			int result = game.GetNthNumber(2020);
			Console.WriteLine(result);

			
			return result;
		}

		public static int Problem2()
		{
			List<int> input = File.ReadAllText("Data/Day15.txt").Split(",").Select(i => i.ToInt()).ToList();

			ElfGame game = new ElfGame();
			
			int result = game.GetNthNumber2(input,30000000);
			Console.WriteLine(result);
			
			return result;
		}


		public class ElfGame
		{
			public LinkedList<int> numbers;
			Dictionary<int, int> numbersDictionary;
			int Max = 0;

			public int GetNthNumber(int n)
			{
				int counter = numbers.Count;
				while (counter < n)
				{
					AddNumber(Findsteps());
					counter++;
				}
				return numbers.Last.Value;
			}

			public int GetNthNumber2(List<int> numbers, int iterations)
			{
				int counter = 1;
				int lastVal = 0;
				foreach (int number in numbers)
				{
					lastVal = AddNumber2(number, counter++);
				}
				while (counter < iterations)
				{
					lastVal = AddNumber2(lastVal, counter++);

				}

				return lastVal;

			}

			public int AddNumber2(int number, int currentCounter)
			{
				int pos;
				if (numbersDictionary.ContainsKey(number))
				{
					pos = numbersDictionary[number];
					numbersDictionary[number] = currentCounter;
					return currentCounter - pos;
				}
				else
				{
					numbersDictionary.Add(number, currentCounter);
				}

				return 0;
			}


			public ElfGame()
			{
				numbers = new LinkedList<int>();
				numbersDictionary = new Dictionary<int, int>();
			}

			public void AddNumber(int number)
			{
				numbers.AddLast(number);

			}

			public int Findsteps()
			{
				int counter = 1;
				int find = numbers.Last();
				if (find > Max)
					return 0;


				LinkedListNode<int> node = numbers.Last;
				node = node.Previous;
				while (node.Value != find && node != numbers.First)
				{
					node = node.Previous;
					counter++;
				}
				if (node.Value == find)
				{
					if (counter > Max)
						Max = counter;
					return counter;
				}
				return 0;
			}



		}


	}
}
