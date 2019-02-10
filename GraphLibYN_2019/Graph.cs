using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibYN_2019
{
    public class Graph
    {
        #region ACCESSORS
        public Vertex this[String Id]
        {
            // have to decide, return null if not found, or throw exception? Think throw exception, 
            // no real reason why this call would be used in a way where failure is an option..
            get { return null; }
        }

        public IEnumerable<Vertex> AllVertices => null;

        public IEnumerable<Vertex> AllPositiveDegreeVertices => null;

        public IEnumerable<Edge> Edges => null;
        #endregion

        #region GRAPH_CREATION_METHODS

        public static Graph CreateGraphFromEdges(IEnumerable<Tuple<String, String>> edges)
        {
            throw new NotImplementedException();
        }

        public static Graph ParseGraphFromEdgeFile(String fullFilePath)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GRAPH_ALTERATION_METHODS

        public bool AddVertex(String id)
        {
            return false;
        }
        // Wrapper to allow vertices to be added as ints, in a loop for example
        public bool AddVertex(int id) => AddVertex(id.ToString());

        public bool RemoveVertex(String id)
        {
            return false;
        }
        public bool RemoveVertex(int id) => RemoveVertex(id.ToString());

        public bool AddEdge(String v1, String v2)
        {
            return false;
        }
        public bool AddEdge(int v1, int v2) => AddEdge(v1.ToString(), v2.ToString());

        public bool RemoveEdge(String v1, String v2)
        {
            return false;
        }
        public bool RemoveEdge(int v1, int v2) => RemoveEdge(v1.ToString(), v2.ToString());

        #endregion

        #region GRAPH_STATS
        #endregion

        #region FACTORY_METHODS

        public void AddNewErVertex(int p)
        {
            throw new NotImplementedException();
        }

        public static Graph NewErGraph(int n, double p)
        {
            throw new NotImplementedException();
        }

        public void AddNewBaVertex(int m, double alpha = 1.0)
        {
            throw new NotImplementedException();
        }

        public static Graph NewBaGraph(int n, int m, double alpha = 1.0)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
