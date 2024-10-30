namespace Common
{
    public class Vector2D : IEquatable<Vector2D>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2D() { }

        public Vector2D(Vector2D vector2D) : this(vector2D.X, vector2D.Y) { }

        public static Vector2D operator +(Vector2D me, Vector2D coord)
        {
            return new Vector2D { X = me.X + coord.X, Y = me.Y + coord.Y };
        }
        public static Vector2D operator -(Vector2D me, Vector2D coord)
        {
            return new Vector2D { X = me.X - coord.X, Y = me.Y - coord.Y };
        }

        public static Vector2D operator *(Vector2D me, int amount)
        {
            return new Vector2D { X = me.X * amount, Y = me.Y * amount };
        }

        public static bool Equals(Vector2D a, Vector2D b)
        {
            if (a is null || b is null)
                return false;

            return a.X == b.X && a.Y == b.Y;
        }
        public bool Equals(Vector2D b)
        {
            return Equals(this, b);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            return Equals((Vector2D)obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public virtual string ToString()
        {
            return $"{X},{Y}";
        }

        public bool In(List<Vector2D> vectors)
        {
            if (vectors == null)
                return false;

            foreach (Vector2D vector in vectors)
                if (Equals(vector)) return true;

            return false;
        }

        public bool In(Vector2D[] vectors)
        {
            if (vectors == null)
                return false;

            foreach (Vector2D vector in vectors)
                if (Equals(vector)) return true;

            return false;
        }

        public Vector2D Clone()
        {
            return new Vector2D(this);
        }

        public List<Vector2D> GetSurrounding()
        {
            return Directions.GetSurroundingCoordsFor(this).ToList();
        }

        public List<Vector2D> GetNeigboringCoords()
        {
            return Directions.GetNeighboringCoordsFor(this).ToList();
        }
    }
}
