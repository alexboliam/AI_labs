using System;

namespace Lab1_Uninformative_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Solver solver = new Solver();
            var path = solver.IDDFS(new GangStateNode());

            int n = 1;
            foreach (var state in path)
            {
                GangStateNode nextState = state;

                Console.WriteLine(n.ToString() + " ");

                if (nextState.IsSolution())
                    Console.WriteLine("Solution");

                Console.WriteLine(nextState.ToString());
                n++;
            }

            Console.ReadKey();
        }
    }
}
