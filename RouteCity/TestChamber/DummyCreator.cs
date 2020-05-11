using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;

namespace TestChamber
{
    internal static class DummyCreator
    {
        internal static Network CreateDummyNetworkOfThreeNodesWithNoConnections()
        {
            Network mellerud = new Network();

            mellerud.AddNode("A");
            mellerud.AddNode("B");
            mellerud.AddNode("C");

            return mellerud;
        }

        internal static Network CreateDummyNetworkOfTenNodesWithConnectionsOption1()
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

        internal static Network CreateDummyNetworkOfTenNodesWithConnectionsOption2()
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

