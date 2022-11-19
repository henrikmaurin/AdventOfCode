using Common;
using System.Collections.Generic;

namespace AdventOfCode2016
{
    public class Day11 : DayBase, IDay
    {
        private List<string> lines;
        public Day11() : base(2016, 11) { lines = input.GetDataCached().SplitOnNewline(true); }
        public void Run()
        {

        }

        enum Isotopes
        {
            Hydrogen = 1,
            Lithium = 2
        }


    }
}
