using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class InExtension
    {
        public static bool In<T>(this T me, T[] values)
        {
            if (values == null)
                return false;

            if (values.Contains(me))
                return true;

            return false;
        }

        public static bool In<T>(this T me, List<T> values)
        {
            if (values == null)
                return false;

            if (values.Contains(me))
                return true;

            return false;
        }

        public static bool In<T>(this T me, T val1, T val2)
        {
            return me.In(new T[] { val1, val2 });
        }
        public static bool In<T>(this T me, T val1, T val2, T val3)
        {
            return me.In(new T[] { val1, val2, val3 });
        }
        public static bool In<T>(this T me, T val1, T val2, T val3, T val4)
        {
            return me.In(new T[] { val1, val2, val3, val4});
        }
        public static bool In<T>(this T me, T val1, T val2, T val3, T val4, T val5)
        {
            return me.In(new T[] { val1, val2, val3, val4, val5});
        }
        public static bool In<T>(this T me, T val1, T val2, T val3, T val4, T val5, T val6)
        {
            return me.In(new T[] { val1, val2, val3, val4, val5, val6 });
        }
    }
}
