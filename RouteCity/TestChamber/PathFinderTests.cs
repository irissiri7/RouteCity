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
            Assert.IsTrue(sut.Result.Count == 3);
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
            Assert.IsTrue(sut.Result.ContainsKey("A"));
            Assert.IsTrue(sut.Result.ContainsKey("B"));
            Assert.IsTrue(sut.Result.ContainsKey("C"));


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
            foreach (KeyValuePair<string, Path> path in sut.Result)
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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 6d;
            List<string> expectedNodesVisited = new List<string> { "A", "C", "F", "E" };

            //ACT
            sut.FindQuickestPath("A", "E");
            double actualQuickestPath = sut.Result["E"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Result["E"].NodesVisited;

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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 12d;
            List<string> expectedNodesVisited = new List<string> { "D", "J", "I", "H" };

            //ACT
            sut.FindQuickestPath("D", "H");
            double actualQuickestPath = sut.Result["H"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Result["H"].NodesVisited;

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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 12d;
            List<string> expectedNodesVisited = new List<string> { "B", "A", "C" };

            //ACT
            sut.FindQuickestPath("B", "C");
            double actualQuickestPath = sut.Result["C"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Result["C"].NodesVisited;

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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 32d;
            List<string> expectedNodesVisited = new List<string> { "A", "C", "F" };

            //ACT
            sut.FindQuickestPath("A", "F");
            double actualQuickestPath = sut.Result["F"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Result["F"].NodesVisited;

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
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption2();
            PathFinder sut = new PathFinder(dummy);
            double expectedQuickestPath = 25d;
            List<string> expectedNodesVisited = new List<string> { "A", "B", "J", "D", "I", "H" };

            //ACT
            sut.FindQuickestPath("A", "H");
            double actualQuickestPath = sut.Result["H"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Result["H"].NodesVisited;

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
            double actualQuickestPath = sut.Result["B"].QuickestTimeFromStart;
            List<string> actualNodesVisited = sut.Result["B"].NodesVisited;

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
            Assert.AreEqual(15, sut.Result["A"].QuickestTimeFromStart);
            Assert.AreEqual(5, sut.Result["B"].QuickestTimeFromStart);
            Assert.AreEqual(13, sut.Result["C"].QuickestTimeFromStart);
            Assert.AreEqual(7, sut.Result["D"].QuickestTimeFromStart);
            Assert.AreEqual(9, sut.Result["I"].QuickestTimeFromStart);
            Assert.AreEqual(0, sut.Result["J"].QuickestTimeFromStart);
            //Should not be explored
            Assert.AreEqual(double.PositiveInfinity, sut.Result["E"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.Result["F"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.Result["G"].QuickestTimeFromStart);
            Assert.AreEqual(double.PositiveInfinity, sut.Result["H"].QuickestTimeFromStart);
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
            foreach(var path in sut.Result.Values)
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
            List<string> actualNodesVisited = sut.Result["J"].NodesVisited;

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
            Assert.Throws<InvalidOperationException>(() => sut.FindQuickestPath("A", "A"), "Start node and end node must be different");


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

            foreach(var i in p.Result.Values)
            {
                if(i.QuickestTimeFromStart == double.PositiveInfinity)
                {
                    Assert.Fail();
                }
            }
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
            Assert.AreEqual(18d, sut.Result["J"].QuickestTimeFromStart);
            
            //ACT, Changing quickest path by adding direct connection
            dummy.AddConnection("A", "J", 1);
            sut.FindQuickestPath("A", "J");
            
            //ASSERT that new quickest path has changed
            Assert.AreEqual(1d, sut.Result["J"].QuickestTimeFromStart);
        }

        [Test]
        public void FindQuickestPath_ChangingConnectionValuesExTwo_UpdatesQuickestPathCorrectly()
        {
            //ARRANGE
            Network dummy = CreateDummyNetworkOfTenNodesWithConnectionsOption1();
            PathFinder sut = new PathFinder(dummy);

            //Original quickest path
            sut.FindQuickestPath("F", "I");
            Assert.AreEqual(16d, sut.Result["I"].QuickestTimeFromStart);

            //ACT, Changing quickest path by adding direct connection
            dummy.AddConnection("F", "I", 1);
            sut.FindQuickestPath("F", "I");

            //ASSERT that new quickest path has changed
            Assert.AreEqual(1d, sut.Result["I"].QuickestTimeFromStart);
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
