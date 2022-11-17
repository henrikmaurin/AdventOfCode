using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day13 : DayBase, IDay
	{
		private List<string> data;
		public Day13() : base(2020, 13)
		{
			data = input.GetDataCached().SplitOnNewline();
		}

		public void Run()
		{
			long result1 = Problem1();
			Console.WriteLine($"P1: Bus ID: {result1}");

			long result2 = Problem2();
			Console.WriteLine($"P2: Timestamp: {result2}");
		}
		public long Problem1()
		{
			int earliest = data[0].ToInt();
			List<string> ids = data[1].Split(",").ToList();

			BusLines busslines = new BusLines();

			long result = busslines.ParseAndGetAnswer(earliest, ids);
			Console.WriteLine(result);
			return result;
		}

		public long Problem2()
		{
			List<string> ids = data[1].Split(",").ToList();

			BusLines busslines = new BusLines();

			long result = busslines.FindSequence2(ids);
			Console.WriteLine(result);
			return result;
		}

		public class BusLines
		{
			private List<Bus> _busses;

			public long ParseAndGetAnswer(long earliest, List<string> Ids)
			{
				List<int> idList = Ids.Where(i => i.IsNumber()).Select(i => i.ToInt()).ToList();

				int lowestId = FindEarliestDepartureId(earliest, idList);
				long earliestDeparture = FindEarliestDeparture(earliest, lowestId);

				long result = (earliestDeparture - earliest) * lowestId;

				return result;
			}

			public long FindSequence(List<string> Ids)
			{
				ParseBusses(Ids);
				long timestamp = 0;

				long timejunmp = 0;

				bool hasChanges = true;

				while (hasChanges)
				{
					hasChanges = false;
					foreach (Bus bus in _busses)
					{
						if (!bus.Matches(timestamp))
						{
							bus.SetEarliest(timestamp);
							hasChanges = true;
							timestamp = bus.EarliestFromTimestamp;
						}
					}
				}

				return timestamp;
			}

			public long FindSequence2(List<string> Ids)
			{
				ParseBusses(Ids);
				long timestamp = 1;
				long timejump = 1;

				foreach (Bus bus in _busses)
				{
					while (!bus.Matches(timestamp))
					{
						timestamp += timejump;
					}
					timejump *= bus.Id;
				}

				return timestamp;
			}

			public void ParseBusses(List<string> ids)
			{
				int counter = 0;
				_busses = new List<Bus>();
				foreach (string id in ids)
				{
					if (id != "x")
						_busses.Add(new Bus
						{
							Id = id.ToInt(),
							Offset = counter
						});
					counter++;
				}
			}

			public int FindEarliestDepartureId(long earliest, List<int> Ids)
			{
				int earliestId = 0;
				long earliestTime = long.MaxValue;
				foreach (int id in Ids)
				{
					long earliestPossible = FindEarliestDeparture(earliest, id);
					if (earliestPossible < earliestTime)
					{
						earliestTime = earliestPossible;
						earliestId = id;
					}
				}
				return earliestId;
			}

			public long FindEarliestDeparture(long earliest, int id, double offset = 0.0)
			{
				return (long)(Math.Ceiling((earliest + offset) / id) * id);
			}
		}

		public class Bus
		{
			public int Id { get; set; }
			public int Offset { get; set; }
			public long Earliest { get; set; }
			public bool MatchesTimestamp { get; set; }
			public long EarliestFromTimestamp { get => Earliest - Offset; }
			public bool Matches(long timestamp)
			{
				return (timestamp + Offset) % Id == 0;
			}

			public void SetEarliest(long fromTimestamp)
			{
				Earliest = (long)(Math.Ceiling((fromTimestamp + (double)Offset) / Id) * Id);
				if (Earliest == fromTimestamp + Offset)
					MatchesTimestamp = true;
				else
					MatchesTimestamp = false;
			}
		}
	}
}
