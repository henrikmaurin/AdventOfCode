namespace Common
{
    public class Map2D<T> : IMap2D<T>
    {
        public T[] Map { get; protected set; }
        public int MaxX { get; protected set; }
        public int MaxY { get; protected set; }
        public bool SafeOperations { get; set; }
        public int MinX { get; protected set; }
        public int MinY { get; protected set; }

        public int SizeX => MaxX - MinX;
        public int SizeY => MaxY - MinY;

        public static Map2D<char> FromStringArray(string[] strings)
        {
            Map2D<char> map = new Map2D<char>();
            map.Init(strings[0].Length, strings.Length);
            for (int y = 0; y < strings.Length; y++)
            {
                for (int x = 0; x < strings[y].Length; x++)
                {
                    map[x, y] = strings[y][x];
                }
            }

            return map;
        }

        public void Init(int sizeX, int sizeY, T? initialvalue = default(T?))
        {
            Init(0, 0, sizeX, sizeY, initialvalue);
        }

        public void Init(int minX, int minY, int maxX, int maxY, T? initialvalue = default(T?))
        {
            MaxX = maxX;
            MaxY = maxY;

            MinX = minX;
            MinY = minY;

            Map = new T[SizeX * SizeY];
            Array.Fill(Map, initialvalue);
        }

        public bool IsInRange(int xPos, int yPos)
        {
            if (!xPos.IsBetween(MinX, MaxX - 1))
                return false;

            if (!yPos.IsBetween(MinY, MaxY - 1))
                return false;

            return true;
        }

        public bool IsInRange (Vector2D coord)
        {
            return IsInRange(coord.X, coord.Y);
        }

        public virtual T? Get(int xPos, int yPos)
        {
            if (Map == null)
                throw new NullReferenceException("Map not initialized");

            if (!IsInRange(xPos, yPos))
                throw new IndexOutOfRangeException();

            return Map[(xPos - MinX) + (yPos - MinY) * SizeX];
        }

        public virtual T? Get(Vector2D coord)
        {
            return Get(coord.X, coord.Y);
        }

        public virtual T? TryGet(int xPos, int yPos)
        {
            if (Map == null)
                return default;

            if (!IsInRange(xPos, yPos))
                return default;

            return Get(xPos, yPos);
        }

        public virtual T? TryGet(Vector2D coord)
        {
            return TryGet(coord.X, coord.Y);
        }

        public virtual void Set(int xPos, int yPos, T value)
        {
            if (Map == null)
                throw new NullReferenceException("Map not initialized");

            if (!IsInRange(xPos, yPos))
                throw new IndexOutOfRangeException();

            Map[(xPos - MinX) + (yPos - MinY) * SizeX] = value;
        }

        public virtual void Set(Vector2D coord, T? value)
        {
            Set(coord.X, coord.Y, value);
        }

        public virtual bool TrySet(int xPos, int yPos, T? value)
        {
            if (Map == null)
                return false;

            if (!IsInRange(xPos, yPos))
                return false;

            Set(xPos, yPos, value);
            return true;
        }

        public virtual bool TrySet(Vector2D coord, T value)
        {
            return TrySet(coord.X, coord.Y, value);
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

        public Map2D<T> CloneEmpty()
        {
            Map2D<T> clone = new Map2D<T>();
            clone.SafeOperations = SafeOperations;
            clone.Init(MinX, MinY, MaxX, MaxY);

            return clone;
        }

        public List<Vector2D> FilterValidCoords(IEnumerable<Vector2D> coords)
        {
            return coords.Where(coord => IsInRange(coord)).ToList();
        }

        public List<Vector2D> GetSurrounding(int xPos, int yPos)
        {
            return Directions.GetSurroundingCoordsFor(xPos, yPos).Where(coord => IsInRange(coord)).ToList();
        }

        public List<Vector2D> GetSurrounding(Vector2D coord)
        {
            return GetSurrounding(coord.X, coord.Y);
        }

        public List<Vector2D> GetNeighbors(int xPos, int yPos)
        {
            return Directions.GetNeighboringCoordsFor(xPos, yPos).Where(coord => IsInRange(coord)).ToList();
        }

        public List<Vector2D> GetNeighbors(Vector2D coord)
        {
            return GetNeighbors(coord.X, coord.Y);
        }

        public Vector2D[] EnumerateCoords()
        {
            Vector2D[] coords = new Vector2D[SizeX * SizeY];
            for (int y = MinY; y < MaxY; y++)
                for (int x = MinX; x < MaxX; x++)
                    coords[(x - MinX) + (y - MinY) * SizeX] = new Vector2D { X = x, Y = y };

            return coords;
        }

        public Vector2D[] EnumerateCoordsVertical()
        {
            List<Vector2D> coords = new List<Vector2D>();
            for (int x = MinX; x < MaxX; x++) 
                for (int y = MinY; y < MaxY; y++)                
                   coords.Add( new Vector2D { X = x, Y = y });

            return coords.ToArray();
        }


        public string Draw(int x1, int y1, int x2, int y2, int? objX = null, int? objY = null, char? sprite = null)
        {
            for (int y = y1; y < y2; y++)
            {
                for (int x = x1; x < x2; x++)
                {
                    if (x == objX && y == objY)
                    {
                        Console.Write(sprite);
                    }
                    else
                        Console.Write(this[x, y].ToString());
                }
                Console.WriteLine();
            }



            return "";
        }

        public int CountInRow(int row, T searchFor)
        {
            int counter = 0;
            for (int x = MinX; x < MaxX; x++)
            {
                if (this[x, row].Equals(searchFor))
                    counter++;
            }
            return counter;
        }
    }

    
}
