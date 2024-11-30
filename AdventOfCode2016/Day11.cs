using Common;

using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016
{

    public class Day11 : DayBase, IDay
    {
        private const int day = 11;
        List<string> data;

        List<Item> Items;

        public Day11(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                return;
            }

            data = input.GetDataCached().SplitOnNewline(true);

            int floor = 0;
            Items = new List<Item>();

            Dictionary<string, string> shortNameMap = new Dictionary<string, string>();
            char currentShortName = 'A';

            foreach (string instruction in data)
            {
                string[] tokens = instruction.Replace(".", "").Replace(",", "").Split(" ");
                for (int i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i].In("generator", "microchip"))
                    {
                        string name = tokens[i - 1].Replace("-compatible", "");
                        if (!shortNameMap.ContainsKey(name))
                            shortNameMap.Add(name, $"{currentShortName++}");

                        string shortName = shortNameMap[name];
                        string type = tokens[i] == "generator" ? "G" : "M";

                        Items.Add(new Item
                        {
                            Isotope = name,
                            Type = type,
                            Floor = floor,
                            Alias = $"{shortName}{type}",
                        });
                    }
                }
                floor++;
            }

            Items = Items.OrderBy(i => i.Alias).ToList();
        }
        public void Run()
        {
            SetupExample();

            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }


        public void SetupExample()
        {
            string testdata = @"The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
The second floor contains a hydrogen generator.
The third floor contains a lithium generator.
The fourth floor contains nothing relevant.";

            data = testdata.SplitOnNewline(true);

            int floor = 0;
            Items = new List<Item>();

            Dictionary<string, string> shortNameMap = new Dictionary<string, string>();
            char currentShortName = 'A';

            foreach (string instruction in data)
            {
                string[] tokens = instruction.Replace(".", "").Replace(",", "").Split(" ");
                for (int i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i].In("generator", "microchip"))
                    {
                        string name = tokens[i - 1].Replace("-compatible", "");
                        if (!shortNameMap.ContainsKey(name))
                            shortNameMap.Add(name, $"{currentShortName++}");

                        string shortName = shortNameMap[name];
                        string type = tokens[i] == "generator" ? "G" : "M";

                        Items.Add(new Item
                        {
                            Isotope = name,
                            Type = type,
                            Floor = floor,
                            Alias = $"{shortName}{type}",
                        });
                    }
                }
                floor++;
            }

            Items = Items.OrderBy(i => i.Alias).ToList();
        }

        public void AddSteps(Queue<QueuedItem> items)
        {
            int floor = 0;
            int steps = 0;

            // 1
            floor = 1;
            steps++;
            List<Item> list = Items.Select(i => i.GetCopy(0)).ToList();
            list[1].Floor = floor;
            items.Enqueue(new QueuedItem
            {
                CurrentFloor = floor,
                Items = list,
                Steps = steps,
            });

            // 2
            floor = 2;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[0].Floor = floor;
            list[1].Floor = floor;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = floor,
                Items = list,
                Steps = steps,
            });

            // 3
            floor = 1;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[1].Floor = 1;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = 1,
                Items = list,
                Steps = steps,
            });

            // 4
            floor = 0;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[1].Floor = 0;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = 0,
                Items = list,
                Steps = steps,
            });

            // 5
            floor = 1;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[1].Floor = 1;
            list[3].Floor = 1;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = 1,
                Items = list,
                Steps = steps,
            });

            // 6
            floor = 2;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[1].Floor = floor;
            list[3].Floor = floor;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = floor,
                Items = list,
                Steps = steps,
            });

            // 7
            floor = 3;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[1].Floor = floor;
            list[3].Floor = floor;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = floor,
                Items = list,
                Steps = steps,
            });

            // 8
            floor = 2;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[1].Floor = floor;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = floor,
                Items = list,
                Steps = steps,
            });

            // 9
            floor = 3;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[0].Floor = floor;
            list[2].Floor = floor;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = floor,
                Items = list,
                Steps = steps,
            });

            // 10
            floor = 2;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[3].Floor = floor;

            items.Enqueue(new QueuedItem
            {
                CurrentFloor = floor,
                Items = list,
                Steps = steps,
            });

            // 11
            floor = 3;
            steps++;
            list = list.Select(i => i.GetCopy(0)).ToList();
            list[1].Floor = floor;
            list[3].Floor = floor;
            items.Enqueue(new QueuedItem
            {
                CurrentFloor = floor,
                Items = list,
                Steps = steps,
            });

        }

        public int Problem1()
        {
            List<string> vailid = new List<string>
            {
                "AG1AM0BG2BM0",
"AG1AM1BG2BM0",
"AG2AM2BG2BM0",
"AG2AM1BG2BM0",
"AG2AM0BG2BM0",
"AG2AM1BG2BM1",
"AG2AM2BG2BM2",
"AG2AM3BG2BM3",
"AG2AM2BG2BM3",
"AG3AM2BG3BM3",
"AG3AM2BG3BM2",
"AG3AM3BG3BM3",
            };

            Queue<QueuedItem> items = new Queue<QueuedItem>();

            items.Enqueue(new QueuedItem
            {
                Items = Items,
                Steps = 0,
                CurrentFloor = 0,
            });

            // AddSteps(items);

            HashSet<string> processed = new HashSet<string>();

            while (items.Count > 0)
            {
                QueuedItem item = items.Dequeue();

                string uniqueKey = item.Uniquestring();

                if (vailid.Contains(uniqueKey))
                {
                    vailid.Remove(uniqueKey);
                }


                if (item.IsAllTop())
                {
                    return item.Steps;
                }

                if (processed.Contains(uniqueKey))
                {
                    continue;
                }

                processed.Add(uniqueKey);

                if (item.HasUnprotectedChip())
                {
                    item.Plot();

                    continue;
                }

                if (uniqueKey == "AG2AM1BG2BM0")
                {
                    int a = 1;
                }

                if (item.CurrentFloor > 0)
                {
                    foreach (List<Item> movedItems in item.GetAllMovements(item.CurrentFloor, -1))
                    {
                        items.Enqueue(new QueuedItem
                        {
                            Items = movedItems,
                            Steps = item.Steps + 1,
                            CurrentFloor = item.CurrentFloor - 1,
                        });
                    }
                }

                if (item.CurrentFloor < 3)
                {
                    foreach (List<Item> movedItems in item.GetAllMovements(item.CurrentFloor, 1))
                    {
                        items.Enqueue(new QueuedItem
                        {
                            Items = movedItems,
                            Steps = item.Steps + 1,
                            CurrentFloor = item.CurrentFloor + 1,
                        });
                    }
                }
            }
            return 0;
        }
        public int Problem2()
        {
            return 0;
        }

        public class QueuedItem
        {
            public List<Item> Items { get; set; }
            public int Steps { get; set; }
            public int CurrentFloor { get; set; }

            public bool IsAllTop()
            {
                return Items.Where(i => i.Floor == 3).Count() == Items.Count;
            }

            public string Uniquestring()
            {
                return $"E{CurrentFloor}"+ string.Join("", Items.Select(i => i.Key));
            }

            public bool HasUnprotectedChip()
            {
                foreach (Item chip in Items.Where(i => i.Type == "M"))
                {
                    if (Items.Where(i => i.Isotope == chip.Isotope).Where(i => i.Type == "G").Single().Floor == chip.Floor)
                    {
                        continue;
                    }
                    if (Items.Where(i => i.Type == "G").Where(i => i.Floor == chip.Floor).Any())
                    {
                        return true;
                    }
                }
                return false;
            }

            public List<List<Item>> GetAllMovements(int floor, int movement)
            {
                List<List<Item>> listOfListOfItems = new List<List<Item>>();

                // 0 moved
                List<Item> movedItems = new List<Item>();
                //foreach (Item item in Items)
                //{
                //    movedItems.Add(item.GetCopy(0));
                //}
                //listOfListOfItems.Add(movedItems);

                List<Item> itemsOnFloor = Items.Where(i => i.Floor == floor).ToList();

                for (int i1 = 0; i1 < itemsOnFloor.Count; i1++)
                {
                    // move single
                    movedItems = new List<Item>();
                    movedItems.Add(itemsOnFloor[i1].GetCopy(movement));
                    movedItems.AddRange(Items
                        .Where(i => i != itemsOnFloor[i1])
                        .Select(i => i
                        .GetCopy(0))
                        );

                    movedItems = movedItems.OrderBy(i => i.Alias).ToList();
                    listOfListOfItems.Add(movedItems);
                    for (int i2 = i1 + 1; i2 < itemsOnFloor.Count; i2++)
                    {
                        movedItems = new List<Item>();
                        movedItems.Add(itemsOnFloor[i1].GetCopy(movement));
                        movedItems.Add(itemsOnFloor[i2].GetCopy(movement));
                        movedItems.AddRange(Items
                            .Where(i => i != itemsOnFloor[i1])
                            .Where(i => i != itemsOnFloor[i2])
                            .Select(i => i.GetCopy(0)));

                        movedItems = movedItems.OrderBy(i => i.Alias).ToList();
                        listOfListOfItems.Add(movedItems);
                    }
                }

                return listOfListOfItems;
            }

            public void Plot()
            {
                for (int floor = 3; floor >= 0; floor--)
                {
                    Console.Write($"F{floor} ");
                    if (CurrentFloor == floor)
                    {
                        Console.Write("E  ");
                    }
                    else
                    {
                        Console.Write(".  ");
                    }

                    for (int item = 0; item < Items.Count; item++)
                    {
                        if (Items[item].Floor == floor)
                        {
                            Console.Write($"{Items[item].Alias} ");
                        }
                        else
                        {
                            Console.Write(".  ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(Uniquestring());

            }

        }

        public class Item
        {
            public string Type { get; set; }
            public string Isotope { get; set; }
            public int Floor { get; set; }
            public string Alias { get; set; }

            public Item GetCopy(int movement)
            {
                return new Item
                {
                    Type = Type,
                    Isotope = Isotope,
                    Floor = Floor + movement,
                    Alias = Alias
                };
            }

            public string Key { get => $"{Alias}{Floor}"; }

        }
    }
}