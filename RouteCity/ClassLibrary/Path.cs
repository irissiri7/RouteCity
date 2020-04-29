using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ClassLibrary
{
    class Path : IComparable<Path>, IEquatable<string>
    {
        //PROPERTIES
        internal string Node { get; set; }
        internal double QuickestTimeFromStart { get; set; }
        internal List<string> NodesVisited { get; set; }

        //CONSTRUCTOR
        public Path(string node, double shortestTimeFromStart = double.PositiveInfinity)
        {
            Node = node;
            QuickestTimeFromStart = shortestTimeFromStart;
            NodesVisited = new List<string>();
            NodesVisited.Add(node);
        }

        public int CompareTo(Path path)
        {
            return this.QuickestTimeFromStart.CompareTo(path.QuickestTimeFromStart);
        }

        public bool Equals(string name)
        {
            if (name == Node)
                return true;
            return false;
        }
    }
}
