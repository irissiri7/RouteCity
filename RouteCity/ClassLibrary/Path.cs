using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ClassLibrary
{
    public class Path : IComparable<Path>
    {
        //PROPERTIES
        internal Node Node { get; set; }
        internal double QuickestTimeFromStart { get; set; }
        public List<string> NodesVisited { get; set; }

        //CONSTRUCTOR
        public Path(Node node, double shortestTimeFromStart = double.PositiveInfinity, List<string> nodesVisited = null)
        {
            Node = node;
            QuickestTimeFromStart = shortestTimeFromStart;
            NodesVisited = new List<string>();
            NodesVisited.Add(node.Name);
            if(nodesVisited != null)
            {
                NodesVisited = nodesVisited;
            }
        }

        public int CompareTo(Path path)
        {
            return this.QuickestTimeFromStart.CompareTo(path.QuickestTimeFromStart);
        }

    }
}
