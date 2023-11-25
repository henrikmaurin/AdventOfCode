using Common;

using System;
using System.Collections.Generic;
using System.Linq;

using static Common.Parser;

namespace AdventOfCode2018
{
    public class Day09 : DayBase, IDay
    {
        private const int day = 9;
        private string data;

        public LinkedList<int> marbles { get; set; }
        public ulong[] score { get; set; }
        public int players { get; set; }
        public int goalscore { get; set; }

        public Day09(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.IsSingleLine();
                return;
            }
            data = input.GetDataCached().IsSingleLine();

            Parsed parsed = new Parsed();
            parsed.Parse(data);

            players = data[0].ToInt();
            goalscore = data[6].ToInt();
        }
        public void Run()
        {
            ulong result1 = Problem1();
            Console.WriteLine($"P1: {result1}");

            ulong result2 = Problem2();
            Console.WriteLine($"P2: {result2}");
        }
        public ulong Problem1()
        {
            Parsed parsed = new Parsed();
            parsed.Parse(data);
            return Play(parsed.NumberOfPlayers,parsed.GoalValue);
        }
        public ulong Problem2()
        {
            Parsed parsed = new Parsed();
            parsed.Parse(data);
            return Play(parsed.NumberOfPlayers, parsed.GoalValue * 100);
        }

        public ulong Play(int players, int targetValue)
        {
            int currentmarble = 1;
            int currentelf = 0;
            int marble = 0;
            score = new ulong[players];
            marbles = new LinkedList<int>();


            LinkedListNode<int> currentNode = marbles.AddFirst(0);

            while (currentmarble <= targetValue)
            {
                if (currentmarble % 23 == 0)
                {
                    currentNode = currentNode.PreviousCircular(7);
                    marble = currentmarble + currentNode.Value;

                    LinkedListNode<int> nextNode;
                    nextNode = currentNode.NextCircular();
                    marbles.Remove(currentNode);
                    currentNode = nextNode;

                    score[currentelf] += (ulong)marble;
                }
                else
                {
                    currentNode = currentNode.NextCircular(2);

                    currentNode = marbles.AddBefore(currentNode, currentmarble);
                }

                currentmarble++;
                currentelf++;
                currentelf = currentelf % players;
            }
            return score.Max();
        }

        public class Parsed : IInDataFormat
        {
            public string DataFormat => @"(\d+) players; last marble is worth (\d+) points";
            public string[] PropertyNames => new string[] { nameof(NumberOfPlayers), nameof(GoalValue) };
            public int NumberOfPlayers { get; set; }
            public int GoalValue { get; set; }

        }
    }
}
