namespace Graphs
{
    /// <summary>
    /// Class repesenting a single Vertex inside a Graph
    /// </summary>
    /// <typeparam name="T">Type of item stored inside the Vertex</typeparam>
    public class Vertex<T>
    {
        private readonly List<Edge<T>> _source;
        private readonly List<Edge<T>> _target;

        internal double Distance = 0;
        internal bool Visited = false;
        /// <summary>
        /// Item stored inside the Vertex
        /// </summary>
        public T Value;
        /// <summary>
        /// All Edges connected to this Vertex in any way
        /// </summary>
        public IEnumerable<Edge<T>> Edges => _source.Concat(_target);
        /// <summary>
        /// Edges that originate in this Vertex
        /// </summary>
        public IReadOnlyCollection<Edge<T>> Source => _source;
        /// <summary>
        /// Edges that end in this Vertex
        /// </summary>
        public IReadOnlyCollection<Edge<T>> Target => _target;
        /// <summary>
        /// All other Vertices connected to this Vertex
        /// </summary>
        public IEnumerable<Vertex<T>> GetNeigbhors()
        {
            List<Vertex<T>> neighbors = new();

            foreach (Edge<T> edge in _source)
            {
                neighbors.Add(edge.Target);
            }

            return neighbors;
        }

        public Vertex(T value)
        {
            Value = value;
            _target = new();
            _source = new();
        }

        /// <summary>
        /// Creates a new edge to specified Vertex and vice versa
        /// </summary>
        /// <param name="vertex">Vertex to connect to</param>
        public void Link(Vertex<T> vertex)
        {
            Edge<T> edge = new(this, vertex);

            this._source.Add(edge);
            vertex._target.Add(edge);
        }
        /// <summary>
        /// Add edge to both collections
        /// Will fail if this Vertex isn't on either end of Edge
        /// </summary>
        /// <param name="edge">Edge to add</param>
        /// <returns>If edge was added sucesfully</returns>
        public bool Link(Edge<T> edge)
        {
            if (edge.Source == this)
            {
                _source.Add(edge);
                edge.Target._target.Add(edge);
                return true;
            }

            if (edge.Target == this)
            {
                _target.Add(edge);
                edge.Source._source.Add(edge);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes all connections to specified Vertex
        /// </summary>
        /// <param name="vertex">Vertex to disconnect from</param>
        /// <returns>If Vertex was in connections</returns>
        public bool Unlink(Vertex<T> vertex)
        {
            int removedSource = this._source.RemoveAll(e => e.Target == vertex);
            int removedTarget = this._target.RemoveAll(e => e.Source == vertex);

            return removedSource > 0 || removedTarget > 0;

        }
        /// <summary>
        /// Removes one specified Edge
        /// Removes from both ends
        /// </summary>
        /// <param name="edge">Edge to remove</param>
        /// <returns>If Vertex was connected</returns>
        public bool Unlink(Edge<T> edge)
        {
            bool removedSource = edge.Source._source.Remove(edge);
            bool removedTarget = edge.Target._target.Remove(edge);

            return removedSource || removedTarget;
        }
    }
    /// <summary>
    /// Class representing an Edge in a Graph
    /// </summary>
    /// <typeparam name="T">Item stored inside the Edge</typeparam>
    public class Edge<T>
    {
        private Vertex<T> _source;
        private Vertex<T> _target;

        /// <summary>
        /// Weight of the Edge
        /// Used for pathfinding algoritmhs
        /// </summary>
        public double Weight;

        /// <summary>
        /// Source of the Edge
        /// </summary>
        public Vertex<T> Source => _source;
        /// <summary>
        /// End of the Edge
        /// </summary>
        public Vertex<T> Target => _target;

        public Edge(Vertex<T> source, Vertex<T> target)
        {
            this._source = source;
            this._target = target;
            this.Weight = 1;
        }
        public Edge(Vertex<T> source, Vertex<T> target, int weight)
        {
            this._source = source;
            this._target = target;
            this.Weight = weight;
        }

        /// <summary>
        /// Returns Other End of an Edge
        /// </summary>
        /// <param name="vertex">One end of the Edge</param>
        /// <returns>Other end of specified Edge from Vertex</returns>
        public Vertex<T> OtherEnd(Vertex<T> vertex)
        {
            if (vertex == _source)
                return _target;
            else
                return _source;
        }
    }
}