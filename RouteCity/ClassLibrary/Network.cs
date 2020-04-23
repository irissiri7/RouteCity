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
       
        }
        
        public void AddNode(string node)
        {
            
        }

        public void AddConnection (string fromNode, string toNode, double timeCost)
        {

        }

        public void RandomizeConnections()
        {

        }

        public void DisplayNetwork()
        {

        }


    }
}
