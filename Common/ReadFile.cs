using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class ReadFile
    {
        static public string ReadText(string file)
        {
            return File.ReadAllText(Path.Combine("data", file));
        }

        static public string[] ReadLines(string file)
        {
            return File.ReadAllLines(Path.Combine("data", file));
        }

    }
}
