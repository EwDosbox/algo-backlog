using Heaps;

public static class Sorts
{
    public static IEnumerable<IComparable> QuickSort(IEnumerable<IComparable> values)
    {
        throw new NotImplementedException();
    }
    public static IEnumerable<IComparable> InsertionSort(IEnumerable<IComparable> values)
    {
        throw new NotImplementedException();
    }
    public static IEnumerable<T> HeapSort<T>(IList<T> values) where T : IComparable
    {

        MinHeap<T, T> minHeap = new(values, values);

        T[] sorted = new T[values.Count];
        int idx = 0;

        while (minHeap.Count > 0)
        {
            sorted[idx] = minHeap.ExtractExtreme();
            idx++;
        }

        return sorted;
    }
    public static IEnumerable<IComparable> MergeSort(IEnumerable<IComparable> values)
    {
        throw new NotImplementedException();
    }
    public static IEnumerable<IComparable> CountingSort(IEnumerable<IComparable> values)
    {
        throw new NotImplementedException();
    }
}