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
            Network dummyNetwork = CreateDummyNetworkOfThreeNodesWithNoConnections();
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
            Network dummyNetwork = CreateDummyNetworkOfThreeNodesWithNoConnections(); //Has nodes "A", "B", "C"
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
            Network dummyNetwork = CreateDummyNetworkOfThreeNodesWithNoConnections(); //Has nodes "A", "B", "C"
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
            Network dummyNetwork = CreateDummyNetworkOfThreeNodesWithNoConnections(); //Has nodes "A", "B", "C"
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
        public void FindQuickestPath_HappyDaysExOne_GivesRightResult()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 6d;
            List<string> expectedNodesVisited = new List<string> { "A", "C", "F", "E" };
            
            //ACT
            sut.FindQuickestPath("A", "E");
            double actualQuickestPath = sut.Paths["E"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Paths["E"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for(int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_HappyDaysExTwo_GivesRightResult()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 12d;
            List<string> expectedNodesVisited = new List<string> { "D", "J", "I", "H" };

            //ACT
            sut.FindQuickestPath("D", "H");
            double actualQuickestPath = sut.Paths["H"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Paths["H"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_HappyDaysExThree_GivesRightResult()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 12d;
            List<string> expectedNodesVisited = new List<string> { "B", "A", "C" };

            //ACT
            sut.FindQuickestPath("B", "C");
            double actualQuickestPath = sut.Paths["C"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Paths["C"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_StoppingAtEndNode_ShortestTimeFromStartForNodeJShouldStillBeInfinite()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestTimeFromStart = double.PositiveInfinity;

            //ACT
            sut.FindQuickestPath("A", "E");
            double actualQuickestTimeFromStart = sut.Paths["J"].QuickestTimeFromStart;

            //ASSERT
            Assert.AreEqual(expectedQuickestTimeFromStart, actualQuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_StoppingAtEndNode_NodesVisitedForNodeJAreCorrect()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);
            List<string> expectedNodesVisited = new List<string> {"J" };

            //ACT
            sut.FindQuickestPath("A", "E");
            List<string> actualNodesVisited = sut.Paths["J"].NodesVisited;

            //ASSERT
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_NOTStoppingAtEndNode_ShortestTimeFromStartForNodeJShouldBeEvaluated()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestTimeFromStart = 18d;

            //ACT
            sut.FindQuickestPath("A", "E", false);
            double actualQuickestTimeFromStart = sut.Paths["J"].QuickestTimeFromStart;

            //ASSERT
            Assert.AreEqual(expectedQuickestTimeFromStart, actualQuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_NOTStoppingAtEndNode_NodesVisitedForNodeJAreCorrect()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);
            List<string> expectedNodesVisited = new List<string> { "A", "C", "F", "E", "D", "J" };


            //ACT
            sut.FindQuickestPath("A", "E", false);
            List<string> actualNodesVisited = sut.Paths["J"].NodesVisited;

            //ASSERT
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        //Integration??
        [Test]
        public void FindQuickestPath_ChangingConnectionValues_UpdatesQuickestPathCorrectly()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);
            
            //Original quickest path
            sut.FindQuickestPath("A", "J");
            Assert.AreEqual(18d, sut.Paths["J"].QuickestTimeFromStart);
            
            //ACT, Changing quickest path by adding direct connection
            dummy.AddConnection("A", "J", 1);
            sut.FindQuickestPath("A", "J");
            
            //ASSERT that new quickest path has changed
            Assert.AreEqual(1d, sut.Paths["J"].QuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_StartNodeAndEndNodeAreSame_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("A","A"), "Start node and end node must be different");


        }

        [Test]
        public void FindQuickestPath_StartNodeDoesNotExist_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("X", "A"), "Both start and end node must be in network");
        }

        [Test]
        public void FindQuickestPath_EndNodeDoesNotExist_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("A", "X"), "Both start and end node must be in network");
        }

        [Test]
        public void FindQuickestPath_StartNodeIsNull_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath(null, "A"), "Can not preform operation if nodes are null");
        }

        [Test]
        public void FindQuickestPath_EndNodeIsNull_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnections();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("A", null), "Can not preform operation if nodes are null");
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Network CreateDummyNetworkOfThreeNodesWithNoConnections()
        {
            Network mellerud = new Network();
            
            mellerud.AddNode("A");
            mellerud.AddNode("B");
            mellerud.AddNode("C");

            return mellerud;
        }

        public Network CreateDummyNetworkOfTenNodesWithConnections()
        {
            Network karlstad = new Network();

            karlstad.AddNode("A");
            karlstad.AddNode("B");
            karlstad.AddNode("C");
            karlstad.AddNode("D");
            karlstad.AddNode("E");
            karlstad.AddNode("F");
            karlstad.AddNode("G");
            karlstad.AddNode("H");
            karlstad.AddNode("I");
            karlstad.AddNode("J");

            karlstad.AddConnection("A", "B", 10);
            karlstad.AddConnection("A", "C", 2);
            karlstad.AddConnection("B", "C", 22);
            karlstad.AddConnection("B", "D", 22);
            karlstad.AddConnection("D", "E", 4);
            karlstad.AddConnection("E", "F", 3);
            karlstad.AddConnection("F", "C", 1);
            karlstad.AddConnection("D", "J", 8);
            karlstad.AddConnection("I", "J", 1);
            karlstad.AddConnection("I", "H", 3);
            karlstad.AddConnection("G", "H", 10);
            karlstad.AddConnection("G", "E", 1);

            return karlstad;
        }
    }
}
