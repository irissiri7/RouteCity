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

        }


    }
}
