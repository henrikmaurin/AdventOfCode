using Common;

namespace AdventOfCode2022
{
    public class Day18 : DayBase, IDay
    {
        private const int day = 18;
        List<string> data;
        List<Droplet> droplets;
        public Day18(string? testdata = null) : base(Global.Year, day, testdata != null)
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
            int result1 = MeasureExecutionTime(() => Problem1());
            WriteAnswer(1, "Result: {result}", result1);

            int result2 = MeasureExecutionTime(() => Problem2());
            WriteAnswer(2, "Result: {result}", result2);
        }
        public int Problem1()
        {
            Parse(data);
            FindNeigbors();
            return CountExposed();

        }
        public int Problem2()
        {
            Parse(data);
            AddVoid();
            FindNeigbors();
            return CountExposed();
        }

        public int CountExposed()
        {
            return droplets.Select(d => d.ExposedSides).Sum(); ;
        }

        public void Parse(List<string> dropletData)
        {
            droplets = new List<Droplet>();
            foreach (string dropletLine in dropletData)
            {
                droplets.Add(new Droplet
                {
                    X = dropletLine.Split(",")[0].ToInt(),
                    Y = dropletLine.Split(",")[1].ToInt(),
                    Z = dropletLine.Split(",")[2].ToInt(),
                    ExposedSides = 6,
                    type = "lava",
                });
            }
        }

        public void FindNeigbors()
        {
            foreach (Droplet droplet in droplets)
            {
                if (droplets.Where(d => d.X == droplet.X - 1 && d.Y == droplet.Y && d.Z == droplet.Z).Any())
                    droplet.ExposedSides--;

                if (droplets.Where(d => d.X == droplet.X + 1 && d.Y == droplet.Y && d.Z == droplet.Z).Any())
                    droplet.ExposedSides--;

                if (droplets.Where(d => d.X == droplet.X && d.Y == droplet.Y - 1 && d.Z == droplet.Z).Any())
                    droplet.ExposedSides--;

                if (droplets.Where(d => d.X == droplet.X && d.Y == droplet.Y + 1 && d.Z == droplet.Z).Any())
                    droplet.ExposedSides--;

                if (droplets.Where(d => d.X == droplet.X && d.Y == droplet.Y && d.Z == droplet.Z - 1).Any())
                    droplet.ExposedSides--;

                if (droplets.Where(d => d.X == droplet.X && d.Y == droplet.Y && d.Z == droplet.Z + 1).Any())
                    droplet.ExposedSides--;
            }
        }

        public void AddVoid()
        {
            for (int x = droplets.Select(d => d.X).Min(); x <= droplets.Select(d => d.X).Max(); x++)
            {
                for (int y = droplets.Select(d => d.Y).Min(); y <= droplets.Select(d => d.Y).Max(); y++)
                {
                    for (int z = droplets.Select(d => d.Z).Min(); z <= droplets.Select(d => d.Z).Max(); z++)
                    {
                        if (droplets.Where(d => d.X == x && d.Y == y && d.Z == z).Any())
                            continue;

                        droplets.Add(new Droplet
                        {
                            X = x,
                            Y = y,
                            Z = z,
                            ExposedSides = 6,
                            type = "unknown"
                        });

                    }
                }
            }

            int updated = 1;
            while (updated > 0)
            {
                updated = 0;
                foreach (Droplet droplet in droplets.Where(d => d.type == "unknown"))
                {
                    if (droplet.X <= droplets.Select(d => d.X).Min()
                        || droplet.X >= droplets.Select(d => d.X).Max()
                        || droplet.Y <= droplets.Select(d => d.Y).Min()
                        || droplet.Y >= droplets.Select(d => d.Y).Max()
                        || droplet.Z <= droplets.Select(d => d.Z).Min()
                        || droplet.Z >= droplets.Select(d => d.Z).Max())
                    {
                        droplet.type = "outside";
                        updated++;
                    }
                    if (droplets.Where(d => d.X == droplet.X - 1 && d.Y == droplet.Y && d.Z == droplet.Z && d.type == "outside").Any()
                    || droplets.Where(d => d.X == droplet.X + 1 && d.Y == droplet.Y && d.Z == droplet.Z && d.type == "outside").Any()
                    || droplets.Where(d => d.X == droplet.X && d.Y == droplet.Y - 1 && d.Z == droplet.Z && d.type == "outside").Any()
                    || droplets.Where(d => d.X == droplet.X && d.Y == droplet.Y + 1 && d.Z == droplet.Z && d.type == "outside").Any()
                    || droplets.Where(d => d.X == droplet.X && d.Y == droplet.Y && d.Z == droplet.Z - 1 && d.type == "outside").Any()
                    || droplets.Where(d => d.X == droplet.X && d.Y == droplet.Y && d.Z == droplet.Z + 1 && d.type == "outside").Any())
                    {
                        droplet.type = "outside";
                        updated++;
                    }
                }
            }

            droplets = droplets.Where(d => d.type != "outside").ToList();
        }

        public class Droplet
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int ExposedSides { get; set; }
            public string type { get; set; }
        }
    }

}
