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
        //PROPERTIES & FIELDS
        public string Name { get; private set; }
        internal Dictionary<string, NodeConnection> Connections { get; private set; }
        internal bool visited;

        //CONSTRUCTOR
        public Node(string name)
        {
            Name = name;
            Connections = new Dictionary<string, NodeConnection>();
            visited = false;
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

        public IEnumerable<KeyValuePair<string, NodeConnection>> GetEachConnection()
        {
            foreach (var element in Connections)
            {
                yield return element;
            }
        }
    }
}
