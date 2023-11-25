using AdventOfCode2015;

using Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using static AdventOfCode2015.Day03Alternative;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day03AlternativeTests
    {
        private Day03Alternative day;
        private Santa santa;

        [TestInitialize]
        public void Init()
        {
            day = new Day03Alternative(true);
            santa = Substitute.ForPartsOf<Santa>();
        }

        [TestMethod]
        public void When_Up_Then_SantaHasMovedUp()
        {
            Vector2D santaStartPos = santa.Coord.Clone();
            santa.MoveUp();

            Assert.AreEqual(santaStartPos.X, santa.Coord.X);
            Assert.AreEqual(santaStartPos.Y - 1, santa.Coord.Y);
        }

        [TestMethod]
        public void When_Down_Then_SantaHasMovedDown()
        {
            Vector2D santaStartPos = santa.Coord.Clone();
            santa.MoveDown();

            Assert.AreEqual(santaStartPos.X, santa.Coord.X);
            Assert.AreEqual(santaStartPos.Y + 1, santa.Coord.Y);
        }

        [TestMethod]
        public void When_Left_Then_SantaHasMovedLeft()
        {
            Vector2D santaStartPos = santa.Coord.Clone();
            santa.MoveLeft();

            Assert.AreEqual(santaStartPos.X - 1, santa.Coord.X);
            Assert.AreEqual(santaStartPos.Y, santa.Coord.Y);
        }

        [TestMethod]
        public void When_Right_Then_SantaHasMovedRight()
        {
            Vector2D santaStartPos = santa.Coord.Clone();
            santa.MoveRight();

            Assert.AreEqual(santaStartPos.X + 1, santa.Coord.X);
            Assert.AreEqual(santaStartPos.Y, santa.Coord.Y);
        }

        [TestMethod]
        public void Given_DirectionUp_When_Travel_Then_MoveUp_IsCalled()
        {
            Instruction instruction = new Instruction();
            instruction.Direction = Directions.Up;

            santa.Travel(instruction);

            santa.Received().MoveUp();
        }

        [TestMethod]
        public void Given_DirectionDown_When_Travel_Then_MoveDown_IsCalled()
        {
            Instruction instruction = new Instruction();
            instruction.Direction = Directions.Down;

            santa.Travel(instruction);

            santa.Received().MoveDown();
        }

        [TestMethod]
        public void Given_DirectionLeft_When_Travel_Then_MoveLeft_IsCalled()
        {
            Instruction instruction = new Instruction();
            instruction.Direction = Directions.Left;

            santa.Travel(instruction);

            santa.Received().MoveLeft();
        }

        [TestMethod]
        public void Given_DirectionRight_When_Travel_Then_MoveRight_IsCalled()
        {
            Instruction instruction = new Instruction();
            instruction.Direction = Directions.Right;

            santa.Travel(instruction);

            santa.Received().MoveRight();
        }

        [TestMethod]
        public void Part_1_Example_1()
        {
            string testdata = ">";
            MapReaderElf elf = new MapReaderElf();
            elf.AddSanta(santa);

            var instructions = Parser.ParseLineOfSingleChars<Instruction,Instruction.Parsed>(testdata);
            elf.FollowInstructions(instructions);

            Assert.AreEqual(2, elf.HousesVisited);
        }

        [TestMethod]
        public void Part_1_Example_2()
        {
            string testdata = "^>v<";
            MapReaderElf elf = new MapReaderElf();
            elf.AddSanta(santa);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            elf.FollowInstructions(instructions);

            Assert.AreEqual(4, elf.HousesVisited);
        }

        [TestMethod]
        public void Part_1_Example_3()
        {
            string testdata = "^v^v^v^v^v";
            MapReaderElf elf = new MapReaderElf();
            elf.AddSanta(santa);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            elf.FollowInstructions(instructions);

            Assert.AreEqual(2, elf.HousesVisited);
        }

        [TestMethod]
        public void Part_2_Example_1()
        {
            string testdata = "^v";
            MapReaderElf elf = new MapReaderElf();
            elf.AddSanta(santa);
            elf.AddSanta(new Santa("RoboSanta"));

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            elf.FollowInstructions(instructions);

            Assert.AreEqual(3, elf.HousesVisited);
        }

        [TestMethod]
        public void Part_2_Example_2()
        {
            string testdata = "^>v<";
            MapReaderElf elf = new MapReaderElf();
            elf.AddSanta(santa);
            elf.AddSanta(new Santa("RoboSanta"));

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            elf.FollowInstructions(instructions);

            Assert.AreEqual(3, elf.HousesVisited);
        }

        [TestMethod]
        public void Part_2_Example_3()
        {
            string testdata = "^v^v^v^v^v";
            MapReaderElf elf = new MapReaderElf();
            elf.AddSanta(santa);
            elf.AddSanta(new Santa("RoboSanta"));

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            elf.FollowInstructions(instructions);

            Assert.AreEqual(11, elf.HousesVisited);
        }
    }
}
