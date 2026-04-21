using Heaps;

namespace Queue
{
    public class PriorityQueue<TItem, TPriority>
    {
        protected MaxHeap<TItem, TPriority> heap;

        public int Count => heap.Count;

        public PriorityQueue()
        {
            this.heap = new();
        }

        public void Enqueue(TItem item, TPriority priority)
        {
            heap.Insert(item, priority);
        }
        public TItem Dequeue()
        {
            return heap.ExtractExtreme();
        }
        public TItem Peek()
        {
            return heap.Extreme();
        }
    }
    public class Queue<T>
    {
        public int Count => throw new NotImplementedException();
        public void Enqueue(T item)
        {
            throw new NotImplementedException();
        }
        public T Dequeue()
        {
            throw new NotImplementedException();
        }
    }
}