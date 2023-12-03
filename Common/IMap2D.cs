namespace Common
{
    public interface IMap2D<T> 
    {
        int MaxX { get; }
        int MaxY { get; }
        int MinX { get; }
        int MinY { get; }
        int SizeX { get; }      
        int SizeY { get; }
     

        T? Get(int xPos, int yPos);
        T? Get(Vector2D coord);
        bool IsInRange(int xPos, int yPos);
        bool IsInRange(Vector2D pos);
        void Set(int xPos, int yPos, T value);
        void Set(Vector2D coord, T value);
        T? TryGet(int xPos, int yPos);
        T? TryGet(Vector2D coord);
        bool TrySet(int xPos, int yPos, T value);
        bool TrySet(Vector2D coord, T value);
    }
}