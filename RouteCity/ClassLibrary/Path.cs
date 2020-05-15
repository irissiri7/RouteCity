using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ClassLibrary
{
    public class Path : IComparable<Path>
    {
        //PROPERTIES
        internal Node Node { get; set; }
        public double QuickestTimeFromStart { get; set; }
        internal List<string> NodesVisited { get; set; }
        public int NodesVisitedCount { get { return NodesVisited.Count; } }

        //CONSTRUCTOR
        internal Path(Node node, double shortestTimeFromStart = double.PositiveInfinity, List<string> nodesVisited = null)
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

        /// <summary>
        /// Returns a the name of a visited node at a specific index 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetValueFromNodesVisitedByIndex(int index)
        {
            return NodesVisited[index];
        }

        public int CompareTo(Path path)
        {
            return this.QuickestTimeFromStart.CompareTo(path.QuickestTimeFromStart);
        }

    }
}
