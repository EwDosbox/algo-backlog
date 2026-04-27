using System.Collections.Immutable;
using Queue;

namespace Graphs
{
    #region BaseGraph
    /// <summary>
    /// Abstract class defining Basic Graph behaviour
    /// </summary>
    /// <typeparam name="T">Type of items stored in Vertices and Edges</typeparam>
    public abstract class BaseGraph<T>
    {
        /// <summary>
        /// Hash table for constant acces to all vertices
        /// </summary>
        protected HashSet<Vertex<T>> _vertices;
        public IReadOnlyCollection<Vertex<T>> Vertices => _vertices;
        public IReadOnlyCollection<Edge<T>> Edges
        {
            get
            {
                List<Edge<T>> edges = new();

                foreach (Vertex<T> vertex in _vertices)
                {
                    edges.Concat(vertex.Source);
                }

                return edges;
            }
        }
        public int NoOfComponents() => GetComponents().Count();

        public IReadOnlyCollection<IReadOnlyCollection<Vertex<T>>> GetComponents()
        {
            List<List<Vertex<T>>> res = new();

            ResetVisited();

            foreach (Vertex<T> vertex in _vertices)
            {
                if (!vertex.Visited)
                {
                    List<Vertex<T>> component = new();
                    DFS_Recursive(v => component.Add(v), vertex);
                    res.Add(component);
                }
            }

            return res;
        }

        public BaseGraph()
        {
            _vertices = new();
        }
        public BaseGraph(IEnumerable<Vertex<T>> vertices)
        {
            this._vertices = new(vertices);
        }
        #region Public Methods
        /// <summary>
        /// Insert a new vertex into the graph
        /// </summary>
        /// <param name="vertexValue">Value of the newly inserted node</param>
        /// <returns> Inserted Vertex of vertexValue</returns>
        public virtual Vertex<T> Insert(T vertexValue)
        {
            Vertex<T> vertex = new(vertexValue);

            _vertices.Add(vertex);

            return vertex;
        }
        /// <summary>
        /// Inserts a new vertex into the graph
        /// Removes its old edges
        /// </summary>
        /// <param name="vertex">Vertex to insert</param>
        /// <returns>Inserted vertex</returns>
        public virtual Vertex<T> Insert(Vertex<T> vertex)
        {
            foreach (Edge<T> e in vertex.Edges)
                vertex.Unlink(e);

            _vertices.Add(vertex);

            return vertex;
        }

        /// <summary>
        /// Links two vertices in the graph
        /// </summary>
        /// <param name="source">source of the new edge</param>
        /// <param name="target">target of the new edge</param>
        /// <returns>If insertion was succesful</returns>
        public abstract bool Link(Vertex<T> source, Vertex<T> target);
        /// <summary>
        /// Unlinks two vertices in the graph
        /// </summary>
        /// <param name="source">source of the new edge</param>
        /// <param name="target">target of the new edge</param>
        /// <returns>If insertion was succesful</returns>
        public abstract bool Unlink(Vertex<T> source, Vertex<T> target);

        /// <summary>
        /// Generic Breadth-First Search that will perform action on every Vertex  
        /// Always searches the full graph component
        /// </summary>
        /// <param name="action">Action to be performed in each Vertex</param>
        /// <param name="startVertex">Start of the Search</param>
        public void BFS(Action<Vertex<T>> action, Vertex<T> startVertex)
        {
            ResetVisited();
            Queue.Queue<Vertex<T>> queue = new();

            startVertex.Visited = true;
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
                Vertex<T> current = queue.Dequeue();

                current.Visited = true;
                action(current);

                foreach (Vertex<T> neighbor in current.GetNeigbhors())
                {
                    if (!neighbor.Visited)
                    {
                        neighbor.Visited = true;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        /// <summary>
        /// Generic Breadth-First Search that evaluates predicate on every Vertex  
        /// Searches until first Vertex that predicate is true for
        /// </summary>
        /// <param name="predicate">Predicate to execute until the first correct Vertex</param>
        /// <param name="startVertex">Start of the Search</param>
        public bool BFS(Func<Vertex<T>, bool> predicate, Vertex<T> startVertex)
        {
            ResetVisited();
            Queue.Queue<Vertex<T>> queue = new();

            startVertex.Visited = true;
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
                Vertex<T> current = queue.Dequeue();

                current.Visited = true;
                if (predicate(current))
                    return true;

                foreach (Vertex<T> neighbor in current.GetNeigbhors())
                {
                    if (!neighbor.Visited)
                    {
                        neighbor.Visited = true;
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Generic Depth-First Search that will execute action on every Vertex
        /// Always searches the full graph component
        /// </summary>
        /// <param name="action">Action to execute on every Vertex</param>
        /// <param name="startVertex">Start of Search</param>
        public void DFS(Action<Vertex<T>> action, Vertex<T> startVertex)
        {
            ResetVisited();
            DFS_Recursive(action, startVertex);
        }
        /// <summary>
        /// Generic Depth-First Search that will evaluate predicate until first true Vertex
        /// Ends on the first true Vertex
        /// </summary>
        /// <param name="predicate">Predicate to evaluate until correct</param>
        /// <param name="startVertex">Start of Search</param>
        /// <returns>IF there is at least one Vertex that predicate is true</returns>
        public bool DFS(Func<Vertex<T>, bool> predicate, Vertex<T> startVertex)
        {
            ResetVisited();
            return DFS_Recursive(predicate, startVertex);
        }
        /// <summary>
        /// Predicate that evaluates if Vertices are in the same component
        /// </summary>
        /// <param name="start">Start of Search</param>
        /// <param name="end">End of Search</param>
        /// <returns>If two vertices are connected</returns>
        public bool AreConnected(Vertex<T> start, Vertex<T> end)
        {
            return DFS(x => x == end, start);
        }
        /// <summary>
        /// Runs Dijkstra's algorithm and Sets the Distance from Start
        /// Property can be seen in Distance property for all Vertices in the component
        /// </summary>
        /// <param name="start">Start of the search</param>
        public void Dijkstra(Vertex<T> start)
        {
            Action<Vertex<T>> prepDjikstra = vertex =>
            {
                vertex.Distance = double.PositiveInfinity;
                vertex.Visited = false;
            };
            DFS(prepDjikstra, start);

            start.Distance = 0;

            Queue.PriorityQueue<Vertex<T>, double> queue = new();
            queue.Enqueue(start, 0);

            while (queue.Count > 0)
            {
                Vertex<T> curr = queue.Dequeue();
                curr.Visited = true;
                foreach (Edge<T> edge in curr.Edges)
                {
                    Vertex<T> neighbor = edge.OtherEnd(curr);
                    if (neighbor.Visited)
                        continue;

                    neighbor.Distance = curr.Distance + edge.Weight;
                    queue.Enqueue(neighbor, neighbor.Distance);
                }

            }
        }
        #endregion

        #region Private methods
        private void DFS_Recursive(Action<Vertex<T>> action, Vertex<T> vertex)
        {
            vertex.Visited = true;
            action(vertex);

            foreach (Vertex<T> neighbor in vertex.GetNeigbhors())
            {
                if (!neighbor.Visited)
                {
                    DFS_Recursive(action, neighbor);
                }
            }
        }
        private bool DFS_Recursive(Func<Vertex<T>, bool> predicate, Vertex<T> vertex)
        {
            vertex.Visited = true;

            if (predicate(vertex))
                return true;

            foreach (Vertex<T> neighbor in vertex.GetNeigbhors())
            {
                if (!neighbor.Visited && DFS_Recursive(predicate, neighbor))
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Resets the Visited property for all Vertices
        /// Should be called before every Search
        /// </summary>
        private void ResetVisited()
        {
            foreach (Vertex<T> vertex in _vertices)
                vertex.Visited = false;
        }
        #endregion
    }
    #endregion

    #region DirectedGraph
    /// <summary>
    /// Basic Directed Graph implementation
    /// </summary>
    /// <typeparam name="T">Type of items stored in Vertices and Edges</typeparam>
    public class DirectedGraph<T> : BaseGraph<T>
    {
        /// <inheritdoc/>
        public override bool Link(Vertex<T> source, Vertex<T> target)
        {
            if (!_vertices.Contains(source) || !_vertices.Contains(target))
                return false;

            source.Link(target);

            return true;
        }

        public override bool Unlink(Vertex<T> source, Vertex<T> target)
        {
            return source.Unlink(target);
        }
    }
    #endregion

    #region Undirected Graph
    /// <summary>
    /// Undirected Graph implemntation
    /// </summary>
    /// <typeparam name="T">Type of items stored in Vertices and Edges</typeparam>
    public class UndirectedGraph<T> : BaseGraph<T>
    {
        /// <inheritdoc/>
        public override bool Link(Vertex<T> source, Vertex<T> target)
        {
            if (!_vertices.Contains(source) || !_vertices.Contains(target))
                return false;

            Edge<T> edge = new(source, target);
            source.Link(edge);
            target.Link(edge);

            return true;
        }
        public bool Link(Vertex<T> source, Vertex<T> target, int weight)
        {
            if (!_vertices.Contains(source) || !_vertices.Contains(target))
                return false;

            Edge<T> edge = new(source, target, weight);
            source.Link(edge);
            target.Link(edge);

            return true;
        }
        public override bool Unlink(Vertex<T> source, Vertex<T> target)
        {
            if (!source.Unlink(target) || !target.Unlink(source))
                return false;

            return true;
        }
    }
    #endregion
}