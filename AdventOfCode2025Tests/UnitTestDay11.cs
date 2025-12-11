using AdventOfCode2025;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay11
    {
        private Day11 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {

        }


        [TestMethod("Day 11, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            data = @"aaa: you hhh
you: bbb ccc
bbb: ddd eee
ccc: ddd eee fff
ddd: ggg
eee: out
fff: out
ggg: out
hhh: ccc fff iii
iii: out"; 
            day = new Day11(data);
           long result = day.Problem1();
            Assert.AreEqual(5, result);
        }

        [TestMethod("Day 11, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            data = @"svr: aaa bbb
aaa: fft
fft: ccc
bbb: tty
tty: ccc
ccc: ddd eee
ddd: hub
hub: fff
eee: dac
dac: fff
fff: ggg hhh
ggg: out
hhh: out"; 
            day = new Day11(data);
 
            long result = day.Problem2();

            Assert.AreEqual(2, result);
        }
    }
}
