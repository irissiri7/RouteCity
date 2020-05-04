using System;
using System.Diagnostics;
using ClassLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            PriorityQueue<int> test = new PriorityQueue<int>();
            Random rdn = new Random();

            for(int i = 0; i < 1000; i++)
            {
                test.Add(rdn.Next(100));
            }

            for(int j = 0; j < 1000; j++)
            {
                int prev = -1;
                int temp = test.Pop();
                
            }
        }
    }
}
