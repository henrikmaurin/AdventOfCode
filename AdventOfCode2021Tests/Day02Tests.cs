using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021Tests
{
    public class Day02Tests
    {
        Day02 day;
        [TestInitialize]
        public void Init()
        {
            day = new Day02();
        }

        [TestMethod]
        public void TestMethod1()
        {
            int[] testData = { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

            Assert.AreEqual(1,1);
        }
    }
}
