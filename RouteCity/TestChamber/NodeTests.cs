using NUnit.Framework;
using ClassLibrary;
using System;

namespace TestChamber
{
    public class NodeTests
    {
        [Test]
        public void Connect_HappyDays()
        {
            //Arrange
            Node sut1 = new Node("Test1");
            Node sut2 = new Node("Test2");

            //Act
            sut1.Connect(sut2, 2);

            //Assert
            Assert.IsTrue(sut1.Connections.Count == 1);
            Assert.IsTrue(sut2.Connections.Count == 1);
            
            Assert.AreSame(sut1.Connections[0].TargetNode, sut2);
            Assert.AreEqual(sut1.Connections[0].TimeCost, 2);

            Assert.AreSame(sut2.Connections[0].TargetNode, sut1);
            Assert.AreEqual(sut2.Connections[0].TimeCost, 2);


        }

        [Test]
        public void Connect_TargetNodeIsNull_ThrowsRightException()
        {
            Node sut = new Node("Test");
            Assert.Throws<ArgumentException>(() => sut.Connect(null, 1), "Target node is null");
        }

        //Unnecessary?
        [Test]
        public void Connect_TargetNodeIsNullAndTimeCostIsNegative_ThrowsRightException()
        {
            Node sut = new Node("Test");
            Assert.Throws<ArgumentException>(() => sut.Connect(null, -1), "Target node is null");
        }

        [Test]
        public void Connect_TryingToAddConnectionToItself_ThrowsRightException()
        {
            Node sut = new Node("Test");

            Assert.Throws<ArgumentException>(() => sut.Connect(sut, 1), "Can not add connection to itself");
        }

        //Unnecessary?
        [Test]
        public void Connect_TryingToAddConnectionToItselfAndTimeCostIsNegative_ThrowsRightException()
        {
            Node sut = new Node("Test");

            Assert.Throws<ArgumentException>(() => sut.Connect(sut, -1), "Can not add connection to itself");
        }


        [Test]
        public void Connect_TimeCostIsNegative_ThrowsRightException()
        {
            Node sut1 = new Node("Test1");
            Node sut2 = new Node("Test2");

            Assert.Throws<ArgumentException>(() => sut1.Connect(sut2, -1), "Time cost must be a positive number");
        }

    }
}