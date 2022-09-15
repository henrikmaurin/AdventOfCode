using Common;

namespace AdventOfCode2021
{
    public class DayBase
    {
        protected AOCGetInput input;
        public DayBase()
        {
            if (File.Exists("AOCCookie.txt"))
            {
                string cookie = File.ReadAllText("AOCCookie.txt");
                input = new AOCGetInput(cookie);
            }
        }

    }
}
