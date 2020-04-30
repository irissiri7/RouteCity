using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class Network
    {
        //PROPERTIES
        internal Dictionary<string, Node> Nodes { get; set; }

        //CONSTRUCTOR
        public Network()
        {
            Nodes = new Dictionary<string, Node>(StringComparer.InvariantCultureIgnoreCase);
        }

        //METHODS
        public void CreateNetwork(List<string> nodeNames)
        {
            foreach (var element in nodeNames)
            {
                AddNode(element);
            }


        }

        public void AddNode(string node)
        {
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

        public void AddConnection(string fromNode, string toNode, double timeCost)
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

        public void RandomizeConnections()
        {
            Random r = new Random();
            List<Node> emptyNodes = new List<Node>(Nodes.Count);
            PriorityQueue<Node> incompleteNodes = new PriorityQueue<Node>();

            // Only if there are over 2 nodes ***FIX***
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

            int currentIndex = r.Next(0, emptyNodes.Count);
            Node currentNode = emptyNodes[currentIndex];
            incompleteNodes.Add(currentNode);
            emptyNodes.RemoveAt(currentIndex);

            Debug.WriteLine("Creating a closed system...");
            while (emptyNodes.Count > 0)
            {
                while (currentNode.Connections.Count < 3 && emptyNodes.Count > 0)
                {
                    int connectToIndex = r.Next(0, emptyNodes.Count);
                    // Måste kolla att connection inte redan finns. 
                    // Men om jag tar bort current innan kopplingar och alla andra ska vara tomma... hur kan det hända? 
                    AddConnection(currentNode.Name, emptyNodes[connectToIndex].Name, (double)r.Next(1, 11));
                    Debug.WriteLine($"{currentNode.Name} connected to {emptyNodes[connectToIndex].Name}"); 
                    incompleteNodes.Add(emptyNodes[connectToIndex]);
                    emptyNodes.RemoveAt(connectToIndex);

                }

                // Maybe I can't do this since it's not sorted, it just pops the one with the least value
                // First current node still surived because of this, it needs to be removed
                // 
                for (int i = 0; i < incompleteNodes.Count(); i++)
                {
                    if (incompleteNodes.GetValueByIndex(i).Connections.Count > 2)
                    {
                        incompleteNodes.RemoveAt(i);
                    }
                }


                currentIndex = r.Next(0, incompleteNodes.Count());
                currentNode = incompleteNodes.GetValueByIndex(currentIndex);

            }

            Debug.WriteLine("Completing nodes...");
            while (incompleteNodes.Count() > 1)
            {
                Debug.Write("IncompleteNodes are: ");
                for (int i = 0; i < incompleteNodes.Count(); i++)
                {
                    Debug.Write($"{incompleteNodes.GetValueByIndex(i).Name}, ");
                }
                
                currentNode = incompleteNodes.Pop();
                Debug.WriteLine($"\nPopped {currentNode.Name}");

                do
                {
                    int connectToIndex = r.Next(0, incompleteNodes.Count());

                    if (!currentNode.Connections.ContainsKey(incompleteNodes.GetValueByIndex(connectToIndex).Name))
                    {
                        AddConnection(currentNode.Name, incompleteNodes.GetValueByIndex(connectToIndex).Name, (double)r.Next(1, 11));
                        Debug.WriteLine($"{currentNode.Name} connected to {incompleteNodes.GetValueByIndex(connectToIndex).Name}" +
                            $"(now has {incompleteNodes.GetValueByIndex(connectToIndex).Connections.Count} connections)");
                    }
                    if (incompleteNodes.GetValueByIndex(connectToIndex).Connections.Count > 2)
                    {
                        Debug.WriteLine($"Removing {incompleteNodes.GetValueByIndex(connectToIndex).Name}, it has " +
                            $"{incompleteNodes.GetValueByIndex(connectToIndex).Connections.Count} connections");
                        incompleteNodes.RemoveAt(connectToIndex);
                        Debug.WriteLine($"{incompleteNodes.Count()} incomplete elements left");
                    }
                } while (currentNode.Connections.Count < 3 && incompleteNodes.Count() > 0);
            }
        }

        public void DisplayNetwork()
        {

        }

        // Could the get property do this already? And Node is just a private variable, not a property?
        //public Dictionary<string, Node> GetNetworkCopy()
        //{
        //    Dictionary<string, Node> copy = new Dictionary<string, Node>();

        //    foreach (var element in Nodes)
        //    {
        //        copy.Add(element.Key, element.Value);
        //    }

        //    return copy;
        //}

    }
}
