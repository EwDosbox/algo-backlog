using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class QueueTests
{
    [TestMethod]
    public void Queue_BuildQueue()
    {
        Queue<string> queue = new();

        queue.Enqueue("first");
        queue.Enqueue("second");
        queue.Enqueue("third");

        Assert.AreEqual("first", queue.Dequeue());
        Assert.AreEqual("second", queue.Dequeue());
        Assert.AreEqual("third", queue.Dequeue());
        Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
    }

    [TestMethod]
    public void EnqueueDequeue_WrapAround()
    {
        Queue<int> queue = new();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Dequeue();
        queue.Enqueue(3);

        Assert.AreEqual(2, queue.Count());
        Assert.AreEqual(2, queue.Dequeue());
        Assert.AreEqual(3, queue.Dequeue());
        Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
    }
}