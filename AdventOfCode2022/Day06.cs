using Common;

namespace AdventOfCode2022
{
    public class Day06 : DayBase, IDay
    {
        private const int day = 6;
        
        string data;
        public Day06(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.IsSingleLine();
                return;
            }

            data = input.GetDataCached().IsSingleLine();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            return FindPacketMarker(4);
        }
        public int Problem2()
        {
            return FindPacketMarker(14);
        }

        public int FindPacketMarker(int markerSize)
        {
            for (int i = 0; i < data.Length-markerSize; i++)
            {
                if (data.ToArray().Skip(i).Take(markerSize).Distinct().Count() == markerSize)
                    return i + markerSize;
            }
            return 0;
        }
    }
}
