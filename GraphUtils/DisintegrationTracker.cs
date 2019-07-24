using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphUtils
{
    // The point of this class is to keep track of the disintegration of a network.
    // It will track the costs and results associated with removing edges from a graph.


    // YN 7/23/19 Keeping the code for now, but decided against this class
    public class DisintegrationTracker
    {
        private List<int> TotalInoculationCostKeys;
        private List<int> TotalInterviewsCostKeys;
        private List<int> TotalVerticesInterviewedCostKeys;
        private List<IEnumerable<Tuple<string, string>>> EdgesRemoved;
        private int[] MaxComponentSizes;

        private int currIndex = 0;

        public DisintegrationTracker(int initialCapacity)
        {
            TotalInoculationCostKeys = new List<int>(initialCapacity);
            TotalInterviewsCostKeys = new List<int>(initialCapacity);
            TotalVerticesInterviewedCostKeys = new List<int>(initialCapacity);
            EdgesRemoved = new List<IEnumerable<Tuple<string, string>>>(initialCapacity);
        }

        public DisintegrationTracker()
        {
            TotalInoculationCostKeys = new List<int>();
            TotalInterviewsCostKeys = new List<int>();
            TotalVerticesInterviewedCostKeys = new List<int>();
            EdgesRemoved = new List<IEnumerable<Tuple<string, string>>>();
        }

        public enum COSTTYPE
        {
            TotalInoculations,
            TotalInterviews,
            TotalVerticesInterviewed
        }

        // The parameters take in the TOTAL costs for each step, not sure if this is better or if it'd be
        // better to take in the additional cost since the last step. Leaving it this way for now, can
        // revisit.. (YN 7/16/19)
        public void AddStep(int inoculations, int totalInterviews, int verticesInterviewed,
            IEnumerable<Tuple<String, String>> edgesRemoved)
        {
            if (MaxComponentSizes != null)
                throw new Exception("Cannot add steps after results are calculated");

            TotalInoculationCostKeys[currIndex] = inoculations;
            TotalInterviewsCostKeys[currIndex] = totalInterviews;
            TotalVerticesInterviewedCostKeys[currIndex] = verticesInterviewed;
            EdgesRemoved[currIndex] = edgesRemoved.ToList();
            currIndex++;
        }

        public void AddFinalRemainingEdges(IEnumerable<Tuple<String, String>> edges)
        {

        }

        private void CreateMaxComponentSizes()
        {
            throw new NotImplementedException();
            /*
            if (MaxComponentSizes != null)
                return;

            MaxComponentSizes = new int[currIndex];
            MaxComponentSizes[currIndex - 1] = 1; // only works if 
            UnionFind<String> unionFind = new UnionFind<String>();

            for (int i = currIndex - 1; i >= 0; i++)
            {
                foreach (var edge in EdgesRemoved[i])
                    unionFind.Union(edge.Item1, edge.Item2, true);

                MaxComponentSizes[i] = unionFind.GetMaxSetCount();
            }
            */
        }

        public SortedList<double, double> GetDisintegrationResults(Func<int, int, int> minMaxToNumBinsFunc,
            Func<int, int, int, double> inocsIntrvVrtxintrvToCostFunc)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<int, int> GetMinKeyValuePair(COSTTYPE costtype)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<int, int> GetMaxKeyValuePair(COSTTYPE costtype)
        {
            throw new NotImplementedException();
        }
    }
}
