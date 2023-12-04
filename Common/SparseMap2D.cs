namespace Common
{
    public static class InitMap
    {
        public static void InitFromStringArray<T>(this IMap2D<T> map, string[] mapData, Func<char, T> mapper)
        {
            map.Init(mapData.Select(m => m.Length).Max(), mapData.Length);

            for (int y = 0; y < map.MaxY; y++)
            {
                for (int x = 0; x < map.MaxX; x++)
                {
                    map.Set(x,y, mapper(mapData.ElementAt(y)[x]));
                }
            }
        }

        public static void InitFromStringArray(this SparseMap2D<char> map, string[] mapData)
        {
            InitFromStringArray<char>(map, mapData, (ch) => { return ch; });
        } 
    }



    public class SparseMap2D<T> : IMap2D<T>
    {
        private int? _minX;
        private int? _maxX;
        private int? _minY;
        private int? _maxY;


        public int MaxX { get => GetMaxX(); protected set => _maxX = value; }
        public int MaxY { get => GetMaxY(); protected set => _maxY = value; }
        public int MinX { get => GetMinX(); protected set => _minX = value; }
        public int MinY { get => GetMinY(); protected set => _minY = value; }
        public bool SafeOperations { get; set; }
        public int SizeX => MaxX - MinX;
        public int SizeY => MaxY - MinY;

        public bool HasBoundaries { get; set; } = false;

        public Dictionary<Vector2D, T> Map { get; protected set; }

        private int GetMinX()
        {
            return _minX ?? Map.Select(m => m.Key.X).Min();
        }
        private int GetMaxX()
        {
            return _maxX ?? Map.Select(m => m.Key.X).Max();
        }
        private int GetMinY()
        {
            return _minY ?? Map.Select(m => m.Key.Y).Min();
        }
        private int GetMaxY()
        {
            return _maxY ?? Map.Select(m => m.Key.Y).Max();
        }

        public void Init(int minX, int minY, int maxX, int maxY, T? initialvalue = default(T?))
        {
            HasBoundaries = true;
            MaxX = maxX;
            MaxY = maxY;

            MinX = minX;
            MinY = minY;

            Init();
        }

        public void Init(int sizeX, int sizeY, T? initialvalue = default(T?))
        {
            Init(0, 0, sizeX, sizeY, initialvalue);
        }

        public void Init()
        {
            Map = new Dictionary<Vector2D, T>();
        }

        public bool IsInRange(int xPos, int yPos)
        {
            if (!HasBoundaries)
                return true;

            if (!_minX.HasValue) return true;
            if (!_minY.HasValue) return true;
            if (!_maxX.HasValue) return true;
            if (!_maxY.HasValue) return true;

            if (!xPos.IsBetween(MinX, MaxX - 1))
                return false;

            if (!yPos.IsBetween(MinY, MaxY - 1))
                return false;

            return true;
        }

        public bool IsInRange(Vector2D pos)
        {
            return IsInRange(pos.X, pos.Y);
        }

        public List<Vector2D> FilterValidCoords(IEnumerable<Vector2D> coords)
        {
            return coords.Where(coord => IsInRange(coord)).ToList();
        }

        public virtual T? Get(int xPos, int yPos)
        {
            Vector2D vector2D = new Vector2D { X = xPos, Y = yPos };

            return Get(vector2D);
        }

        public virtual T? Get(Vector2D pos)
        {
            if (Map == null)
                throw new NullReferenceException("Map not initialized");

            if (!IsInRange(pos))
                throw new IndexOutOfRangeException();

            if (Map.ContainsKey(pos))
                return Map[pos];

            return default;
        }

        public virtual void Set(int xPos, int yPos, T value)
        {
            Vector2D vector2D = new Vector2D { X = xPos, Y = yPos };
            Set(vector2D, value);
            return;
        }

        public virtual void Set(Vector2D pos, T value)
        {
            T? data = Get(pos);

            if (data == null)
                Map.Add(pos, value);
            else
                Map[pos] = value;

            return;
        }

        public virtual T? TryGet(int xPos, int yPos)
        {
            if (Map == null)
                return default;

            if (!IsInRange(xPos, yPos))
                return default;

            return Get(xPos, yPos);
        }

        public T? TryGet(Vector2D coord)
        {
            return TryGet(coord.X, coord.Y);
        }

        public bool TrySet(int xPos, int yPos, T value)
        {
            if (Map == null)
                return false;

            if (!IsInRange(xPos, yPos))
                return false;

            Set(xPos, yPos, value);
            return true;
        }

        public bool TrySet(Vector2D coord, T value)
        {
            return TrySet(coord.X, coord.Y, value);
        }

        public Vector2D[] EnumerateCoords()
        {
            Vector2D[] coords = new Vector2D[SizeX * SizeY];
            for (int y = MinY; y < MaxY; y++)
                for (int x = MinX; x < MaxX; x++)
                    coords[(x - MinX) + (y - MinY) * SizeX] = new Vector2D { X = x, Y = y };

            return coords;
        }

        public List<Tuple<Vector2D, T>> EnumerateMap()
        {
            return Map.Select(m => new Tuple<Vector2D, T>(m.Key, m.Value)).ToList();
        }

        public virtual T this[int x, int y]
        {
            get { return SafeOperations ? TryGet(x, y) : Get(x, y); }
            set { if (SafeOperations) TrySet(x, y, value); else Set(x, y, value); }
        }

        public virtual T this[Vector2D coord]
        {
            get { return this[coord.X, coord.Y]; }
            set { this[coord.X, coord.Y] = value; }
        }
    }
}
