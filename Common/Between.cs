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

        public static bool IsBetween(this int item, int start, int end, bool inclusiveLower, bool inclusiveHigher)
        {
            if (start > end)
            {
                int temp = end;
                end = start;
                start = temp;
            }

            if (!inclusiveLower)
            {
                start++;
            }

            if (!inclusiveHigher)
            {
                end--;
            }
            return item >= start && item <= end;
        }
    }
}
