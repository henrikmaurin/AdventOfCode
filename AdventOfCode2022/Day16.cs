using Combinatorics.Collections;
using Common;
using Dijkstra.NET.Graph.Simple;
using Dijkstra.NET.ShortestPath;
using System.Text.RegularExpressions;
using System.Windows.Markup;

namespace AdventOfCode2022
{
    public class Day16 : DayBase, IDay
    {
        private const int day = 16;
        List<string> data;

        private Dictionary<uint, Valve> valves;
        private Dictionary<string, uint> valvesByName;
        private Graph graph;
        private Dictionary<string, int> TravelCache = new Dictionary<string, int>();

        private int bestScore = 0;


        public Day16(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            Parse(data);
            return Travel("AA", HasPressure(), new KeepScore { Score = 0, TicksLeft = 30 });
        }
        public int Problem2()
        {
            return UseElephant();
        }

        public int UseElephant()
        {
            string[] valvesWithPreassure = HasPressure();
            int best = 0;

            for (int meCount = 1; meCount <= valvesWithPreassure.Count() / 2; meCount++)
            {            
                Combinations<string> combinations = new Combinations<string>(valvesWithPreassure, meCount);
                Console.WriteLine($"Visiting {meCount} valves with {combinations.Count} combinations");


                foreach (var combination in combinations)
                {
                    string[] elephantValves = valvesWithPreassure.Where(v => !v.In(combination.ToArray())).ToArray();
                    bestScore = 0;
                    int mescore = Travel("AA", combination.ToArray(), new KeepScore { Score = 0, TicksLeft = 26 });
                    bestScore = 0;
                    int elephantscore = Travel("AA", elephantValves, new KeepScore { Score = 0, TicksLeft = 26 });
                    int score = mescore + elephantscore;
                    if (score > best) best = score;
                }
            }
            return best;
        }


        public int Travel(string from, string[] leftToVisit, KeepScore score)
        {
            if (TravelCache.ContainsKey($"{from}, {string.Join(",", leftToVisit)}, {score.TicksLeft}:{score.Score}"))
            {
                int lscore = TravelCache[$"{from}, {string.Join(",", leftToVisit)}, {score.TicksLeft}:{score.Score}"];

                if (lscore > bestScore)
                    bestScore = lscore;

            }
            else
            {
                foreach (string visit in leftToVisit)
                {
                    KeepScore scoreForThis = CalcGoTo(from, visit, score);
                    string[] elephantLeftToVisit = leftToVisit.Where(v => v != visit).ToArray();


                    if (scoreForThis.TicksLeft >= 0)
                    {
                        if (scoreForThis.Score > bestScore)
                        {
                            bestScore = scoreForThis.Score;
                        }

                        if (leftToVisit.Count() > 1)
                        {
                            string[] visitList = leftToVisit.Where(v => v != visit).ToArray();
                            Travel(visit, visitList, scoreForThis);
                        }

                    }
                }
            }

            return bestScore;
        }

        Dictionary<(uint, uint), int> Distances;

        public KeepScore CalcGoTo(string from, string to, KeepScore score)
        {
            uint fromIndex = valvesByName[from];
            uint toIndex = valvesByName[to];

            int distance = 0;

            if (Distances.ContainsKey((fromIndex, toIndex)))
            {
                distance = Distances[(fromIndex, toIndex)];
            }
            else
            {
                distance = graph.Dijkstra(fromIndex, toIndex).Distance;
                Distances.Add((fromIndex, toIndex), distance);
            }
            int newTicksLeft = score.TicksLeft - distance - 1;

            return new KeepScore { TicksLeft = newTicksLeft, Score = score.Score + newTicksLeft * valves[toIndex].Flowrate };
        }

        public string[] HasPressure()
        {
            return valves.Where(v => v.Value.Flowrate > 0).Select(v => v.Value.Name).ToArray();
        }

        public class KeepScore
        {
            public int TicksLeft { get; set; }
            public int Score { get; set; }
        }

        public void Parse(List<string> valvedata)
        {
            bestScore = 0;
            valves = new Dictionary<uint, Valve>();
            valvesByName = new Dictionary<string, uint>();
            uint index = 0;
            graph = new Graph();
            Distances = new Dictionary<(uint, uint), int>();
            TravelCache = new Dictionary<string, int>();
            foreach (string valve in valvedata)
            {
                string[] split = valve.Replace(";", "").Replace(",", "").Replace("=", " ").Split(" ");
                Valve newValve = new Valve
                {
                    Name = split[1],
                    Flowrate = split[5].ToInt(),
                    TunnelsTo = split[10..].ToList(),
                };
                index = graph.AddNode();
                valves.Add(index, newValve);
                valvesByName.Add(newValve.Name, index);
            }

            foreach (uint from in valves.Keys)
            {
                foreach (string tunnel in valves[from].TunnelsTo)
                {
                    uint to = valves.Where(v => v.Value.Name == tunnel).Single().Key;
                    graph.Connect(from, to, 1);
                }
            }
        }
    }

    public class Valve
    {
        public string Name { get; set; }
        public int Flowrate { get; set; }
        public List<string> TunnelsTo { get; set; }

    }
}
