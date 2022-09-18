namespace Common
{
    public static class ArrayHelper
    {
        public static void Clear<T>(this T[] array)
        {
            array = new T[array.Length];
        }
    }
}
