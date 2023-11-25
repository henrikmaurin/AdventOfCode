using Common;

namespace AdventOfCode2015
{
    public class Day15 : DayBase, IDay
    {
        private const int day = 15;
        private string[] data;
        public Day15(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }

            data = input.GetDataCached().SplitOnNewlineArray();
        }
        public int Problem1(int traveldistance = 2503)
        {
            return GenerateRecipie(new Recipie { Ingredients = new List<Amount>() }, data.Select(d => new Ingredient(d)).ToList());
        }
        public int Problem2(int traveldistance = 2503)
        {

            return GenerateRecipie(new Recipie { Ingredients = new List<Amount>() }, data.Select(d => new Ingredient(d)).ToList(), 500);
        }

        public void Run()
        {
            int distance = Problem1();
            Console.WriteLine($"P1: Best recipie score: {distance}");

            int position = Problem2();
            Console.WriteLine($"P2: Best recipie with 500 calories: {position}");
        }

        private int GenerateRecipie(Recipie recipie, List<Ingredient> ingredients, int? targetCalories = null)
        {
            if (ingredients.Count == 0)
                return recipie.GetScore(targetCalories);

            int maxScore = 0;
            int totalTeaspoons = recipie.Ingredients.Select(i => i.Teaspoons).Sum();

            Ingredient currentIngredient = ingredients.Last();
            List<Ingredient> remainingIngredients = new List<Ingredient>(ingredients);
            remainingIngredients.RemoveAt(remainingIngredients.Count - 1);
            Recipie newRecipie = new Recipie();
            newRecipie.Ingredients = new List<Amount>(recipie.Ingredients);
            Amount amount = new Amount { Ingredient = currentIngredient };
            newRecipie.Ingredients.Add(amount);
            for (int i = 0; i <= 100 - totalTeaspoons; i++)
            {
                amount.Teaspoons = i;
                int score = GenerateRecipie(newRecipie, remainingIngredients, targetCalories);
                if (score > maxScore)
                    maxScore = score;
            }

            return maxScore;
        }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }
        public Ingredient() { }
        public Ingredient(string data)
        {
            Parse(data);
        }

        public void Parse(string data)
        {
            string[] parsed = data.Tokenize();
            Name = parsed[0].Replace(":", "");
            Capacity = parsed[2].Replace(",", "").ToInt();
            Durability = parsed[4].Replace(",", "").ToInt();
            Flavor = parsed[6].Replace(",", "").ToInt();
            Texture = parsed[8].Replace(",", "").ToInt();
            Calories = parsed[10].Replace(",", "").ToInt();
        }
    }

    public class Amount
    {
        public Ingredient Ingredient { get; set; }
        public int Teaspoons { get; set; }
    }

    public class Recipie
    {
        public List<Amount> Ingredients { get; set; }
        public int GetScore(int? targetCalories = null)
        {
            if (!IsValid())
                return 0;

            if (targetCalories != null && Ingredients.Select(i => i.Ingredient.Calories * i.Teaspoons).Sum() != targetCalories)
                return 0;


            int Capacity = Ingredients.Select(i => i.Ingredient.Capacity * i.Teaspoons).Sum();
            int Durability = Ingredients.Select(i => i.Ingredient.Durability * i.Teaspoons).Sum();
            int Flavor = Ingredients.Select(i => i.Ingredient.Flavor * i.Teaspoons).Sum();
            int Texture = Ingredients.Select(i => i.Ingredient.Texture * i.Teaspoons).Sum();

            if (Capacity < 0 || Durability < 0 || Flavor < 0 || Texture < 0)
                return 0;

            return Capacity * Durability * Flavor * Texture;
        }

        public bool IsValid()
        {
            return Ingredients.Select(i => i.Teaspoons).Sum() == 100;
        }
    }

}
