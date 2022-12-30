using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay13
    {
        private Day13 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   ";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day13(data);
        }


        [TestMethod("Day 13, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            day.InitMap();
            Assert.AreEqual("7,3", day.RunUntilCrash().ToString());
        }

        [TestMethod("Day 13, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            data = @"/>-<\  
|   |  
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/";
            day = new Day13(data);
            day.InitMap();
            Assert.AreEqual("6,4", day.RunUntilLastCrash().ToString());
        }
    }
}
