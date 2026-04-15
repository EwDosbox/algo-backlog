namespace Graphs
{
    public abstract class BaseGraph<T>
    {
        protected HashSet<Vertex<T>> vertices;
        public IEnumerable<Vertex<T>> Vertices => vertices.AsEnumerable();
        public IEnumerable<Edge<T>> Edges => vertices.SelectMany(v => v.Edges).Distinct();

        public BaseGraph()
        {
            vertices = new();
        }
        public BaseGraph(IEnumerable<Vertex<T>> vertices)
        {
            this.vertices = new(vertices);
        }
        #region Public Methods
        public virtual Vertex<T> Insert(T vertexValue)
        {
            Vertex<T> vertex = new(vertexValue);

            vertices.Add(vertex);

            return vertex;
        }
        public virtual Vertex<T> Insert(Vertex<T> vertex)
        {
            vertices.Add(vertex);

            return vertex;
        }

        public abstract bool Link(Vertex<T> source, Vertex<T> target);
        public abstract bool Unlink(Vertex<T> source, Vertex<T> target);

        public void BFS(Action<Vertex<T>> action, Vertex<T> startVertex)
        {
            ResetVisited();
            Queue<Vertex<T>> queue = new();

            startVertex.Visited = true;
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
                Vertex<T> current = queue.Dequeue();

                current.Visited = true;
                action(current);

                foreach (Vertex<T> neighbor in current.Neighbors)
                {
                    if (!neighbor.Visited)
                    {
                        neighbor.Visited = true;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
        public bool BFS(Func<Vertex<T>, bool> predicate, Vertex<T> startVertex)
        {
            ResetVisited();
            Queue<Vertex<T>> queue = new();

            startVertex.Visited = true;
            queue.Enqueue(startVertex);

            while (queue.Count > 0)
            {
                Vertex<T> current = queue.Dequeue();

                current.Visited = true;
                if (predicate(current))
                    return true;

                foreach (Vertex<T> neighbor in current.Neighbors)
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

        public void DFS(Action<Vertex<T>> action, Vertex<T> startVertex)
        {
            ResetVisited();
            DFS_Recursive(action, startVertex);
        }
        public bool DFS(Func<Vertex<T>, bool> predicate, Vertex<T> startVertex)
        {
            ResetVisited();
            return DFS_Recursive(predicate, startVertex);
        }
        private void DFS(Vertex<T> startVertex)
        {
            DFS_Recursive(startVertex);
        }

        public bool AreConnected(Vertex<T> start, Vertex<T> end)
        {
            return DFS(x => x == end, start);
        }

        public int CountComponents()
        {
            int components = 0;
            ResetVisited();

            foreach (Vertex<T> vertex in vertices)
            {
                if (!vertex.Visited)
                {
                    DFS(vertex);
                    components++;
                }
            }

            return components;
        }
        /// <summary>
        /// Returns shortest path between two vertices in a graph using Dijikstra's algorithm
        /// </summary>
        /// <param name="start">Start vertex. Has to be in Graph</param>
        /// <param name="end">Final vertex; Has to be in graph</param>
        /// <returns></returns>
        public IEnumerable<Vertex<T>> ShortestPath(Vertex<T> start, Vertex<T> end)
        {
            bool foundEnd = false;
            HashSet<Vertex<T>> component = new();
            Action<Vertex<T>> prepDjikstra = vertex =>
            {
                if (vertex == end)
                    foundEnd = true;
                component.Add(vertex);
                vertex.Distance = double.PositiveInfinity;
            };
            DFS(prepDjikstra, start);

            if (!foundEnd)
                return new List<Vertex<T>>();

            ResetVisited();
            start.Distance = 0;
            List<Vertex<T>> shortestPath = new();
            Queue<Vertex<T>> queue = new();
            queue.Enqueue(start);
            shortestPath.Add(start);

            while (queue.Count > 0)
            {
                Vertex<T> curr = queue.Dequeue();
                curr.Visited = true;

                if (curr == shortestPath.Last())
                {
                    Vertex<T> closestVertex = curr;
                    foreach (Edge<T> edge in curr.Edges)
                    {
                        Vertex<T> neighbor = edge.OtherEnd(curr);
                        if (neighbor.Visited)
                            continue;

                        neighbor.Distance = curr.Distance + edge.Weight;
                        queue.Enqueue(neighbor);
                        closestVertex = (closestVertex.Distance > neighbor.Distance) ? neighbor : closestVertex;
                    }
                    shortestPath.Add(closestVertex);
                }
                else
                {

                    foreach (Edge<T> edge in curr.Edges)
                    {
                        Vertex<T> neighbor = edge.OtherEnd(curr);
                        if (neighbor.Visited)
                            continue;

                        neighbor.Distance = curr.Distance + edge.Weight;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return shortestPath;
        }
        #endregion
        #region Private methods
        private void DFS_Recursive(Action<Vertex<T>> action, Vertex<T> vertex)
        {
            vertex.Visited = true;
            action(vertex);

            foreach (Vertex<T> neighbor in vertex.Neighbors)
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

            foreach (Vertex<T> neighbor in vertex.Neighbors)
            {
                if (!neighbor.Visited && DFS_Recursive(predicate, neighbor))
                    return true;
            }

            return false;
        }
        private void DFS_Recursive(Vertex<T> vertex)
        {
            vertex.Visited = true;

            foreach (Vertex<T> neighbor in vertex.Neighbors)
            {
                if (!neighbor.Visited)
                    DFS_Recursive(neighbor);
            }
        }

        private void ResetVisited()
        {
            foreach (Vertex<T> vertex in vertices)
                vertex.Visited = false;
        }
        #endregion
    }
    public class DirectedGraph<T> : BaseGraph<T>
    {
        public override bool Link(Vertex<T> source, Vertex<T> target)
        {
            if (!vertices.Contains(source) || !vertices.Contains(target))
                return false;

            source.Link(target);

            return true;
        }

        public override bool Unlink(Vertex<T> source, Vertex<T> target)
        {
            return source.Unlink(target);
        }
    }

    public class UndirectedGraph<T> : BaseGraph<T>
    {
        public override bool Link(Vertex<T> source, Vertex<T> target)
        {
            if (!vertices.Contains(source) || !vertices.Contains(target))
                return false;

            Edge<T> edge = new(source, target);
            source.Link(edge);
            target.Link(edge);

            return true;
        }
        public bool Link(Vertex<T> source, Vertex<T> target, int weight)
        {
            if (!vertices.Contains(source) || !vertices.Contains(target))
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
}