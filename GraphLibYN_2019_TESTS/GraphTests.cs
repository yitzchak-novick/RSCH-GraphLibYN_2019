using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GraphLibYN_2019;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibYN_2019_TESTS
{
    [TestClass]
    public class GraphTests
    {

        #region TEST_GRAPHS

        // Some graphs to use for testing
        private Graph TriangleWithLeg;
        private Graph StarWithFiveSpokes;
        private Graph TwoConnectedSquares;

        [TestInitialize]
        public void Setup()
        {
            TriangleWithLeg = Graph.CreateGraphFromEdges(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("1", "2"),
                new Tuple<string, string>("2", "3"),
                new Tuple<string, string>("3", "1"),
                new Tuple<string, string>("3", "4")
            });

            StarWithFiveSpokes = Graph.CreateGraphFromEdges(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("1", "2"),
                new Tuple<string, string>("1", "3"),
                new Tuple<string, string>("1", "4"),
                new Tuple<string, string>("1", "5"),
                new Tuple<string, string>("1", "6")
            });

            TwoConnectedSquares = Graph.CreateGraphFromEdges(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("1", "2"),
                new Tuple<string, string>("2", "3"),
                new Tuple<string, string>("3", "4"),
                new Tuple<string, string>("4", "1"),
                new Tuple<string, string>("4", "5"),
                new Tuple<string, string>("5", "6"),
                new Tuple<string, string>("6", "7"),
                new Tuple<string, string>("7", "8"),
                new Tuple<string, string>("8", "5"),
            });
        }

        // Methods that will alter the test graphs
        private void AttachNewVerticesToTestGraphs()
        {
            TriangleWithLeg.AddEdge("4", "5");
            StarWithFiveSpokes.AddEdge("7", "1");
            TwoConnectedSquares.AddEdge("8", "9");
            TwoConnectedSquares.AddEdge("5", "10");
        }

        private void RemoveExistingVerticesInTestGraphs()
        {
            TriangleWithLeg.RemoveVertex("3");
            StarWithFiveSpokes.RemoveVertex("6");
            TwoConnectedSquares.RemoveVertex("5");
            TwoConnectedSquares.RemoveVertex("2");
        }

        private void AddAndRemoveVerticesInTestGraphs()
        {
            TriangleWithLeg.RemoveVertex("4");
            TriangleWithLeg.AddEdge("5", "1");
            StarWithFiveSpokes.AddEdge("7", "1");
            StarWithFiveSpokes.RemoveVertex("6");
            TwoConnectedSquares.AddEdge("8", "9");
            TwoConnectedSquares.RemoveVertex("3");
            TwoConnectedSquares.AddEdge("9", "10");
            TwoConnectedSquares.RemoveVertex("6");
        }

        private void AddEdgesBetweenExistingVerticesInTestGraphs()
        {
            TriangleWithLeg.AddEdge("1", "4");
            StarWithFiveSpokes.AddEdge("2", "3");
            TwoConnectedSquares.AddEdge("3", "6");
            TwoConnectedSquares.AddEdge("2", "7");
            TwoConnectedSquares.AddEdge("4", "6");
        }

        private void RemoveEdgesBetweenExistingVerticesInTestGraphs()
        {
            TriangleWithLeg.RemoveEdge("1", "2");
            StarWithFiveSpokes.RemoveEdge("1", "2");
            TwoConnectedSquares.RemoveEdge("2", "1");
            TwoConnectedSquares.RemoveEdge("5", "6");
            TwoConnectedSquares.RemoveEdge("7", "8"); 
        }

        private void AddAndRemoveEdgesBetweenExistingVerticesInTestGraphs()
        {
            TriangleWithLeg.RemoveEdge("1", "2");
            TriangleWithLeg.AddEdge("2", "4");
            StarWithFiveSpokes.RemoveEdge("1", "2");
            StarWithFiveSpokes.AddEdge("2", "3");
            TwoConnectedSquares.AddEdge("2", "6");
            TwoConnectedSquares.AddEdge("4", "8");
            TwoConnectedSquares.RemoveEdge("1", "2");
            TwoConnectedSquares.RemoveEdge("5", "8");
        }
        #endregion

        #region GRAPH_CREATION_METHODS

        [TestMethod]
        public void CreateGraphFromEdgesCreatesCorrectStructure()
        {
            List<Tuple<String, String>> edges = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("1", "2"),
                new Tuple<string, string>("2", "3"),
                new Tuple<string, string>("3", "1"),
                new Tuple<string, string>("3", "4")
            };
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

        #region GRAPH_ALTERATION_METHODS

        [TestMethod]
        public void AddVertexAddsAZeroDegreeVertex()
        {
            Graph graph = new Graph();
            graph.AddVertex("1");
            Assert.AreEqual(0, graph["1"].Degree);

            graph = new Graph();
            graph.AddEdge("1", "2");
            graph.AddVertex("3");
            Assert.AreEqual(0, graph["3"].Degree);
        }

        [TestMethod]
        public void AddEdgeResultsInAdjacentVertices()
        {
            Graph graph = new Graph();
            graph.AddVertex("1");
            graph.AddVertex("2");
            graph.AddEdge("3", "4");
            graph.AddEdge("1", "2");
            Assert.IsTrue(graph["1"].IsAdjacentTo("2"));
            Assert.IsTrue(graph["3"].IsAdjacentTo("4"));
        }

        [TestMethod]
        public void AddEdgeResultsInEdgesInGraph()
        {
            Graph graph = new Graph();
            graph.AddVertex("1");
            graph.AddVertex("2");
            graph.AddEdge("3", "4");
            graph.AddEdge("1", "2");
            Assert.IsTrue(graph.Edges.Any(e => e.v1.Id == "1" && e.v2.Id == "2"));
            Assert.IsTrue(graph.Edges.Any(e => e.v1.Id == "3" && e.v2.Id == "4"));
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

        [TestMethod]
        public void AllVerticesWorksOnCreatedGraph()
        {
            var AllVerticesList = TriangleWithLeg.AllVertices.ToList();
            Assert.AreEqual("1,2,3,4", String.Join(",", AllVerticesList.OrderBy(v => v.Id)));

            AllVerticesList = StarWithFiveSpokes.AllVertices.ToList();
            Assert.AreEqual("1,2,3,4,5,6", String.Join(",", AllVerticesList.OrderBy(v => v.Id)));

            AllVerticesList = TwoConnectedSquares.AllVertices.ToList();
            Assert.AreEqual("1,2,3,4,5,6,7,8", String.Join(",", AllVerticesList.OrderBy(v => v.Id)));

        }


        [TestMethod]
        public void AllPositiveDegreeVerticesWorksOnCreatedGraph()
        {
            var AllVerticesList = TriangleWithLeg.AllPositiveDegreeVertices.ToList();
            Assert.AreEqual("1,2,3,4", String.Join(",", AllVerticesList.OrderBy(v => v.Id)));

            AllVerticesList = StarWithFiveSpokes.AllPositiveDegreeVertices.ToList();
            Assert.AreEqual("1,2,3,4,5,6", String.Join(",", AllVerticesList.OrderBy(v => v.Id)));

            AllVerticesList = TwoConnectedSquares.AllPositiveDegreeVertices.ToList();
            Assert.AreEqual("1,2,3,4,5,6,7,8", String.Join(",", AllVerticesList.OrderBy(v => v.Id)));

        }
        #endregion
    }
}
