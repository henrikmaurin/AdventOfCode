namespace Common
{
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

        public static class Vector
        {
            public static Vector2D Up { get => GetDirection(Directions.Up); }
            public static Vector2D Down { get => GetDirection(Directions.Down); }
            public static Vector2D Left { get => GetDirection(Directions.Left); }
            public static Vector2D Right { get => GetDirection(Directions.Right); }
            public static Vector2D UpLeft { get => GetDirection(Directions.UpLeft); }
            public static Vector2D UpRight { get => GetDirection(Directions.UpRight); }
            public static Vector2D DownLeft { get => GetDirection(Directions.DownLeft); }
            public static Vector2D DownRight { get => GetDirection(Directions.DownRight); }

        }

        private static Vector2D[] _directionsVectors = {
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

        private static Direction[] _directions =
        {
            new Direction{DirectionId=None, Vector= _directionsVectors[None]},
            new Direction{DirectionId=Up, Vector= _directionsVectors[Up]},
            new Direction{DirectionId=Left, Vector= _directionsVectors[Left]},
            new Direction{DirectionId=Right, Vector= _directionsVectors[Right]},
            new Direction{DirectionId=Down, Vector= _directionsVectors[Down]},
            new Direction{DirectionId=UpLeft, Vector= _directionsVectors[UpLeft]},
            new Direction{DirectionId=UpRight, Vector= _directionsVectors[UpRight]},
            new Direction{DirectionId=DownLeft, Vector= _directionsVectors[DownLeft]},
            new Direction{DirectionId=DownRight, Vector= _directionsVectors[DownRight]},
        };

        public static Direction Direction(int direction)
        {
            return _directions[direction];
        }

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
            return _directionsVectors[direction];
        }

        public static Vector2D[] GetNeigboingCoords()
        {
            return _directionsVectors[GetNeighbors().First()..GetNeighbors().Last()];
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
        public static Vector2D[] GetNeighboringCoordsFor(Vector2D pos)
        {
            return GetNeighboringCoordsFor(pos.X, pos.Y);
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

        public static Vector2D[] GetSurroundingCoordsFor(Vector2D pos)
        {
            return GetSurroundingCoordsFor(pos.X, pos.Y);
        }

        public static Vector2D[] GetSurroundingCoords()
        {
            return _directionsVectors[GetSurrounding().First()..GetSurrounding().Last()];
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

    public class Direction
    {
        public Vector2D Vector { get; set; }
        public int DirectionId { get; set; }
    }
}
