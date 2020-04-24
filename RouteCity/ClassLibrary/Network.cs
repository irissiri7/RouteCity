using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Network
    {
        //PROPERTIES
        internal Dictionary<string, Node> Nodes { get ; set; }

        //CONSTRUCTOR
        public Network()
        {
            Nodes = new Dictionary<string, Node>();
        }

        //METHODS
        public void CreateNetwork(List<string> nodeNames)
        {

        }
        
        public void AddNode(string node)
        {
            // Refuse if node already exist?
            // if string is null or empty or space?
            // big letters should not count

        }

        public void AddConnection (string fromNode, string toNode, double timeCost)
        {
            // Refuse if connection is already there - Or should the Connect() take care of that?
            // Or if one of the nodes doesn't exist
            // Negative time cost is already checked by connect
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
