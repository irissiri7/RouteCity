using System;
using System.Diagnostics.CodeAnalysis;

namespace ClassLibrary
{
    public class NodeConnection
    {
        //PROPERTIES
        public Node TargetNode { get; private set; }
        public double TimeCost { get; private set; }

        //CONSTRUCTOR
        public NodeConnection(Node targetNode, double timeCost)
        {
            TargetNode = targetNode;
            TimeCost = timeCost;
        }
    }
}
