using AdventOfCode2023;

using Common;

using static AdventOfCode2023.Day12;

namespace Tests
{
    [TestClass]
    public class UnitTestDay12
    {
        private Day12 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day12(data);
        }


        [TestMethod("Day 12, Part 1")]
        [TestCategory("Example data")]
        public void Part1_Example1()
        {
            data = @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1";

            ConditionRecords records = new ConditionRecords();
            records.Records = Parser.ParseLinesDelimitedByNewline<RecordLine, RecordLine.Parsed>(data).ToList();

            long result = SpringOperator.CountAllVariations(records);

            Assert.AreEqual(21, result);
        }

        [TestMethod("Day 12, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {
            data = @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1";

            ConditionRecords records = new ConditionRecords();
            records.Records = Parser.ParseLinesDelimitedByNewline<RecordLine, RecordLine.Parsed>(data).ToList();

            long result = SpringOperator.CountAllVariations(records, 5);

            Assert.AreEqual(525152, result);
        }
    }
}
