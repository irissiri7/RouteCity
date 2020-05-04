using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ClassLibrary
{
    class Path : IComparable<Path>
    {
        //PROPERTIES
        internal Node Node { get; set; }
        internal double QuickestTimeFromStart { get; set; }
        internal List<string> NodesVisited { get; set; }

        //CONSTRUCTOR
        public Path(Node node, double shortestTimeFromStart = double.PositiveInfinity)
        {
            Node = node;
            QuickestTimeFromStart = shortestTimeFromStart;
            NodesVisited = new List<string>();
            NodesVisited.Add(node.Name);
        }

        public int CompareTo(Path path)
        {
            return this.QuickestTimeFromStart.CompareTo(path.QuickestTimeFromStart);
        }

    }
}
