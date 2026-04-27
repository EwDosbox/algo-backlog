public static class General
{
    public static bool IsSorted<T>(IList<T> values) where T : IComparable
    {
        bool isSorted = true;

        for (int i = 1; i < values.Count; i++)
            if (values[i].CompareTo(values[i - 1]) > 0)
            {
                isSorted = false;
                break;
            }

        return isSorted;
    }

    public static void Swap<T>(ref T x, ref T y)
    {
        (y, x) = (x, y);
    }

    public static void Swap<T>(IList<T> list, int i, int j)
    {
        T bucket = list[j];

        list[j] = list[i];
        list[i] = bucket;
    }
}

public enum Order
{
    PreOrder,
    InOrder,
    PostOrder
}
public enum NodeColor
{
    Red,
    Black
}