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
                Assert.IsTrue(dummyNetwork.Nodes.ContainsKey(path.Value.Node.Name));
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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
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
        public void FindQuickestPath_HappyDaysExFour_GivesRightResult()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 32d;
            List<string> expectedNodesVisited = new List<string> { "A", "C", "F" };

            //ACT
            sut.FindQuickestPath("A", "F");
            double actualQuickestPath = sut.Paths["F"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Paths["F"].NodesVisited;

            //ASSERT
            Assert.AreEqual(expectedQuickestPath, actualQuickestPath);
            for (int i = 0; i < expectedNodesVisited.Count; i++)
            {
                Assert.AreEqual(expectedNodesVisited[i], actualNodesVisited[i]);
            }
        }

        [Test]
        public void FindQuickestPath_HappyDaysExFive_GivesRightResult()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 25d;
            List<string> expectedNodesVisited = new List<string> { "A", "B", "J", "D", "I", "H" };

            //ACT
            sut.FindQuickestPath("A", "H");
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
        public void FindQuickestPath_FindsTwoEqualPaths_SticksWithTheFirstFoundOne()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 10d;
            List<string> expectedNodesVisited = new List<string> { "A", "B"};

            //ACT
            sut.FindQuickestPath("A", "B");
            double actualQuickestPath = sut.Paths["B"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Paths["B"].NodesVisited;

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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            

            //ACT
            sut.FindQuickestPath("J", "D");

            //ASSERT
            //Should be explored
            Assert.AreEqual(15, sut.Paths["A"].QuickestTimeFromStart);
            Assert.AreEqual(5, sut.Paths["B"].QuickestTimeFromStart);
            Assert.AreEqual(13, sut.Paths["C"].QuickestTimeFromStart);
            Assert.AreEqual(7, sut.Paths["D"].QuickestTimeFromStart);
            Assert.AreEqual(9, sut.Paths["I"].QuickestTimeFromStart);
            Assert.AreEqual(0, sut.Paths["J"].QuickestTimeFromStart);
            //Should not be explored
            Assert.AreEqual(double.PositiveInfinity, sut.Paths["E"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.Paths["F"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.Paths["G"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.Paths["H"].QuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_NOTStoppingAtEndNode_NoPathsShouldBeUnexplored()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT
            sut.FindQuickestPath("A", "E", false);

            //ASSERT
            foreach(var path in sut.Paths.Values)
            {
                Assert.IsTrue(path.QuickestTimeFromStart != double.PositiveInfinity);
            }
        }

        [Test]
        public void FindQuickestPath_NOTStoppingAtEndNode_NodesVisitedForNodeJAreCorrect()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
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

        [Test]
        public void FindQuickestPath_StartNodeAndEndNodeAreSame_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("A","A"), "Start node and end node must be different");


        }

        [Test]
        public void FindQuickestPath_StartNodeDoesNotExist_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("X", "A"), "Both start and end node must be in network");
        }

        [Test]
        public void FindQuickestPath_EndNodeDoesNotExist_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("A", "X"), "Both start and end node must be in network");
        }

        [Test]
        public void FindQuickestPath_StartNodeIsNull_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath(null, "A"), "Can not preform operation if nodes are null");
        }

        [Test]
        public void FindQuickestPath_EndNodeIsNull_ThrowsRightException()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //ACT/ASSERT
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("A", null), "Can not preform operation if nodes are null");
        }

        ///INTEGRATION///////////////////////////////////////////////////////////////////////////////////
        
        [Test]
        public void FindQuickestPath_ChangingConnectionValuesExOne_UpdatesQuickestPathCorrectly()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
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
        public void FindQuickestPath_ChangingConnectionValuesExTwo_UpdatesQuickestPathCorrectly()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //Original quickest path
            sut.FindQuickestPath("F", "I");
            Assert.AreEqual(16d, sut.Paths["I"].QuickestTimeFromStart);

            //ACT, Changing quickest path by adding direct connection
            dummy.AddConnection("F", "I", 1);
            sut.FindQuickestPath("F", "I");

            //ASSERT that new quickest path has changed
            Assert.AreEqual(1d, sut.Paths["I"].QuickestTimeFromStart);
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

        public Network CreateDummyNetworkOfTenNodesWithConnectionsOption1()
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

        public Network CreateDummyNetworkOfTenNodesWithConnectionsOption2()
        {
            Network stockholm = new Network();

            stockholm.AddNode("A");
            stockholm.AddNode("B");
            stockholm.AddNode("C");
            stockholm.AddNode("D");
            stockholm.AddNode("E");
            stockholm.AddNode("F");
            stockholm.AddNode("G");
            stockholm.AddNode("H");
            stockholm.AddNode("I");
            stockholm.AddNode("J");

            stockholm.AddConnection("A", "B", 10);
            stockholm.AddConnection("A", "C", 2);
            stockholm.AddConnection("C", "B", 8);
            stockholm.AddConnection("B", "J", 5);
            stockholm.AddConnection("J", "D", 7);
            stockholm.AddConnection("D", "I", 2);
            stockholm.AddConnection("I", "H", 1);
            stockholm.AddConnection("H", "F", 9);
            stockholm.AddConnection("H", "G", 2);
            stockholm.AddConnection("F", "E", 4);
            stockholm.AddConnection("F", "C", 30);
            stockholm.AddConnection("E", "G", 8);



            return stockholm;
        }
    }
}
