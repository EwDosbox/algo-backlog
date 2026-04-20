# Heaps

Namespace `Heaps` implements array backed binary heaps.

These heaps are `List` backed, so they are fast and dynamic.

### Methods

- `n` is number of items in a heap
- `h` is depth of a heap, defined as $log_2(n)$

| Operation | Time Complexity | Space Complexity | Description |
| --------- | --------------- | ---------------- | ----------- |
| Extreme | O(1) | O(1) | Peeks at the root of the heap |
| Extract Extreme | O(h) | O(1) | Takes the root and restores structure |
| Increase / Decrease Key| O(h) | O(1) | Changes priority and moves it accordingly |
| Insert |O(h) | O(1) | Inserts a new item and restores heap protperties |
## Class hierarchy

### BaseHeap

`BaseHeap` is an abstract class that implements most of the heap's hard work. Handles the lookup dictionary and parent, children logic.

It is generic based so it can store any item and use anything as a priority if there is an `Comparer` acompannied with it.

### MinHeap

Implementation of Minimal Binary Heap `BaseHeap`. Root always has the lowest priority. Can be used as a Priority Queue for lowest priorities.

### MaxHeap

Implementation of Maximal Binary Heap, derived from `BaseHeap`. Root is always the highest priority.

Can be used as a Priority Queue, but i reccomend using `Queues.PriorityQueue`, which is just a wrapper but makes work  easier.
