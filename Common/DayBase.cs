namespace Common
{

    public class DayBase
    {

        public DayBase(int year, int day, bool runtests = false)
        {
            Year = year;
            Day = day;

            if (runtests)
                return;

            if (!File.Exists("AOCCookie.txt"))
            {
                SetCookie();
            }

            if (!File.Exists("AOCEmail.txt"))
            {
                SetEmail();
            }

            if (File.Exists("AOCCookie.txt") && File.Exists("AOCEmail.txt"))
            {
                string cookie = File.ReadAllText("AOCCookie.txt");
                string email = File.ReadAllText("AOCEmail.txt");
                input = new AOCGetInput(cookie, email, Year, Day);
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

        public void SetEmail()
        {
            Console.Write("Enter email: ");
            Email = Console.ReadLine();

            if (!string.IsNullOrEmpty(Email))
            {
                File.WriteAllText("AOCEmail.txt", Email);
            }
        }


        public int Year { get; private set; }
        public int Day { get; private set; }
        public string? Cookie { get; private set; }
        public string? Email { get; private set; }
        protected AOCGetInput input;

    }



}
