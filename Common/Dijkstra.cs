using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Dijkstra<T>
    {
        private Dictionary<uint, T> nodes;
        private List<Connection> connections;

        public Dijkstra()
        {
            nodes = new Dictionary<uint, T>();
            connections = new List<Connection>();
        }

        public uint AddNode(T item)
        {
            uint newId = nodes?.Keys.Max() ?? 0 + 1;
            nodes.Add(newId, item);
            return newId;
        }

        public bool Connect(uint from, uint to, int cost)
        {
            if (!nodes.ContainsKey(from))
                return false;
            if (!nodes.ContainsKey(to))
                return false;

            connections.Add(new Connection
            {
                From = from,
                To = to,
                Cost = cost
            });
            return true;
        }

        private class Connection
        {
            public uint From { get; set; }
            public uint To { get; set; }
            public int Cost { get; set; }
        }
    }
}
