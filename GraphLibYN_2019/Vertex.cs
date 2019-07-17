using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibYN_2019
{
    public class Vertex
    {
        public string Id { get; }

        public Vertex(String Id)
        {
            if (String.IsNullOrWhiteSpace(Id))
                throw new Exception("Cannot create Vertex with empty Id.");
            this.Id = Id;
        }

        protected HashSet<Vertex> _neighbors = new HashSet<Vertex>();
        public IEnumerable<Vertex> Neighbors => _neighbors;

        public int Degree => _neighbors.Count;
        public int ExcessDegree => Degree - 1;

        public bool IsAdjacentTo(Vertex v) => _neighbors.Contains(v);

        protected HashSet<Edge> _edges = new HashSet<Edge>();
        public IEnumerable<Edge> Edges => _edges;
        /*
        private double _fi = double.NegativeInfinity;
        public double Fi => -1.0;

        public double LnFi => Math.Log(Fi);

        private double _pla = double.NegativeInfinity;
        public double Pla => -1.0;

        private double _tla = double.NegativeInfinity;
        public double Tla => -1.0;
        */
    }
}
