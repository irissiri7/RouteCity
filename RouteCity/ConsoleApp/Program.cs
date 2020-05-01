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
            for(int i = 0; i < 1000; i++)
            {
                stockholm.AddNode(i.ToString());
            }
            stockholm.RandomizeConnections();

            PathFinder p = new PathFinder(stockholm);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            p.FindQuickestPath("1", "40");
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
