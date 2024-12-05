using AdventOfCode2024;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay05
    {
        private Day05 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day05(data);
        }


        [TestMethod("Day 5, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            int result = day.Problem1();
            Assert.AreEqual(143, result);
        }

        [TestMethod("Day 5, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            int result = day.Problem2();
            Assert.AreEqual(123, result);
        }
    }
}
