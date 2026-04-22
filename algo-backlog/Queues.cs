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
        private T[] _values;
        private int _head = 0;
        private int _tail = 0;

        private int _capacity = 2;
        private int _count = 0;


        public int Count => _count;

        public Queue()
        {
            _values = new T[_capacity];
        }

        public void Enqueue(T item)
        {
            if (_count == _capacity)
                IncreaseCapacity();

            _values[_tail] = item;
            _tail = (_tail + 1) % _capacity;
            _count++;
        }
        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty");

            T item = _values[_head];
            _values[_head] = default;

            _count--;
            _head = (_head + 1) % _capacity;

            return item;
        }

        private void IncreaseCapacity()
        {
            int oldCapacity = _capacity;
            _capacity *= 2;

            T[] res = new T[_capacity];

            for (int i = 0; i < _count; i++)
            {
                res[i] = _values[(_head + i) % oldCapacity];
            }

            _values = res;
            _tail = 0;
            _head = _count;
        }
    }
}