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
            return Formula1(data);
        }
        public int Problem2()
        {      
            return Formula2(data);
        }

        public void Run()
        {
            int finalFloor = Problem1();
            Console.WriteLine($"P1: Lowest house number: {finalFloor}");

            int position = Problem2();
            Console.WriteLine($"P2: Santa ends up in basemant at position: {position}");
        }

        public int Formula1(int target)
        {
            int housenumber = 1;
            while ( housenumber < target/10 )
            {
                var factors = MathHelpers.GetFactors(housenumber );
                int sum = factors.Sum();
                if (sum*10>target)
                    return housenumber;
                housenumber++;
            }
            return 0;
        }

        public int Formula2(int target)
        {
            int housenumber = 1;
            while (housenumber < target / 10)
            {
                var factors = MathHelpers.GetFactors(housenumber);
                int sum = factors.Where(f=>f*50>=housenumber).Sum();
                if (sum*11 > target)
                    return housenumber;
                housenumber++;
            }
            return 0;
        }


    }
}
