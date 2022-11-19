using Common;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day09 : DayBase, IDay
    {
        public Day09() : base(2018, 9)
        {
            string[] data = input.GetDataCached().IsSingleLine().Split(" ");

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

        public LinkedList<int> marbles { get; set; }
        public ulong[] score { get; set; }
        public int players { get; set; }
        public int goalscore { get; set; }

        public ulong Play(int targetValue)
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

        public ulong Problem1()
        {
            return Play(goalscore);
        }
        public ulong Problem2()
        {
            return Play(goalscore * 100);
        }


    }


}
