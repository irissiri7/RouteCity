using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestChamber")]

namespace ClassLibrary
{

    public class PathFinder
    {
        //PROPERTIES
        private Network Network { get; set; }
        internal Dictionary<string, Path> QuickestPathResults { get; private set; }
        private bool NeedsReset { get => QuickestPathResults.Count > 0; }
        public int ResultCount { get => QuickestPathResults.Count; }

        //CONSTRUCTOR
        public PathFinder(Network network)
        {
            if (network == null)
                throw new InvalidOperationException("Can not create a Pathfinder if Network is null");
            if (network.Nodes.Count < 3)
                throw new InvalidOperationException("Can not create a Pathfinder if Network has less than 3 nodes");

            Network = network;
            QuickestPathResults = new Dictionary<string, Path>();
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
        public void FindQuickestPath(string startNode, string endNode, bool stopAtEndNode = true)
        {
            if (startNode == null || endNode == null)
                throw new ArgumentNullException("Can not preform operation if nodes are null");
            if (startNode.Equals(endNode))
                throw new ArgumentException("Start node and end node must be different");
            if (!Network.Nodes.Any(n => n.Key == startNode) || !Network.Nodes.Any(n => n.Key == endNode))
                throw new ArgumentException("Both start and end node must be in network");

            InitializeQuickestPathResults(startNode);
            ProcessPaths(startNode, endNode, stopAtEndNode);
        }

        // Initializing the QuickestPathResults dictionary so that all Paths have a QuickestTimeFromStart set to infinity.
        internal void InitializeQuickestPathResults(string startNode)
        {
            // If the Pathfinder has been used previously we want to reset and start with a clean slate.
            if (NeedsReset)
            {
                QuickestPathResults.Clear();
                Network.ResetNodes();
            }

            // We always (re)build the QuickestPathResults dictionary in case nodes has been added/removed from network.
            foreach (var node in Network.Nodes)
            {
                QuickestPathResults.Add(node.Key, new Path(node.Value));
            }

            // Setting startNode accordingly
            QuickestPathResults[startNode].QuickestTimeFromStart = 0;
        }


        // Going through all potential paths to process the connections to each Node
        private void ProcessPaths(string startNode, string endNode, bool stopAtEndNode)
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
        private PriorityQueue<Path> ConstructPriorityQueueOfPotentialPaths(string startNode)
        {
            PriorityQueue<Path> queue = new PriorityQueue<Path>();

            // Making a copy of the starting Path and adding to queue for further processing,
            // reference to the actual node will be the same so that we can keep track of 
            // which nodes we have visited
            Node start = QuickestPathResults[startNode].Node;
            double timeFromStart = QuickestPathResults[startNode].QuickestTimeFromStart;
            queue.Add(new Path(start, timeFromStart));

            return queue;
        }

        // This method will pop the Priority Queueu getting the path with quickestTimeFromStart
        private Path GetPathWithCurrentQuickestTimeFromStart(PriorityQueue<Path> queue)
        {
            bool finished = false;
            Path path = null;
            do
            {
                try
                {
                    path = queue.Pop();

                    //If the node has NOT been visited already we want to explore this path
                    if (!path.Node.visited)
                    {
                        finished = true;
                        path.Node.visited = true;
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
        private void ProcessConnections(Path path, PriorityQueue<Path> queue)
        {
            // Relevant connections are the connections we have not already explored.
            var connections = GetRelevantConnections(path);

            foreach (var connection in connections)
            {
                string targetNode = connection.TargetName;

                double distance = path.QuickestTimeFromStart + connection.TimeCost;

                if (distance < QuickestPathResults[targetNode].QuickestTimeFromStart)
                {
                    List<string> nodesVisited = GetListOfNodesVisited(path, QuickestPathResults[targetNode]);

                    UpdateResult(targetNode, distance, nodesVisited);
                    queue.Add(new Path(QuickestPathResults[targetNode].Node, distance, nodesVisited));

                }
            }
        }

        // This method will return a list of NodeConnections given that the targetNode
        // has not already been visited.
        private List<NodeConnection> GetRelevantConnections(Path path)
        {
            string nodeName = path.Node.Name;
            var allConnections = Network.Nodes[nodeName].Connections.ToList();
            List<NodeConnection> relevantConnections = new List<NodeConnection>();
            foreach (var c in allConnections)
            {
                if (!c.Value.TargetNode.visited)
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

        // When a quicker path has been found this method will update the QuickestPathResults dictionary
        private void UpdateResult(string targetNode, double distance, List<string> nodesVisited)
        {
            QuickestPathResults[targetNode].QuickestTimeFromStart = distance;
            QuickestPathResults[targetNode].NodesVisited = nodesVisited;
        }

        /// <summary>
        /// Returns desired path object from a Dictionary already containing the results from PathFinder. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// 
        public Path GetQuickestPathTo(string name)
        {
            return QuickestPathResults[name];
        }
    }
}
