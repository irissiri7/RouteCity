using System;
using System.Diagnostics;
using ClassLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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

            PathFinder p = new PathFinder(karlstad);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            p.FindQuickestPath("A", "J");
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
