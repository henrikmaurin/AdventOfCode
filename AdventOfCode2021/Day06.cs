using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day06 : DayBase
    {
        Fish fishes;
        public Day06() : base() { fishes = new Fish(); }

        public long Problem1()
        {
            int[] data = input.GetDataCached(2021, 6).Split(",").ToInt();

            long total = 0;
            int days = 80;

            foreach (int i in data)
            {
                total += fishes.GetSpawns(i, days);
            }

            return total;
        }

        public long Problem2()
        {
            int[] data = input.GetDataCached(2021, 6).Split(",").ToInt();

            long total = 0;
            int days = 256;

            foreach (int i in data)
            {
                total += fishes.GetSpawns(i, days);
            }

            return total;
        }
    }

    public  class Fish
    {
        Dictionary<string, long> cache = new Dictionary<string, long>();


        public long GetSpawns(int timer, int days)
        {
            string key = $"{timer}#{days}";

            if (cache.ContainsKey(key))
                return cache[key];

            long total = 1;
            while (days > 0)
            {
                timer--;
                days--;
                if (timer<0)
                {
                    timer = 6;
                    total += GetSpawns(8, days);
                }
                ;
                
            }
            cache.Add(key, total);  
            return total;           
        }

    }
}
