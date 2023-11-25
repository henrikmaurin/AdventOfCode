using Common;

using static Common.Parser;

namespace AdventOfCode2015
{
    public class Day02Alternative : DayBase, IDay
    {
        private const int day = 2;
        private IEnumerable<Present> _presents;
        public Day02Alternative(bool runtests = false) : base(Global.Year, day, runtests)
        {
            if (runtests)
                return;

            string data = input.GetDataCached();
            _presents = ParseLinesDelimitedByNewline<Present, Present.Parsed>(data);
        }

        public Day02Alternative(string data,bool runtests = false) : base(Global.Year, day, runtests)
        {
            _presents = ParseLinesDelimitedByNewline<Present, Present.Parsed>(data);
        }

        public void Run()
        {
            int paperToOrder = Problem1();
            Console.WriteLine($"P1: The elves nedds to order (paper): {paperToOrder} ");

            int ribbontoOrder = Problem2();
            Console.WriteLine($"P1: The elves nedds to order (ribbon): {ribbontoOrder}");
        }

        public int Problem1()
        {
            return _presents.Select(p => p.WrappingArea).Sum();
        }

        public int Problem2()
        {
            return _presents.Select(p => p.RibbonLength).Sum();
        }

        public class Present : IParsedDataFormat
        {
            public class Parsed : IInDataFormat
            {
                public string DataFormat => "(\\d+)x(\\d+)x(\\d+)";

                public string[] PropertyNames => new string[] { nameof(X), nameof(Y), nameof(Z) };
                public int X { get; set; }
                public int Y { get; set; }
                public int Z { get; set; }
            }


            public int Width { get; set; }
            public int Heigth { get; set; }
            public int Depth { get; set; }

            public IEnumerable<int> SidesAreas { get => new List<int>() { Width * Heigth, Heigth * Depth, Depth * Width }; }
            public int WrappingArea { get => 2 * SidesAreas.Sum() + SidesAreas.Min(); }

            public IEnumerable<int> Perimeters { get => new List<int> { 2 * (Width + Heigth), 2 * (Heigth + Depth), 2 * (Depth + Width) }; }
            public int Volume { get => Width * Heigth * Depth; }

            public int RibbonLength { get => Perimeters.Min() + Volume; }

            public Type GetReturnType()
            {
                return typeof(Present);
            }

            public void Transform(IInDataFormat data)
            {
                Parsed presentDimension = (Parsed)data;

                Width = presentDimension.X;
                Heigth = presentDimension.Y;
                Depth = presentDimension.Z;
            }
        }
    }
}
