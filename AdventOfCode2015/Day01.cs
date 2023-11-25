using Common;

namespace AdventOfCode2015
{
    public class Day01 : DayBase, IDay, IAnimation
    {
        private const int day = 1;

        public Day01(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;
        }

        public void Animate()
        {
            Graphics graphics = new Graphics(Year,Day,20,10);
            string data = input.GetDataCached();
            int floor = 0;

            graphics.Text(9, 2, data.SafeSubstring(0,10));
            graphics.Text(5, 4, $"Floor: {floor}");

            graphics.SaveFrame();
            graphics.NewFrame();

            for (int i = 0; i < data.Length; i++)
            {
                char nextInstruction = data[i];
                string nextInstructionText = "";

                if (nextInstruction == '(')
                {
                    floor++;
                    nextInstructionText = "^";
                }
                else if (nextInstruction == ')')
                {
                    floor--;
                    nextInstructionText = "v";
                } 

                graphics.Text(5, 2, nextInstruction.ToString());
                graphics.Text(5, 3, nextInstructionText);
                graphics.Text(5, 4, $"Floor: {floor}");
                graphics.Text(9, 2, data.Substring(0, 10));
                graphics.SaveFrame();
                graphics.NewFrame();
            }


        }

        public int Problem1()
        {
            string data = input.GetDataCached();

            return Travel(data);
        }
        public int Problem2()
        {
            string data = input.GetDataCached();

            return TravelTo(data, -1);
        }

        public void Run()
        {
            int finalFloor = Problem1();
            Console.WriteLine($"P1: Santa ends up on floor: {finalFloor}");

            int position = Problem2();
            Console.WriteLine($"P2: Santa ends up in basement at position: {position}");
        }

        public int Travel(string directions)
        {
            return directions.ToCharArray().Where(a => a == '(').Count() - directions.ToCharArray().Where(a => a == ')').Count();
        }

        public int TravelTo(string directions, int target)
        {
            int steps = 0;
            int level = 0;
            while (level != target)
            {

                if (directions[steps] == '(')
                    level++;
                else if (directions[steps] == ')')
                    level--;
                steps++;
            }

            return steps;
        }
    }
}
