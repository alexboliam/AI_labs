using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Informative_Search
{
    public class AStarSearch<T> where T:IComparable
    {
        #region Properties
        public TableState<T> RootState { get; set; } // начальная расстановка
        public TableState<T> Target { get; set; } // целевое размещение
        public Dictionary<TableState<T>, int> costSoFar = // стоимости шагов
            new Dictionary<TableState<T>, int>();
        #endregion

        #region Ctors
        public AStarSearch(TableState<T> RootState)
        {
            this.RootState = RootState;
        }
        #endregion

        public int Heuristic(TableState<T> state) // эвристическая функция, высчитываем количество неправильных размещений фишек на данном шаге
        {
            int WrongPositions = 0; 

            // если позиция фишки не совпадает с искомой, увеличиваем значение функции
            for (int i = 0; i < state.CurrentTable.Length; i++)
            {
                for (int j = 0; j < state.CurrentTable[i].Length; j++)
                {
                    if (!state.CurrentTable[i][j].Equals(state.TargetTable[i][j]))
                        WrongPositions++;
                }
            }
            return WrongPositions;
        }
        public void Print()
        {
            // ищем обратный путь
            LinkedList<TableState<T>> path = new LinkedList<TableState<T>>();
            var end = Target;
            while (end != null)
            {
                path.AddFirst(end);
                end = end.Parent;
            }

            //выводим все в консоль
            int counter = 1;
            foreach (var item in path)
            {
                var wrongs = 0;
                costSoFar.TryGetValue(item, out wrongs);
                Console.WriteLine($"Step #{counter}.  Number of wrong positions: {wrongs}");
                Console.WriteLine( item.ToString() ); 
                counter++;
            }
        }
        public void Solve() // поиск пути
        {
            var frontier = new PriorityQueue<TableState<T>>(); // очередь с приоритетами, достается для рассмотрения элемент с наименьшей стоимостью (высшим приоритетом)
            frontier.Enqueue(RootState, 0); // добавляем на рассмотрение стартовое расположение

            costSoFar[RootState] = Heuristic(RootState);

            while(frontier.Count > 0) // проходим по всем возможным шагам
            {
                var current = frontier.Dequeue(); // достаем приоритетное расположение из очереди

                if(current.IsTargetState()) // если оно есть конечным, записываем это расположение и завершаем поиск
                {
                    Target = current;
                    break;
                }

                current.FindMoves(); // ищем возможные перемещения из данного расположения
                foreach (var next in current.Moves) // проверяем эти перемещения
                {
                    int newCost = Heuristic(next); // записываем стоимость (в нашем случае к-во неправильных размещений фишек)
                    if(!costSoFar.ContainsKey(next)) // если размещение еще не рассматривалось
                    {   
                        // то записываем его в словарь стоимостей и добавляем в очередь на рассмотрение
                        costSoFar[next] = newCost;
                        frontier.Enqueue(next, newCost);
                    }
                }
            }
        }

        
    }
}
