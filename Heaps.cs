namespace Heaps
{
    public abstract class BaseHeap<T>
    {
        protected List<HeapItem<T>> array;
        protected Dictionary<HeapItem<T>, int> translate;

        public BaseHeap()
        {
            array = new();
            translate = new();
        }

        public T Extreme()
        {
            if (array.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            return array[0].Item;
        }

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

        public void Insert(T item, int priority)
        {
            HeapItem<T> toInsert = new(item, priority);
            array.Add(toInsert);

            translate[toInsert] = array.Count - 1;

            SiftUp(toInsert);
        }

        public virtual void IncreaseKey(HeapItem<T> item, int newPriority)
        {
            item.Priority = newPriority;
        }

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


    public class MinHeap<T> : BaseHeap<T>
    {
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

    public class MaxHeap<T> : BaseHeap<T>
    {
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
        public int Priority;
        public T Item;

        public HeapItem(T item, int priority)
        {
            this.Item = item;
            this.Priority = priority;
        }
    }
}