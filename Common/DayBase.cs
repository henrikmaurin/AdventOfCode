using Common;

namespace AdventOfCode
{

    public class DayBase
    {
        public DayBase(int year, int day)
        {
            Year = year;
            Day = day;

            if (File.Exists("AOCCookie.txt"))
            {
                string cookie = File.ReadAllText("AOCCookie.txt");
                input = new AOCGetInput(cookie, Year, Day);
            }
        }

        public DayBase(bool runtests)
        {

        }

        public int Year { get; private set; }
        public int Day { get; private set; }
        protected AOCGetInput input;

    }
}
