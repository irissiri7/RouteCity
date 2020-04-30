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
            for(int i = 0; i < 100; i++)
            {
                stockholm.AddNode(i.ToString());
            }
            stockholm.RandomizeConnections();

            PathFinder p = new PathFinder(stockholm);
            OldPathfinder op = new OldPathfinder(stockholm);
            Stopwatch watch = new Stopwatch();
            
            //Första testet med vår PriorityQueue
            watch.Start();
            p.FindQuickestPath("1", "40");
            watch.Stop();
            Console.WriteLine("With PQ " + watch.ElapsedMilliseconds);
            
            watch.Reset();
            
            //Andra testet utan vår PriorityQueue
            watch.Start();
            op.FindQuickestPath("1", "40");
            watch.Stop();
            Console.WriteLine("Without PQ " + watch.ElapsedMilliseconds);

        }
    }
}
