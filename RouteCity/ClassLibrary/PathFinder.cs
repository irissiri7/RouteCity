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
        public Dictionary<string, Path> Result { get; private set; }
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
        /// <summary>
        /// This is the main method of the class. Given a start and end node it works its way through
        /// the network and find the quickest path. The default behavior of the method is to stop when the
        /// end node is reached (given that all the paths to this node has indeed been explored) but it is
        /// possible to use the method to explore all nodes in the network.
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <param name="stopAtEndNode"></param>
        /// <returns></returns>
        public Dictionary<string, Path> FindQuickestPath(string startNode, string endNode, bool stopAtEndNode = true)
        {
            if (startNode == null || endNode == null)
                throw new InvalidOperationException("Can not preform operation if nodes are null");
            if (startNode.Equals(endNode))
                throw new InvalidOperationException("Start node and end node must be different");
            if (!Network.Nodes.Any(n => n.Key == startNode) || !Network.Nodes.Any(n => n.Key == endNode))
                throw new InvalidOperationException("Both start and end node must be in network");
            
            InitializeResult(startNode);
            ProcessPaths(startNode, endNode, stopAtEndNode);
            return Result;
        }

        // Initializing the result dictionary so that all Paths have a QuickestTimeFromStart set to infinity.
        internal void InitializeResult(string startNode)
        {
            // If the Pathfinder has been used previously we want to reset and start with a clean slate.
            if (NeedsReset)
                ResetResult();

            // We always (re)build the Result dictionary in case nodes has been added/removed from network.
            foreach (var node in Network.Nodes)
            {
                Result.Add(node.Key, new Path(node.Value));
            }

            // Setting startNode accordingly
            Result[startNode].QuickestTimeFromStart = 0;
        }

        // Emptying the Result dictionary and setting all the nodes as unvisited.
        private void ResetResult()
        {
            Result.Clear();
            foreach (var n in Network.Nodes)
            {
                n.Value.Visited = false;
            }
        }

        // Going through all potential paths to process the connections to each Node
        internal void ProcessPaths(string startNode, string endNode, bool stopAtEndNode)
        {
            bool finished = false;

            // A queue of all potential paths where we will always process the most promising paths
            // first, i.e the paths with current QuickestTimeFromStart
            PriorityQueue<Path> potentialPaths = ConstructPriorityQueueOfPotentialPaths(startNode);

            while (!finished)
            {
                Path nextPath = GetPathWithCurrentQuickestTimeFromStart(potentialPaths);

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

        // Making a priority queue of paths where the priority is based on QuickestTimeFromStart
        internal PriorityQueue<Path> ConstructPriorityQueueOfPotentialPaths(string startNode)
        {
            PriorityQueue<Path> queue = new PriorityQueue<Path>();
            
            // Making a copy of the starting Path and adding to queue for further processing,
            // reference to the actual node will be the same so that we can keep track of 
            // which nodes we have visited
            Node start = Result[startNode].Node;
            double timeFromStart = Result[startNode].QuickestTimeFromStart;
            queue.Add(new Path(start, timeFromStart));
            
            return queue;
        }

        // This method will pop the Priority Queueu getting the path with quickestTimeFromStart
        internal Path GetPathWithCurrentQuickestTimeFromStart(PriorityQueue<Path> queue)
        {
            bool finished = false;
            Path path = null;
            do
            {
                try
                {
                    path = queue.Pop();
                    
                    //If the node has NOT been visited already we want to explore this path
                    if (!path.Node.Visited)
                    {
                        finished = true;
                        path.Node.Visited = true;
                    }
                }
                // This exception is thrown when we try to Pop() an empty queue 
                // and we have no more paths to explore, path will be returned as null
                catch (InvalidOperationException)
                {
                    return path;
                }
            } while (!finished);
            
            return path;
        }

        // Processing the connections to each node
        internal void ProcessConnections(Path path, PriorityQueue<Path> queue)
        {
            // Relevant connections are the connections we have not already explored.
            var connections = GetRelevantConnections(path);

            foreach (var connection in connections)
            {
                string targetNode = connection.TargetNode.Name;

                double distance = path.QuickestTimeFromStart + connection.TimeCost;

                if (distance < Result[targetNode].QuickestTimeFromStart)
                {
                    List<string> nodesVisited = GetListOfNodesVisited(path, Result[targetNode]);
                    
                    UpdateResult(targetNode, distance, nodesVisited);
                    queue.Add(new Path(Result[targetNode].Node, distance, nodesVisited));

                }
            }
        }

        // This method will return a list of NodeConnections given that the targetNode
        // has not already been visited.
        internal List<NodeConnection> GetRelevantConnections(Path path)
        {
            string nodeName = path.Node.Name;
            var allConnections = Network.Nodes[nodeName].Connections.ToList();
            List<NodeConnection> relevantConnections = new List<NodeConnection>();
            foreach(var c in allConnections)
            {
                if(!c.Value.TargetNode.Visited)
                    relevantConnections.Add(c.Value);
            }
            return relevantConnections;
        }

        // All paths hold a list with the name of the nodes the path has traversed. If the
        // path of the visiting node turns out to be more efficient than the current path 
        // of the node getting visited,this method will copy the list of the visiting node 
        // and also append the name of the node getting visited and returning a new list
        private List<string> GetListOfNodesVisited(Path visiting, Path gettingVisited)
        {
            List<string> newPath = new List<string>();
            foreach (var node in visiting.NodesVisited)
            {
                newPath.Add(node);
            }

            newPath.Add(gettingVisited.Node.Name);

            return newPath;
        }
        
        // When a quicker path has been found this method will update the Result dictionary
        private void UpdateResult(string targetNode, double distance, List<string> nodesVisited )
        {
            Result[targetNode].QuickestTimeFromStart = distance;
            Result[targetNode].NodesVisited = nodesVisited;
        }

        
    }
}
