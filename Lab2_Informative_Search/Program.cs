using System;

namespace Lab2_Informative_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            // создаем наши размещения
            int[][] start = new int[][] { new int[]{ 2, 8, 3 }, new int[]{ 1, 6, 4 }, new int[]{ 7, 0, 5 } };
            int[][] target = new int[][] { new int[] { 1, 2, 3 }, new int[] { 8, 0, 4 }, new int[] { 7, 6, 5 } };

            // создаем расстановку на основе введенных данных
            TableState<int> initTable = new TableState<int>(start, target);

            //создаем экземпляр поисковика
            AStarSearch<int> aStar = new AStarSearch<int>(initTable);

            //ищем и выводим решение
            aStar.Solve();
            aStar.Print();
        }
    }
}
