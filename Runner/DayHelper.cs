using Common;

namespace Runner
{
    public static class DayHelper
    {
        private static Dictionary<int, IYear> _years;

        private static void InitYears()
        {
            _years = new Dictionary<int, IYear>();
            _years.Add(2015, new AdventOfCode2015.Year());
            _years.Add(2016, new AdventOfCode2016.Year());
            _years.Add(2017, new AdventOfCode2017.Year());
            _years.Add(2018, new AdventOfCode2018.Year());
            _years.Add(2019, new AdventOfCode2019.Year());
            _years.Add(2020, new AdventOfCode2020.Year());
            _years.Add(2021, new AdventOfCode2021.Year());
            _years.Add(2022, new AdventOfCode2022.Year());
            _years.Add(2023, new AdventOfCode2023.Year());
        }

        public static IEnumerable<int> GetAvailableYears()
        {
            EnsureYears();
            return _years.Select(y => y.Key).ToList();
        }

        private static void EnsureYears()
        {
            if (_years == null)
                InitYears();
        }

        public static IYear? GetYear(int year)
        {
            EnsureYears();
            return _years[year];
        }
    }
}
