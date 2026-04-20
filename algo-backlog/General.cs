public static class General
{
    public static bool IsSorted(int[] array)
    {
        bool isSorted = true;

        for (int i = 1; i < array.Length; i++)
            if (array[i] < array[i - 1])
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