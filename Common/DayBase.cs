using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using static Crayon.Output;

namespace Common
{

    public class DayBase
    {

        public DayBase(int year, int day, bool runtests = false)
        {
            Year = year;
            Day = day;

            _executionTime = null;

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

        protected void WriteAnswer<T>(int problemNumber, string message, T result)
        {
            WriteAnswer(problemNumber, message, result.ToString());
        }

        protected void WriteAnswer(int problemNumber, string message, string result)
        {
            long? timeTaken = null;
            string key = $"Problem{problemNumber}";
            if (_executionTime?.ContainsKey(key) == true)
                timeTaken = _executionTime[key];

            message = message.Replace("{result}", Bold().Yellow($"{result}"));

            string outputString = $"P{problemNumber}: {message}";
            if (timeTaken != null)
                outputString += $" ({Bold().Green($"{timeTaken}")} ms)";

            Console.WriteLine(outputString);
        }


        protected T MeasureExecutionTime<T>(Expression<Func<T>> function)
        {
            if (_executionTime == null)
                _executionTime = new Dictionary<string, long>();


            var methodCall = function.Body as MethodCallExpression;
            string name = methodCall.Method.Name;

            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                return function.Compile().Invoke();
            }
            finally
            {
                stopwatch.Stop();
                if (!_executionTime.ContainsKey(name))
                    _executionTime.Add(name, stopwatch.ElapsedMilliseconds);
                else
                    _executionTime[name] = stopwatch.ElapsedMilliseconds;
            }


        }

        public DayBase(bool runtests)
        {

        }

        public string ExecutionTime(string key)
        {
            if (!_executionTime?.ContainsKey(key) == true)
                return "N/A";

            return "(" + Bold().Green($"{_executionTime[key]} ms") + ")";
        }

        public static string Answer(string answer)
        {
            return Bold().Yellow(answer);
        }


        public static string Answer(int answer)
        {
            return Bold().Yellow($"{answer}");
        }

        public static string Answer(long answer)
        {
            return Bold().Yellow($"{answer}");
        }

        public static string Answer(double answer)
        {
            return Bold().Yellow($"{answer}");
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

        private Dictionary<string, long> _executionTime;

    }



}
