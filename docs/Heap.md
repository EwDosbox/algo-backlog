# Heaps

Namespace Heaps implements binary heaps. 

These heaps are list backed, so they are fast and dynamic.

## BaseHeap

- `n` is number of items in a heap
- `h` is depth of a heap, defined as $log_2(n)$

| Operation | Time Complexity | Space Complexity |
| --------- | --------------- | ---------------- |
| Extreme | O(1) | O(1) |
| Extract Extreme | O(h) | O(1) |
| Increase Key| O(h) | O(1) |
| Decrease Key| O(h) | O(1) | 
| Insert |O(h) | O(1) | 
## MinHeap
## MaxHeap