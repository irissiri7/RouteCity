using System;
using System.Diagnostics;
using ClassLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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

            PathFinder p = new PathFinder(stockholm);

            Console.WriteLine(p.FindQuickestPath("A", "J", false));
            
        }
    }
}
