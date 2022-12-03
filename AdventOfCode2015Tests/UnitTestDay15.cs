using AdventOfCode2015;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class UnitTestDay15
    {
        private Day15 day;
        private string data;
        private string[] testdata;
        [TestInitialize]
        public void Init()
        {
            data = @"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";

            testdata = data.SplitOnNewlineArray();

            day = new Day15(data);
        }


        [TestMethod("Day 15, Example 1")]
        [TestCategory("Example data")]
        public void Example1_1()
        {
            Recipie recipie = new Recipie();
            recipie.Ingredients = new List<Amount>();
            recipie.Ingredients.Add(new Amount
            {
                Ingredient = new Ingredient(testdata[0]),
                Teaspoons = 44,
            });

            recipie.Ingredients.Add(new Amount
            {
                Ingredient = new Ingredient(testdata[1]),
                Teaspoons = 56,
            });

            Assert.AreEqual(62842880, recipie.GetScore());
        }

        [TestMethod("Day 15, Problem 1")]
        [TestCategory("Problem")]
        public void Problem1()
        {
            Assert.AreEqual(62842880, day.Problem1());
        }

        [TestMethod("Day 15, Problem 2")]
        [TestCategory("Problem")]
        public void Problem2()
        {
            Assert.AreEqual(57600000, day.Problem2());
        }


    }
}
