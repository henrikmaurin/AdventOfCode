// See https://aka.ms/new-console-template for more information


using Common;

Console.WriteLine("Year:");
string year = Console.ReadLine();
Console.WriteLine("Day:");
int day = int.Parse(Console.ReadLine());

if (!Run(year, day))
{
    Console.WriteLine("Not implemented yet");
}
//Console.ReadLine();



bool Run(string year, int day)
{
    IYear yearToRun = null;
    switch (year)
    {
        case "2015":
            yearToRun = new AdventOfCode2015.Year();
            break;
        case "2016":
            yearToRun = new AdventOfCode2016.Year();
            break;
    }

    if (yearToRun == null)
    {
        Console.WriteLine("Could not find year");
        return false;
    }

    IDay dayToRun = yearToRun.Day(day);
    if (dayToRun == null)
    {
        Console.WriteLine("Could not find day");
        return false;
    }

    dayToRun.Run();

    return true;
}