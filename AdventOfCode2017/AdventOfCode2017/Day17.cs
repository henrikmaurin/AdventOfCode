using AdventOfCode;
using Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    public class Day17 : DayBase, IDay
    {
        private int jump;
        public LinkedList<int> Spinlock { get; set; }
        public Day17() : base(2017, 17)
        {
            jump = input.GetDataCached().IsSingleLine().ToInt();
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Next Value: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Next Value: {result2}");
        }

        public int Problem1()
        {
            Spinlock = new LinkedList<int>();
            Spinlock.AddFirst(0);

            int value = 1;

            LinkedListNode<int> node = Spinlock.First;

            while (value <= 2017)
            {
                for (int i = 0; i < jump; i++)
                {
                    node = node.NextCircular();
                }

                node = Spinlock.AddAfter(node, value++);
            }

            return node.NextCircular().Value;
        }

        public int Problem2()
        {
            Spinlock = new LinkedList<int>();
            Spinlock.AddFirst(0);

            int value = 1;
            //jump = 3;

            int nextValue = 0;
            int pos = 0;
            int size = 1;

            while (value <= 50000000)
            {
                pos += jump;
                pos %= size;
                if (pos == 0)
                    nextValue = value;
                pos++;
                value++;
                size++;
            }

            return nextValue;

        }


    }
}
