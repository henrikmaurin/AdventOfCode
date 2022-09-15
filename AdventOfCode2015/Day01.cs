using AdventOfCode;
using Common;

namespace AdventOfCode2015
{
    public class Day01 : DayBase, IDay
    {
        public Day01() : base(2015, 1) { }
        public int Problem1()
        {
            //string data = ReadFile.ReadText("Day01.txt");
            string data = input.GetDataCached();

            return Travel(data);
        }
        public int Problem2()
        {
            string data = input.GetDataCached();

            return TravelTo(data, -1);
        }

        public void Run()
        {
            int finalFloor = Problem1();
            Console.WriteLine($"P1: Santa ends up on floor: {finalFloor}");

            int position = Problem2();
            Console.WriteLine($"P2: Santa ends up in basemant at position: {position}");
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
