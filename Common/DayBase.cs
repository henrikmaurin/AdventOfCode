namespace Common
{

    public class DayBase
    {

        public DayBase(int year, int day)
        {
            Year = year;
            Day = day;


            if (!File.Exists("AOCCookie.txt"))
            {
                SetCookie();
            }

            if (File.Exists("AOCCookie.txt"))
            {
                string cookie = File.ReadAllText("AOCCookie.txt");
                input = new AOCGetInput(cookie, Year, Day);
            }
        }

        public DayBase(bool runtests)
        {

        }

        public void SetCookie()
        {
            Console.Write("Enter cookie: ");
            Cookie = Console.ReadLine();

            if (!string.IsNullOrEmpty(Cookie))
            {
                File.WriteAllText("AOCCookie.txt", Cookie);
            }
        }


        public int Year { get; private set; }
        public int Day { get; private set; }
        public string? Cookie { get; private set; }
        protected AOCGetInput? input;

    }



}
