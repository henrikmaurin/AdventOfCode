using Common;
using System.Net.Sockets;

namespace AdventOfCode2022
{
    public class Day20 : DayBase, IDay
    {
        private const int day = 20;
        List<string> data;
        LinkedList<Node> coords;
        List<Node> order;
        public Day20(string testdata=null) : base(Global.Year, day, testdata != null)
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
            long result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            long result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public long Problem1()
        {
            Parse(data);
            return Mix();
        }
        public long Problem2()
        {
            Parse(data, 811589153);
            for (int i = 0; i < 9; i++)
                Mix(); 
            return Mix();
        }

        public long Mix()
        {
            foreach (Node node in order) {
                var coord=coords.Find(node);
                long n = coord.Value.x;
                LinkedListNode<Node> foundnode = coord;

                long steps = (Math.Abs(n) % (coords.LongCount()-1)) ;

                Node tempNode = new Node { x = long.MaxValue };
                if (n > 0)
                {
                    foundnode = coord.NextCircular();
                    coords.Remove(coord);
                    for  (int i= 0; i< steps ; i++)
                    {
                        foundnode = foundnode.NextCircular();                                       
                    }
                    coords.AddBefore(foundnode, coord);
                }
                if (n < 0)
                {
                    foundnode = coord.PreviousCircular();
                    coords.Remove(coord);
                    for (int i = 0; i < steps; i++)
                    {
                        foundnode = foundnode.PreviousCircular();
                    }
                    coords.AddAfter(foundnode, coord);
                }
            }
            long sum = 0;
            Node nodeZero = order.Where(o=>o.x==0).FirstOrDefault();
            

            LinkedListNode<Node> c = coords.Find(nodeZero);
            for (int n = 0; n < 3; n++)
            {
                for (int i = 0; i < 1000; i++)
                    c = c.NextCircular();
                sum += c.Value.x;
            }
            return sum;
                  }

        public void Parse(List<string> numbers,int multiplier = 1)
        {
            coords= new LinkedList<Node>();
            order = new List<Node>();
            foreach(string number in numbers) {
                Node n = new Node { x = number.ToLong() * multiplier};
                coords.AddLast(n);
                order.Add(n);            
            }
        }

    }
    class Node
    {
        public long x;
    }
}
