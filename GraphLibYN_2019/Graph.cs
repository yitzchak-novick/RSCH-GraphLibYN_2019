using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsYN;

namespace GraphLibYN_2019
{
    public class Graph
    {
        #region PRIVATE_MEMBERS 
        // This will be a private class that will be used internally in the Graph class
        // so that the properties of a vertex can be manipulated.
        private class GVertex : Vertex
        {
            public GVertex(string id) : base(id)
            {
            }

            public bool AddNeighbor(GVertex n, Edge e)
            {
                if (this == n)
                    throw new Exception($"No self loops, (Id: {n.Id}).");

                if (_neighbors.Contains(n))
                    return false;

                _neighbors.Add(n);
                this._edges.Add(e);
                return true;
            }

            public bool RemoveNeighbor(GVertex n, Edge e)
            {
                _neighbors.Remove(n);
                return _edges.Remove(e);
            }
        }

        private Dictionary<String, GVertex> _vertices = new Dictionary<string, GVertex>();
        private Dictionary<String, Edge> _edges = new Dictionary<string, Edge>();
        #endregion

        #region ACCESSORS

        public Vertex this[String id]
        {
            get
            {
                if (!_vertices.ContainsKey(id))
                    throw new Exception($"Vertex id: {id} not found.");

                return _vertices[id];
            }
        }

        public Vertex this[int id] => this[id.ToString()];
        public IEnumerable<Vertex> Vertices => _vertices.Values;
        public IEnumerable<Vertex> PositiveDegreeVertices => Vertices.Where(v => v.Degree > 0);
        public IEnumerable<Edge> Edges => _edges.Values;
        #endregion

        #region GRAPH_CREATION_METHODS

        public static Graph CreateGraphFromEdges(IEnumerable<Tuple<String, String>> edges)
        {
            Graph graph = new Graph();
            foreach (var edge in edges)
                graph.AddEdge(edge.Item1, edge.Item2);
            return graph;
        }

        public static Graph ParseGraphFromEdgeFile(String fullFilePath)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GRAPH_ALTERATION_METHODS

        // returning a bool, but not sure why
        public bool AddVertex(String id)
        {
            // For a vertex, throw an exception if it exists already, because it may have edges and 
            // just ignoring the request to add it could mislead
            if (_vertices.ContainsKey(id))
                throw new Exception($"Cannot add new vertex with id {id}, vertex exists already.");
            _vertices[id] = new GVertex(id);
            return true;
        }
        // Wrapper to allow vertices to be added as ints, in a loop for example
        public bool AddVertex(int id) => AddVertex(id.ToString());

        // Here just return false if it isn't found, no exception
        public bool RemoveVertex(String id)
        {
            if (!_vertices.ContainsKey(id))
                return false;

            var vertex = _vertices[id];
            var edges = vertex.Edges.ToList();
            edges.ForEach(e => _edges.Remove(e.ToString()));
            // Use the edges to remove the neighbors, faster than using Neighbors collection and searching for edges
            foreach (var e in edges)
            {
                var neighbor = (GVertex)(e.v1 == vertex ? e.v2 : e.v1);
                neighbor.RemoveNeighbor(vertex, e);
                vertex.RemoveNeighbor(neighbor, e);
                _edges.Remove(e.ToString());
            }

            _vertices.Remove(id);
            return true;
        }
        public bool RemoveVertex(int id) => RemoveVertex(id.ToString());

        // For AddEdge we will automatically add the Vertex(ices) if it/they aren't there, design
        // decision, simpler this way.
        private bool AddEdge(GVertex v1, GVertex v2)
        {
            if (v1.IsAdjacentTo(v2))
                return false;

            var edge = new Edge(v1, v2);
            v1.AddNeighbor(v2, edge);
            v2.AddNeighbor(v1, edge);
            _edges.Add(edge.ToString(), edge);
            return true;
        }
        public bool AddEdge(String id1, String id2)
        {
            if (!_vertices.ContainsKey(id1))
                _vertices[id1] = new GVertex(id1);
            if (!_vertices.ContainsKey(id2))
                _vertices[id2] = new GVertex(id2);

            return AddEdge(_vertices[id1], _vertices[id2]);
        }
        public bool AddEdge(int v1, int v2) => AddEdge(v1.ToString(), v2.ToString());
        
        // Here we will throw an Exception if either *Vertex* isn't present, but 
        // just return false if they aren't neighbors
        public bool RemoveEdge(String v1, String v2)
        {
            if (!(_vertices.ContainsKey(v1) && _vertices.ContainsKey(v2)))
                throw new Exception($"Trying to remove edge ({v1}, {v2}), Vertex not found.");
            var vertex1 = _vertices[v1];
            var vertex2 = _vertices[v2];
            if (!vertex1.IsAdjacentTo(vertex2))
                return false;

            var edge = _edges[Edge.EdgeAsString(vertex1, vertex2)];
            vertex1.RemoveNeighbor(vertex2, edge);
            vertex2.RemoveNeighbor(vertex1, edge);

            _edges.Remove(edge.ToString());

            return true;
        }
        public bool RemoveEdge(int v1, int v2) => RemoveEdge(v1.ToString(), v2.ToString());

        #endregion

        #region GRAPH_STATS
        #endregion

        #region FACTORY_METHODS
        
        // Very hard to test this method, have to just walk through it and see if it looks good
        public void AddNewErVertex(String id, double p, Random random = null)
        {
            if (random == null)
                random = TSRandom.NextRandom();
            var vertex = _vertices[id] = new GVertex(id);

            foreach (var neighbor in _vertices.Values)
            {
                if (neighbor == vertex)
                    continue;
                if (random.NextDouble() < p)
                    AddEdge(vertex, neighbor);
            }
        }

        public static Graph NewErGraph(int n, double p)
        {
            Random rand = TSRandom.NextRandom();
            Graph graph = new Graph();
            for (int i = 1; i <= n; i++)
            {
                graph.AddNewErVertex(i.ToString(), p, rand);
            }

            return graph;
        }

        public void AddNewBaVertex(String id, int m, Random random = null, double alpha = 1.0)
        {
            if (random == null)
                random = TSRandom.NextRandom();

            var selectedNeighbors = _vertices.Values.ChooseBiasedSubset(m, v => Math.Pow(v.Degree, alpha));
            var vertex = _vertices[id] = new GVertex(id);
            foreach (var neighbor in selectedNeighbors)
                AddEdge(vertex, neighbor);
        }

        public static Graph NewBaGraph(int n, int m, double alpha = 1.0)
        {
            Random random = TSRandom.NextRandom();
            Graph graph = new Graph();
            int i;
            for (i = 1; i <= m; i++)
                graph.AddVertex(i.ToString());

            var existingVertices = graph._vertices.Values.ToList();
            var nextVertex = graph._vertices[i.ToString()] = new GVertex(i.ToString());
            foreach (var graphVertex in existingVertices)
                graph.AddEdge(nextVertex, graphVertex);
            
            for (i = m + 2; i <= n; i++)
                graph.AddNewBaVertex(i.ToString(), m, random, alpha);
            return graph;
        }

        public static Graph Clone(Graph g)
        {
            Graph graph = new Graph();
            foreach (var vertex in g.Vertices)
                graph.AddVertex(vertex.Id);
            foreach (var edge in g.Edges)
                graph.AddEdge(edge.v1.Id, edge.v2.Id);
            return graph;
        }

        public Graph Clone() => Clone(this);

        #endregion


    }
}
