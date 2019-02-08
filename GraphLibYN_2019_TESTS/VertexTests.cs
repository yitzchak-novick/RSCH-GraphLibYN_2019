using System;
using System.Text;
using System.Collections.Generic;
using GraphLibYN_2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibYN_2019_TESTS
{
    // Most vertex functionality will be tested directly on the graph, but some things
    // are purely functions of the vertex and should be tested here
    [TestClass]
    public class VertexTests
    {
        [TestMethod]
        public void VertexIsAdjacentToMethodWorks()
        {
            Graph graph = new Graph();
            graph.AddEdge("1", "2");
            graph.AddEdge("2", "3");
            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["3"].IsAdjacentTo(graph["2"]));
            Assert.IsFalse(graph["3"].IsAdjacentTo(graph["1"]));
        }
    }
}
