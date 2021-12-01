using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day9 : AdventOfCode2018
    {
        public Day9()
        {
            marbles = new LinkedList<int>();
            players = 471;
            //players = 10;
            score = new ulong[players];
            goalscore = 72026;
            //goalscore = 1618;
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

            LinkedListNode<int> currentNode = marbles.AddFirst(0);

            while (currentmarble != targetValue + 1)
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

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            Console.WriteLine($"Score: {Play(goalscore)}");
        }
        public void Problem2()
        {
            Console.WriteLine("Problem 2");
            Console.WriteLine($"Score: {Play(goalscore * 100)}");
        }


    }


}
