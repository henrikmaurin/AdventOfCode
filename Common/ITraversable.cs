namespace Common
{
    public interface ITraversable<T>
    {
        public bool TraversableFrom(ITraversable<T> from);
    }

    public interface ICostTraversable<T>
    {
        public int CostTraversableFrom(ITraversable<T> from);
    }
}
