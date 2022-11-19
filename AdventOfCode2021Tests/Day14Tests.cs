using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day14Tests
    {
        Day14 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day14(true);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] data = {"NNCB",
                            "",
                            "CH -> B",
                            "HH -> N",
                            "CB -> H",
                            "NH -> C",
                            "HB -> C",
                            "HC -> B",
                            "HN -> C",
                            "NN -> C",
                            "BH -> H",
                            "NC -> B",
                            "NB -> B",
                            "BN -> B",
                            "BB -> N",
                            "BC -> B",
                            "CC -> N",
                            "CN -> C",};

            day.Parse(data);
            day.Iterate(10);
            Assert.AreEqual(1588, day.GetValue());


        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] data = {"NNCB",
                            "",
                            "CH -> B",
                            "HH -> N",
                            "CB -> H",
                            "NH -> C",
                            "HB -> C",
                            "HC -> B",
                            "HN -> C",
                            "NN -> C",
                            "BH -> H",
                            "NC -> B",
                            "NB -> B",
                            "BN -> B",
                            "BB -> N",
                            "BC -> B",
                            "CC -> N",
                            "CN -> C",};

            day.Parse(data);

            Assert.AreEqual(1588, day.RunIterations(10));


        }


        [TestMethod]
        public void TestMethod5()
        {
            string[] data = {"NNCB",
                            "",
                            "CH -> B",
                            "HH -> N",
                            "CB -> H",
                            "NH -> C",
                            "HB -> C",
                            "HC -> B",
                            "HN -> C",
                            "NN -> C",
                            "BH -> H",
                            "NC -> B",
                            "NB -> B",
                            "BN -> B",
                            "BB -> N",
                            "BC -> B",
                            "CC -> N",
                            "CN -> C",};

            day.Parse(data);

            Assert.AreEqual(2188189693529, day.RunIterations(40));


        }

        [TestMethod]
        public void TestMethod3()
        {
            string[] data = {"NNCB",
                            "",
                            "CH -> B",
                            "HH -> N",
                            "CB -> H",
                            "NH -> C",
                            "HB -> C",
                            "HC -> B",
                            "HN -> C",
                            "NN -> C",
                            "BH -> H",
                            "NC -> B",
                            "NB -> B",
                            "BN -> B",
                            "BB -> N",
                            "BC -> B",
                            "CC -> N",
                            "CN -> C",};

            day.Parse(data);
            Assert.AreEqual(5, day.RunIterations(2));
        }

        [TestMethod]
        public void TestMethod4()
        {
            string[] data = {"NN",
                            "",
                            "CH -> B",
                            "HH -> N",
                            "CB -> H",
                            "NH -> C",
                            "HB -> C",
                            "HC -> B",
                            "HN -> C",
                            "NN -> C",
                            "BH -> H",
                            "NC -> B",
                            "NB -> B",
                            "BN -> B",
                            "BB -> N",
                            "BC -> B",
                            "CC -> N",
                            "CN -> C",};

            day.Parse(data);
            Assert.AreEqual(1, day.RunIterations(1));


        }
    }
}
