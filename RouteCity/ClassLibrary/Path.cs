using System.Collections.Generic;

namespace ClassLibrary
{
    class Path
    {
        //PROPERTIES
        internal string Node { get; set; }
        internal double ShortestTimeFromStart { get; set; }
        internal List<string> NodesVisited { get; set; }

        //CONSTRUCTOR
        public Path(string node, double shortestTimeFromStart = double.PositiveInfinity)
        {
            Node = node;
            ShortestTimeFromStart = shortestTimeFromStart;
            NodesVisited = new List<string>();
            NodesVisited.Add(node);
        }

    }
}
