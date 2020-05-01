using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("TestChamber")]

namespace ClassLibrary
{
    public class Node : IComparable<Node>, ICloneable<Node>
    {
        //PROPERTIES
        public string Name { get; private set; }
        internal Dictionary<string, NodeConnection> Connections { get; set; }

        //CONSTRUCTOR
        public Node(string name)
        {
            Name = name;
            Connections = new Dictionary<string, NodeConnection>();
        }

        //METHODS
        internal void Connect(Node targetNode, double timeCost)
        {
            Connections.Add(targetNode.Name, new NodeConnection(targetNode, timeCost));
            targetNode.Connections.Add(this.Name, new NodeConnection(this, timeCost));
        }

        public int CompareTo(Node obj) 
        { 
            return this.Connections.Count.CompareTo(obj.Connections.Count);
        }

        public Node Clone()
        {
            Node copy = new Node(this.Name);
            foreach (var element in this.Connections)
            {
                copy.Connections.Add(element.Key, element.Value);
            }

            return copy;
        }
    }
}
