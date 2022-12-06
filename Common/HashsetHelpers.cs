using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class HashsetHelpers
    {
        public static bool TryAdd<T>(this HashSet<T> set,T value)
        {
            if (set.Contains(value))
                return false;

            set.Add(value);
            return true;
        }
    }
}
