using NUnit.Framework;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestChamber
{
    public class NetworkTests
    {
        // CreateNetwork()
        //[Test]
        //public void CreateNetwork_RandomConnections_RespectsMinAndMaxConnections()
        //{
        //    Network network = new Network();
        //    List<string> fiveElements = new List<string>() { "one", "two", "three", "four", "five" };
        //    network.CreateNetwork(fiveElements);

        //    foreach (var element in network.Nodes)
        //    {
        //        if (element.Value.Connections.Count < 2 || element.Value.Connections.Count > 3)
        //        {
        //            Assert.Fail();
        //        }
        //    }

        //    Assert.IsTrue(network.Nodes.Count > 0);
        //}

        [Test]
        public void CreateNetwork_Randomize10Connections_AllNodesAreIndirectlyReachableFromEveryNode()
        {
            for (int i = 0; i < 10000; i++)
            {
                List<string> names = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                Network network = new Network();
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                network.CreateNetwork(names);
                //stopwatch.Stop();
                //Debug.WriteLine($"Creating the network took {stopwatch.Elapsed.TotalSeconds}");
                //stopwatch.Restart();
                PathFinder finder = new PathFinder(network);
                //stopwatch.Stop();
                //Debug.WriteLine($"Finding path took {stopwatch.Elapsed.TotalSeconds}");

                string result = finder.FindQuickestPath("G", "D", false);

                foreach (var element in finder.Paths)
                {
                    if (double.IsPositiveInfinity(element.Value.QuickestTimeFromStart))
                    {
                        Assert.Fail($"{element.Value.Node} was {element.Value.QuickestTimeFromStart}");
                    }
                }
            }

        }

        [Test]
        public void CreateNetwork_ListIsNull_ReturnsArgumentNullException()
        {
            Network network = new Network();
            List<string> notInitialized = null;
            Assert.Throws<ArgumentNullException>(() => network.CreateNetwork(notInitialized));
        }

        [Test]
        public void CreateNetwork_ListCountIsBelow3_ReturnsArgumentException()
        {
            Network network = new Network();
            List<string> onlyTwoElements = new List<string>() { "one", "two" };
            Assert.Throws<ArgumentException>(() => network.CreateNetwork(onlyTwoElements));
        }

        //[Test]
        //public void CreateNetwork_MaxConnectionsIsTooHigh_ReturnsArgumentException()
        //{
        //    Network network = new Network();
        //    List<string> fiveElements = new List<string>() { "one", "two", "three", "four", "five" };
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, 5));
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, 6));
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, 15));
        //}

        //[Test]
        //public void CreateNetwork_MaxConnectionsIsTooLow_ReturnsArgumentException()
        //{
        //    Network network = new Network();
        //    List<string> fiveElements = new List<string>() { "one", "two", "three", "four", "five" };
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, 0));
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, -1));
        //}

        //AddNode()
        [Test]
        public void AddNode_NodeAlreadyExists_ReturnsArgumentException()
        {

            Network network = new Network();

            network.Nodes.Add("A", new Node("A"));
            network.Nodes.Add("B", new Node("B"));

            Assert.Throws<ArgumentException>(() => network.AddNode("B"));
        }

        [Test]
        public void AddNode_NodeAlreadyExistsButWithDifferentCapitalization_ReturnsArgumentException()
        {
            Network network = new Network();
            network.Nodes.Add("A", new Node("A"));
            network.Nodes.Add("B", new Node("B"));

            Assert.Throws<ArgumentException>(() => network.AddNode("b"));
        }

        [Test]
        public void AddNode_StringIsNull_ReturnsArgumentNullException()
        {
            Network network = new Network();
            Assert.Throws<ArgumentNullException>(() => network.AddNode(null));
        }

        [Test]
        public void AddNode_StringIsEmpty_ReturnsArgumentException()
        {

            Network network = new Network();
            Assert.Throws<ArgumentException>(() => network.AddNode(""));
        }

        [Test]
        public void AddNode_StringOnlyContainsSpaces_ReturnsArgumentException()
        {
            Network network = new Network();
            Assert.Throws<ArgumentException>(() => network.AddNode(" "));
            Assert.Throws<ArgumentException>(() => network.AddNode("  "));
        }

        [Test]
        public void AddNode_StringDoesNotContainLettersOrNumbers_ReturnsArgumentException()
        {
            Network network = new Network();
            Assert.Throws<ArgumentException>(() => network.AddNode("#"));
            Assert.Throws<ArgumentException>(() => network.AddNode(","));
        }

        [Test]
        public void AddNode_ActuallyAdds()
        {
            Network network = new Network();
            network.AddNode("Test");
            Assert.IsTrue(network.Nodes.ContainsKey("Test") && network.Nodes.Count == 1);
        }

        //RandomizeConnections()
        [Test]
        public void RandomizeConnections_ThereAreAlreadyConnectionsEstablished_ReturnsInvalidOperationException()
        {
            Network network = new Network();
            network.Nodes.Add("A", new Node("A"));
            network.Nodes.Add("B", new Node("B"));
            network.Nodes.Add("C", new Node("C"));
            network.Nodes.Add("D", new Node("D"));
            network.Nodes.Add("E", new Node("E"));
            network.Nodes.Add("F", new Node("F"));
            network.Nodes.Add("G", new Node("G"));
            network.Nodes["A"].Connect(network.Nodes["B"], 3);

            Assert.Throws<InvalidOperationException>(() => network.RandomizeConnections());
        }

        [Test]
        public void RandomizeConnections_LessThanThreeNodes_ReturnsInvalidOperationException()
        {
            Network network = new Network();
            network.Nodes.Add("A", new Node("A"));
            network.Nodes.Add("B", new Node("B"));

            Assert.Throws<InvalidOperationException>(() => network.RandomizeConnections());
        }

        [Test]
        public void RandomizeConnections_ThereAre7Nodes_ReturnsNodesWith2Or3Connections()
        {
            for (int i = 0; i < 5000; i++)
            {
                Network network = new Network();
                network.Nodes.Add("A", new Node("A"));
                network.Nodes.Add("B", new Node("B"));
                network.Nodes.Add("C", new Node("C"));
                network.Nodes.Add("D", new Node("D"));
                network.Nodes.Add("E", new Node("E"));
                network.Nodes.Add("F", new Node("F"));
                network.Nodes.Add("G", new Node("G"));

                network.RandomizeConnections();

                foreach (var element in network.Nodes)
                {
                    if (element.Value.Connections.Count > 3 || element.Value.Connections.Count < 2)
                    {
                        Assert.Fail();
                    }
                }
            }

        }

        [Test]
        public void RandomizeConnections_ThereAre10Nodes_ReturnsNodesWith2Or3Connections()
        {
            for (int i = 0; i < 5000; i++)
            {
                Network network = new Network();
                network.Nodes.Add("A", new Node("A"));
                network.Nodes.Add("B", new Node("B"));
                network.Nodes.Add("C", new Node("C"));
                network.Nodes.Add("D", new Node("D"));
                network.Nodes.Add("E", new Node("E"));
                network.Nodes.Add("F", new Node("F"));
                network.Nodes.Add("G", new Node("G"));
                network.Nodes.Add("H", new Node("H"));
                network.Nodes.Add("I", new Node("I"));
                network.Nodes.Add("J", new Node("J"));

                network.RandomizeConnections();

                foreach (var element in network.Nodes)
                {
                    if (element.Value.Connections.Count > 3 || element.Value.Connections.Count < 2)
                    {
                        Assert.Fail();
                    }
                }
            }

        }

        //AddConnection()
        [Test]
        public void AddConnection_AtLeastOneNodeDoesNotExist_ReturnsArgumentException()
        {
            Network network = new Network();
            network.Nodes.Add("Exists", new Node("Exists"));
            Assert.Throws<ArgumentException>(() => network.AddConnection("Exists", "DoesNotExist", 6));
        }

        [Test]
        public void AddConnection_ActuallyAddsConnection()
        {
            Network network = new Network();
            network.Nodes.Add("First", new Node("First"));
            network.Nodes.Add("Second", new Node("Second"));
            network.Nodes.Add("Third", new Node("Third"));
            network.AddConnection(network.Nodes["First"].Name, network.Nodes["Second"].Name, 5);
            Assert.IsTrue(network.Nodes["First"].Connections.Count == 1 && network.Nodes["First"].Connections.Count == 1 &&
                network.Nodes["Third"].Connections.Count == 0);
        }

        // Comes in what order?
        [Test]
        public void CompareTo_ReturnsCorrectOrder()
        {
            Network network = new Network();
            network.Nodes.Add("A", new Node("A"));
            network.Nodes.Add("B", new Node("B"));
            network.Nodes.Add("C", new Node("C"));
            network.Nodes.Add("D", new Node("D"));
            network.Nodes.Add("E", new Node("E"));
            network.Nodes.Add("F", new Node("F"));
            network.Nodes.Add("G", new Node("G"));
            network.Nodes.Add("H", new Node("H"));
            network.Nodes.Add("I", new Node("I"));
            network.Nodes.Add("J", new Node("J"));

       
            network.AddConnection("A", "B", 3);
            network.AddConnection("A", "C", 3);
            network.AddConnection("A", "D", 3);
            network.AddConnection("B", "D", 3);
            network.AddConnection("C", "D", 3);
            network.AddConnection("E", "F", 3);
            network.AddConnection("F", "G", 3);

            PriorityQueue<Node> list = new PriorityQueue<Node>();
            foreach (var element in network.Nodes)
            {
                list.Add(element.Value);
            }

            int listCount = list.Count();
            int lowersNumber = -1;
            for (int j = 0; j < listCount; j++)
            {
                int currentValue = list.Pop().Connections.Count;
                if (currentValue < lowersNumber)
                {
                    Assert.Fail($"Failed at iteration {j}. Value was {currentValue} and the lowest was {lowersNumber}");
                }
                else
                {
                    lowersNumber = currentValue;
                }
                
            }


        }
    }
}