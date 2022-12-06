using AdventOfCode2015;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class UnitTestDay19
    {
        private Day19 day;
        private string data;
        private string[][] testdata;
        [TestInitialize]
        public void Init()
        {
            data = @"H => HO
H => OH
O => HH

HOH";
            testdata = data.GroupByEmptyLine();

            day = new Day19(data);
        }


        [TestMethod("Day 19, Example 1")]
        [TestCategory("Example data")]
        public void Example1_1()
        {

            Assert.AreEqual(4, day.Calibrate(testdata[1][0]));
        }

        [TestMethod("Day 19, Example 2")]
        [TestCategory("Example data")]
        public void Example1_2()
        {
            
            Assert.AreEqual(7, day.Calibrate("HOHOHO"));
        }


    }
}
