using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("TestChamber")]

namespace ClassLibrary
{
    public class PathFinder
    {
        //PROPERTIES
        private Network Network { get; set; }
        internal Dictionary<string, Path> Result { get; set; }
        public bool NeedsReset { get => Result.Count > 0; }

        //CONSTRUCTOR
        public PathFinder(Network network)
        {
            if (network == null)
                throw new InvalidOperationException("Can not create a Pathfinder if Network is null");
            if (network.Nodes.Count < 3)
                throw new InvalidOperationException("Can not create a Pathfinder if Network has less than 3 nodes");
            
            Network = network;
            Result = new Dictionary<string, Path>();
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
            
            InitializeResult(startNode);
            ProcessPaths(startNode, endNode, stopAtEndNode);
            return ExtractResult(startNode);
        }

        // Setting QuickestTimeFromStart to infinite
        internal void InitializeResult(string startNode)
        {
            if (NeedsReset)
                ResetResult();

            //We always need to construct a new Result dictionary 
            //in the case that nodes have been added to the network since last        
            foreach (var node in Network.Nodes)
            {
                Result.Add(node.Key, new Path(node.Value));
            }

            Result[startNode].QuickestTimeFromStart = 0;
        }

        private void ResetResult()
        {
            Result.Clear();
            foreach (var n in Network.Nodes)
            {
                n.Value.Visited = false;
            }
        }

        // Going through all Paths to process the connections to each Node
        internal void ProcessPaths(string startNode, string endNode, bool stopAtEndNode)
        {
            bool finished = false;

            // A queue of all potential Paths, we will always proces the most promising paths
            //first, aka with shortest time from start
            PriorityQueue<Path> potentialPaths = ConstructPriorityQueueOfPotentialPaths(startNode);

            while (!finished)
            {
                Path nextPath = GetPathWithCurrentLowestQuickestTimeFromStart(potentialPaths);

                if (nextPath != null)
                {
                    ProcessConnections(nextPath, potentialPaths);
                    if (stopAtEndNode && nextPath.Node.Name == endNode)
                    {
                        finished = true;
                    }
                }
                else
                {
                    finished = true;
                }
            }
        }

        internal PriorityQueue<Path> ConstructPriorityQueueOfPotentialPaths(string startNode)
        {
            PriorityQueue<Path> queue = new PriorityQueue<Path>();
            
            //Making a copy of the starting Path and adding to queue for further processing
            Node start = Result[startNode].Node;
            double timeFromStart = Result[startNode].QuickestTimeFromStart;
            queue.Add(new Path(start, timeFromStart));
            
            return queue;
        }

        internal Path GetPathWithCurrentLowestQuickestTimeFromStart(PriorityQueue<Path> queue)
        {
            bool finished = false;
            Path path = null;
            do
            {
                try
                {
                    path = queue.Pop();
                }
                catch (InvalidOperationException)
                {
                    return path;
                }
            
                if (path.QuickestTimeFromStart == double.PositiveInfinity)
                {
                    path = null;
                    finished = true;
                }
                else if (!path.Node.Visited)
                {
                    path.Node.Visited = true;
                    finished = true;
                }

            } while (!finished);
            
            return path;
        }

        internal List<NodeConnection> GetRelevantConnections(Path path)
        {
            var allConnections = Network.Nodes[path.Node.Name].Connections.ToList();
            List<NodeConnection> relevantConnections = new List<NodeConnection>();
            foreach(var c in allConnections)
            {
                if(!c.Value.TargetNode.Visited)
                    relevantConnections.Add(c.Value);
            }
            return relevantConnections;
        }

        // Processing the connections to each node
        internal void ProcessConnections(Path path, PriorityQueue<Path> paths)
        {
            var connections = GetRelevantConnections(path);

            foreach (var connection in connections)
            {
                string connectingNode = connection.TargetNode.Name;

                double distance = path.QuickestTimeFromStart + connection.TimeCost;

                if (distance < Result[connectingNode].QuickestTimeFromStart)
                {
                    Result[connectingNode].QuickestTimeFromStart = distance;
                    Result[connectingNode].NodesVisited = UsePath(path, Result[connectingNode]);
                    paths.Add(new Path(Result[connectingNode].Node, distance, UsePath(path, Result[connectingNode])));

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

            foreach (var path in Result)
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
