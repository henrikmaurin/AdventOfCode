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

        public virtual T TryGet(int xPos, int yPos)
        {
            if (Map == null)
                return default;

            if (!xPos.IsBetween(0, SizeX-1))
                return default;

            if (!yPos.IsBetween(0, SizeY - 1))
                return default;

            return Get(xPos, yPos);
        }

        public virtual void Set(int xPos, int yPos, T value)
        {
            if (Map == null)
                throw new NullReferenceException("Map not initialized");

            if (!(xPos * yPos).IsBetween(0, SizeX * SizeY))
                throw new IndexOutOfRangeException();

            Map[xPos + yPos * SizeX] = value;
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

        public virtual T this[int x, int y]
        {
            get { return SafeOperations?TryGet(x,y): Get(x,y); }
            set { if (SafeOperations) TrySet(x, y, value); else Set(x, y, value); }
        }

        public Map2D<T> CloneEmpty()
        {
            Map2D< T> clone = new Map2D<T>();
            clone.SafeOperations = SafeOperations;
            clone.Init(SizeX, SizeY);

            return clone;
        }

    }
}
