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
            Network dummyNetwork = DummyCreator.CreateDummyNetworkOfThreeNodesWithNoConnections();
            PathFinder sut = new PathFinder(dummyNetwork);
            Assert.IsNotNull(sut);
        }

        [Test]
        public void CreatePathFinder_NetworkIsNull_ThrowsRightException()
        {
            Assert.Throws<InvalidOperationException>(() => new PathFinder(null), "Can not create a Pathfinder if Network is null");
        }

        [Test]
        public void CreatePathFinder_NetworkHasLessThanThreeNodes_ThrowsRightException()
        {
            Network dummyNetwork = new Network();
            dummyNetwork.AddNode("A");
            dummyNetwork.AddNode("B");
            Assert.Throws<InvalidOperationException>(() => new PathFinder(dummyNetwork), "Can not create a Pathfinder if Network has less than three nodes");
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Test]
        public void InitializePaths_HappyDays_HasAddedThreeEntriesToPathDictionary()
        {
            //Arrange
            Network dummyNetwork = DummyCreator.CreateDummyNetworkOfThreeNodesWithNoConnections(); //Has nodes "A", "B", "C"
            PathFinder sut = new PathFinder(dummyNetwork);

            //Act
            sut.InitializeQuickestPathResults("A");

            //Assert
            Assert.IsTrue(sut.QuickestPathResults.Count == 3);
        }

        [Test]
        public void InitializePaths_HappyDays_HasAddedRightKeysToPathDictionary()
        {
            //Arrange
            Network dummyNetwork = DummyCreator.CreateDummyNetworkOfThreeNodesWithNoConnections(); //Has nodes "A", "B", "C"
            PathFinder sut = new PathFinder(dummyNetwork);

            //Act
            sut.InitializeQuickestPathResults("A");

            //Assert
            Assert.IsTrue(sut.QuickestPathResults.ContainsKey("A"));
            Assert.IsTrue(sut.QuickestPathResults.ContainsKey("B"));
            Assert.IsTrue(sut.QuickestPathResults.ContainsKey("C"));


        }

        [Test]
        public void InitializePaths_HappyDays_HasAddedRightPathObjectsToPathDictionary()
        {
            //Arrange
            Network dummyNetwork = DummyCreator.CreateDummyNetworkOfThreeNodesWithNoConnections(); //Has nodes "A", "B", "C"
            PathFinder sut = new PathFinder(dummyNetwork);
            string startNode = "A";

            //Act
            sut.InitializeQuickestPathResults(startNode);

            //Assert
            foreach (KeyValuePair<string, Path> path in sut.QuickestPathResults)
            {
                Assert.IsTrue(dummyNetwork.Nodes.ContainsKey(path.Value.Node.Name));
                Assert.IsTrue(path.Value.NodesVisited.Count == 1);
                if (path.Key == startNode)
                {
                    Assert.AreEqual(path.Value.QuickestTimeFromStart, 0);
                }
                else
                {
                    Assert.AreEqual(path.Value.QuickestTimeFromStart, double.PositiveInfinity);

                }
            }


        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Test]
        public void FindQuickestPath_HappyDaysScenarioOne_GivesRightResult()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 6d;
            List<string> expectedNodesVisited = new List<string> { "A", "C", "F", "E" };

            //ACT
            sut.FindQuickestPath("A", "E");
            double actualQuickestPath = sut.QuickestPathResults["E"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.QuickestPathResults["E"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_HappyDaysScenarioTwo_GivesRightResult()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 12d;
            List<string> expectedNodesVisited = new List<string> { "D", "J", "I", "H" };

            //ACT
            sut.FindQuickestPath("D", "H");
            double actualQuickestPath = sut.QuickestPathResults["H"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.QuickestPathResults["H"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_HappyDaysScenarioThree_GivesRightResult()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 12d;
            List<string> expectedNodesVisited = new List<string> { "B", "A", "C" };

            //ACT
            sut.FindQuickestPath("B", "C");
            double actualQuickestPath = sut.QuickestPathResults["C"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.QuickestPathResults["C"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_HappyDaysScenarioFour_GivesRightResult()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 32d;
            List<string> expectedNodesVisited = new List<string> { "A", "C", "F" };

            //ACT
            sut.FindQuickestPath("A", "F");
            double actualQuickestPath = sut.QuickestPathResults["F"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.QuickestPathResults["F"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_HappyDaysScenarioFive_GivesRightResult()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 25d;
            List<string> expectedNodesVisited = new List<string> { "A", "B", "J", "D", "I", "H" };

            //ACT
            sut.FindQuickestPath("A", "H");
            double actualQuickestPath = sut.QuickestPathResults["H"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.QuickestPathResults["H"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_FindsTwoEqualPaths_SticksWithTheFirstFoundOne()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 10d;
            List<string> expectedNodesVisited = new List<string> { "A", "B" };

            //ACT
            sut.FindQuickestPath("A", "B");
            double actualQuickestPath = sut.QuickestPathResults["B"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.QuickestPathResults["B"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_StoppingAtEndNode_RightPathsShouldBeExploredAndNotExplored()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);


            //ACT
            sut.FindQuickestPath("J", "D");

            //ASSERT
            //Should be explored
            Assert.AreEqual(15, sut.QuickestPathResults["A"].QuickestTimeFromStart);
            Assert.AreEqual(5, sut.QuickestPathResults["B"].QuickestTimeFromStart);
            Assert.AreEqual(13, sut.QuickestPathResults["C"].QuickestTimeFromStart);
            Assert.AreEqual(7, sut.QuickestPathResults["D"].QuickestTimeFromStart);
            Assert.AreEqual(9, sut.QuickestPathResults["I"].QuickestTimeFromStart);
            Assert.AreEqual(0, sut.QuickestPathResults["J"].QuickestTimeFromStart);
            //Should not be explored
            Assert.AreEqual(double.PositiveInfinity, sut.QuickestPathResults["E"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.QuickestPathResults["F"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.QuickestPathResults["G"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.QuickestPathResults["H"].QuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_NOTStoppingAtEndNode_NoPathsShouldBeUnexplored()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT
            sut.FindQuickestPath("A", "E", false);

            //ASSERT
            foreach (var path in sut.QuickestPathResults.Values)
            {
                Assert.IsTrue(path.QuickestTimeFromStart != double.PositiveInfinity);
            }
        }

        [Test]
        public void FindQuickestPath_NOTStoppingAtEndNode_NodesVisitedForNodeJAreCorrect()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);
            List<string> expectedNodesVisited = new List<string> { "A", "C", "F", "E", "D", "J" };


            //ACT
            sut.FindQuickestPath("A", "E", false);
            List<string> actualNodesVisited = sut.QuickestPathResults["J"].NodesVisited;

            //ASSERT
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_StartNodeAndEndNodeAreSame_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<ArgumentException>(() => sut.FindQuickestPath("A", "A"), "Start node and end node must be different");


        }

        [Test]
        public void FindQuickestPath_StartNodeDoesNotExist_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<ArgumentException>(() => sut.FindQuickestPath("X", "A"), "Both start and end node must be in network");
        }

        [Test]
        public void FindQuickestPath_EndNodeDoesNotExist_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<ArgumentException>(() => sut.FindQuickestPath("A", "X"), "Both start and end node must be in network");
        }

        [Test]
        public void FindQuickestPath_StartNodeIsNull_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<ArgumentNullException>(() => sut.FindQuickestPath(null, "A"), "Can not preform operation if nodes are null");
        }

        [Test]
        public void FindQuickestPath_EndNodeIsNull_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = DummyCreator.CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<ArgumentNullException>(() => sut.FindQuickestPath("A", null), "Can not preform operation if nodes are null");
        }

        [Test]
        public void FindQuickestPath_NOTStoppingAtEndNode_NoPathsShouldBeUnexploredEXTENDED()
        {
            Network mellerud = new Network();

            mellerud.AddNode("A");
            mellerud.AddNode("B");
            mellerud.AddNode("C");
            mellerud.AddNode("D");
            mellerud.AddNode("E");
            mellerud.AddNode("F");
            mellerud.AddNode("G");
            mellerud.AddNode("H");
            mellerud.AddNode("I");
            mellerud.AddNode("J");

            mellerud.AddConnection("A", "B", 10);
            mellerud.AddConnection("A", "D", 10);
            mellerud.AddConnection("A", "J", 10);
            mellerud.AddConnection("B", "F", 10);
            mellerud.AddConnection("B", "E", 10);
            mellerud.AddConnection("C", "D", 10);
            mellerud.AddConnection("C", "E", 10);
            mellerud.AddConnection("C", "H", 10);
            mellerud.AddConnection("F", "E", 10);
            mellerud.AddConnection("J", "F", 10);
            mellerud.AddConnection("H", "G", 10);
            mellerud.AddConnection("H", "I", 10);
            mellerud.AddConnection("I", "G", 1);

            PathFinder p = new PathFinder(mellerud);
            p.FindQuickestPath("G", "D", false);

            foreach (var i in p.QuickestPathResults.Values)
            {
                if (i.QuickestTimeFromStart == double.PositiveInfinity)
                {
                    Assert.Fail();
                }
            }
        }

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
            foreach (var path in sut.QuickestPathResults)
            {
                if (path.Key != "A")
                {
                    Assert.IsTrue(double.IsPositiveInfinity(path.Value.QuickestTimeFromStart));
                }
            }
            foreach (var node in network.Nodes)
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
    }
}
