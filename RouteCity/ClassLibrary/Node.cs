using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    class Node
    {
        //PROPERTIES
        public string Name { get; private set; }
        internal List<NodeConnection> Connections { get; set; }

        //CONSTRUCTOR
        public Node(string name)
        {
            Name = name;
            Connections = new List<NodeConnection>();
        }

        //METHODS
        public void Connect(Node targetNode, double timeCost)
        {
            if (targetNode == null)
                throw new ArgumentException("Target node is null");
            if (targetNode == this)
                throw new ArgumentException("Can not add connection to itself");
            if (timeCost < 0)
                throw new ArgumentException("Distance must be a positive number");

            Connections.Add(new NodeConnection(targetNode, timeCost));
            targetNode.Connections.Add(new NodeConnection(this, timeCost));
        }


    }
}
