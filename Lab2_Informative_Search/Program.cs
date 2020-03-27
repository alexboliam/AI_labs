using System;

namespace Lab2_Informative_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int[][] a = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                a[i] = new int[2];
                for (int j = 0; j < 2; j++)
                    a[i][j] = 5;
            }
            int[][] b = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                b[i] = new int[2];
                for (int j = 0; j < 2; j++)
                    b[i][j] = 3;
            }
            int[][] c = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                c[i] = new int[2];
                for (int j = 0; j < 2; j++)
                    c[i][j] = 3;
            }

            TableState<int> table = new TableState<int>(a, b);
            Console.WriteLine(table.ArraysEquals(a, b));
            Console.WriteLine(table.ArraysEquals(b, c));
            Console.WriteLine(table.ToString());
        }
    }
}
