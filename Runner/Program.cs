// See https://aka.ms/new-console-template for more information


using AdventOfCode;
using Common;
using Runner;

Console.WriteLine("Year:");
int year = Console.ReadLine().ToInt();
Console.WriteLine("Day:");
int day = Console.ReadLine().ToInt();

if (!Run(year, day))
{
    Console.WriteLine("Not implemented yet");
}
//Console.ReadLine();



bool Run(int year, int day)
{
    IYear? yearToRun = DayHelper.GetYear(year);

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