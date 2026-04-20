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
    public static IEnumerable<T> HeapSort<T>(IEnumerable<T> values) where T : IComparable<T>
    {
        throw new NotImplementedException();
        /*
        MinHeap<IComparable> minHeap = new MinHeap<IComparable>(values);
        List<IComparable> res = new();

        while (minHeap.Count > 0)
            res.Add(minHeap.ExtractExtreme());

        return res.AsEnumerable();
        */
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