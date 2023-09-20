using Common;
using System;
using System.Diagnostics;
using static AdventOfCode2022.Day24;

namespace AdventOfCode2022
{
    public class Day24 : DayBase, IDay
    {
        private const int day = 24;
        List<string> data;
        Vector2D start;
        Vector2D goal;
        List<Blizzard> Blizzards;
        Dictionary<int, Map2D<MapCacheData>> mapstates;

        PriorityQueue<NextMove, int> nextMoves;
        int sizeX;
        int sizeY;
        public Day24(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
        }
        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Result: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Result: {result2}");
        }
        public int Problem1()
        {
            Parse(data);
            return Start();
        }
   /*     public int Problem2()
        {
            Parse(data);
            int moves = Start();

            return RunForgot(moves);
        }*/

        public int Problem2()
        {
            Parse(data);
            int moves = Start();
   
            for (int i = 0; i < 10000; i++)
                moves = RunForgot(moves);

            return moves;
        }

        public int Start()
        {
            nextMoves = new PriorityQueue<NextMove, int>();
            nextMoves.Enqueue(new NextMove
            {
                Position = start + Directions.GetDirection(Directions.Down),
                Moves = 1,
                History = "Down"
            }, 1); ;
            nextMoves.Enqueue(new NextMove
            {
                Position = start,
                Moves = 1,
                History = "Stay"
            }, 1);

            return Process(goal);
        }

        public int RunForgot(int moves)
        {
            nextMoves = new PriorityQueue<NextMove, int>();
            nextMoves.Enqueue(new NextMove
            {
                Position = goal + Directions.GetDirection(Directions.Up),
                Moves = moves + 1,
                History = "Up"
            }, 1); ;
            nextMoves.Enqueue(new NextMove
            {
                Position = goal,
                Moves = moves + 1,
                History = "Stay"
            }, 1);

            moves = Process(start);

            nextMoves = new PriorityQueue<NextMove, int>();
            nextMoves.Enqueue(new NextMove
            {
                Position = start + Directions.GetDirection(Directions.Down),
                Moves = moves + 1,
                History = "Down"
            }, 1); ;
            nextMoves.Enqueue(new NextMove
            {
                Position = start,
                Moves = moves + 1,
                History = "Stay"
            }, 1);

            return Process(goal);
        }

        public int RunCourse()
        {
            nextMoves = new PriorityQueue<NextMove, int>();
            Vector2D position = new Vector2D { X = start.X, Y = start.Y };

            int moveNum = 1;
            position += Directions.GetDirection(Directions.Down);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
                History = "Down,"
            }, moveNum);

            moveNum = 2;
            position += Directions.GetDirection(Directions.Down);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;

            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Up);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum = 5;
            position += Directions.GetDirection(Directions.Right);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);


            moveNum++;
            position += Directions.GetDirection(Directions.Right);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Down);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Left);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Up);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);


            moveNum = 10;
            position += Directions.GetDirection(Directions.Right);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Down);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Down);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Right);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum = 15;
            position += Directions.GetDirection(Directions.Right);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Right);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum++;
            position += Directions.GetDirection(Directions.Down);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);

            moveNum = 18;
            position += Directions.GetDirection(Directions.Down);
            nextMoves.Enqueue(new NextMove
            {
                Position = position,
                Moves = moveNum,
            }, moveNum);


            return Process(goal,true);
        }

        private class NextMove
        {
            public Vector2D Position { get; set; }
            public int Moves { get; set; }
            public string History { get; set; }
        }  

        public int Process(Vector2D endPos, bool runcourse = false)
        {
            int currentMoves = 0;
            HashSet<string> tried = new HashSet<string>();
            while (nextMoves.Count > 0)
            {
                NextMove nextMove = nextMoves.Dequeue();
                if (tried.Contains($"{nextMove.Position.X},{nextMove.Position.Y},{nextMove.Moves}"))
                    continue;

                tried.Add($"{nextMove.Position.X},{nextMove.Position.Y},{nextMove.Moves}");
              //  if ("Down,Down,Stay,Up,Right,Right,Down,Left,Up,Right,Stay,Down,Down,Right,Right,Right,Down,Down".StartsWith(nextMove.History))
              //  { int a = 1; }

                if (nextMove.Position.X <= 0 || nextMove.Position.X > sizeX - 2 || nextMove.Position.Y < 1 || nextMove.Position.Y > sizeY - 2)
                {
                    if (nextMove.Position.X == 1 && nextMove.Position.Y == 0)
                    {

                    }
                    else
                    { int a = 1; }
                }

                if (nextMove.Position.X == endPos.X && nextMove.Position.Y == endPos.Y)
                  return nextMove.Moves ;

                int nextMoveModulo = nextMove.Moves % mapstates.Count;

                if (mapstates[nextMoveModulo][nextMove.Position].Traverible == false)
                    continue;

                if (!runcourse)
                {
                    //Right 
                    if (nextMove.Position.X +1 < sizeX - 1 && !(nextMove.Position.X == start.X && nextMove.Position.Y == start.Y) && !(nextMove.Position.X == goal.X && nextMove.Position.Y == goal.Y))
                    {
                        nextMoves.Enqueue(new NextMove
                        {
                            Moves = nextMove.Moves + 1,
                            Position = nextMove.Position + Directions.GetDirection(Directions.Right),
                            History = nextMove.History + ",Right",
                        }, nextMove.Position.ManhattanDistance(endPos) + nextMove.Moves + 1);
                    }

                    //Down 
                    if (nextMove.Position.Y + 1 < sizeY - 1 || (nextMove.Position.X == goal.X && nextMove.Position.Y == goal.Y -1))
                    {
                        nextMoves.Enqueue(new NextMove
                        {
                            Moves = nextMove.Moves + 1,
                            Position = nextMove.Position + Directions.GetDirection(Directions.Down),
                            History = nextMove.History + ",Down",
                        }, nextMove.Position.ManhattanDistance(endPos) + nextMove.Moves + 1);
                    }

                    //Left 
                    if (nextMove.Position.X - 1 > 0 &&  !(nextMove.Position.X == start.X && nextMove.Position.Y == start.Y) && !(nextMove.Position.X == goal.X && nextMove.Position.Y == goal.Y))
                    {
                        nextMoves.Enqueue(new NextMove
                        {
                            Moves = nextMove.Moves + 1,
                            Position = nextMove.Position + Directions.GetDirection(Directions.Left),
                            History = nextMove.History + ",Left",
                        }, nextMove.Position.ManhattanDistance(endPos) + nextMove.Moves + 1);
                    }
                    //Up 
                    if (nextMove.Position.Y - 1 > 0 || (nextMove.Position.X == start.X && nextMove.Position.Y == start.Y + 1))
                    {
                        nextMoves.Enqueue(new NextMove
                        {
                            Moves = nextMove.Moves + 1,
                            Position = nextMove.Position + Directions.GetDirection(Directions.Up),
                            History = nextMove.History + ",Up",
                        }, nextMove.Position.ManhattanDistance(endPos) + nextMove.Moves + 1);
                    }

                    nextMoves.Enqueue(new NextMove
                    {
                        Moves = nextMove.Moves + 1,
                        Position = nextMove.Position,
                        History = nextMove.History + ",Stay",
                    }, nextMove.Position.ManhattanDistance(endPos) + nextMove.Moves + 1);
                }
            }

            return int.MaxValue;
        }

        public List<Blizzard> Parse(List<string> mapData)
        {
            Blizzards = new List<Blizzard>();
            sizeY = mapData.Count;
            sizeX = mapData.First().Length;
            for (int x = 0; x < sizeX; x++)
            {
                if (mapData.First()[x] == '.')
                    start = new Vector2D { X = x, Y = 0 };
                if (mapData.Last()[x] == '.')
                    goal = new Vector2D { X = x, Y = mapData.Count - 1 };
            }

            for (int y = 0; y < sizeY - 1; y++)
            {
                for (int x = 0; x < sizeX - 1; x++)
                {
                    switch (mapData[y][x])
                    {
                        case ' ':
                        case '#':
                            continue;
                        case '^':
                            Blizzards.Add(new Blizzard
                            {
                                Position = new Vector2D { X = x, Y = y },
                                Direction = Directions.GetDirection(Directions.Up)
                            });
                            break;
                        case '>':
                            Blizzards.Add(new Blizzard
                            {
                                Position = new Vector2D { X = x, Y = y },
                                Direction = Directions.GetDirection(Directions.Right)
                            });
                            break;
                        case 'v':
                            Blizzards.Add(new Blizzard
                            {
                                Position = new Vector2D { X = x, Y = y },
                                Direction = Directions.GetDirection(Directions.Down)
                            });
                            break;
                        case '<':
                            Blizzards.Add(new Blizzard
                            {
                                Position = new Vector2D { X = x, Y = y },
                                Direction = Directions.GetDirection(Directions.Left)
                            });
                            break;
                    }
                }
            }

            int stateCount = MathHelpers.LeastCommonMultiple(sizeX - 2, sizeY - 2);

            mapstates = new Dictionary<int, Map2D<MapCacheData>>();
            for (int i = 0; i < stateCount; i++)
            {
                Map2D<MapCacheData> thisMapState = new Map2D<MapCacheData>();
                thisMapState.Init(sizeX, sizeY, new MapCacheData { Traverible = false ,StepsToGoal = int.MaxValue, StepsToStart =int.MaxValue });
                thisMapState[start].Traverible = true;
                thisMapState[goal].Traverible = true ;
                mapstates.Add(i, thisMapState);
                foreach (Blizzard blizzard in Blizzards)
                {
                    thisMapState[blizzard.Position].Traverible = true;
                    blizzard.Position = blizzard.Position + blizzard.Direction;
                    if (blizzard.Position.X == 0)
                        blizzard.Position.X = sizeX - 2;

                    if (blizzard.Position.X == sizeX - 1)
                        blizzard.Position.X = 1;

                    if (blizzard.Position.Y == 0)
                        blizzard.Position.Y = sizeY - 2;

                    if (blizzard.Position.Y == sizeY - 1)
                        blizzard.Position.Y = 1;
                }



            }

            return Blizzards;
        }

        class MapCacheData
        {
            public bool Traverible { get; set; }
            public int StepsToStart { get; set; }
            public int StepsToGoal { get; set; }
        }

        public class Blizzard
        {
            public Vector2D Position { get; set; }
            public Vector2D Direction { get; set; }
            public string ToString()
            {
                return $"{Position.X},{Position.Y}";
            }
        }
    }
}
