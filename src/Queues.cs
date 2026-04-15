using Heaps;

namespace Queue
{
    public class PriorityQueue<T>
    {
        protected MaxHeap<T> heap;

        public int Count => heap.Count;

        public PriorityQueue()
        {
            this.heap = new();
        }

        public void Enqueue(T item, double priority)
        {
            heap.Insert(item, priority);
        }
        public T Dequeue()
        {
            return heap.ExtractExtreme();
        }
        public T Peek()
        {
            return heap.Extreme();
        }
    }
}