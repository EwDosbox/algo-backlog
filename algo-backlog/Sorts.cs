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
        List<T> sorted = new(values.Count);
        MinHeap<T, T> minHeap = new(values, values);

        while (minHeap.Count > 0)
        {
            sorted.Add(minHeap.ExtractExtreme());
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