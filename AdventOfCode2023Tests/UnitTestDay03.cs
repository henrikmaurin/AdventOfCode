using AdventOfCode2023;

using Common;

using static AdventOfCode2023.Day03;

namespace Tests
{
    [TestClass]
    public class UnitTestDay03
    {
        private Day03 day;
        private string data;


        private static readonly string[] testdata =  new string[] {
                    "467..114..",
                    "...*......",
                    "..35..633.",
                    "......#...",
                    "617*......",
                    ".....+.58.",
                    "..592.....",
                    "......755.",
                    "...$.*....",
                    ".664.598..",
                };
      
        [TestInitialize]
        public void Init()
        {
            data = @"";

            day = new Day03(data);
        }


        [TestMethod("Day 3, Part 1")]
        [TestCategory("Example data")]
        public void Part1()
        {
            GondolaEngine gondolaEngine = new GondolaEngine();
            gondolaEngine.ReadSchematics(testdata);
            gondolaEngine.Annotate();

            var result = SchematicsInterpreter.FindSum(gondolaEngine);

            Assert.AreEqual(4361, result);
        }

        [TestMethod("Day 3, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            GondolaEngine gondolaEngine = new GondolaEngine();
            gondolaEngine.ReadSchematics(testdata);
            gondolaEngine.Annotate();

            var result = SchematicsInterpreter.FindGearRatio(gondolaEngine);

            Assert.AreEqual(467835, result);
        }
    }
}
