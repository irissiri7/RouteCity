using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestChamber")]

namespace ClassLibrary
{
    public class Network
    {
        //PROPERTIES

        public Dictionary<string, Node> Nodes { get; private set; }
       
        // The Node-class includes a list of objects called NodeConnections, which keeps track of all the connections that specific node has.
        // For example: A includes information that it's connected to C, while C also includes info that it's connected to A. 
        // With that said, this Dictionaty named connectionsPath is different. It keeps track of HOW all of the nodes were connected to eachother, 
        // which is useful when displaying connections without displaying the same connection twice.
        public Dictionary<string, List<NodeConnection>> ConnectionPath {get; private set;}

        //CONSTRUCTOR
        public Network()
        {
            Nodes = new Dictionary<string, Node>(StringComparer.InvariantCultureIgnoreCase);
            ConnectionPath = new Dictionary<string, List<NodeConnection>>();
        }

        //METHODS

        /// <summary>
        /// Creates a network of nodes with random connections based on the names that are put in as arguments. 
        /// </summary>
        /// <param name="nodeNames"></param>
        public void CreateNetwork(List<string> nodeNames)
        {
            if (nodeNames == null)
            {
                throw new ArgumentNullException("List of names is null");
            }
            else if (nodeNames.Count < 3)
            {
                throw new ArgumentException("List of names is below 3");
            }

            foreach (var element in nodeNames)
            {
                AddNode(element);
            }

            RandomizeConnections();
        }

        /// <summary>
        /// Adds a node to the Dictionary called Nodes.
        /// </summary>
        /// <param name="node"></param>
        internal void AddNode(string node)
        {
            // Makes sure that the name consists of letter or numbers, while allowing spaces and - inbetween. 
            Regex reg = new Regex("^[^A-Öa-ö0-9]|[^\\w]{2}|[^A-Öa-ö0-9]$");

            if (node == null)
            {
                throw new ArgumentNullException("String was null");
            }
            else if (node.Equals(""))
            {
                throw new ArgumentException("String was empty");
            }
            else if (reg.IsMatch(node))
            {
                throw new ArgumentException("There are too many characters that are not letters or numbers");
            }
            else if (Nodes.ContainsKey(node))
            {
                throw new ArgumentException("Element already exists in the set");
            }

            Nodes.Add(node, new Node(node));

        }

        /// <summary>
        /// Adds a two-way connection between nodes.
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <param name="timeCost"></param>
        internal void AddConnection(string fromNode, string toNode, double timeCost)
        {
            if (fromNode == null || toNode == null)
            {
                throw new ArgumentException("One of the nodes is null");
            }
            else if (!Nodes.ContainsKey(fromNode))
            {
                throw new ArgumentException("Host node does not exist");
            }
            else if (!Nodes.ContainsKey(toNode))
            {
                throw new ArgumentException("Target node does not exist");
            }
            else if (fromNode.ToLower().Equals(toNode.ToLower()))
            {
                throw new ArgumentException("Can not add connection to itself");
            }
            else if (timeCost < 0)
            {
                throw new ArgumentException("Time cost must be a positive number");
            }

            Nodes[fromNode].Connect(Nodes[toNode], timeCost);
        }

        /// <summary>
        /// Randomizes connections between the nodes in "Nodes", creating a closed system of connections 
        /// </summary>
        internal void RandomizeConnections()
        {
            Random r = new Random();
            List<Node> emptyNodes = new List<Node>(Nodes.Count);
            PriorityQueue<Node> incompleteNodes = new PriorityQueue<Node>();

            if (Nodes.Count < 3)
            {
                throw new InvalidOperationException("Tried to randomize paths between 2 or less connections.");
            }

            // Adding all nodes from Nodes to "emptyNodes". If one node is not empty then throw an exception. 
            foreach (var element in Nodes)
            {
                if (element.Value.Connections.Count > 0)
                {
                    throw new InvalidOperationException("Cannot Randomize connections in a network where connections are alrady made.");
                }
                else
                {
                    emptyNodes.Add(element.Value);
                }
            }

            // Getting a random node from emptyNodes. Once chosen, the node is considered incomplete (not empty) since we'll now
            // be adding connections to it.
            int currentIndex = r.Next(0, emptyNodes.Count);
            Node currentNode = emptyNodes[currentIndex];
            incompleteNodes.Add(currentNode);
            emptyNodes.RemoveAt(currentIndex);

            // This will help sort later
            bool insideIncompleteNodes = false;

            while (emptyNodes.Count > 0)
            {
                // Connect currentNode to three randomized empty nodes.
                while (currentNode.Connections.Count < 3 && emptyNodes.Count > 0)
                {
                    int connectToIndex = r.Next(0, emptyNodes.Count);
                    double timeCost = r.Next(1, 11);
                    string fromNode = currentNode.Name;
                    Node toNode = emptyNodes[connectToIndex];
                    AddConnection(fromNode, toNode.Name, timeCost);
                    AddConnectionPath(fromNode, toNode, timeCost);
                    
                    // We only need to use SortAt() when we've picked a random node from incompleteNodes
                    // Sorting needs to be done explicitly since we are updating the Node outside of the PriorityQueue. 
                    if (insideIncompleteNodes)
                    {
                        incompleteNodes.SortAt(currentIndex);
                    }

                    // Each node that the current one has connected to is also considered incomplete. 
                    incompleteNodes.Add(emptyNodes[connectToIndex]);
                    emptyNodes.RemoveAt(connectToIndex);

                }

                // Remove any and all nodes with more than 3 connections.

                //***********************************************************
                //*** SEE IF THIS IS NEEDED*** OR IF THERE'S A BETTER WAY ***
                // Maybe have an ifstatement check before sort above. Checking both from and to node. Or only to?
                //for (int i = 0; i < incompleteNodes.Count(); i++)
                //{
                //    if (incompleteNodes.GetValueByIndex(i).Connections.Count > 2)
                //    {
                //        incompleteNodes.RemoveAt(i);
                //    }
                //}

                if (currentNode.Connections.Count > 2)
                {
                    incompleteNodes.RemoveAt(currentIndex);
                }

                // Picking a new node from incomplete nodes. We are now picking from there, instead of empty nodes
                // to make sure that all nodes can be reached from all other nodes. 
                currentIndex = r.Next(0, incompleteNodes.Count());
                currentNode = incompleteNodes.GetValueByIndex(currentIndex);
                insideIncompleteNodes = true;

            }

            // After having created a closed system of connections, this loop makes sure that each node has between 2 - 3 connections. 
            while (incompleteNodes.Count() > 1)
            {
                // Getting the node with the least connections. 
                currentNode = incompleteNodes.Pop();

                // This loop sees to that the current node gets between 2 - 3 connections. 
                do
                {
                    int connectToIndex = r.Next(0, incompleteNodes.Count());

                    // Add connection if it doesn't already exist. 
                    if (!currentNode.Connections.ContainsKey(incompleteNodes.GetValueByIndex(connectToIndex).Name))
                    {
                        double timeCost = r.Next(1, 11);
                        string fromNode = currentNode.Name;
                        Node toNode = incompleteNodes.GetValueByIndex(connectToIndex);
                        AddConnection(fromNode, toNode.Name, timeCost);
                        AddConnectionPath(fromNode, toNode, timeCost);
                    }

                    // If the connected node has more than 2 connections, it's considered complete. 
                    if (incompleteNodes.GetValueByIndex(connectToIndex).Connections.Count > 2)
                    {
                        incompleteNodes.RemoveAt(connectToIndex);
                    }
                    else
                    {
                        // If the node was not removed, then it needs to be sorted
                        incompleteNodes.SortAt(connectToIndex);
                    }

                // If there are more than one incomplete node, then we know that there are connections still to be made. 
                // If there is one incompletenode then it depends on how many connections that node has. If the current node
                // and the last incompletenode both have two connections BUT it's to each other, then there are no other possible connections. 
                } while (
                (incompleteNodes.Count() > 1 && currentNode.Connections.Count < 3) ||
                (incompleteNodes.Count() == 1 && incompleteNodes.Peek().Connections.Count < 2) ||
                (incompleteNodes.Count() == 1 && incompleteNodes.Peek().Connections.Count == 2 && currentNode.Connections.Count == 2 && !incompleteNodes.Peek().Connections.ContainsKey(currentNode.Name))
                );
            }
        }

        // Resetting all nodes as being unvisited
        public void ResetNodes()
        {
            foreach (var n in Nodes)
            {
                n.Value.visited = false;
            }
        }

        private void AddConnectionPath(string fromNode, Node toNode, double timeCost)
        {
            if (ConnectionPath.ContainsKey(fromNode))
            {
                ConnectionPath[fromNode].Add(new NodeConnection(toNode, timeCost));
            }
            else
            {
                ConnectionPath.Add(fromNode, new List<NodeConnection> {new NodeConnection(toNode, timeCost) });
            }
        }

    }
}
