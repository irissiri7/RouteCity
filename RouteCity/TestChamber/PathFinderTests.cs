﻿using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;
using NUnit.Framework;


namespace TestChamber
{
    class PathFinderTests
    {
        //Creating PathFinder////////////////////////////////////////////////////////////////////////////////////////////////

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

        //InitializeQuickestPathResults()////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Test]
        public void InitializeQuickestPathResults_HappyDays_HasAddedThreeEntriesToPathDictionary()
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
        public void InitializeQuickestPathResults_HappyDays_HasAddedRightKeysToPathDictionary()
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
        public void InitializeQuickestPathResults_HappyDays_HasAddedRightPathObjectsToPathDictionary()
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
        
        //FindQuickestPath()/////////////////////////////////////////////////////////////////////////////////////////////////////////

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
            //Arrange
            List<string> nodes = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            
            for(int i = 0; i < 10000; i++)
            {
                Network dummy = new Network();
                dummy.CreateNetwork(nodes);
                PathFinder sut = new PathFinder(dummy);
                
                //Act
                sut.FindQuickestPath("A", "E", false);
                
                //Assert
                foreach (var path in sut.QuickestPathResults.Values)
                {
                    Assert.IsTrue(path.QuickestTimeFromStart != double.PositiveInfinity);
                }
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


    }
}
