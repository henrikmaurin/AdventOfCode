using AdventOfCode2015;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using static AdventOfCode2015.Day05Alternative;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day05AlternativeTests
    {
        private Day05Alternative day;
        private StringClassifier stringClassifier;

        [TestInitialize]
        public void Init()
        {
            day = new Day05Alternative(true);
            stringClassifier = Substitute.ForPartsOf<StringClassifier>();
        }

        [TestMethod]
        public void Given_StringWithEnoughVowels_When_HasAtleastThreeVowel_Then_RulePassed()
        {
            string s = "aei";

            bool result = stringClassifier.HasAtleastThreeVowels(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_StringWithNotEnoughVowels_When_HasAtleastThreeVowel_Then_RuleFailed()
        {
            string s = "aet";

            bool result = stringClassifier.HasAtleastThreeVowels(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given_StringWithDoubleLetters_When_HasDoubleLetter_Then_RulePassed()
        {
            string s = "abcdde";

            bool result = stringClassifier.HasDoubleLetter(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_StringWithNoDoubleLetters_When_HasDoubleLetter_Then_RuleFailed()
        {
            string s = "abcdef";

            bool result = stringClassifier.HasDoubleLetter(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given_StringWithNoForbiddenStrings_When_DoesNotContainForbiddenStrings_Then_RulePassed()
        {
            string s = "acegts";

            bool result = stringClassifier.DoesNotContainForbiddenStrings(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_StringWithNoForbiddenStrings_When_DoesNotContainForbiddenStrings_Then_RuleFailed()
        {
            string s = "abcdef";

            bool result = stringClassifier.DoesNotContainForbiddenStrings(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Problem1_Example1()
        {
            string s = "ugknbfddgicrmopn";

            bool result = stringClassifier.Nice(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Problem1_Example2()
        {
            string s = "aaa";

            bool result = stringClassifier.Nice(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Problem1_Example3()
        {
            string s = "jchzalrnumimnmhp";

            bool result = stringClassifier.Nice(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Problem1_Example4()
        {
            string s = "haegwjzuvuyypxyu";

            bool result = stringClassifier.Nice(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Problem1_Example5()
        {
            string s = "dvszwmarrgswjxmb";

            bool result = stringClassifier.Nice(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given_StringWithTwoPairs_When_HasDoublePairsOfLetters_Then_RulePassed()
        {
            string s = "aabcdefgaa";

            bool result = stringClassifier.HasDoublePairsOfLetters(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_StringWithTrippleA_When_HasDoublePairsOfLetters_Then_RuleFailed()
        {
            string s = "aaa";

            bool result = stringClassifier.HasDoublePairsOfLetters(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given_StringWithSymetry_When_HasSymetricLetters_Then_RulePassed()
        {
            string s = "abcdefeghi";

            bool result = stringClassifier.HasSymetricLetters(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_StringWithoutSymetry_When_HasSymetricLetters_Then_RuleFailed()
        {
            string s = "abcdefrghi";

            bool result = stringClassifier.HasSymetricLetters(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Problem2_Example1()
        {
            string s = "qjhvhtzxzqqjkmpb";

            bool result = stringClassifier.BetterNice(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Problem2_Example2()
        {
            string s = "xxyxx";

            bool result = stringClassifier.BetterNice(s);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Problem2_Example3()
        {
            string s = "uurcxstgmygtbstg";

            bool result = stringClassifier.BetterNice(s);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Problem2_Example4()
        {
            string s = "ieodomkazucvgmuy";

            bool result = stringClassifier.BetterNice(s);

            Assert.IsFalse(result);
        }
    }
}
