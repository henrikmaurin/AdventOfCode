namespace Common
{
    public class Graphics
    {
        public int Year { get; set; }
        public int Day { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public char[] Screen { get; set; }
        public int FrameCount { get; set; }

        public Graphics(int year, int day, int sizeX, int sizeY)
        {
            Year = year;
            Day = day;
            SizeX = sizeX;
            SizeY = sizeY;
            FrameCount = 0;
            ClearFrame();
        }

        public void NewFrame()
        {
            FrameCount++;
            ClearFrame();
        }

        public void ClearFrame()
        {
            Screen = new char[SizeX * SizeY];
        }

        public void SaveFrame()
        {
            string filename = $"Y{Year}D{Day}F{FrameCount}.txt";
            string outFolder = Path.Combine("C:\\temp", "AocAnimation", Year.ToString(), Day.ToString());
            if (!Directory.Exists(outFolder))
                Directory.CreateDirectory(outFolder);

            string path = Path.Combine(outFolder, filename);
            for (int y = 0; y < SizeY; y++)
            {
                string line = "";
                for (int x = 0; x < SizeX; x++)
                {
                    line += Screen[y * SizeX + x];
                }
                System.IO.File.AppendAllText(path, line + Environment.NewLine);
            }
        }

        public void CopyFrame()
        {
            FrameCount++;
        }

        public void Draw(int x, int y, char c)
        {
            if (x < 0 || x >= SizeX)
                return;
            if (y < 0 || y >= SizeY)
                return;

            Screen[y * SizeX + x] = c;
        }

        public void Text(int x, int y, string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Draw(x + i, y, text[i]);
            }
        }
    }
}
