using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public class SimplePathFinder
    {
        public Map2D<IPathFinderAttributes> Map { get; set; }
        public Map2D<int?> DistanceMap { get; set; }

        public void CalcDistances(int startPointX, int startPointY)
        {
            DistanceMap = new Map2D<int?>();
            DistanceMap.Init(Map.SizeX, Map.SizeY, null);
            DistanceMap.SafeOperations = true;

            bool changed = true;
            int distance = 0;

            DistanceMap[startPointX, startPointY] = distance;

            while (changed)
            {
                changed = false;
                 for (int y = 0; y < Map.SizeY; y++)
                {
                    for (int x = 0; x < Map.SizeX; x++)
                    {
                        if (DistanceMap[x, y] == distance)
                        {
                            List<Vector2D> neighbors = DistanceMap.GetNeighbors(x, y);
                            foreach (var neighbor in neighbors)
                            {
                                if (DistanceMap[neighbor.X, neighbor.Y] == null && Map[x, y].Traversable)
                                {
                                    DistanceMap[neighbor.X, neighbor.Y] = distance + 1;
                                    changed = true;
                                }
                            }
                        }
                    }
                }
                distance++;
            }
        }

        public List<Vector2D> GetPath(int fromX, int fromY, int toX, int toY)
        {
            CalcDistances(fromX, fromY);
            List<Vector2D> path = new List<Vector2D>();

            int? distance = DistanceMap[toX, toY];
            if (distance == null)
                return null;
            
            Vector2D currentPos= new Vector2D { X= toX ,Y = toY };

            while (distance>0)
            {
                List<Vector2D> neighbors = DistanceMap.GetNeighbors(currentPos.X, currentPos.Y);
                foreach (var neighbor in neighbors)
                {
                    if (DistanceMap[neighbor.X,neighbor.Y]== distance-1)
                    {
                        path.Add(neighbor); 
                        break;
                    }
                }
                distance--;
            }
            
            return path;
        }


    }

    public interface IPathFinderAttributes
    {
        public bool Traversable { get; set; }
    }

}
