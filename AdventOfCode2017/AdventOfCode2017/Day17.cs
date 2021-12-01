using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Days
{
    public class Day17 :AdventOfCode2017
    {
        public LinkedList<int> Spinlock { get; set; }
        public Day17()
        {
            Spinlock = new LinkedList<int>();
            Spinlock.AddFirst(0);
        }

        public void Problem1()
        {
            Console.WriteLine("Problem 1");
            int jump = 314;
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

            Console.WriteLine($"Next Value {node.NextCircular().Value}");
        }

        public void Problem2()
        {
            Console.WriteLine("Problem 2");
            int jump = 314;
            int value = 1;
            //jump = 3;

            int nextValue = 0;
            int pos=0;
            int size=1;
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







            LinkedListNode<int> node = Spinlock.First;
            LinkedListNode<int> nodeZero = Spinlock.First;

            while (value <= 50000000)
            {
                for (int i = 0; i < jump; i++)
                {
                    node = node.NextCircular();
                }

                node = Spinlock.AddAfter(node, value++);
            }
                                  

            Console.WriteLine($"Next Value {nodeZero.NextCircular().Value}");
        }


    }
}
