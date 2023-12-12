namespace Common
{
    public static class Between
    {
        public static bool IsBetween<T>(this T item, T start, T end)
        {
            if (Comparer<T>.Default.Compare(start, end) > 0)
            {
                T temp = end;
                end = start;
                start = temp;
            }


            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }
    }
}
