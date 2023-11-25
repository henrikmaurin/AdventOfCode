using AdventOfCode2015;

using Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using static AdventOfCode2015.Day01Alternative;

namespace AdventOfCode2015Tests
{
    [TestClass]
    public class Day01AlternativeTests
    {
        private Day01Alternative day;
        private IElevator elevator;

        [TestInitialize]
        public void Init()
        {
            elevator = Substitute.ForPartsOf<Elevator>();
            day = new Day01Alternative(elevator, true);           
        }

        [TestMethod]
        public void When_Up_Then_FloorIsIncreased()
        {
            Elevator elevator = new Elevator();
            int currentFloor = elevator.Floor;
            int destinationFloor = elevator.Up();

            Assert.AreEqual(currentFloor + 1, elevator.Floor);
            Assert.AreEqual(destinationFloor, elevator.Floor);
        }

        [TestMethod]
        public void When_Down_Then_FloorIsDecreased()
        {
            Elevator elevator = new Elevator();
            int currentFloor = elevator.Floor;
            int destinationFloor = elevator.Down();

            Assert.AreEqual(currentFloor - 1, elevator.Floor);
            Assert.AreEqual(destinationFloor, elevator.Floor);
        }

        [TestMethod]
        public void Given_DirectionUp_When_Travel_Then_UpIsCalled()
        {
            Instruction instruction = new Instruction();
            instruction.Direction = Instruction.Directions.Up;


            elevator.Up().Returns(1);

            elevator.Travel(instruction);

            // Assert Up is Called
            elevator.Received().Up();
        }

        [TestMethod]
        public void Given_DirectionDown_When_Travel_Then_DownIsCalled()
        {
            Instruction instruction = new Instruction();
            instruction.Direction = Instruction.Directions.Down;

            elevator.Down().Returns(-1);

            elevator.Travel(instruction);

            // Assert Up is Called
            elevator.Received().Down();
        }

        [TestMethod]
        public void Part_1_Example_1()
        {
            string testdata = "(())";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction,Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(0, elevator.Floor);
        }

        [TestMethod]
        public void Part_1_Example_2()
        {
            string testdata = "()()";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(0, elevator.Floor);
        }

        [TestMethod]
        public void Part_1_Example_3()
        {
            string testdata = "(((";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(3, elevator.Floor);
        }

        [TestMethod]
        public void Part_1_Example_4()
        {
            string testdata = "(()(()(";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(3, elevator.Floor);
        }

        [TestMethod]
        public void Part_1_Example_5()
        {
            string testdata = "))(((((";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(3, elevator.Floor);
        }

        [TestMethod]
        public void Part_1_Example_6()
        {
            string testdata = "())";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(-1, elevator.Floor);
        }
        [TestMethod]
        public void Part_1_Example_7()
        {
            string testdata = "))(";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(-1, elevator.Floor);
        }
        [TestMethod]
        public void Part_1_Example_8()
        {
            string testdata = ")))";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(-3, elevator.Floor);
        }
        [TestMethod]
        public void Part_1_Example_9()
        {
            string testdata = ")())())";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            bellBoy.FollowInstructions(instructions);

            Assert.AreEqual(-3, elevator.Floor);
        }
        [TestMethod]
        public void Part_2_Example_1()
        {
            string testdata = ")())())";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            int buttonPresses = bellBoy.FollowInstructionsUntil(instructions, -1);

            Assert.AreEqual(1, buttonPresses);
        }
        [TestMethod]
        public void Part_2_Example_2()
        {
            string testdata = "()())";
            BellBoy bellBoy = new BellBoy(elevator);

            var instructions = Parser.ParseLineOfSingleChars<Instruction, Instruction.Parsed>(testdata);
            int buttonPresses = bellBoy.FollowInstructionsUntil(instructions, -1);

            Assert.AreEqual(5, buttonPresses);
        }
    }
}
