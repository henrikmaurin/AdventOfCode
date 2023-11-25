namespace Common
{
    public class SparseMap2D<T> : IMap2D<T>
    {
        public int? MaxX { get; protected set; }
        public int? MaxY { get; protected set; }
        public int? MinX { get; set; }
        public int? MinY { get; set; }
        public bool SafeOperations { get; set; }
        public int? SizeX => MaxX - MinX;
        public int? SizeY => MaxY - MinY;

        public bool HasBoundaries { get; set; } = false;

        public Dictionary<string, T> Map { get; protected set; }

        public void Init(int minX, int minY, int maxX, int maxY, T? initialvalue = default(T?))
        {
            HasBoundaries = true;
            MaxX = maxX;
            MaxY = maxY;

            MinX = minX;
            MinY = minY;

            Init();
        }

        public void Init()
        {
            Map = new Dictionary<string, T>();
        }

        public bool IsInRange(int xPos, int yPos)
        {
            if (!HasBoundaries)
                return true;

            if (!MinX.HasValue) return true;
            if (!MinY.HasValue) return true;
            if (!MaxX.HasValue) return true;
            if (!MaxY.HasValue) return true;

            if (!xPos.IsBetween(MinX.Value, MaxX.Value - 1))
                return false;

            if (!yPos.IsBetween(MinY.Value, MaxY.Value - 1))
                return false;

            return true;
        }

        public virtual T Get(int xPos, int yPos)
        {
            if (Map == null)
                throw new NullReferenceException("Map not initialized");

            if (!IsInRange(xPos, yPos))
                throw new IndexOutOfRangeException();

            Vector2D vector2D = new Vector2D { X = xPos, Y = yPos };

            Map.TryGetValue(vector2D.ToString(), out T result);

            return result??default(T);
        }
    }
}
