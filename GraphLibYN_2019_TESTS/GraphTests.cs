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

        // Some graphs to use for testing (see TestGraphs.docx in the project directory)
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
        [Ignore]
        public void CreateGraphFromEdgesCreatesCorrectStructure()
        {
            Assert.Fail("Have to rewrite this test, postponing for now");
            /*
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
            */
        }

        [TestMethod]
        [Ignore]
        public void ParseGraphFromEdgeFileCreatesCorrectStructure()
        {
            Assert.Fail("Have to rewrite this test, postponing for now");
            /*
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
            */
        }


        [TestMethod]
        [Ignore]
        public void ParseGraphFromEdgeFileIgnoresCommentsAndEmptyLines()
        {
            Assert.Fail("Have to rewrite this test, postponing for now");
            /*
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
            */
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
        [ExpectedException(typeof(Exception))]
        public void AddingExistingVertexIdAgainThrowsException()
        {
            TriangleWithLeg.AddVertex("2");
        }

        [TestMethod]
        public void RemoveVertexRemovesTheVertex()
        {
            RemoveExistingVerticesInTestGraphs();
            Assert.IsFalse(TriangleWithLeg.Vertices.Any(v => v.Id == "3"));
            Assert.IsFalse(StarWithFiveSpokes.Vertices.Any(v => v.Id == "6"));
            Assert.IsFalse(TwoConnectedSquares.Vertices.Any(v => v.Id == "5"));
        }

        [TestMethod]
        public void RemoveVertexRemovesTheEdges()
        {
            Assert.AreEqual(4, TriangleWithLeg.Edges.Count());
            TriangleWithLeg.RemoveVertex("2");
            Assert.AreEqual(2, TriangleWithLeg.Edges.Count());
            Assert.AreEqual(3, TwoConnectedSquares["4"].Edges.Count());
            Assert.AreEqual(2, TwoConnectedSquares["6"].Edges.Count());
            TwoConnectedSquares.RemoveVertex("5");
            Assert.AreEqual(6, TwoConnectedSquares.Edges.Count());
            Assert.AreEqual(2, TwoConnectedSquares["4"].Edges.Count());
            Assert.AreEqual(1, TwoConnectedSquares["6"].Edges.Count());
        }

        [TestMethod]
        public void RemoveVertexReturnsFalseIfVertexDoesNotExist()
        {
            Graph graph = new Graph();
            graph.AddEdge("1", "2");
            Assert.IsFalse(graph.RemoveVertex("3"));
        }

        [TestMethod]
        public void AddEdgeResultsInAdjacentVertices()
        {
            Graph graph = new Graph();
            graph.AddVertex("1");
            graph.AddVertex("2");
            graph.AddEdge("3", "4");
            graph.AddEdge("1", "2");
            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["3"].IsAdjacentTo(graph["4"]));
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

        [TestMethod]
        public void AddEdgeSecondTimeInOtherOrderDoesntAddAgain()
        {
            Graph graph = new Graph();
            graph.AddVertex("2");
            graph.AddVertex("1");

            graph.AddEdge("1", "2");
            Assert.AreEqual(1, graph.Edges.Count());
            graph.AddEdge("2", "1");
            Assert.AreEqual(1, graph.Edges.Count());

            // Again with new vertices
            graph.AddEdge("3", "4");
            Assert.AreEqual(2, graph.Edges.Count());
            Assert.IsFalse(graph.AddEdge("4", "3")); // check that the call returns false
            Assert.AreEqual(2, graph.Edges.Count()); // check that the edge wasn't added again
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RemoveEdgeThrowsExceptionIfVertexIsNotPresent()
        {
            TriangleWithLeg.RemoveEdge("5", "2");
        }

        [TestMethod]
        public void RemoveEdgeReturnsFalseIfEdgeDoesNotExist()
        {
            Assert.IsFalse(TriangleWithLeg.RemoveEdge("1", "4"));
        }

        [TestMethod]
        public void RemoveEdgeRemovesFromCollection()
        {
            Graph graph = new Graph();
            graph.AddEdge("1", "2");
            graph.AddEdge("3", "4");
            Assert.AreEqual(2, graph.Edges.Count());
            graph.RemoveEdge("1", "2");
            graph.RemoveEdge("4", "3");
            Assert.AreEqual(0, graph.Edges.Count());
        }

        [TestMethod]
        public void VerticesArentAdjacentAfterEdgeIsRemoved()
        {
            Graph graph = new Graph();
            graph.AddEdge("1", "2");
            graph.AddEdge("3", "4");
            Assert.IsTrue(graph["1"].IsAdjacentTo(graph["2"]));
            Assert.IsTrue(graph["4"].IsAdjacentTo(graph["3"]));
            graph.RemoveEdge("1", "2");
            graph.RemoveEdge("4", "3");
            Assert.IsFalse(graph["1"].IsAdjacentTo(graph["2"]));
            Assert.IsFalse(graph["4"].IsAdjacentTo(graph["3"]));
        }

        [TestMethod]
        public void DegreesAreCorrectInCreatedGraphs()
        {
            Assert.AreEqual(3, TriangleWithLeg["3"].Degree);
            Assert.AreEqual(1, TriangleWithLeg["4"].Degree);
            Assert.AreEqual(5, StarWithFiveSpokes["1"].Degree);
            Assert.AreEqual(1, StarWithFiveSpokes["3"].Degree);
            Assert.AreEqual(3, TwoConnectedSquares["5"].Degree);
            Assert.AreEqual(2, TwoConnectedSquares["6"].Degree);
        }

        [TestMethod]
        public void DegreesAreCorrectAfterAddingVertices()
        {
            AttachNewVerticesToTestGraphs();
            Assert.AreEqual(1, TriangleWithLeg["5"].Degree);
            Assert.AreEqual(6, StarWithFiveSpokes["1"].Degree);
            Assert.AreEqual(2, TwoConnectedSquares["2"].Degree);
        }

        [TestMethod]
        public void DegreesAreCorrectAfterRemovingVertices()
        {
            RemoveExistingVerticesInTestGraphs();
            Assert.AreEqual(0, TriangleWithLeg["4"].Degree);
            Assert.AreEqual(1, TriangleWithLeg["1"].Degree);

        }

        [TestMethod]
        public void DegreesAreCorrectAfterAddingEdgesBetweenExistingVertices()
        {
            AddEdgesBetweenExistingVerticesInTestGraphs();
            Assert.AreEqual(2, TriangleWithLeg["4"].Degree);
            Assert.AreEqual(3, TriangleWithLeg["1"].Degree);
            Assert.AreEqual(3, TriangleWithLeg["3"].Degree);
        }

        [TestMethod]
        public void NeighborsCollectionIsCorrectInCreatedGraphs()
        {
            Assert.AreEqual("1,2,4",
                String.Join(",", TriangleWithLeg["3"].Neighbors.OrderBy(v => v.Id).Select(v => v.Id)));
            Assert.AreEqual("1,3,5",
                String.Join(",", TwoConnectedSquares["4"].Neighbors.OrderBy(v => v.Id).Select(v => v.Id)));
        }

        [TestMethod]
        public void NeighborsCollectionIsCorrectAfterAddingEdges()
        {
            AddEdgesBetweenExistingVerticesInTestGraphs();
            Assert.AreEqual("2,3,4",
                String.Join(",", TriangleWithLeg["1"].Neighbors.OrderBy(v => v.Id).Select(v => v.Id)));
            Assert.AreEqual("3,4,5,7",
                String.Join(",", TwoConnectedSquares["6"].Neighbors.OrderBy(v => v.Id).Select(v => v.Id)));
        }

        [TestMethod]
        public void NeighborsCollectionIsCorrectAfterAddingAndRemovingEdges()
        {
            AddAndRemoveEdgesBetweenExistingVerticesInTestGraphs();
            Assert.AreEqual("3,4,5,6",
                String.Join(",", StarWithFiveSpokes["1"].Neighbors.OrderBy(v => v.Id).Select(v => v.Id)));
            Assert.AreEqual("2,4",
                String.Join(",", TwoConnectedSquares["3"].Neighbors.OrderBy(v => v.Id).Select(v => v.Id)));
        }

        [TestMethod]
        public void EdgeCollectionInVerticesIsCorrectInCreatedGraphs()
        {
            Assert.IsTrue(TriangleWithLeg["3"].Edges.Any(e => e.v1.Id == "1" && e.v2.Id == "3"));
            Assert.IsTrue(TriangleWithLeg["3"].Edges.Any(e => e.v1.Id == "2" && e.v2.Id == "3"));
            Assert.IsTrue(TriangleWithLeg["3"].Edges.Any(e => e.v1.Id == "3" && e.v2.Id == "4"));
            Assert.IsTrue(TriangleWithLeg["4"].Edges.Any(e => e.v1.Id == "3" && e.v2.Id == "4"));
            Assert.IsFalse(TriangleWithLeg["4"].Edges.Any(e => e.v1.Id == "1" && e.v2.Id == "4"));
        }

        [TestMethod]
        public void EdgeCollectionInVerticesIsCorrectAfterAddingAndRemovingEdges()
        {
            AddAndRemoveEdgesBetweenExistingVerticesInTestGraphs();
            Assert.IsTrue(StarWithFiveSpokes["1"].Edges.Any(e => e.v1.Id == "1" && e.v2.Id == "3"));
            Assert.IsTrue(StarWithFiveSpokes["1"].Edges.Any(e => e.v1.Id == "1" && e.v2.Id == "6"));
            Assert.IsTrue(StarWithFiveSpokes["3"].Edges.Any(e => e.v1.Id == "2" && e.v2.Id == "3"));
            Assert.IsFalse(StarWithFiveSpokes["2"].Edges.Any(e => e.v1.Id == "1" && e.v2.Id == "2"));
        }

        /*
         * TODO: Degree after add and remove vertices
         * TODO: Degree after adding edge
         * TODO: Degree after removing edge
         * TODO: Degree after add and remove edges
         * TODO: Neighbors are adjacent after adding a new edge
         * TODO: Neighbors aren't adjacent after removing an edge
         */

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
            Assert.AreEqual("1", graph["1"].Id);
            Assert.AreEqual("2", graph["2"].Id);
            Assert.AreEqual("4", graph["4"].Id);
        }

        [TestMethod]
        public void VerticesWorksOnCreatedGraph()
        {
            var AllVerticesList = TriangleWithLeg.Vertices.ToList();
            Assert.AreEqual("1,2,3,4", String.Join(",", AllVerticesList.OrderBy(v => v.Id).Select(v => v.Id)));

            AllVerticesList = StarWithFiveSpokes.Vertices.ToList();
            Assert.AreEqual("1,2,3,4,5,6", String.Join(",", AllVerticesList.OrderBy(v => v.Id).Select(v => v.Id)));

            AllVerticesList = TwoConnectedSquares.Vertices.ToList();
            Assert.AreEqual("1,2,3,4,5,6,7,8", String.Join(",", AllVerticesList.OrderBy(v => v.Id).Select(v => v.Id)));

        }


        [TestMethod]
        public void PositiveDegreeVerticesWorksOnCreatedGraph()
        {
            var AllVerticesList = TriangleWithLeg.PositiveDegreeVertices.ToList();
            Assert.AreEqual("1,2,3,4", String.Join(",", AllVerticesList.OrderBy(v => v.Id).Select(v => v.Id)));

            AllVerticesList = StarWithFiveSpokes.PositiveDegreeVertices.ToList();
            Assert.AreEqual("1,2,3,4,5,6", String.Join(",", AllVerticesList.OrderBy(v => v.Id).Select(v => v.Id)));

            AllVerticesList = TwoConnectedSquares.PositiveDegreeVertices.ToList();
            Assert.AreEqual("1,2,3,4,5,6,7,8", String.Join(",", AllVerticesList.OrderBy(v => v.Id).Select(v => v.Id)));

        }

        #endregion
    }
}
