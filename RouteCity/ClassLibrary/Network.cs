using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Network
    {
        //PROPERTIES
        internal Dictionary<string, Node> Nodes { get; set; }

        //CONSTRUCTOR
        public Network()
        {
            Nodes = new Dictionary<string, Node>();
        }

        //METHODS
        public void CreateNetwork(List<string> nodeNames)
        {
            // Gå ur om listan är mindre än... ?
            // Ta in hur många connections varje ska ha som parameter? Om Två noder kan varje bara ha en. Om Tre kan det vara finnas två. 
            // Om fyra då kan varje ha tre. 
            // Max antal kopplingar är antalet noder - 1
            // Vad händer om null?
            // number of max connection is bwlow 1

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
        }

        public void RandomizeConnections()
        {
            // Assumes there are no connections already?
            // Should is handle if there are some connections already made? Maybe if time. 
        }

        public void DisplayNetwork()
        {

        }


    }
}
