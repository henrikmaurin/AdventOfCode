using System.Numerics;

namespace Common
{
    static public class EnumerableHelpers
    {
        static public T Multiply<T>(this IEnumerable<T>? source) where T : INumber<T>
        {
            if (source == null) return T.Zero;

            T result = T.One;
            foreach (T item in source)
            {
                result *= item;
            }

            return result;
        }
    }
}
