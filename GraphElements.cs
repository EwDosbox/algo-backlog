namespace Graphs
{
    public class Vertex<T>
    {
        private List<Edge<T>> edges;

        public double Distance = 0;
        public bool Visited = false;
        public Vertex<T>? Parent = null;
        public T Value;
        public IEnumerable<Edge<T>> Edges => edges.Distinct();
        public IEnumerable<Edge<T>> Source => edges.Where(e => e.Source == this);
        public IEnumerable<Edge<T>> Target => edges.Where(e => e.Target == this);
        public IEnumerable<Vertex<T>> Neighbors => edges.Select(e => e.OtherEnd(this));

        public Vertex(T value)
        {
            Value = value;
            edges = new();
        }

        public void Link(Vertex<T> vertex)
        {
            Edge<T> edge = new(this, vertex);

            this.edges.Add(edge);
            vertex.edges.Add(edge);
        }
        public bool Link(Edge<T> edge)
        {
            if (edge.Source != this && edge.Target != this)
                return false;

            edges.Add(edge);
            return true;
        }

        public bool Unlink(Vertex<T> vertex)
        {
            return edges.RemoveAll(e => e.Target == vertex) > 0;
        }
        public bool Unlink(Edge<T> edge)
        {
            if (edge.Source != this && edge.Target != this)
                return false;

            edge.Source.edges.Remove(edge);
            edge.Target.edges.Remove(edge);
            return true;
        }
    }

    public class Edge<T>
    {
        private Vertex<T> source;
        private Vertex<T> target;

        public double Weight;

        public Vertex<T> Source => source;
        public Vertex<T> Target => target;

        public Edge(Vertex<T> source, Vertex<T> target)
        {
            this.source = source;
            this.target = target;
            this.Weight = 1;
        }
        public Edge(Vertex<T> source, Vertex<T> target, int weight)
        {
            this.source = source;
            this.target = target;
            this.Weight = weight;
        }

        public Vertex<T> OtherEnd(Vertex<T> vertex)
        {
            if (vertex == source)
                return target;
            else
                return source;
        }
    }
}