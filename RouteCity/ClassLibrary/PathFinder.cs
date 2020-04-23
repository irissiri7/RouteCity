using System.Collections.Generic;

namespace ClassLibrary
{
    class PathFinder
    {
        //PROPERTIES
        private Network Network { get; set; }
        private string StartNode { get; set; }
        private string EndNode { get; set; }
        private Dictionary<Node, Path> Paths { get; set; }

        //CONSTRUCTOR
        public PathFinder(Network network, string startNode, string endNode)
        {
            Network = network;
            StartNode = startNode;
            EndNode = endNode;
            Paths = new Dictionary<Node, Path>();
        }

        public void FindShortestPath()
        {

        }

        public string ShowShortestPath()
        {
            return "The shortcut is really quick";
        }



    }
}
