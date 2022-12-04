using AdventOfCode2015;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class UnitTestDay17
    {
        private Day17 day;
        private string data;
        private List<int> testdata;
        [TestInitialize]
        public void Init()
        {
            data = @"20
15
10
5
5";
            testdata = data.SplitOnNewline().ToInt();

            day = new Day17(data);
        }


        [TestMethod("Day 17, Example 1")]
        [TestCategory("Example data")]
        public void Example1_1()
        {

            Assert.AreEqual(4, day.FillRefrigerator(new List<int>(), 0, 25));
        }

        [TestMethod("Day 17, Example 2")]
        [TestCategory("Example data")]
        public void Example2_1()
        {
            day.FillRefrigerator(new List<int>(), 0, 25);
            Assert.AreEqual(3, day.GetnumberOfCombinationsUsingLeastAmountOfBuckets());
        }


    }
}
