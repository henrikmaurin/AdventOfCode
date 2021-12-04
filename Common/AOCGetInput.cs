using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AOCGetInput
    {
        private string _cookie;
        public AOCGetInput(string cookie)
        {
            _cookie = cookie;
        }

        public string GetData(int year, int day)
        {
            var baseAddress = new Uri("https://adventofcode.com/");
            using (var handler = new HttpClientHandler { UseCookies = false })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var message = new HttpRequestMessage(HttpMethod.Get, $"/{year}/day/{day}/input");
                message.Headers.Add("Cookie", $"session={_cookie}");
                var result = client.SendAsync(message).Result;
                return result.Content.ReadAsStringAsync().Result;
            }
        }

        public string GetDataCached(int year, int day)
        {
            string file = Path.Combine("data", $"Day{day.ToString("D2")}.txt");
            if (File.Exists(file))
                return File.ReadAllText(file);

            string fileContent = GetData(year, day);

            File.WriteAllText(file, fileContent);
            return fileContent;
        }
            }

    public class AdventOfCodeSecrets
    {
        public string Cookie { get; set; }
    }
}
