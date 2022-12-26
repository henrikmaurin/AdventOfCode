namespace Common
{
    public class Map2D<T>
    {
        public T[] Map { get; protected set; }
        public int MaxX { get; protected set; }
        public int MaxY { get; protected set; }
        public bool SafeOperations { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }

        public int SizeX => MaxX - MinX;
        public int SizeY => MaxY - MinY;


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

        public virtual T Get(int xPos, int yPos)
        {
            if (Map == null)
                throw new NullReferenceException("Map not initialized");

            if (!IsInRange(xPos, yPos))
                throw new IndexOutOfRangeException();

            return Map[(xPos - MinX) + (yPos - MinY) * SizeX];
        }

        public virtual T Get(Vector2D coord)
        {
            return Get(coord.X, coord.Y);
        }

        public virtual T TryGet(int xPos, int yPos)
        {
            if (Map == null)
                return default;

            if (!IsInRange(xPos, yPos))
                return default;

            return Get(xPos, yPos);
        }

        public virtual T TryGet(Vector2D coord)
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

        public virtual void Set(Vector2D coord, T value)
        {
            Set(coord.X, coord.Y, value);
        }

        public virtual bool TrySet(int xPos, int yPos, T value)
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
            clone.Init(MaxX, MaxY);

            return clone;
        }

        public bool IsValidCoord(int x, int y)
        {
            return IsInRange(x, y);
        }

        public bool IsValidCoord(Vector2D coord)
        {
            return IsValidCoord(coord.X, coord.Y);
        }

        public List<Vector2D> GetSurrounding(int xPos, int yPos)
        {
            return Directions.GetSurroundingCoordsFor(xPos, yPos).Where(coord => IsValidCoord(coord)).ToList();
        }

        public List<Vector2D> GetSurrounding(Vector2D coord)
        {
            return GetSurrounding(coord.X, coord.Y);
        }

        public List<Vector2D> GetNeighbors(int xPos, int yPos)
        {
            return Directions.GetNeighboringCoordsFor(xPos, yPos).Where(coord => IsValidCoord(coord)).ToList();
        }

        public List<Vector2D> GetNeighbors(Vector2D coord)
        {
            return GetNeighbors(coord.X, coord.Y);
        }

        public Vector2D[] Enumerate()
        {
            Vector2D[] coords = new Vector2D[SizeX * SizeY];
            for (int y = MinY; y < MaxY; y++)
                for (int x = MinX; x < MaxX; x++)
                    coords[(x - MinX) + (y - MinY) * SizeX] = new Vector2D { X = x, Y = y };

            return coords;
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

    public static class Directions
    {
        public const int None = 0;
        public const int Top = 1;
        public const int Left = 2;
        public const int Right = 3;
        public const int Bottom = 4;
        public const int TopLeft = 5;
        public const int TopRight = 6;
        public const int BottomLeft = 7;
        public const int BottomRight = 8;

        public const int Up = 1;
        public const int Down = 4;
        public const int UpLeft = 5;
        public const int DownLeft = 7;
        public const int UpRight = 6;
        public const int DownRight = 8;

        private static Vector2D[] _directions = {
            new Vector2D{X=0,Y=0},
            new Vector2D{X=0,Y=-1},
            new Vector2D{X=-1,Y=0},
            new Vector2D{X=1,Y=0},
            new Vector2D{X=0,Y=1},
            new Vector2D{X=-1,Y=-1},
            new Vector2D{X=1,Y=-1},
            new Vector2D{X=-1,Y=1},
            new Vector2D{X=1,Y=1},
        };

        public static int[] GetNeighbors()
        {
            return new int[] { Top, Left, Right, Bottom };
        }

        public static int[] GetSurrounding()
        {
            return new int[] { TopLeft, Top, TopRight, Left, Right, BottomLeft, Bottom, BottomRight };
        }

        public static Vector2D GetDirection(int direction)
        {
            return _directions[direction];
        }

        public static Vector2D[] GetNeigboingCoords()
        {
            return _directions[GetNeighbors().First()..GetNeighbors().Last()];
        }

        public static Vector2D[] GetNeighboringCoordsFor(int xPos, int yPos)
        {
            Vector2D[] retVal = new Vector2D[4];
            int counter = 0;
            foreach (int direction in GetNeighbors())
            {
                Vector2D c = GetDirection(direction);
                retVal[counter++] = new Vector2D { X = xPos + c.X, Y = yPos + c.Y };
            }

            return retVal;
        }

        public static Vector2D[] GetSurroundingCoordsFor(int xPos, int yPos)
        {
            Vector2D[] retVal = new Vector2D[8];
            int counter = 0;
            foreach (int direction in GetSurrounding())
            {
                Vector2D c = GetDirection(direction);
                retVal[counter++] = new Vector2D { X = xPos + c.X, Y = yPos + c.Y };
            }

            return retVal;
        }

        public static Vector2D[] GetSurroundingCoords()
        {
            return _directions[GetSurrounding().First()..GetSurrounding().Last()];
        }

        public static int SquareRadius(this Vector2D me, Vector2D other)
        {
            return (new int[] { Math.Abs(me.X - other.X), Math.Abs(me.Y - other.Y) }).Max();
        }

        public static int ManhattanDistance(this Vector2D me, Vector2D other)
        {
            return (Math.Abs(me.X - other.X) + Math.Abs(me.Y - other.Y));
        }

        public static int TurnRight(int direction)
        {
            switch (direction)
            {
                case Up: return Right;
                case Right: return Down;
                case Down: return Left;
                case Left: return Up;
            }
            return 0;
        }
        public static int TurnLeft(int direction)
        {
            switch (direction)
            {
                case Up: return Left;
                case Right: return Up;
                case Down: return Right;
                case Left: return Down;
            }
            return 0;
        }



        public static Vector2D GetDirectionFrom(Vector2D from, Vector2D to)
        {
            if (from.X == to.X && from.Y < to.Y)
                return GetDirection(Down);
            else if (from.X == to.X && from.Y > to.Y)
                return GetDirection(Up);
            else if (from.X > to.X && from.Y == to.Y)
                return GetDirection(Left);
            else if (from.X < to.X && from.Y == to.Y)
                return GetDirection(Right);
            return GetDirection(None);
        }


    }

    public class Vector2D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static Vector2D operator +(Vector2D me, Vector2D coord)
        {
            return new Vector2D { X = me.X + coord.X, Y = me.Y + coord.Y };
        }
        public static Vector2D operator -(Vector2D me, Vector2D coord)
        {
            return new Vector2D { X = me.X - coord.X, Y = me.Y - coord.Y };
        }
        public static bool Equals(Vector2D a, Vector2D b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public bool Equals(Vector2D b)
        {
            return X == b.X && Y == b.Y;
        }
        public string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
