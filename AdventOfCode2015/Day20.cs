using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day20 : DayBase, IDay
    {
        private const int day = 20;
        private int data;
        private Dictionary<int, int> ToAdd ;
        public Day20(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().IsSingleLine().ToInt();
            ToAdd= new Dictionary<int, int>();
        }
        public int Problem1()
        {
            return Formula(data);
        }
        public int Problem2()
        {
            string data = input.GetDataCached();

            return 0;
        }

        public void Run()
        {
            int finalFloor = Problem1();
            Console.WriteLine($"P1: Santa ends up on floor: {finalFloor}");

            int position = Problem2();
            Console.WriteLine($"P2: Santa ends up in basemant at position: {position}");
        }

        public int Formula(int target)
        {
            int number = 1;
            int counter = 1;
            while (number<target/10)
            {
                number *= counter++;
            }
            return counter;            
        }

       
    }
}
