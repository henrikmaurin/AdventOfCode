using Common;

namespace AdventOfCode2015
{
    public class Day21 : DayBase, IDay
    {
        private const int day = 21;
        private string[] data;
        private List<StoreItem> Store;
        private Dictionary<string, Category> Categories;
        public Day21(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            data = input.GetDataCached().SplitOnNewlineArray();
            SetupStore();
        }
        public int Problem1()
        {
            return TestAllCombinations();
        }
        public int Problem2()
        {
            return TestAllCombinations(true); 
        }

        public void Run()
        {
            int finalFloor = Problem1();
            Console.WriteLine($"P1: Least amount of gold spent: {finalFloor}");

            int position = Problem2();
            Console.WriteLine($"P2: Most amount of gold spent: {position}");
        }

        public int TestAllCombinations(bool fightForMaxGold = false)
        {
            int minGold = int.MaxValue;
            int maxGold = int.MinValue;
            List<StoreItem> weapons = Store.Where(storeitem => storeitem.Category == "Weapons").ToList();
            List<StoreItem> Armor = Store.Where(storeitem => storeitem.Category == "Armor").ToList();
            List<StoreItem> Rings = Store.Where(storeitem => storeitem.Category == "Rings").ToList();

            for (int weapon = 0; weapon < weapons.Count; weapon++)
            {
                for (int numOfArmor = Categories["Armor"].MinItems; numOfArmor <= Categories["Armor"].MaxItems; numOfArmor++)
                {
                    for (int armorPiece = 0; armorPiece < Armor.Count; armorPiece++)
                    {
                        for (int numOfRings = 0; numOfRings <= 2; numOfRings++)
                        {
                            for (int ring1 = 0; ring1 < Rings.Count; ring1++)
                            {
                                for (int ring2 = ring1 + 1; ring2 < Rings.Count; ring2++)
                                {
                                    Combatant Hero = new Combatant { Name = "Hero", Hitpoints = 100 };
                                    Hero.Items.Add(weapons[weapon]);
                                    if (numOfArmor > 0)
                                        Hero.Items.Add(Armor[armorPiece]);
                                    if (numOfRings > 0)
                                        Hero.Items.Add(Rings[ring1]);
                                    if (numOfRings > 1 && ring2 < Rings.Count)
                                        Hero.Items.Add(Rings[ring2]);

                                    Combatant Enemy = ParseEnemy(data);

                                    int cost = Hero.Items.Select(i => i.Cost).Sum();
                                    if (FightUntilDeath(Hero,Enemy))
                                    {                                         
                                        if (cost < minGold)
                                            minGold = cost;                                    
                                    }
                                    else
                                    {
                                        if (cost>maxGold)
                                            maxGold = cost;
                                    }

                                }
                            }
                        }
                    }
                }
            }



            return fightForMaxGold? maxGold: minGold;
        }

        public void SetupStore()
        {
            Store = new List<StoreItem>
            {
                new StoreItem { Name = "Dagger", Category = "Weapons", Armor = 0, Damage = 4, Cost = 8 },
                new StoreItem { Name = "Shortsword", Category = "Weapons", Armor = 0, Damage = 5, Cost = 10 },
                new StoreItem { Name = "Warhammer", Category = "Weapons", Armor = 0, Damage = 6, Cost = 25 },
                new StoreItem { Name = "Longsword", Category = "Weapons", Armor = 0, Damage = 7, Cost = 40 },
                new StoreItem { Name = "Greataxe", Category = "Weapons", Armor = 0, Damage = 8, Cost = 74 },

                new StoreItem { Name = "Leather", Category = "Armor", Armor = 1, Damage = 0, Cost = 13 },
                new StoreItem { Name = "Chainmail", Category = "Armor", Armor = 2, Damage = 0, Cost = 31 },
                new StoreItem { Name = "Splintmail", Category = "Armor", Armor = 3, Damage = 0, Cost = 53 },
                new StoreItem { Name = "Bandedmail", Category = "Armor", Armor = 4, Damage = 0, Cost = 75 },
                new StoreItem { Name = "Platemail", Category = "Armor", Armor = 5, Damage = 0, Cost = 102 },

                new StoreItem { Name = "Platemail", Category = "Rings", Armor = 0, Damage = 1, Cost = 25 },
                new StoreItem { Name = "Platemail", Category = "Rings", Armor = 0, Damage = 2, Cost = 50 },
                new StoreItem { Name = "Platemail", Category = "Rings", Armor = 0, Damage = 3, Cost = 100 },
                new StoreItem { Name = "Platemail", Category = "Rings", Armor = 1, Damage = 0, Cost = 20 },
                new StoreItem { Name = "Platemail", Category = "Rings", Armor = 2, Damage = 0, Cost = 40 },
                new StoreItem { Name = "Platemail", Category = "Rings", Armor = 3, Damage = 0, Cost = 80 }
            };
            Categories = new Dictionary<string, Category>
            {
                { "Weapons", new Category { Name = "Weapons", MinItems = 1, MaxItems = 1 } },
                { "Armor", new Category {Name="Armor",MinItems=0,MaxItems=1} },
                { "Rings",new Category {Name="Rings",MinItems=0,MaxItems=2}},
            };
        }


        public bool FightUntilDeath(Combatant hero, Combatant enemy)
        {
            while (hero.Hitpoints > 0)
            {
                enemy.Hitpoints -= MathHelpers.Highest(1, hero.Items.Select(i => i.Damage).Sum() - enemy.Items.Select(i => i.Armor).Sum());
                if (enemy.Hitpoints <= 0)
                {
                    return true;
                }
                hero.Hitpoints -= MathHelpers.Highest(1, enemy.Items.Select(i => i.Damage).Sum() - hero.Items.Select(i => i.Armor).Sum());

            }
            return false;
        }

        public Combatant ParseEnemy(string[] enemydata)
        {
            Combatant enemy = new Combatant();
            enemy.Name = "Boss";
            enemy.Hitpoints = enemydata[0].Split(' ').Last().ToInt();
            enemy.Items.Add(new StoreItem { Name = "Weapon", Damage = enemydata[1].Split(' ').Last().ToInt() });
            enemy.Items.Add(new StoreItem { Name = "Armor", Armor = enemydata[2].Split(" ").Last().ToInt() });
            return enemy;
        }

        public class StoreItem
        {
            public string Name { get; set; }
            public int Cost { get; set; }
            public int Damage { get; set; }
            public int Armor { get; set; }
            public string Category { get; set; }
        }

        public class Category
        {
            public string Name { get; set; }
            public int MaxItems { get; set; }
            public int MinItems { get; set; }
        }

        public class Combatant
        {
            public string Name { get; set; }
            public int Hitpoints { get; set; }
            public List<StoreItem> Items { get; set; } = new List<StoreItem>();
        }


    }
}
