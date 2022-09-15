using AdventOfCode;
using Common;

namespace AdventOfCode2015
{
    public class Day09 : DayBase, IDay
    {
        private Dictionary<string, int> _distances;

        public Day09() : base(2015, 9) { }

        public int Problem1()
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray(true);
            List<string> cities = Setup(data);

            return VisitShortest(null, cities, 0);
        }

        public int Problem2()
        {
            string[] data = input.GetDataCached().SplitOnNewlineArray(true);
            List<string> cities = Setup(data);

            return VisitLongest(null, cities, 0);
        }

        public int VisitShortest(string city, List<string> citiesLeft, int traveledDistance)
        {
            if (citiesLeft.Count == 0)
                return traveledDistance;

            int shortestDistance = int.MaxValue;

            for (int i = 0; i < citiesLeft.Count; i++)
            {
                string nextCity = citiesLeft[i];
                int distance = 0;
                if (!string.IsNullOrEmpty(city))
                    distance = _distances[CombineOrdered(city, nextCity)];
                int thisDistance = traveledDistance + distance;
                List<string> remainingCities = new List<string>(citiesLeft);
                remainingCities.RemoveAt(i);

                int totalDistance = VisitShortest(nextCity, remainingCities, thisDistance);

                if (totalDistance < shortestDistance)
                    shortestDistance = totalDistance;
            }
            return shortestDistance;
        }

        public int VisitLongest(string city, List<string> citiesLeft, int traveledDistance)
        {
            if (citiesLeft.Count == 0)
                return traveledDistance;

            int longestDistance = int.MinValue;

            for (int i = 0; i < citiesLeft.Count; i++)
            {
                string nextCity = citiesLeft[i];
                int distance = 0;
                if (!string.IsNullOrEmpty(city))
                    distance = _distances[CombineOrdered(city, nextCity)];
                int thisDistance = traveledDistance + distance;
                List<string> remainingCities = new List<string>(citiesLeft);
                remainingCities.RemoveAt(i);

                int totalDistance = VisitLongest(nextCity, remainingCities, thisDistance);

                if (totalDistance > longestDistance)
                    longestDistance = totalDistance;
            }
            return longestDistance;
        }

        public List<string> Setup(string[] distances)
        {
            List<string> cities = new List<string>();

            _distances = new Dictionary<string, int>();
            foreach (string distance in distances)
            {
                string[] tokenized = distance.Split(" ");
                string city1 = tokenized.ElementAt(0);
                string city2 = tokenized.ElementAt(2);
                int dist = tokenized.ElementAt(4).ToInt();
                string citiesCombined = CombineOrdered(city1, city2);
                _distances.TryAdd(citiesCombined, dist);
                if (!cities.Contains(city1))
                    cities.Add(city1);
                if (!cities.Contains(city2))
                    cities.Add(city2);
            }
            return cities;
        }

        public static string CombineOrdered(string str1, string str2)
        {
            return string.Compare(str1, str2) <= 0 ? str1 + str2 : str2 + str1;
        }


        public void Run()
        {
            int shortestRoute = Problem1();
            Console.WriteLine($"P1: The shortest route is: {shortestRoute}");

            int longestRoute = Problem2();
            Console.WriteLine($"P2: The longest route is: {longestRoute}");
        }
    }
}