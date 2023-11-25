// See https://aka.ms/new-console-template for more information

using AdventOfCode2015;

using Common;

using Runner;


string lastrunfile = "lastrun.txt";
string lastrun = string.Empty;
int lastRunYear = 0;
int lastRunDay = 0;



if (File.Exists(lastrunfile))
{
    lastrun = File.ReadAllText(lastrunfile);
    if (!string.IsNullOrEmpty(lastrun))
    {
        lastRunYear = lastrun.Tokenize(',').First().ToInt();
        lastRunDay = lastrun.Tokenize(',').Last().ToInt();
    }

}

/*
VisualAoC visualAoC = new VisualAoC(120, 50);
visualAoC.ProjectName = "Year2022Day01.aoc";

visualAoC.Data = @"Rad1
Rad 2
Rad3
Rad1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111".SplitOnNewlineArray();
visualAoC.CurrentDataLine = 1;
visualAoC.Variables.Add("bananer", "4");
visualAoC.Variables.Add("sopor", "100000000000000000000000000");


visualAoC.DrawInterface();
visualAoC.PrintAtDrawArea(1, 1, "Hej!");
visualAoC.Display();
*/



Console.WriteLine("Today(Enter)/Repeat(R)/Year(YYYY)/Animate(A):");
string? input = Console.ReadLine();

int year = 0;
int day = 0;

bool animate = false;

switch (input)
{
    case "":
        if (DateTime.Now.Month == 12)
        {
            year = DateTime.Now.Year;
            day = DateTime.Now.Day;
        }
        else
            Console.WriteLine("Current month is not December");
        break;
    case "R":
    case "r":
        year = lastRunYear;
        day = lastRunDay;
        break;
    case "A":
    case "a":
        Console.WriteLine("Year:");
        year = Console.ReadLine().ToInt();
        Console.WriteLine("Day:");
        day = Console.ReadLine().ToInt();
        animate = true;
        break;
    default:
        year = input.ToInt();
        Console.WriteLine("Day/All(A):");
        day = Console.ReadLine().Replace("A", "0").Replace("a", "0").ToInt();

        break;
}

if (year != 0 && day != 0)
    File.WriteAllText(lastrunfile, $"{year},{day}");

if (!Run(year, day, animate))
{
    Console.WriteLine("Not implemented yet");
}
//Console.ReadLine();



bool Run(int year, int day, bool animate = false)
{
    IYear? yearToRun = DayHelper.GetYear(year);

    if (yearToRun == null)
    {
        Console.WriteLine("Could not find year");
        return false;
    }

    if (day == 0)
    {
        Console.WriteLine($"Running all days for {year}");

        for (int i = 1; i <= 25; i++)
        {
            Console.WriteLine($"Day {i}");
            IDay dayToRun = yearToRun.Day(i);
            if (dayToRun != null)
                dayToRun.Run();
        }
    }
    else
    {
        if (animate)
        {
            IAnimation dayToRun = yearToRun.DayAnimation(day);
            if (dayToRun == null)
            {
                Console.WriteLine("Could not find day");
                return false;
            }

            dayToRun.Animate();
            return true;
        }
        else
        {

            IDay dayToRun = yearToRun.Day(day);
            if (dayToRun == null)
            {
                Console.WriteLine("Could not find day");
                return false;
            }

            dayToRun.Run();
        }
    }

    return true;
}