using AdventOfCode;
using Common;

namespace AdventOfCode2015
{
    public class Day09 : DayBase, IDay
    {
        public Day09() : base(2015, 8) { }

        public int GetShortestRoute(string[] distances)
        {
            int shortest = int.MaxValue;


            return shortest;
        }

        public int Visit(string city, List<string> citiesleft, int traveledDistance, int shortest)
        {
            if (citiesleft.Count == 0)
                return traveledDistance;

            foreach (string nextCity in citiesleft)





        }


        public void Run()
        {
            /*int difference = Problem1();
            Console.WriteLine($"P1: String literals minus number characters: {difference}");

            int expandedDifference = Problem2();
            Console.WriteLine($"P2: Expanded chars minus number characters: {expandedDifference}");
            */
        }
    }
}
