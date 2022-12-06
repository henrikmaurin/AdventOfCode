using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Map2D<T>
    {
        public T[] Map { get; protected set; }
        public int SizeX { get; protected set; }
        public int SizeY { get; protected set; }
        public bool SafeOperations { get; set; }

        public void Init(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            Map = new T[sizeX * sizeY];
        }

        public virtual T Get(int xPos, int yPos)
        {
            if (Map == null)
                throw new NullReferenceException("Map not initialized");

            if (!(xPos * yPos).IsBetween(0, SizeX * SizeY))
                throw new IndexOutOfRangeException();

            return Map[xPos + yPos * SizeX];
        }

        public virtual T Get(Coord2D coord)
        {
            return Get(coord.X, coord.Y);
        }

        public virtual T TryGet(int xPos, int yPos)
        {
            if (Map == null)
                return default;

            if (!xPos.IsBetween(0, SizeX - 1))
                return default;

            if (!yPos.IsBetween(0, SizeY - 1))
                return default;

            return Get(xPos, yPos);
        }

        public virtual T TryGet(Coord2D coord)
        {
            return TryGet(coord.X, coord.Y);
        }

        public virtual void Set(int xPos, int yPos, T value)
        {
            if (Map == null)
                throw new NullReferenceException("Map not initialized");

            if (!(xPos * yPos).IsBetween(0, SizeX * SizeY))
                throw new IndexOutOfRangeException();

            Map[xPos + yPos * SizeX] = value;
        }

        public virtual void Set(Coord2D coord, T value)
        {
            Set(coord.X, coord.Y, value);
        }

        public virtual bool TrySet(int xPos, int yPos, T value)
        {
            if (Map == null)
                return false;

            if (!(xPos * yPos).IsBetween(0, SizeX * SizeY))
                return false;

            Map[xPos + yPos * SizeX] = value;
            return true;
        }

        public virtual bool TrySet(Coord2D coord, T value)
        {
            return TrySet(coord.X, coord.Y, value);
        }

        public virtual T this[int x, int y]
        {
            get { return SafeOperations ? TryGet(x, y) : Get(x, y); }
            set { if (SafeOperations) TrySet(x, y, value); else Set(x, y, value); }
        }

        public Map2D<T> CloneEmpty()
        {
            Map2D<T> clone = new Map2D<T>();
            clone.SafeOperations = SafeOperations;
            clone.Init(SizeX, SizeY);

            return clone;
        }

        public bool IsValidCoord(int x, int y)
        {
            if (!x.IsBetween(0, SizeX - 1))
                return false;

            if (!y.IsBetween(0, SizeY - 1))
                return false;
            return true;
        }

        public bool IsValidCoord(Coord2D coord)
        {
            return (IsValidCoord(coord.X, coord.Y));
        }

        public List<Coord2D> GetNeighbors(int xPos, int yPos)
        {
            List<Coord2D> neighbors = new List<Coord2D>();
            for (int y = yPos - 1; y <= yPos + 1; y++)
            {
                for (int x = xPos - 1; x <= xPos + 1; x++)
                {
                    if (!(x == xPos && y == yPos) && IsValidCoord(xPos, yPos))
                        neighbors.Add(new Coord2D { X = x, Y = y });
                }
            }
            return neighbors;
        }

        public List<Coord2D> GetNeighbors(Coord2D coord)
        {
            return GetNeighbors(coord.X, coord.Y);
        }
    }

    public class Coord2D
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
