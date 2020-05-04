using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("TestChamber")]

namespace ClassLibrary
{
    public class OldPathfinder
    {
        //PROPERTIES
        private Network Network { get; set; }
        internal Dictionary<string, Path> Paths { get; set; }

        //CONSTRUCTOR
        public OldPathfinder(Network network)
        {
            if (network == null)
                throw new InvalidOperationException("Can not create a Pathfinder if Network is null");
            if (network.Nodes.Count < 3)
                throw new InvalidOperationException("Can not create a Pathfinder if Network has less than 3 nodes");

            Network = network;
            Paths = new Dictionary<string, Path>();
        }

        //METHODS
        public string FindQuickestPath(string startNode, string endNode, bool stopAtEndNode = true)
        {
            if (startNode == null || endNode == null)
                throw new InvalidOperationException("Can not preform operation if nodes are null");
            if (startNode.Equals(endNode))
                throw new InvalidOperationException("Start node and end node must be different");
            if (!Network.Nodes.Any(n => n.Key == startNode) || !Network.Nodes.Any(n => n.Key == endNode))
                throw new InvalidOperationException("Both start and end node must be in network");

            InitializePaths(startNode);
            ProcessPaths(endNode, stopAtEndNode);
            return ExtractResult(startNode);
        }

        // Setting QuickestTimeFromStart to infinite
        internal void InitializePaths(string startNode)
        {
            Paths.Clear();
            foreach (var node in Network.Nodes)
            {
                Paths.Add(node.Key, new Path(node.Value));
            }

            Paths[startNode].QuickestTimeFromStart = 0;
        }

        // Going through all Paths to process the connections to each Node
        internal void ProcessPaths(string endNode, bool stopAtEndNode)
        {
            bool finished = false;

            // A list of all the Nodes
            var pathQueue = Paths.Values.ToList();

            while (!finished)
            {
                Path nextPath = pathQueue.OrderBy(n => n.QuickestTimeFromStart).FirstOrDefault(
                    n => !double.IsPositiveInfinity(n.QuickestTimeFromStart));

                if (nextPath != null)
                {
                    ProcessConnections(nextPath, pathQueue);
                    if (stopAtEndNode)
                    {
                        if (nextPath.Node.Name == endNode)
                        {
                            finished = true;
                        }
                    }
                    pathQueue.Remove(nextPath);
                }
                else
                {
                    finished = true;
                }
            }
        }


        // Processing the connections to each node
        internal void ProcessConnections(Path path, List<Path> paths)
        {
            var connections = Network.Nodes[path.Node.Name].Connections.Where(c => paths.Any(p => p.Node.Name == c.Key));

            foreach (var connection in connections)
            {
                string connectingNode = connection.Key;

                double distance = path.QuickestTimeFromStart + connection.Value.TimeCost;

                if (distance < Paths[connectingNode].QuickestTimeFromStart)
                {
                    Paths[connectingNode].QuickestTimeFromStart = distance;
                    Paths[connectingNode].NodesVisited = UsePath(path, Paths[connectingNode]);

                }
            }
        }

        private List<string> UsePath(Path visiting, Path gettingVisited)
        {
            List<string> newPath = new List<string>();
            foreach (var node in visiting.NodesVisited)
            {
                newPath.Add(node);
            }

            newPath.Add(gettingVisited.Node.Name);

            return newPath;
        }

        private string ExtractResult(string startNode)
        {
            StringBuilder result = new StringBuilder();
            result.Append($"Start Node {startNode}\n\n");

            foreach (var path in Paths)
            {
                if (path.Key == startNode)
                    continue;
                result.Append($"Node:{path.Key}\nShortest Time Cost: {(double.IsPositiveInfinity(path.Value.QuickestTimeFromStart) ? "Infinity" : path.Value.QuickestTimeFromStart.ToString())}\nVia Node: {ExtractPath(path.Value)} \n\n");
            }
            return result.ToString();
        }

        private string ExtractPath(Path path)
        {
            StringBuilder str = new StringBuilder();
            char[] charsToTrim = { '-', '>', ' ' };

            foreach (var node in path.NodesVisited)
            {
                str.Append($"{node} -> ");
            }
            return str.ToString().TrimEnd(charsToTrim);
        }


    }
}
