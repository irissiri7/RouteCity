using NUnit.Framework;
using ClassLibrary;
using System;
using System.Collections.Generic;

namespace TestChamber
{
    public class NetworkTests
    {
        [Test]
        public void CreateNetwork_RandomConnections_RespectsMinAndMaxConnections()
        {
            Network network = new Network();
            List<string> fiveElements = new List<string>() { "one", "two", "three", "four", "five" };
            network.CreateNetwork(fiveElements);
            // Have a minimum and a maximum?
            // can i promise that all should have max if min and max are the same? Automatic minimum?
        }

        [Test]
        public void CreateNetwork_RandomConnections_AllNodesAreIndirectlyReachableFromEveryNode()
        {
            
        }

        [Test]
        public void CreateNetwork_ListIsNull_ReturnsNullReferenceException()
        {
            Network network = new Network();
            List<string> notInitialized = null;
            Assert.Throws<ArgumentNullException>(() => network.CreateNetwork(notInitialized));
        }

        [Test]
        public void CreateNetwork_ListCountIsBelow3_ReturnsInvalidOperationException()
        {
            Network network = new Network();
            List<string> onlyTwoElements = new List<string>() { "one", "two" };
            Assert.Throws<ArgumentException>(() => network.CreateNetwork(onlyTwoElements));
        }

        //[Test]
        //public void CreateNetwork_MaxConnectionsIsTooHigh_ReturnsInvalidOperationException()
        //{
        //    Network network = new Network();
        //    List<string> fiveElements = new List<string>() { "one", "two", "three", "four", "five" };
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, 5));
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, 6));
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, 15));
        //}

        //[Test]
        //public void CreateNetwork_MaxConnectionsIsTooLow_ReturnsInvalidOperationException()
        //{
        //    Network network = new Network();
        //    List<string> fiveElements = new List<string>() { "one", "two", "three", "four", "five" };
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, 0));
        //    Assert.Throws<ArgumentException>(() => network.CreateNetwork(fiveElements, -1));
        //}
    }
}