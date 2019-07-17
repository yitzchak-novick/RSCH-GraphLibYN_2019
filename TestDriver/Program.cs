using GraphLibYN_2019;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDriver
{
    // For areas where unit testing is difficult for whatever reason, can use this
    // for some quick and dirty testing on the fly
    class Program
    {
        static void Main(string[] args)
        {
            SomeBaCreation();
        }

        static void SomeBaCreation()
        {
            Graph graph = Graph.NewBaGraph(1000, 3, 2);
            Console.WriteLine(String.Join("\n", graph.Vertices.GroupBy(v => v.Degree).OrderByDescending(g => g.Key).Select(g => $"{g.Key}: {g.Count()}")));
            Console.ReadKey();
        }

        static void WalkThroughErGraphCreation()
        {
            foreach (var p in new[] { 0.01, 0.25, 0.5, 0.75, 0.99})
            {
                List<int> edgeCounts = new List<int>();
                for (int i = 0; i < 150; i++)
                {
                    edgeCounts.Add(Graph.NewErGraph(500, p).Edges.Count());
                }

                Console.WriteLine(p + ": " + edgeCounts.Average());
            }
            Console.ReadKey();
        }
    }
}
