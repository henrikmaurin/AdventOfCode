namespace Common
{
    public class AOCGetInput
    {
        private string _cookie;
        private int _year;
        private int _day;
        public AOCGetInput(string cookie, int year, int day)
        {
            _cookie = cookie;
            _year = year;
            _day = day;
        }

        public string GetData()
        {
            var baseAddress = new Uri("https://adventofcode.com/");
            using (var handler = new HttpClientHandler { UseCookies = false })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var message = new HttpRequestMessage(HttpMethod.Get, $"/{_year}/day/{_day}/input");
                message.Headers.Add("Cookie", $"session={_cookie}");
                var result = client.SendAsync(message).Result;
                return result.Content.ReadAsStringAsync().Result;
            }
        }

        public string GetDataCached()
        {
            string path = Path.Combine("data", $"{_year}");
            string file = Path.Combine(path, $"Day{_day.ToString("D2")}.txt");
            string? fileContent = null;

            if (File.Exists(file))
            {
                fileContent = File.ReadAllText(file);
            }

            if (string.IsNullOrEmpty(fileContent))
                fileContent = GetData();

            if (fileContent.StartsWith("Puzzle inputs differ by user."))
            {
                if (File.Exists(file))
                    File.Delete(file);
                throw new Exception("Wrong Cookie");
            }


            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(file, fileContent);
            return fileContent;
        }
    }
}
