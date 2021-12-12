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
    public class Day10Tests
    {
        Day10 day;

        [TestInitialize]
        public void Init()
        {
            day = new Day10();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] instructions = new string[]
            {
                "[({(<(())[]>[[{[]{<()<>>",
                "[(()[<>])]({[<{<<[]>>(",
                "{([(<{}[<>[]}>{[]{[(<()>",
                "(((({<>}<{<{<>}{[]{[]{}",
                "[[<[([]))<([[{}[[()]]]",
                 "[{[{({}]{}}([{[{{{}}([]",
                "{<[[]]>}<{[{[{[]{()[[[]",
                "[<(<(<(<{}))><([]([]()",
                "<{([([[(<>()){}]>(<<{{",
                "<{([{{}}[<[[[<>{}]]]>[]]",
            };

            Assert.AreEqual(1197, day.ErrorValue(instructions[2]));
            Assert.AreEqual(3, day.ErrorValue(instructions[4]));


            Assert.AreEqual(26397, day.TotalErrorValue(instructions));

        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] instructions = new string[]
            {
                "[({(<(())[]>[[{[]{<()<>>",
                "[(()[<>])]({[<{<<[]>>(",
                "{([(<{}[<>[]}>{[]{[(<()>",
                "(((({<>}<{<{<>}{[]{[]{}",
                "[[<[([]))<([[{}[[()]]]",
                 "[{[{({}]{}}([{[{{{}}([]",
                "{<[[]]>}<{[{[{[]{()[[[]",
                "[<(<(<(<{}))><([]([]()",
                "<{([([[(<>()){}]>(<<{{",
                "<{([{{}}[<[[[<>{}]]]>[]]",
            };

            Assert.AreEqual(294, day.AutoComplete(instructions[9]));
            

            Assert.AreEqual(288957, day.GetMiddleAutocompleteValue(instructions));

        }

    }
}
