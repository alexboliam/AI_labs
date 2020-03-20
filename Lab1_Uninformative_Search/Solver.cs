using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_Uninformative_Search
{
    public class Solver
    {
        public LinkedList<GangStateNode> IDDFS(GangStateNode root) // метод поиска в глубь с итеративным углублением
        {
            int depth = 0; // сначала глубина равна 0
            while(true) // проходим по дереву методом ограниченного поиска вглубь, увеличивая постепенно максимальную глубину
            {
                var result = DLS(root, depth); // ищем методом огр. поиска вглубь нашу цель
                if (result != null && result.IsSolution()) // если она нашлась, восстанавливаем путь и возвращаем его
                    return FindPath(result);
                depth += 1; // если не нашлась, увеличиваем глубину
            }
        }
        public GangStateNode DLS(GangStateNode node, int depth) // метод поиска вглубь с ограничением глубины
        {
            if (depth == 0 && node.IsSolution()) // проверяем есть ли состояние искомым, когда текущая глубина равна нулю
                return node;
            else if (depth > 0) 
            {
                var moves = node.GetPossibleMoves(); // высчитываем все возможные ходы из данного состояния
                foreach (var child in moves) // рекурсивного проходим по ним до заданной глубины, проверяем на наличие искомого состояния
                {
                    var result = DLS(child, depth - 1);
                    if (result != null) return result;
                    else continue;
                }
                return null;
            }
            else
                return null;
        }
        private LinkedList<GangStateNode> FindPath(GangStateNode solution) // восстанавливаем путь от найденого состояния до начального
        {
            LinkedList<GangStateNode> path = new LinkedList<GangStateNode>();

            while (solution != null)
            {
                path.AddFirst(solution);
                solution = solution.Parent;
            }

            return path;
        }
    }
}
