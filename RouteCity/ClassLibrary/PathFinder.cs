using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("TestChamber")]

namespace ClassLibrary
{
    class PathFinder
    {
        //PROPERTIES
        private Network Network { get; set; }
        internal Dictionary<string, Path> Paths { get; set; }

        //CONSTRUCTOR
        public PathFinder(Network network)
        {
            if (network == null)
                throw new InvalidOperationException("Can not create a Pathfinder if Network is null");
            if (network.Nodes.Count <= 0)
                throw new InvalidOperationException("Can not create a Pathfinder if Network has 0 nodes");
            
            Network = network;
            Paths = new Dictionary<string, Path>();
        }

        //METHODS
        public void FindQuickestPath(string startNode, string endNode, bool stopAtEndNode = true)
        {
            //Main method, this one will give the 
        }

        internal void InitializePaths(string startNode)
        {
            foreach (var node in Network.Nodes)
            {
                Paths.Add(node.Key, new Path(node.Key));
            }

            Paths[startNode].ShortestTimeFromStart = 0;
        }

        public string ShowShortestPath()
        {
            return "The shortcut is really quick";
        }



    }
}
