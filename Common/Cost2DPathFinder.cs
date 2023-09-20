/*

namespace Common
{
    public class Cost2DPathFinder<T> where T: ICostTraversable<T>
    {
        public Map2D<T> Map { get; set; }
        public Map2D<int?> DistanceMap { get; set; }

        public Cost2DPathFinder(Map2D<T> map)
        {
            Map = map;
        }

        public void CalcDistances(int startPointX, int startPointY)
        {
            DistanceMap = new Map2D<int?>();
            DistanceMap.Init(Map.MaxX, Map.MaxY, null);
            DistanceMap.SafeOperations = true;

            bool changed = true;
            int distance = 0;

            DistanceMap[startPointX, startPointY] = distance;

            while (changed)
            {
                changed = false;
                foreach (Vector2D coordinate in Map.EnumerateCoords())
                {
                    if (DistanceMap[coordinate] == distance)
                    {
                        List<Vector2D> neighbors = DistanceMap.GetNeighbors(coordinate);
                        foreach (var neighbor in neighbors)
                        {
                            if (DistanceMap[neighbor] == null && Map[neighbor].TraversableFrom(Map[coordinate]))
                            {
                                DistanceMap[neighbor] = distance + 1;
                                changed = true;

                            }
                        }
                    }
                }
                distance++;
            }
        }

        public void CalcDistancesReverse(int endPointX, int endPointY)
        {
            DistanceMap = new Map2D<int?>();
            DistanceMap.Init(Map.MaxX, Map.MaxY, null);
            DistanceMap.SafeOperations = true;

            bool changed = true;
            int distance = 0;

            DistanceMap[endPointX, endPointY] = distance;

            while (changed)
            {
                changed = false;
                foreach (Vector2D coordinate in Map.EnumerateCoords())
                {
                    if (DistanceMap[coordinate] == distance)
                    {
                        List<Vector2D> neighbors = DistanceMap.GetNeighbors(coordinate);
                        foreach (var neighbor in neighbors)
                        {
                            if (DistanceMap[neighbor] == null && Map[coordinate].TraversableFrom(Map[neighbor]))
                            {
                                DistanceMap[neighbor] = distance + 1;
                                changed = true;

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

            Vector2D currentPos = new Vector2D { X = toX, Y = toY };

            while (distance > 0)
            {
                List<Vector2D> neighbors = DistanceMap.GetNeighbors(currentPos.X, currentPos.Y);
                foreach (var neighbor in neighbors)
                {
                    if (DistanceMap[neighbor.X, neighbor.Y] == distance - 1)
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
}
*/