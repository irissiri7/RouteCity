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
            // Assumes there are no connections already?
            // Should is handle if there are some connections already made? Maybe if time. 
            // Refuses if element in list already contains connections
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
