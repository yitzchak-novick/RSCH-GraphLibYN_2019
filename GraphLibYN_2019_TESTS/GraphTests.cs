using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphLibYN_2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibYN_2019_TESTS
{
    [TestClass]
    public class GraphTests
    {
        #region GRAPH_CREATION_METHODS
        [TestMethod]
        public void CreateGraphFromEdgesCreatesCorrectStructure()
        {
            List<Tuple<String, String>> edges = new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("1", "2"),
                    new Tuple<string, string>("2", "3"),
                    new Tuple<string, string>("3", "1"),
                    new Tuple<string, string>("3", "4")
                }
                ;
            Graph graph = Graph.CreateGraphFromEdges(edges);
            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["3"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["3"]));
            Assert.IsTrue(graph["4"].IsAdjacentTo(graph["3"]));
            Assert.IsFalse(graph["1"].IsAdjacentTo(graph["4"]));
            Assert.IsFalse(graph["4"].IsAdjacentTo(graph["2"]));
        }

        [TestMethod]
        public void ParseGraphFromEdgeFileCreatesCorrectStructure()
        {
            var fileName = "testgraph.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);
            StringBuilder fileContents = new StringBuilder();
            fileContents.AppendLine("1\t2");
            fileContents.AppendLine("2\t3");
            fileContents.AppendLine("3\t1");
            fileContents.AppendLine("3\t4");
            File.WriteAllText(fileName, fileContents.ToString());

            Graph graph = Graph.ParseGraphFromEdgeFile(fileName);

            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["3"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["3"]));
            Assert.IsTrue(graph["4"].IsAdjacentTo(graph["3"]));
            Assert.IsFalse(graph["1"].IsAdjacentTo(graph["4"]));
            Assert.IsFalse(graph["4"].IsAdjacentTo(graph["2"]));
            File.Delete(fileName);
        }

        [TestMethod]
        public void ParseGraphFromEdgeFileIgnoresCommentsAndEmptyLines()
        {
            var fileName = "testgraph.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);
            StringBuilder fileContents = new StringBuilder();
            fileContents.AppendLine();
            fileContents.AppendLine("1\t2");
            fileContents.AppendLine("%4\t1");
            fileContents.AppendLine("2\t3");
            fileContents.AppendLine("3\t1");
            fileContents.AppendLine("#4\t2");
            fileContents.AppendLine("3\t4");
            File.WriteAllText(fileName, fileContents.ToString());

            Graph graph = Graph.ParseGraphFromEdgeFile(fileName);

            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["3"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["3"]));
            Assert.IsTrue(graph["4"].IsAdjacentTo(graph["3"]));
            Assert.IsFalse(graph["1"].IsAdjacentTo(graph["4"]));
            Assert.IsFalse(graph["4"].IsAdjacentTo(graph["2"]));
            File.Delete(fileName);
        }
        #endregion

        #region ACCESSORS
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GraphIndexingThrowsExceptionWhenVertexIsntPresent()
        {
            Graph graph = new Graph();
            graph.AddEdge("2", "1");
            graph.AddVertex("4");
            var v = graph["3"];
        }

        [TestMethod]
        public void GraphIndexingGivesVertexWhenPresent()
        {
            Graph graph = new Graph();
            graph.AddEdge("2", "1");
            graph.AddVertex("4");
            Assert.AreEqual(graph["1"].Id, "1");
            Assert.AreEqual(graph["2"].Id, "2");
            Assert.AreEqual(graph["4"].Id, "4");
        }
        #endregion
    }
}
