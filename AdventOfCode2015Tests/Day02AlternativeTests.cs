using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AdventOfCode2015;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;


using static AdventOfCode2015.Day02Alternative;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day02AlternativeTests
    {
        private Day02Alternative day;

        [TestInitialize]
        public void Init()
        {
            day = new Day02Alternative(true);
        }

        [TestMethod]
        public void Part_1_Example_1()
        {
            Present present = new Present
            {
                Width = 2,
                Heigth = 3,
                Depth = 4,
            };

            Assert.AreEqual(58, present.WrappingArea);
        }

        [TestMethod]
        public void Part_1_Example_2()
        {
            Present present = new Present
            {
                Width = 1,
                Heigth = 1,
                Depth = 10,
            };

            Assert.AreEqual(43, present.WrappingArea);
        }

        [TestMethod]
        public void Part_1_Summed()
        {
            string data = @"2x3x4
1x1x10";

            day = new Day02Alternative(data, true);

            Assert.AreEqual(58 + 43, day.Problem1());
        }

        [TestMethod]
        public void When_Volume_Then_VolumeIsCalculated()
        {
            Present present = new Present
            {
                Width = 2,
                Heigth = 3,
                Depth = 4,
            };

            Assert.AreEqual(2 * 3 * 4, present.Volume);
        }

        [TestMethod]
        public void Part_2_Example_1()
        {
            Present present = new Present
            {
                Width = 2,
                Heigth = 3,
                Depth = 4,
            };

            Assert.AreEqual(34, present.RibbonLength);
        }

        [TestMethod]
        public void Part_2_Example_2()
        {
            Present present = new Present
            {
                Width = 1,
                Heigth = 1,
                Depth = 10,
            };

            Assert.AreEqual(14, present.RibbonLength);
        }

        [TestMethod]
        public void Part_2_Summed()
        {
            string data = @"2x3x4
1x1x10";

            day = new Day02Alternative(data, true);

            Assert.AreEqual(34 + 14, day.Problem2());
        }
    }
}
