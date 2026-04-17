using Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class HeapTests
{
    [TestMethod]
    public void BuildMinHeap()
    {
        MinHeap<string, int> minHeap = new();

        minHeap.Insert("second", 2);
        minHeap.Insert("first", 1);
        minHeap.Insert("third", 3);

        Assert.AreEqual("first", minHeap.ExtractExtreme());
        Assert.AreEqual("second", minHeap.ExtractExtreme());
        Assert.AreEqual("third", minHeap.ExtractExtreme());
        Assert.ThrowsException<InvalidOperationException>(() => minHeap.ExtractExtreme());
    }

    [TestMethod]
    public void BuildMaxHeap()
    {
        MaxHeap<string, int> maxHeap = new();

        maxHeap.Insert("second", 2);
        maxHeap.Insert("first", 3);
        maxHeap.Insert("third", 1);

        Assert.AreEqual("first", maxHeap.ExtractExtreme());
        Assert.AreEqual("second", maxHeap.ExtractExtreme());
        Assert.AreEqual("third", maxHeap.ExtractExtreme());
        Assert.ThrowsException<InvalidOperationException>(() => maxHeap.ExtractExtreme());
    }
}