# Graphs

This namespace defines discrete mathemathics graphs in both its most used forms: directed and undirected

## Methods


## Namespace hierarchy

### Graphs.cs

This file defines the abstract class `BaseGraph` which implements most of the hard work.

Derived classes `UndirectedGraph` and `DirectedGraph` add functionality for the `Link` and `Unlink` functions depending on wanted direction.

### GraphElements.cs

This file defines and implements two classes that are used in the `Graphs` namespace

Generic class `Vertex` 
Generic class `Edge` 