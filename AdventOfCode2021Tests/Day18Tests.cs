using AdventOfCode2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021Tests
{
    [TestClass]
    public class Day18Tests
    {
        Day18 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day18(true);
        }

        [TestMethod]
        public void TestMetod1()
        {
            string data = "[[[[[9,8],1],2],3],4]";
            day.Init(data);
            day.ParseUntilDone();
            Assert.AreEqual("[[[[0,9],2],3],4]", day.ToString());
        }

        [TestMethod]
        public void TestMetod2()
        {
            string data = "[7,[6,[5,[4,[3,2]]]]]";
            day.Init(data);
            day.ParseUntilDone();
            Assert.AreEqual("[7,[6,[5,[7,0]]]]", day.ToString());
        }

        [TestMethod]
        public void TestMetod3()
        {
            string data = "[[6,[5,[4,[3,2]]]],1]";
            day.Init(data);
            day.ParseUntilDone();
            Assert.AreEqual("[[6,[5,[7,0]]],3]", day.ToString());
        }

        [TestMethod]
        public void TestMetod4()
        {
            string data = "[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]";
            day.Init(data);
            day.ParseUntilDone();
            Assert.AreEqual("[[3,[2,[8,0]]],[9,[5,[7,0]]]]", day.ToString());
        }

        [TestMethod]
        public void TestMetod5()
        {
            string data = "[[[[4,3],4],4],[7,[[8,4],9]]]";
            day.Init(data);
            string toAdd = "[1,1]";
            day.Add(toAdd);
            Assert.AreEqual("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", day.ToString());
            day.Parse();
            Assert.AreEqual("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", day.ToString());
            day.Parse();
            Assert.AreEqual("[[[[0,7],4],[15,[0,13]]],[1,1]]", day.ToString());
            day.Parse();
            Assert.AreEqual("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", day.ToString());
            day.Parse();
            Assert.AreEqual("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]", day.ToString());
            day.Parse();
            Assert.AreEqual("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", day.ToString());
        }

        [TestMethod]
        public void TestMetod6()
        {
            string data = "[1,1]";
            day.Init(data);
            day.ParseUntilDone();
            day.Add("[2,2]");
            day.ParseUntilDone();
            day.Add("[3,3]");
            day.ParseUntilDone();
            day.Add("[4,4]");
            day.ParseUntilDone();
            day.Add("[5,5]");
            day.ParseUntilDone();
            Assert.AreEqual("[[[[3,0],[5,3]],[4,4]],[5,5]]", day.ToString());
        }


        [TestMethod]
        public void TestMetod7()
        {
            string data = "[1,1]";
            day.Init(data);
            day.ParseUntilDone();
            day.Add("[2,2]");
            day.ParseUntilDone();
            day.Add("[3,3]");
            day.ParseUntilDone();
            day.Add("[4,4]");
            day.ParseUntilDone();
            day.Add("[5,5]");
            day.ParseUntilDone();
            day.Add("[6,6]");
            day.ParseUntilDone();
            Assert.AreEqual("[[[[5,0],[7,4]],[5,5]],[6,6]]", day.ToString());
        }

        [TestMethod]
        public void TestMethod8()
        {
            string[] data = {
                "[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]",
                "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
                "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
                "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
                "[7,[5,[[3,8],[1,4]]]]",
                "[[2,[2,2]],[8,[8,1]]]",
                "[2,9]",
                "[1,[[[9,3],9],[[9,0],[0,7]]]]",
                "[[[5,[7,4]],7],1]",
                "[[[[4,2],2],6],[8,7]]",
            };

            day.AddAllNumbers(data);
            Assert.AreEqual("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", day.ToString());

        }

        [TestMethod]
        public void TestMethod9()
        {
            string data = "[9,1]";
            var tree = day.ParseToTree(data);

            Assert.AreEqual(29, tree.Magnitude());
        }

        [TestMethod]
        public void TestMethod10()
        {
            string data = "[[9,1],[1,9]]";
            var tree = day.ParseToTree(data);

            Assert.AreEqual(129, tree.Magnitude());
        }

        [TestMethod]
        public void TestMethod11()
        {
            string data = "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]";
            var tree = day.ParseToTree(data);

            Assert.AreEqual(3488, tree.Magnitude());
        }

        [TestMethod]
        public void TestMethod12()
        {
            string[] data = {
                "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
                "[[[5,[2,8]],4],[5,[[9,9],0]]]",
                "[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
                "[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
                "[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
                "[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
                "[[[[5,4],[7,7]],8],[[8,3],8]]",
                "[[9,3],[[9,9],[6,[4,9]]]]",
                "[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
                "[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]",

            };


            Assert.AreEqual(3993, day.FindMax(data));


        }

    }
}
