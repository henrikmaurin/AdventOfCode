using AdventOfCode2015;

using Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using static AdventOfCode2015.Day06Alternative;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day06AlternativeTests
    {
        private Day06Alternative day;
        private LightDisplay lightDisplay;

        [TestInitialize]
        public void Init()
        {
            day = new Day06Alternative(true);
            lightDisplay = Substitute.ForPartsOf<LightDisplay>(10,10);
        }

        [TestMethod]
        public void Given_EmptyDisplaySegment_When_Get_Then_SegmentIsOff()
        {
            Vector2D vector2D = new Vector2D(0, 0);

            Assert.IsFalse(lightDisplay.GetSegment(vector2D));
        }

        [TestMethod]
        public void Given_EmptyDisplaySegment_When_Set_Then_SegmentIsSet()
        {
            Vector2D vector2D = new Vector2D(0, 0);

            lightDisplay.Set(vector2D);

            Assert.IsTrue(lightDisplay.GetSegment(vector2D));
        }

        [TestMethod]
        public void Given_EmptyDisplaySegment_When_Clear_Then_SegmentIsUnset()
        {
            Vector2D vector2D = new Vector2D(0, 0);

            lightDisplay.Clear(vector2D);

            Assert.IsFalse(lightDisplay.GetSegment(vector2D));
        }

        [TestMethod]
        public void Given_SetDisplaySegment_When_Clear_Then_SegmentIsUnset()
        {
            Vector2D vector2D = new Vector2D(0, 0);
            
            lightDisplay.Set(vector2D);
            lightDisplay.Clear(vector2D);

            Assert.IsFalse(lightDisplay.GetSegment(vector2D));
        }

        [TestMethod]
        public void Given_EmptyDisplaySegment_When_Toggle_Then_SegmentIsSet()
        {
            Vector2D vector2D = new Vector2D(0, 0);

            lightDisplay.Toggle(vector2D);

            Assert.IsTrue(lightDisplay.GetSegment(vector2D));
        }

        [TestMethod]
        public void Given_SetDisplaySegment_When_Toggle_Then_SegmentIsUnset()
        {
            Vector2D vector2D = new Vector2D(0, 0);

            lightDisplay.Set(vector2D);
            lightDisplay.Toggle(vector2D);

            Assert.IsFalse(lightDisplay.GetSegment(vector2D));
        }

        [TestMethod]
        public void Given_UnSetDisplaySegment_When_Toggle_Then_SegmentIsSet()
        {
            Vector2D vector2D = new Vector2D(0, 0);

            lightDisplay.Clear(vector2D);
            lightDisplay.Toggle(vector2D);

            Assert.IsTrue(lightDisplay.GetSegment(vector2D));
        }
    }
}
