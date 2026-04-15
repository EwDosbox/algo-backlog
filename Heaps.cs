namespace Heaps
{
    public abstract class BaseHeap<T>
    {
        protected List<HeapItem<T>> array;

        public BaseHeap()
        {
            array = new();
        }

        public T Extreme()
        {
            return array[0].Item;
        }
        public void Insert(T item, int priority)
        {
            HeapItem<T> toInsert = new(item, priority);
            array.Append(toInsert);

            Heapify(toInsert);
        }
        public void IncreaseKey(HeapItem<T> item, int newPriority)
        {
            item.Priority = newPriority;

            Heapify(item);
        }

        protected abstract void Heapify(HeapItem<T> item);
        protected int? Parent(int i) => i == 0 ? null : (i - 1) / 2;
        protected int? Left(int i) => (2 * i + 1 < array.Count) ? 2 * i + 1 : null;
        protected int? Right(int i) => (2 * i + 2 < array.Count) ? 2 * i + 2 : null;

        protected void Swap(int i, int j)
        {
            General.Swap(array, i, j);
        }
    }

    public class MinHeap<T> : BaseHeap<T>
    {
        protected override void Heapify(HeapItem<T> item)
        {
            int currentIndex = array.IndexOf(item);
            SiftUp(currentIndex);
        }

        private void SiftUp(int index)
        {
            int? parentIndex = Parent(index);

            if (parentIndex.HasValue && array[index].Priority < array[parentIndex.Value].Priority)
            {
                Swap(index, parentIndex.Value);
                SiftUp(parentIndex.Value);
            }
        }

        public void SiftDown(int index)
        {
            int? left = Left(index);
            int? right = Right(index);
            int smallest = index;

            if (left.HasValue && array[left.Value].Priority < array[smallest].Priority)
                smallest = left.Value;

            if (right.HasValue && array[right.Value].Priority < array[smallest].Priority)
                smallest = right.Value;

            if (smallest != index)
            {
                Swap(index, smallest);
                SiftDown(smallest);
            }
        }
    }

    public class MaxHeap<T> : BaseHeap<T>
    {
        protected override void Heapify(HeapItem<T> item)
        {
            throw new NotImplementedException();
        }

    }
    public class HeapItem<T>
    {
        public int Priority;
        public T Item;

        public HeapItem(T item, int priority)
        {
            this.Item = item;
            this.Priority = priority;
        }
    }
}