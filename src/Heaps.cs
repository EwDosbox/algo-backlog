namespace Heaps
{
    /// <summary>
    /// Abstract definition of an array backed binary heap with priority
    /// </summary>
    /// <typeparam name="T">Type of items stored in the heap</typeparam>
    public abstract class BaseHeap<T>
    {
        protected List<HeapItem<T>> array;
        protected Dictionary<HeapItem<T>, int> translate;

        /// <summary>
        /// Number of items in heap
        /// </summary>
        public int Count => array.Count;

        /// <summary>
        /// Initializes an empty heap
        /// </summary>
        public BaseHeap()
        {
            array = new();
            translate = new();
        }
        /// <summary>
        /// Initializes and builds an heap in linear time
        /// </summary>
        /// <param name="values">Collection of items and priorities to make a heap out of</param>
        public BaseHeap(IEnumerable<(T item, double priority)> values)
        {
            array = new();
            translate = new();

            foreach (var (item, priority) in values)
            {
                HeapItem<T> heapItem = new(item, priority);
                array.Add(heapItem);
            }

            for (int i = (array.Count / 2) - 1; i >= 0; i--)
            {
                SiftDown(i);
            }

            for (int i = 0; i < array.Count; i++)
            {
                translate[array[i]] = i;
            }
        }
        /// <summary>
        /// Peeks at the root
        /// </summary>
        /// <returns>Root of the heap</returns>
        /// <exception cref="InvalidOperationException">If heap is empty</exception>
        public T Extreme()
        {
            if (array.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            return array[0].Item;
        }
        /// <summary>
        /// Removes root and restores heap structure
        /// </summary>
        /// <returns>Old root</returns>
        /// <exception cref="InvalidOperationException">If heap is empty</exception>
        public T ExtractExtreme()
        {
            if (array.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            HeapItem<T> extreme = array[0];

            Swap(0, array.Count - 1);

            array.RemoveAt(array.Count - 1);
            translate.Remove(extreme);

            if (array.Count > 0)
                SiftDown(0);

            return extreme.Item;
        }
        /// <summary>
        /// Inserts a new item into the heap and restores structure
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <param name="priority">Priority that determines location in the heap</param>
        public void Insert(T item, double priority)
        {
            HeapItem<T> toInsert = new(item, priority);
            array.Add(toInsert);

            translate[toInsert] = array.Count - 1;

            SiftUp(toInsert);
        }
        /// <summary>
        /// Increases priority and restores structure
        /// </summary>
        /// <param name="item">Item to change, has to be in the heap</param>
        /// <param name="newPriority">New priority, determines new location</param>
        public virtual void IncreaseKey(HeapItem<T> item, int newPriority)
        {
            item.Priority = newPriority;
        }
        /// <summary>
        /// Decreses priority and restores structure
        /// </summary>
        /// <param name="item">Item to change, has to be in the heap</param>
        /// <param name="newPriority">New priority, determines new location</param>
        public virtual void DecreaseKey(HeapItem<T> item, int newPriority)
        {
            item.Priority = newPriority;
        }

        protected abstract void SiftUp(HeapItem<T> item);
        protected abstract void SiftUp(int index);
        protected abstract void SiftDown(HeapItem<T> item);
        protected abstract void SiftDown(int index);
        protected int? Parent(int i) => i == 0 ? null : (i - 1) / 2;
        protected int? Left(int i) => (2 * i + 1 < array.Count) ? 2 * i + 1 : null;
        protected int? Right(int i) => (2 * i + 2 < array.Count) ? 2 * i + 2 : null;

        protected void Swap(int i, int j)
        {
            HeapItem<T> temp = array[i];
            array[i] = array[j];
            array[j] = temp;

            translate[array[i]] = i;
            translate[array[j]] = j;
        }
    }

    /// <summary>
    /// Minimal Binary heap with priority
    /// </summary>
    /// <typeparam name="T">Item stored in the heap</typeparam>
    public class MinHeap<T> : BaseHeap<T>
    {
        /// <inheritdoc />
        public override void IncreaseKey(HeapItem<T> item, int newPriority)
        {
            base.IncreaseKey(item, newPriority);
            SiftDown(item);
        }
        public override void DecreaseKey(HeapItem<T> item, int newPriority)
        {
            base.DecreaseKey(item, newPriority);
            SiftUp(item);
        }

        protected override void SiftUp(HeapItem<T> item)
        {
            int idx = translate[item];

            SiftUp(idx);
        }
        protected override void SiftUp(int index)
        {
            while (index > 0 && array[index].Priority < array[(index - 1) / 2].Priority)
            {
                int parentIdx = (index - 1) / 2;
                Swap(index, parentIdx);
                index = parentIdx;
            }
        }

        protected override void SiftDown(HeapItem<T> item)
        {
            int idx = translate[item];

            SiftDown(idx);
        }

        protected override void SiftDown(int index)
        {
            int smallest;
            do
            {
                int? left = Left(index);
                int? right = Right(index);
                smallest = index;

                if (left.HasValue && array[left.Value].Priority < array[smallest].Priority)
                    smallest = left.Value;

                if (right.HasValue && array[right.Value].Priority < array[smallest].Priority)
                    smallest = right.Value;

                if (smallest != index)
                {
                    Swap(index, smallest);
                    index = smallest;
                }

            } while (smallest != index);
        }
    }

    /// <summary>
    /// Maximal Binary heap with priority
    /// </summary>
    /// <typeparam name="T">Item stored in the heap</typeparam>
    public class MaxHeap<T> : BaseHeap<T>
    {
        /// <inheritdoc />
        public override void IncreaseKey(HeapItem<T> item, int newPriority)
        {
            base.IncreaseKey(item, newPriority);
            SiftUp(item);
        }
        public override void DecreaseKey(HeapItem<T> item, int newPriority)
        {
            base.DecreaseKey(item, newPriority);
            SiftDown(item);
        }

        protected override void SiftUp(HeapItem<T> item)
        {
            int idx = translate[item];

            SiftUp(idx);
        }
        protected override void SiftUp(int index)
        {
            while (index > 0 && array[index].Priority > array[(index - 1) / 2].Priority)
            {
                int parentIdx = (index - 1) / 2;
                Swap(index, parentIdx);
                index = parentIdx;
            }
        }

        protected override void SiftDown(HeapItem<T> item)
        {
            int idx = translate[item];

            SiftDown(idx);
        }
        protected override void SiftDown(int index)
        {
            int smallest;
            do
            {
                int? left = Left(index);
                int? right = Right(index);
                smallest = index;

                if (left.HasValue && array[left.Value].Priority > array[smallest].Priority)
                    smallest = left.Value;

                if (right.HasValue && array[right.Value].Priority > array[smallest].Priority)
                    smallest = right.Value;

                if (smallest != index)
                {
                    Swap(index, smallest);
                    index = smallest;
                }

            } while (smallest != index);
        }

    }
    public class HeapItem<T>
    {
        public double Priority;
        public T Item;

        public HeapItem(T item, double priority)
        {
            this.Item = item;
            this.Priority = priority;
        }
    }
}