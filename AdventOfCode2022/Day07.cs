﻿using Common;

namespace AdventOfCode2022
{
    public class Day07 : DayBase, IDay
    {
        private const int day = 7;
        List<string> data;
        public Day07(string testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewline(); return;
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
            return GetSumOfDirectories(data, 100000);
        }
        public int Problem2()
        {
            return 0;
        }

        public int GetSumOfDirectories(List<string> lines, int maxSize)
        {
            int dirSum = 0;
            Directory root = new Directory { Name = "/" };
            root.ParentDir = root;
            Directory currentDir = root;
            int currentline = 0;
            while (currentline < lines.Count)
            {
                if (lines[currentline].StartsWith("$ cd"))
                {
                    dirSum = 0;
                    if (lines[currentline].Split(' ').Last() == "/")
                        currentDir = root;
                    else if (lines[currentline].Split(' ').Last() == "..")
                    {
                        currentDir = currentDir.ParentDir;
                    }
                    else
                    {
                        string dirname = lines[currentline].Split(' ').Last();
                        currentDir = currentDir.Directories.Where(d => d.Name == dirname).Single();
                    }

                    currentline++;
                    continue;
                }

                if (lines[currentline] == "$ ls" && currentline < lines.Count - 1)
                {
                    dirSum = 0;
                    currentline++;
                    while (currentline < lines.Count - 1 && !lines[currentline].StartsWith("$"))
                    {
                        if (lines[currentline].StartsWith("dir"))
                        {
                            Directory newDir = new Directory();
                            newDir.Name = lines[currentline].Split(" ").Last();
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

            var dirs = root.Flatten();

            int used = root.GetTotalSize();

            int totalfree = (70000000 - used);

            int needToFree = 30000000 - totalfree;

            int smallest = dirs.Where(d => d.GetTotalSize() >= needToFree).OrderBy(d => d.GetTotalSize()).Select(d => d.GetTotalSize()).First();
            return smallest;


            int sum = 0;
            foreach (var dir in dirs)
            {
                int s = dir.GetTotalSize();
                if (s <= maxSize)
                    sum += s;

            }


            return sum;
        }
    }

    class Directory
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

    }
}
