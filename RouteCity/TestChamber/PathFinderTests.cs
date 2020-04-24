using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;
using NUnit.Framework;


namespace TestChamber
{
    class PathFinderTests
    {
        [Test]
        public void CreatePathFinder_Happydays()
        {
            Network dummyNetwork = CreateDummyNetwork();
            PathFinder sut = new PathFinder(dummyNetwork);
            Assert.IsNotNull(sut);
        }

        [Test]
        public void CreatePathFinder_NetworkIsNull_ThrowsRightException()
        {
            Assert.Throws<InvalidOperationException>(() => new PathFinder(null), "Can not create a Pathfinder if Network is null");
        }

        [Test]
        public void CreatePathFinder_NetworkHasZeroNodes_ThrowsRightException()
        {
            Network dummyNetwork = new Network();
            Assert.Throws<InvalidOperationException>(() => new PathFinder(dummyNetwork), "Can not create a Pathfinder if Network has 0 nodes");
        }

        // These test needs to be rewritten when AddNode() has been implemented, in the mean time
        // using CreateDummyNetwork() to "hard code" nodes in the network
        [Test]
        public void InitializePaths_HappyDays_HasAddedThreeEntriesToPathDictionary()
        {
            //Arrange
            Network dummyNetwork = CreateDummyNetwork(); //Has nodes "A", "B", "C"
            PathFinder sut = new PathFinder(dummyNetwork);

            //Act
            sut.InitializePaths("A");

            //Assert
            Assert.IsTrue(sut.Paths.Count == 3);
        }

        [Test]
        public void InitializePaths_HappyDays_HasAddedRightKeysToPathDictionary()
        {
            //Arrange
            Network dummyNetwork = CreateDummyNetwork(); //Has nodes "A", "B", "C"
            PathFinder sut = new PathFinder(dummyNetwork);

            //Act
            sut.InitializePaths("A");

            //Assert
            Assert.IsTrue(sut.Paths.ContainsKey("A"));
            Assert.IsTrue(sut.Paths.ContainsKey("B"));
            Assert.IsTrue(sut.Paths.ContainsKey("C"));


        }

        [Test]
        public void InitializePaths_HappyDays_HasAddedRightPathObjectsToPathDictionary()
        {
            //Arrange
            Network dummyNetwork = CreateDummyNetwork(); //Has nodes "A", "B", "C"
            PathFinder sut = new PathFinder(dummyNetwork);
            string startNode = "A";

            //Act
            sut.InitializePaths(startNode);

            //Assert
            foreach (KeyValuePair<string, Path> path in sut.Paths)
            {
                Assert.IsTrue(dummyNetwork.Nodes.ContainsKey(path.Value.Node));
                Assert.IsTrue(path.Value.NodesVisited.Count == 1);
                if(path.Key == startNode)
                {
                    Assert.AreEqual(path.Value.ShortestTimeFromStart, 0);
                }
                else
                {
                    Assert.AreEqual(path.Value.ShortestTimeFromStart, double.PositiveInfinity);

                }
            }
            

        }


        //Helper methods, will be modified when AddNode is implemented
        public Network CreateDummyNetwork()
        {
            Network dummyNetwork = new Network();
            List<string> nodes = new List<string> { "A", "B", "C" };
            foreach (var node in nodes)
            {
                dummyNetwork.Nodes.Add(node, new Node(node));
            }

            return dummyNetwork;
        }
    }
}
