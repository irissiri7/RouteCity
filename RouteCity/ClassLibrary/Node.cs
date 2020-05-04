using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("TestChamber")]

namespace ClassLibrary
{
    public class Node : IComparable<Node>
    {
        //PROPERTIES
        public string Name { get; private set; }
        internal Dictionary<string, NodeConnection> Connections { get; set; }
        internal bool Visited;

        //CONSTRUCTOR
        public Node(string name)
        {
            Name = name;
            Connections = new Dictionary<string, NodeConnection>();
            Visited = false;
        }

        //METHODS
        internal void Connect(Node targetNode, double timeCost)
        {
            Connections.Add(targetNode.Name, new NodeConnection(targetNode, timeCost));
            targetNode.Connections.Add(this.Name, new NodeConnection(this, timeCost));
        }

        public int CompareTo(Node node) 
        { 
            return this.Connections.Count.CompareTo(obj.Connections.Count);
        }

    }
}
