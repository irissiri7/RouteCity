using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;
using NUnit.Framework;


namespace TestChamber
{
    class IntegrationTests
    {

        ///INTEGRATION///////////////////////////////////////////////////////////////////////////////////
        [Test]
        public void InitializeResult_PathFinderHasBeenUsedPreviously_DoesResetResultDictionary()
        {
            //ARRANGE
            Network network = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(network);
            sut.FindQuickestPath("A", "D");
            
            //ACT
            sut.InitializeQuickestPathResults("A");

            //ASSERT
            foreach(var path in sut.QuickestPathResults)
            {
                if(path.Key != "A")
                {
                    Assert.IsTrue(double.IsPositiveInfinity(path.Value.QuickestTimeFromStart));
                }
            }
            foreach(var node in network.Nodes)
            {
                Assert.IsTrue(node.Value.visited == false);
            }

        }

        [Test]
        public void FindQuickestPath_UsingPathFinderOnSameNetworkManyTimesWithSamePaths_GivesRightResult()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //Original quickest path
            sut.FindQuickestPath("A", "J");
            Assert.AreEqual(18d, sut.QuickestPathResults["J"].QuickestTimeFromStart);
            sut.FindQuickestPath("A", "J");
            //ASSERT that new quickest path has changed
            Assert.AreEqual(18d, sut.QuickestPathResults["J"].QuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_UsingPathFinderOnSameNetworkManyTimesWithNewPath_GivesRightResult()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //Original quickest path
            sut.FindQuickestPath("A", "J");
            Assert.AreEqual(18d, sut.QuickestPathResults["J"].QuickestTimeFromStart);
            sut.FindQuickestPath("A", "E");
            //ASSERT that new quickest path has changed
            Assert.AreEqual(6d, sut.QuickestPathResults["E"].QuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_UsingPathFinderOnSameNetworkManyTimesAddingNewNodeToNetwork_GivesRightResult()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //Original quickest path
            sut.FindQuickestPath("A", "J");
            Assert.AreEqual(18d, sut.QuickestPathResults["J"].QuickestTimeFromStart);

            //ACT, Adding new node and connection
            dummy.AddNode("K");
            dummy.AddConnection("K", "A", 1);
            dummy.AddConnection("K", "J", 1);
            sut.FindQuickestPath("K", "J");

            //ASSERT
            Assert.AreEqual(1d, sut.QuickestPathResults["J"].QuickestTimeFromStart);
            sut.FindQuickestPath("A", "J");
            Assert.AreEqual(2d, sut.QuickestPathResults["J"].QuickestTimeFromStart);
            
            Assert.AreEqual("A", sut.QuickestPathResults["J"].NodesVisited[0]);
            Assert.AreEqual("K", sut.QuickestPathResults["J"].NodesVisited[1]);
            Assert.AreEqual("J", sut.QuickestPathResults["J"].NodesVisited[2]);
        }

        [Test]
        public void FindQuickestPath_ChangingConnectionValuesExOne_UpdatesQuickestPathCorrectly()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);
            
            //Original quickest path
            sut.FindQuickestPath("A", "J");
            Assert.AreEqual(18d, sut.QuickestPathResults["J"].QuickestTimeFromStart);
            
            //ACT, Changing quickest path by adding direct connection
            dummy.AddConnection("A", "J", 1);
            sut.FindQuickestPath("A", "J");
            
            //ASSERT that new quickest path has changed
            Assert.AreEqual(1d, sut.QuickestPathResults["J"].QuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_ChangingConnectionValuesExTwo_UpdatesQuickestPathCorrectly()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //Original quickest path
            sut.FindQuickestPath("F", "I");
            Assert.AreEqual(16d, sut.QuickestPathResults["I"].QuickestTimeFromStart);

            //ACT, Changing quickest path by adding direct connection
            dummy.AddConnection("F", "I", 1);
            sut.FindQuickestPath("F", "I");

            //ASSERT that new quickest path has changed
            Assert.AreEqual(1d, sut.QuickestPathResults["I"].QuickestTimeFromStart);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
