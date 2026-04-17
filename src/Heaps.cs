namespace Heaps
{
    /// <summary>
    /// Abstract definition of an array backed binary heap with priority
    /// </summary>
    /// <typeparam name="T">Type of items stored in the heap</typeparam>
    public abstract class BaseHeap<TItem, TPriority>
    {
        protected List<HeapItem<TItem, TPriority>> array;
        protected Dictionary<HeapItem<TItem, TPriority>, int> translate;
        protected readonly IComparer<TPriority> Comparer;

        /// <summary>
        /// Number of items in heap
        /// </summary>
        public int Count => array.Count;

        /// <summary>
        /// Initializes an empty heap
        /// </summary>
        public BaseHeap(IComparer<TPriority>? comparer = null)
        {
            array = new();
            translate = new();

            Comparer = comparer ?? Comparer<TPriority>.Default;
        }
        /// <summary>
        /// Initializes and builds an heap in linear time
        /// </summary>
        /// <param name="values">Collection of items and priorities to make a heap out of</param>
        public BaseHeap(IEnumerable<(TItem item, TPriority priority)> values, IComparer<TPriority>? comparer = null) : this(comparer)
        {
            foreach (var (item, priority) in values)
            {
                HeapItem<TItem, TPriority> heapItem = new(item, priority);
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
        public TItem Extreme()
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
        public TItem ExtractExtreme()
        {
            if (array.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            HeapItem<TItem, TPriority> extreme = array[0];

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
        public void Insert(TItem item, TPriority priority)
        {
            HeapItem<TItem, TPriority> toInsert = new(item, priority);
            array.Add(toInsert);

            translate[toInsert] = array.Count - 1;

            SiftUp(toInsert);
        }
        /// <summary>
        /// Increases priority and restores structure
        /// </summary>
        /// <param name="item">Item to change, has to be in the heap</param>
        /// <param name="newPriority">New priority, determines new location</param>
        public virtual void IncreaseKey(HeapItem<TItem, TPriority> item, TPriority newPriority)
        {
            item.Priority = newPriority;
        }
        /// <summary>
        /// Decreses priority and restores structure
        /// </summary>
        /// <param name="item">Item to change, has to be in the heap</param>
        /// <param name="newPriority">New priority, determines new location</param>
        public virtual void DecreaseKey(HeapItem<TItem, TPriority> item, TPriority newPriority)
        {
            item.Priority = newPriority;
        }

        protected abstract void SiftUp(HeapItem<TItem, TPriority> item);
        protected abstract void SiftUp(int index);
        protected abstract void SiftDown(HeapItem<TItem, TPriority> item);
        protected abstract void SiftDown(int index);
        protected int? Parent(int i) => i == 0 ? null : (i - 1) / 2;
        protected int? Left(int i) => (2 * i + 1 < array.Count) ? 2 * i + 1 : null;
        protected int? Right(int i) => (2 * i + 2 < array.Count) ? 2 * i + 2 : null;

        protected void Swap(int i, int j)
        {
            HeapItem<TItem, TPriority> temp = array[i];
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
    public class MinHeap<TItem, TPriority> : BaseHeap<TItem, TPriority>
    {
        /// <inheritdoc />
        public MinHeap(IEnumerable<(TItem item, TPriority priority)> values, IComparer<TPriority>? comparer = null) : base(values, comparer) { }
        public MinHeap(IComparer<TPriority>? comparer = null) : base(comparer) { }

        public override void IncreaseKey(HeapItem<TItem, TPriority> item, TPriority newPriority)
        {
            base.IncreaseKey(item, newPriority);
            SiftDown(item);
        }
        public override void DecreaseKey(HeapItem<TItem, TPriority> item, TPriority newPriority)
        {
            base.DecreaseKey(item, newPriority);
            SiftUp(item);
        }

        protected override void SiftUp(HeapItem<TItem, TPriority> item)
        {
            int idx = translate[item];

            SiftUp(idx);
        }
        protected override void SiftUp(int index)
        {
            while (index > 0 && Comparer.Compare(array[index].Priority, array[(index - 1) / 2].Priority) < 0)
            {
                int parentIdx = (index - 1) / 2;
                Swap(index, parentIdx);
                index = parentIdx;
            }
        }

        protected override void SiftDown(HeapItem<TItem, TPriority> item)
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

                if (left.HasValue && Comparer.Compare(array[left.Value].Priority, array[smallest].Priority) < 0)
                    smallest = left.Value;

                if (right.HasValue && Comparer.Compare(array[right.Value].Priority, array[smallest].Priority) < 0)
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
    public class MaxHeap<TItem, TPriority> : BaseHeap<TItem, TPriority>
    {
        /// <inheritdoc />
        public MaxHeap(IEnumerable<(TItem item, TPriority priority)> values, IComparer<TPriority>? comparer = null) : base(values, comparer) { }
        public MaxHeap(IComparer<TPriority>? comparer = null) : base(comparer) { }
        public override void IncreaseKey(HeapItem<TItem, TPriority> item, TPriority newPriority)
        {
            base.IncreaseKey(item, newPriority);
            SiftUp(item);
        }
        public override void DecreaseKey(HeapItem<TItem, TPriority> item, TPriority newPriority)
        {
            base.DecreaseKey(item, newPriority);
            SiftDown(item);
        }

        protected override void SiftUp(HeapItem<TItem, TPriority> item)
        {
            int idx = translate[item];

            SiftUp(idx);
        }
        protected override void SiftUp(int index)
        {
            while (index > 0 && Comparer.Compare(array[index].Priority, array[(index - 1) / 2].Priority) < 0)
            {
                int parentIdx = (index - 1) / 2;
                Swap(index, parentIdx);
                index = parentIdx;
            }
        }

        protected override void SiftDown(HeapItem<TItem, TPriority> item)
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

                if (left.HasValue && Comparer.Compare(array[left.Value].Priority, array[smallest].Priority) < 0)
                    smallest = left.Value;

                if (right.HasValue && Comparer.Compare(array[right.Value].Priority, array[smallest].Priority) < 0)
                    smallest = right.Value;

                if (smallest != index)
                {
                    Swap(index, smallest);
                    index = smallest;
                }

            } while (smallest != index);
        }

    }
    public class HeapItem<TItem, TPriority>
    {
        public TPriority Priority;
        public TItem Item;

        public HeapItem(TItem item, TPriority priority)
        {
            this.Item = item;
            this.Priority = priority;
        }
    }
}