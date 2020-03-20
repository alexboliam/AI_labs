using System;

namespace Lab1_Uninformative_Search
{
    class Program
    {
        static void Main(string[] args)
        {

            Solver solver = new Solver(); // создаем решатор
            var path = solver.IDDFS(new GangStateNode()); // решаем задачу, возвращается путь

            int n = 1;
            foreach (var state in path) // выводим путь в консоль 
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
