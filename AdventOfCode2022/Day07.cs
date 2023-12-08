using Common;

namespace AdventOfCode2022
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        private List<string> data;
        public Directory Filesystem { get; protected set; }
        public Day07(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline(); return;
            }

            data = input.GetDataCached().SplitOnNewline();
            RunLog(data);
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
            return Filesystem.Flatten().Select(d => d.GetTotalSize()).Where(ts => ts <= 100000).Sum();
        }
        public int Problem2()
        {
            var dirs = Filesystem.Flatten();
            int used = Filesystem.GetTotalSize();

            int totalfree = (70000000 - used);
            int needToFree = 30000000 - totalfree;

            int smallest = dirs.Where(d => d.GetTotalSize() >= needToFree).OrderBy(d => d.GetTotalSize()).Select(d => d.GetTotalSize()).First();

            return smallest;
        }

        public void RunLog(List<string> lines)
        {
            int dirSum = 0;
            Filesystem = new Directory { Name = "/" };
            Filesystem.ParentDir = Filesystem;
            Directory currentDir = Filesystem;
            int currentline = 0;
            while (currentline < lines.Count)
            {
                if (lines[currentline].StartsWith("$ cd"))
                {
                    dirSum = 0;
                    if (lines[currentline].Split(' ').Last() == "/")
                        currentDir = Filesystem;
                    else if (lines[currentline].Split(' ').Last() == "..")
                    {
                        currentDir = currentDir.ParentDir;
                    }
                    else
                    {
                        string dirname = (currentDir.Name + "/" + lines[currentline].Split(' ').Last()).Replace("//", "/");
                        currentDir = currentDir.Directories.Where(d => d.Name == dirname).Single();
                    }

                    currentline++;
                    continue;
                }

                if (lines[currentline] == "$ ls" && currentline < lines.Count - 1)
                {
                    dirSum = 0;
                    currentline++;
                    while (currentline < lines.Count && !lines[currentline].StartsWith("$"))
                    {
                        if (lines[currentline].StartsWith("dir"))
                        {
                            Directory newDir = new Directory();
                            newDir.Name = (currentDir.Name + "/" + lines[currentline].Split(" ").Last()).Replace("//", "/");
                            newDir.ParentDir = currentDir;
                            currentDir.Directories.Add(newDir);

                        }
                        else if (int.TryParse(lines[currentline].Split(' ').First(), out int n))
                            currentDir.FileSizes += n;
                        currentline++;
                    }
                    continue;
                }
                currentline++;

            }


        }
    }

    public class Directory
    {
        public static int Acc;
        public string Name { get; set; }
        public int FileSizes { get; set; }
        public List<Directory> Directories { get; set; } = new List<Directory>();
        public Directory ParentDir = null;

        public int GetTotalSize()
        {
            int totalsize = FileSizes + Directories.Select(d => d.GetTotalSize()).Sum();

            return totalsize;
        }

        public List<Directory> Flatten()
        {
            List<Directory> dirs = new List<Directory>();
            dirs.Add(this);
            foreach (Directory directory in Directories)
            {
                dirs.AddRange(directory.Flatten());
            }
            return dirs;
        }

        public string ToString()
        {
            return $"Name:{Name}, Files:{FileSizes}, TotalSize:{GetTotalSize}";
        }
    }
}