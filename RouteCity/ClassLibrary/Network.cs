using System;
using System.Collections.Generic;
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
            List<Node> incompleteNodes = new List<Node>();
            List<Node> completeNodes = new List<Node>();
            PriorityQueue<Node> queueForCompletion = new PriorityQueue<Node>();

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

            while (emptyNodes.Count > 0)
            {
                while (currentNode.Connections.Count < 3 && emptyNodes.Count > 0)
                {
                    int connectToIndex = r.Next(0, emptyNodes.Count);
                    AddConnection(currentNode.Name, emptyNodes[connectToIndex].Name, (double)r.Next(1, 11));
                    incompleteNodes.Add(emptyNodes[connectToIndex]);
                    emptyNodes.RemoveAt(connectToIndex);

                }

                if (currentNode.Connections.Count > 2)
                {
                    incompleteNodes.Remove(currentNode);

                    // Behöver jag detta om noderna redan är addade? kopplingarna uppdateras väl?
                    completeNodes.Add(currentNode);
                }

                currentIndex = r.Next(0, incompleteNodes.Count);
                currentNode = incompleteNodes[currentIndex];

            }

            foreach (var element in incompleteNodes)
            {
                queueForCompletion.Add(element);
            }

            while (queueForCompletion.Count() > 0)
            {
                while (queueForCompletion.Peek().Connections.Count < 2 && queueForCompletion.Count() > 0)
                {
                    currentNode = queueForCompletion.Pop();
                }

                // Risk for loop if already full
                // Remove at... ändra värdet på index till max och sedan sortera neråt. Sedan ta bort. 
                // Precis som om jag tagit upp processen att ta bort top?... fast vad händer om det är på en sida i trädet där slutet
                // inte finns? Kanske skicka upp till toppen och sedan sortera ner?
                while (currentNode.Connections.Count < 3 && queueForCompletion.Count() > 0)
                {
                    int connectToIndex = r.Next(0, queueForCompletion.Count());

                    if (!currentNode.Name.Equals(queueForCompletion.GetValueByIndex(connectToIndex, false).Name))
                    {
                        AddConnection(currentNode.Name, queueForCompletion.GetValueByIndex(connectToIndex, false).Name, (double)r.Next(1, 11));
                    }

                }

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
