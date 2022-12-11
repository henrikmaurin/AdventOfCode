using Common;

namespace AdventOfCode
{
    public class VisualAoC
    {
        private string[] _buffer;
        private int _sizeX;
        private int _sizeY;


        private int _leftSide = 39;
        private int _datalinecount = 30;

        public string ProjectName { get; set; }
        public string[] Data { get; set; }
        public int CurrentDataLine { get; set; }
        public Dictionary<string,string> Variables { get; set; }
        public bool Enabled { get; set; }

        public VisualAoC(int bufferSizeX, int bufferSizeY)
        {
            _sizeX= bufferSizeX;
            _sizeY= bufferSizeY;
            ClearBuffer();
            Variables= new Dictionary<string,string>();
        }

        public void ClearBuffer()
        {
            _buffer= new string[_sizeY];
            for (int line =0;line< _buffer.Length;line++)
            {
                _buffer[line] = new string(' ', _sizeX);
            }
        }

        public void DrawInterface()
        {
            _buffer[0] = new string('-', _sizeX);
            PrintAt(3, 0, $" Visual AoC - {ProjectName} ");
            for (int y=1;y< _sizeY-1;y++) {
                PrintAt(0, y, "|");
                PrintAt(_leftSide, y, "|");
                PrintAt(_sizeX-1, y, "|");
            }
            _buffer[_sizeY-1] = new string('-', _sizeX);

            PrintAt(3, 1, "Data");
            int startline = 0;
            if (CurrentDataLine > _datalinecount -3)
                startline= CurrentDataLine +_datalinecount - 3;

            for (int y = 0;y<_datalinecount;y++)
            {
                if (y + startline >= Data.Length)
                    break;
                if (y + startline == CurrentDataLine)
                    PrintAt(1, y + 2, ">");
                PrintAt(2, y + 2, Data[y+startline],_leftSide-3);
            }

            PrintAt(3, _datalinecount+3, "Variables");
            int ypos = _datalinecount + 4;
            foreach (var valuepair in Variables.ToList())
            {
                PrintAt(2, ypos, valuepair.Key, 20);
                PrintAt(24,ypos, valuepair.Value, 15);
                ypos++;
            }
        }

        public void PrintAt(int x, int y, string content, int length =0,bool pad = false)
        {
            if (length == 0 && pad)
                throw new Exception("Padding not available when length not set");

            if (x < 0 || y < 0)
                throw new Exception("Negative coords");

            int printLength = content.Length;
            if (length.IsBetween(1, printLength))
            {
                printLength= length;
            }
            if(printLength < length && pad)
            {
                printLength = length;
                content += new string(' ', printLength - content.Length);
            }
            if(x+printLength> _sizeX)
            {
                printLength= _sizeX-x;
            }
            _buffer[y] = _buffer[y].Substring(0, x) + content.Substring(0,printLength)+ _buffer[y].Substring(x+printLength);
        }        

        public void PrintAtDrawArea(int x, int y, string content, int length = 0, bool pad = false)
        {
            int upperLeftX = _leftSide + 1;
            int upperLeftY = 1;
            PrintAt(x+upperLeftX,y+upperLeftY,content);
        }

        public void Display()
        {
            if (!Enabled)
                return;
            Console.Clear();
            foreach(string line in _buffer)
            {
                Console.WriteLine(line);
            }
        }

    }
}