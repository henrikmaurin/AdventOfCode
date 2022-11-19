using Common;
using Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
	public class Day12 : DayBase, IDay
	{

		private List<string> data;
		public Day12() : base(2020, 12)
		{
			data = input.GetDataCached().SplitOnNewline();
		}
		public void Run()
		{
			long result1 = Problem1();
			Console.WriteLine($"P1: Distance: {result1}");

			long result2 = Problem2();
			Console.WriteLine($"P2: Distance: {result2}");
		}
		public long Problem1()
		{
			Navigation nav = new Navigation();

			long result = nav.RunInstructions(data);
			Console.WriteLine(result);
			return result;
		}

		public long Problem2()
		{
			Navigation nav = new Navigation();

			long result = nav.RunInstructions2(data);
			Console.WriteLine(result);
			return result;
		}

		public class Navigation
		{
			private long _posX = 0;
			private long _posY = 0;

			private Waypoint _waypoint;

			public override string ToString()
			{
				string retString = string.Empty;
				if (_posX < 0)
					retString += "east ";
				else
					retString += "west ";
				retString += Math.Abs(_posX) + ", ";
				if (_posY < 0)
					retString += "north ";
				else
					retString += "south ";
				retString += Math.Abs(_posY);

				return retString;
			}

			public void SetWaypoint(int x, int y)
			{
				_waypoint = new Waypoint
				{
					X = x,
					Y = y,
				};
			}

			public long ManhattanDistance { get => Math.Abs(_posX) + Math.Abs(_posY); }
			public Waypoint CurrentWaypoint { get => _waypoint; }

			public long RunInstructions(List<string> instructions)
			{
				SetWaypoint(1, 0);
				foreach (string instruction in instructions)
				{
					Decode(instruction);
				}
				return ManhattanDistance;
			}

			public long RunInstructions2(List<string> instructions)
			{
				SetWaypoint(10, -1);
				foreach (string instruction in instructions)
				{
					DecodeWaypoints(instruction);
				}
				return ManhattanDistance;
			}

			public void Decode(string instruction)
			{
				string command = instruction.Substring(0, 1);
				int value = instruction.Substring(1).ToInt();
				switch (command)
				{
					case "N":
						_posY -= value;
						break;
					case "E":
						_posX += value;
						break;
					case "S":
						_posY += value;
						break;
					case "W":
						_posX -= value;
						break;
					case "R":
					case "L":
						Rotate(instruction);
						break;
					case "F":
						_posX += _waypoint.X * value;
						_posY += _waypoint.Y * value;
						break;
				}
			}

			public void DecodeWaypoints(string instruction)
			{
				string command = instruction.Substring(0, 1);
				int value = instruction.Substring(1).ToInt();
				switch (command)
				{
					case "N":
						_waypoint.Y -= value;
						break;
					case "E":
						_waypoint.X += value;
						break;
					case "S":
						_waypoint.Y += value;
						break;
					case "W":
						_waypoint.X -= value;
						break;
					case "R":
					case "L":
						Rotate(instruction);
						break;
					case "F":
						_posX += _waypoint.X * value;
						_posY += _waypoint.Y * value;
						break;
				}
			}

			private void Rotate(string instruction)
			{
				int newX = 0;
				int newY = 0;

				switch (instruction)
				{
					case "L90":
					case "R270":
						newX = _waypoint.Y;
						newY = -_waypoint.X;
						break;
					case "L180":
					case "R180":
						newX = -_waypoint.X;
						newY = -_waypoint.Y;
						break;
					case "L270":
					case "R90":
						newX = -_waypoint.Y;
						newY = _waypoint.X;
						break;
				}

				_waypoint.X = newX;
				_waypoint.Y = newY;
			}

			public class Waypoint
			{
				public int X { get; set; }
				public int Y { get; set; }
			}

		}
	}
}
