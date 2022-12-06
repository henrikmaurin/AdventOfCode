using Common;
using System.Text;

namespace AdventOfCode2015
{
    public class Day19 : DayBase, IDay
    {
        private const int day = 19;
        private string[][] data;
        List<Replacement> replacements;
        private Dictionary<string, int> generations;
        public Day19(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.GroupByEmptyLine();
                replacements = data[0].Select(i => new Replacement(i)).ToList();
                return;
            }

            data = input.GetDataCached().GroupByEmptyLine();
            replacements = data[0].Select(i => new Replacement(i)).ToList();
            generations = new Dictionary<string, int>();
        }
        public int Problem1()
        {         
            return Calibrate(data[1][0]);
        }
        public int Problem2()
        {
            string target = data[1][0];
            int uppercase = target.Where(t => t == t.ToUpper()).Count();

            return (uppercase - Count("Rn", target) - Count("Ar", target) - 2 * Count("Y", target) - 1);




            int generation = 1;
           
            Expand(target, "e", generation);
            while (generation < 300)
            {
                Console.WriteLine(generation + " " + generations.Where(g => g.Value == generation).Count());
                foreach (string possible in generations.Where(g=>g.Value==generation).Select(g=>g.Key).ToList())
                {
                    if (Expand(target, possible, generation+1))
                        return generation;
                }

               generation++;
            }

            return 0;
        }

        private int Count(string what, string molecule)
        {int pos = 0;
            int count = 0;
            while (pos < molecule.Length) {
            int nextmatch = molecule.IndexOf(what, pos);
                if (nextmatch == -1)
                    break;
                count++;
            
                pos = nextmatch + what.Length;
            
            }
            return count;
        }

        public void Run()
        {
            int result1 = Problem1();
            Console.WriteLine($"P1: Calibration: {result1}");

            int result2 = Problem2();
            Console.WriteLine($"P2: Least amount of iterations: {result2}");
        }

        public int Calibrate(string calibratondata)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (Replacement replacement in replacements) {
                int i = 0;
                string padded = $" {calibratondata} ";
                while (i< padded.Length)
                {                 

                    int nextMatch = padded.IndexOf(replacement.From,i);
                    if (nextMatch == -1)
                        break;

                    string newString = padded.Substring(0,nextMatch);
                    newString += replacement.To;
                    i = nextMatch + replacement.From.Length;
                    newString += padded.Substring(i);
                    set.TryAdd(newString.Trim());
                }              
            }
            return set.Count();
        }

        public bool Expand(string target, string seed, int generation)
        {            
            foreach (Replacement replacement in replacements)
            {
                int i = 0;
                string padded = $" {seed} ";
                while (i < padded.Length)
                {

                    int nextMatch = padded.IndexOf(replacement.From, i);
                    if (nextMatch == -1)
                        break;

                    string newString = padded.Substring(0, nextMatch);
                    newString += replacement.To;
                    i = nextMatch + replacement.From.Length;
                    newString += padded.Substring(i);

                    if (newString.Trim() == target)
                        return true;

                    if (newString.Trim().Length >= target.Length)
                        return false;

                    if (generations.ContainsKey(newString.Trim()))
                        return false;

                    generations.Add(newString.Trim(), generation);
                }
            }
            return false;
        }




        internal class Replacement
        {
            public string From { get; set; }
            public string To { get; set; }
            public Replacement(string instuction)
            {
                From= instuction.Split("=>").First().Trim();
                To= instuction.Split("=>").Last().Trim();   
            }
        }
       


    }
}
