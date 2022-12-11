using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class MathHelpers
    {
        public static int Product(this IEnumerable<int> values)
        {
            int result = 1;
            foreach (int value in values)
                result *= value;
            return result;
        }

        public static long Product(this IEnumerable<long> values)
        {
            long result = 1;
            foreach (int value in values)
                result *= value;
            return result;
        }

        public static int GreatesCommonFactor(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long GreatesCommonFactor(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static int LeastCommonMultiple(int a, int b)
        {
            return (a / GreatesCommonFactor(a, b)) * b;
        }

        public static long LeastCommonMultiple(long a, long b)
        {
            return (a / GreatesCommonFactor(a, b)) * b;
        }

    }
}
