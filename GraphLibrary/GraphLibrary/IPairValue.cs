namespace GraphLibrary
{
    public interface IPairValue<T>
    {
        T GetFirst();
        T GetSecond();
        bool Contains(T value);
    }
}
