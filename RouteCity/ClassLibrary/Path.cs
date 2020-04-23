namespace ClassLibrary
{
    class Path
    {
        //PROPERTIES
        internal Node Node { get; set; }
        internal double ShortestTimeFromStartNode { get; set; }
        internal Node PreviousNode { get; set; }

        //CONSTRUCTOR
        public Path(Node node, double shortestTimeFromStartNode = double.PositiveInfinity, Node previousNode = null)
        {
            Node = node;
            ShortestTimeFromStartNode = shortestTimeFromStartNode;
            PreviousNode = previousNode;
        }

    }
}
