using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibYN_2019
{
    public class Vertex
    {
        private readonly string _id;
        public string Id => _id;

        private int _degree = Int32.MinValue;
        public int Degree => -1;
        public int ExcDegree => Degree - 1;

        public bool IsAdjacentTo(Vertex v) => false;
        public bool IsAdjacentTo(String vId) => false;

        private double _fi = double.NegativeInfinity;
        public double Fi => -1.0;

        public double LnFi => Math.Log(Fi);

        private double _pla = double.NegativeInfinity;
        public double Pla => -1.0;

        private double _tla = double.NegativeInfinity;
        public double Tla => -1.0;
    }
}
