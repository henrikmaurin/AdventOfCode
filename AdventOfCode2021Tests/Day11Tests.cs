using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day11Tests
    {
        Day11 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day11();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] instructions = new string[]
            {
                "5483143223",
                "2745854711",
                "5264556173",
                "6141336146",
                "6357385478",
                "4167524645",
                "2176841721",
                "6882881134",
                "4846848554",
                "5283751526",
            };

            day.Init(instructions);

        
            Assert.AreEqual(0, day.CountFlashes(1));
            Assert.AreEqual(35, day.CountFlashes(1));
            Assert.AreEqual(80,day.CountFlashes(1));
            Assert.AreEqual(96, day.CountFlashes(1));
            Assert.AreEqual(104, day.CountFlashes(1));
            Assert.AreEqual(105, day.CountFlashes(1));
            Assert.AreEqual(112, day.CountFlashes(1));
            Assert.AreEqual(136, day.CountFlashes(1));
            Assert.AreEqual(136+39, day.CountFlashes(1));
            Assert.AreEqual(136 + 39+29, day.CountFlashes(1));

        }
        [TestMethod]
        public void TestMethod2()
        {
            string[] instructions = new string[]
            {
                "5483143223",
                "2745854711",
                "5264556173",
                "6141336146",
                "6357385478",
                "4167524645",
                "2176841721",
                "6882881134",
                "4846848554",
                "5283751526",
            };

            day.Init(instructions);


        
            Assert.AreEqual(204, day.CountFlashes(10));

        }

        [TestMethod]
        public void TestMethod3()
        {
            string[] instructions = new string[]
            {
                "5483143223",
                "2745854711",
                "5264556173",
                "6141336146",
                "6357385478",
                "4167524645",
                "2176841721",
                "6882881134",
                "4846848554",
                "5283751526",
            };

            day.Init(instructions);



            Assert.AreEqual(195, day.FindTotalFlash());

        }
    }
}
