using AdventOfCode2018;

using Common;

namespace Tests
{
    [TestClass]
    public class UnitTestDay15
    {
        private Day15 day;
        private string data;
        private string[] testdata;

        [TestInitialize]
        public void Init()
        {
            data = @"";
            testdata = data.SplitOnNewlineArray(false);

            day = new Day15(data);
        }


        [TestMethod("Day 15")]
        [TestCategory("Combatant order")]
        public void SelectOrder()
        {
            string mapdata = @"#######
#.G.E.#
#E.G.E#
#.G.E.#
#######";

            day = new Day15(mapdata);
            day.InitMap();

            List<Combatant> combatants = day.GetRemaingCombatants();

            Assert.AreEqual('G', combatants[0].Type);
            Assert.AreEqual('E', combatants[1].Type);
            Assert.AreEqual('E', combatants[2].Type);
            Assert.AreEqual('G', combatants[3].Type);
            Assert.AreEqual('E', combatants[4].Type);
            Assert.AreEqual('G', combatants[5].Type);
            Assert.AreEqual('E', combatants[6].Type);
        }

        [TestMethod("Day 15")]
        [TestCategory("Attack order")]
        public void AtackOrder()
        {
            string mapdata = @"G....
..G..
..EG.
..G..
...G.";

            day = new Day15(mapdata);
            day.InitMap();
            day.Combatants[0].HP = 9;
            day.Combatants[1].HP = 4;
            day.Combatants[2].HP = 10; // E
            day.Combatants[3].HP = 2;
            day.Combatants[4].HP = 2;
            day.Combatants[5].HP = 1;

            List<Combatant> combatants = day.FindAdjacent(day.Combatants[2]);

            Assert.AreEqual(3, combatants.Count);
            Assert.AreEqual(day.Combatants[3], combatants.First());
            Assert.AreEqual(day.Combatants[4], combatants.Skip(1).First());
        }


        [TestMethod("Day 15")]
        [TestCategory("Movement")]
        public void Movement_1()
        {
            string mapdata = @"#######
#E..G.#
#...#.#
#.G.#G#
#######";

            day = new Day15(mapdata);
            day.InitMap();
            Vector2D nextMove = day.GetNextStep(day.Combatants[0]);

            Assert.IsTrue(nextMove.Equals(day.Combatants[0].Position + Directions.GetDirection(Directions.Right)));

        }

        [TestMethod("Day 15")]
        [TestCategory("Movement")]
        public void Movement_2()
        {
            string mapdata = @"#######
#.E...#
#.....#
#...G.#
#######";

            day = new Day15(mapdata);
            day.InitMap();
            Vector2D nextMove = day.GetNextStep(day.Combatants[0]);

            Assert.IsTrue(nextMove.Equals(day.Combatants[0].Position + Directions.GetDirection(Directions.Right)));

        }

        [TestMethod("Day 15")]
        [TestCategory("Movement")]
        public void Movement_3()
        {
            string mapdata = @"#########
#G..G..G#
#.......#
#.......#
#G..E..G#
#.......#
#.......#
#G..G..G#
#########";

            day = new Day15(mapdata);
            day.InitMap();

            day.BattleRound();
            Assert.IsTrue(day.Combatants[0].Position.Equals(new Vector2D { X = 2, Y = 1 }));
            Assert.IsTrue(day.Combatants[1].Position.Equals(new Vector2D { X = 4, Y = 2 }));
            Assert.IsTrue(day.Combatants[2].Position.Equals(new Vector2D { X = 6, Y = 1 }));
            Assert.IsTrue(day.Combatants[3].Position.Equals(new Vector2D { X = 2, Y = 4 }));
            Assert.IsTrue(day.Combatants[4].Position.Equals(new Vector2D { X = 4, Y = 3 })); // E
            Assert.IsTrue(day.Combatants[5].Position.Equals(new Vector2D { X = 7, Y = 3 }));
            Assert.IsTrue(day.Combatants[6].Position.Equals(new Vector2D { X = 1, Y = 6 }));
            Assert.IsTrue(day.Combatants[7].Position.Equals(new Vector2D { X = 4, Y = 6 }));
            Assert.IsTrue(day.Combatants[8].Position.Equals(new Vector2D { X = 7, Y = 6 }));

            day.BattleRound();
            Assert.IsTrue(day.Combatants[0].Position.Equals(new Vector2D { X = 3, Y = 1 }));
            Assert.IsTrue(day.Combatants[1].Position.Equals(new Vector2D { X = 4, Y = 2 }));
            Assert.IsTrue(day.Combatants[2].Position.Equals(new Vector2D { X = 5, Y = 1 }));
            Assert.IsTrue(day.Combatants[3].Position.Equals(new Vector2D { X = 2, Y = 3 }));
            Assert.IsTrue(day.Combatants[4].Position.Equals(new Vector2D { X = 4, Y = 3 })); // E
            Assert.IsTrue(day.Combatants[5].Position.Equals(new Vector2D { X = 6, Y = 3 }));
            Assert.IsTrue(day.Combatants[6].Position.Equals(new Vector2D { X = 1, Y = 5 }));
            Assert.IsTrue(day.Combatants[7].Position.Equals(new Vector2D { X = 4, Y = 5 }));
            Assert.IsTrue(day.Combatants[8].Position.Equals(new Vector2D { X = 7, Y = 5 }));

            day.BattleRound();
            Assert.IsTrue(day.Combatants[0].Position.Equals(new Vector2D { X = 3, Y = 2 }));
            Assert.IsTrue(day.Combatants[1].Position.Equals(new Vector2D { X = 4, Y = 2 }));
            Assert.IsTrue(day.Combatants[2].Position.Equals(new Vector2D { X = 5, Y = 2 }));
            Assert.IsTrue(day.Combatants[3].Position.Equals(new Vector2D { X = 3, Y = 3 }));
            Assert.IsTrue(day.Combatants[4].Position.Equals(new Vector2D { X = 4, Y = 3 })); // E
            Assert.IsTrue(day.Combatants[5].Position.Equals(new Vector2D { X = 5, Y = 3 }));
            Assert.IsTrue(day.Combatants[6].Position.Equals(new Vector2D { X = 1, Y = 4 }));
            Assert.IsTrue(day.Combatants[7].Position.Equals(new Vector2D { X = 4, Y = 4 }));
            Assert.IsTrue(day.Combatants[8].Position.Equals(new Vector2D { X = 7, Y = 5 }));



        }

        [TestMethod("Day 15")]
        [TestCategory("Battle")]
        public void Battle()
        {
            string mapdata = @"#######
#.G...#
#...EG#
#.#.#G#
#..G#E#
#.....#
#######";

            day = new Day15(mapdata);
            day.InitMap();

            // Start
            Assert.AreEqual("G: 2,1 - (200)", day.Combatants[0].ToString());
            Assert.AreEqual("E: 4,2 - (200)", day.Combatants[1].ToString());
            Assert.AreEqual("G: 5,2 - (200)", day.Combatants[2].ToString());
            Assert.AreEqual("G: 5,3 - (200)", day.Combatants[3].ToString());
            Assert.AreEqual("G: 3,4 - (200)", day.Combatants[4].ToString());
            Assert.AreEqual("E: 5,4 - (200)", day.Combatants[5].ToString());
            
            // Round 1
            day.BattleRound();
            Assert.AreEqual("G: 3,1 - (200)", day.Combatants[0].ToString());
            Assert.AreEqual("E: 4,2 - (197)", day.Combatants[1].ToString());
            Assert.AreEqual("G: 5,2 - (197)", day.Combatants[2].ToString());
            Assert.AreEqual("G: 5,3 - (197)", day.Combatants[3].ToString());
            Assert.AreEqual("G: 3,3 - (200)", day.Combatants[4].ToString());
            Assert.AreEqual("E: 5,4 - (197)", day.Combatants[5].ToString());

            // Round 2
            day.BattleRound();
            Assert.AreEqual("G: 4,1 - (200)", day.Combatants[0].ToString());
            Assert.AreEqual("E: 4,2 - (188)", day.Combatants[1].ToString());
            Assert.AreEqual("G: 5,2 - (194)", day.Combatants[2].ToString());
            Assert.AreEqual("G: 5,3 - (194)", day.Combatants[3].ToString());
            Assert.AreEqual("G: 3,2 - (200)", day.Combatants[4].ToString());
            Assert.AreEqual("E: 5,4 - (194)", day.Combatants[5].ToString());



        }

        [TestMethod("Day 15, Part 2")]
        [TestCategory("Example data")]
        public void Part2()
        {

            Assert.AreEqual(2, 2);
        }
    }
}
