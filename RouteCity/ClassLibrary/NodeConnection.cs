namespace ClassLibrary
{
    class NodeConnection
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
