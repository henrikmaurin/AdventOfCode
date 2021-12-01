using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day01
    {
        public int Problem1()
        {
            string data = ReadFile.ReadText("Day01.txt");

            return Travel(data);
        }
        public int Problem2()
        {
            string data = ReadFile.ReadText("Day01.txt");

            return TravelTo(data, -1);
        }


        public int Travel(string directions)
        {
            return directions.ToCharArray().Where(a => a == '(').Count() - directions.ToCharArray().Where(a => a == ')').Count();
        }

        public int TravelTo(string directions, int target)
        {
            int steps = 0;
            int level = 0;
            while (level != -1)
            {

                if (directions[steps] == '(')
                    level++;
                else if (directions[steps] == ')')
                    level--;
                steps++;
            }

            return steps;
        }
    }
}
