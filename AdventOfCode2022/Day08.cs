using Common;
using System.Drawing;
using System.Text;

namespace AdventOfCode2022
{
    public class Day08 : DayBase, IDay
    {
        private const int day = 8;
        private List<string> data;
        private Map2D<Tree> Map;
        public Day08(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline();
                Parse(data.ToArray());
                return;
            }

            data = input.GetDataCached().SplitOnNewline();
            Parse(data.ToArray());
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
           
            return Map.Map.Where(m => m.Visible == true).Count();
        }
        public int Problem2()
        {
            return Map.Map.Select(m => m.ScenicScore).Max(); ;
        }

        public void Parse(string[] mapdata)
        {
            int sizeX = mapdata[0].Length;
            int sizeY = mapdata.Length;
            Map = new Map2D<Tree>();
            Map.SafeOperations = true;
            Map.Init(sizeX, sizeY);

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    Map[x, y] = new Tree();
                    Map[x, y].Height = mapdata[y][x].ToInt();
                }
            }

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    Map[x, y].Visible = CanSeeBorder(x, y);
                    Map[x, y].ScenicScore = ScenicScore(x, y);
                }
            }

        }

        public bool CanSeeBorder(int xPos, int yPos)
        {
            bool canSeeLeft = true;
            bool canSeeRight = true;
            bool canSeeUp = true;
            bool canSeeDown = true;

       

            for (int x = 0; x < xPos; x++)
                if (Map[x, yPos].Height >= Map[xPos, yPos].Height)
                    canSeeLeft = false;

            for (int x = xPos + 1; x < Map.MaxX; x++)
                if (Map[x, yPos].Height >= Map[xPos, yPos].Height)
                    canSeeRight = false;

            for (int y = 0; y < yPos; y++)
                if (Map[xPos, y].Height >= Map[xPos, yPos].Height)
                    canSeeUp = false;

            for (int y = yPos + 1; y < Map.MaxY; y++)
                if (Map[xPos, y].Height >= Map[xPos, yPos].Height)
                    canSeeDown = false;


            return canSeeDown || canSeeLeft || canSeeRight || canSeeUp;
        }

        public int ScenicScore(int xPos, int yPos)
        {
            int canSeeLeft = 0;
            int canSeeRight = 0;
            int canSeeUp = 0;
            int canSeeDown = 0;

            var diections = Directions.GetNeigboingCoords();


            for (int x = xPos-1; x >=0; x--)
                if (Map[x, yPos].Height < Map[xPos, yPos].Height)
                    canSeeLeft++;
                else
                {
                    canSeeLeft++;
                    break;
                }
            for (int x = xPos + 1; x < Map.MaxX; x++)
                if (Map[x, yPos].Height < Map[xPos, yPos].Height)
                    canSeeRight++;
                else
                {
                    canSeeRight++;
                    break;
                }
            for (int y = yPos - 1; y >= 0; y--)
                if (Map[xPos, y].Height < Map[xPos, yPos].Height)
                    canSeeUp++;
                else
                {
                    canSeeUp++;
                    break;
                }
            for (int y = yPos + 1; y < Map.MaxY; y++)
                if (Map[xPos, y].Height < Map[xPos, yPos].Height)
                    canSeeDown++;
                else
                {
                    canSeeDown++;
                    break;
                } 

            return canSeeDown * canSeeLeft * canSeeRight * canSeeUp;

        }
    }

    class Tree
    {
        public bool Visible { get; set; }
        public int Height { get; set; }
        public int ScenicScore { get; set; }
    }
}
