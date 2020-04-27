using NUnit.Framework;
using ClassLibrary;
using System;
using System.Collections.Generic;

namespace TestChamber
{
    public class NetworkTests
    {
        // CreateNetwork()
        [Test]
        public void CreateNetwork_RandomConnections_RespectsMinAndMaxConnections()
        {
            Network network = new Network();
            List<string> fiveElements = new List<string>() { "one", "two", "three", "four", "five" };
            network.CreateNetwork(fiveElements);

            foreach (var element in network.Nodes)
            {
                if (element.Value.Connections.Count < 2 || element.Value.Connections.Count > 3)
                {
                    Assert.Fail();
                }
            }

            Assert.IsTrue(network.Nodes.Count > 0);
        }

        [Test]
        public void CreateNetwork_RandomConnections_AllNodesAreIndirectlyReachableFromEveryNode()
        {
            // Implement when djikstras is done
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
        public void RandomizeConnections_ThereAreAlreadyConnectionsEstablished_ReturnsArgumentException()
        {
            Network network = new Network();
            network.Nodes.Add("A", new Node("A"));
            network.Nodes.Add("B", new Node("B"));
            network.Nodes.Add("C", new Node("C"));
            network.Nodes.Add("D", new Node("D"));
            network.Nodes["A"].Connect(network.Nodes["B"], 3);
            
            Assert.Throws<ArgumentException>(() => network.RandomizeConnections());
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
            network.AddConnection(network.Nodes["First"], network.Nodes["Second"], 5);
            Assert.IsTrue(network.Nodes["Exists"].Connections.Count == 1);
        }
    }
}