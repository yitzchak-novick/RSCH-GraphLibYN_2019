using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibYN_2019
{
    public class Edge
    {
        public readonly Vertex v1;
        public readonly Vertex v2;

        // Always make v1 the one with the earlier Id
        public Edge(Vertex v1, Vertex v2)
        {
            this.v1 = v1.Id.CompareTo(v2.Id) < 0 ? v1 : v2;
            this.v2 = this.v1 == v1 ? v2 : v1;
        }

        public override string ToString()
        {
            return EdgeAsString(v1, v2);
        }

        public static String EdgeAsString(String id1, String id2)
        {
            return "(" + (id1.CompareTo(id2) < 0 ? id1 : id2) + ", " + (id1.CompareTo(id2) < 0 ? id2 : id1) + ")";
        }

        public static String EdgeAsString(Vertex v1, Vertex v2) => EdgeAsString(v1.Id, v2.Id);
    }
}
