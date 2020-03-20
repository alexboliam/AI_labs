using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_Uninformative_Search
{
    public class Solver
    {
        public LinkedList<GangStateNode> IDDFS(GangStateNode root)
        {
            int depth = 0;
            while(true)
            {
                var result = DLS(root, depth);
                if (result != null && result.IsSolution())
                    return FindPath(result);
                depth += 1;
            }
        }
        public GangStateNode DLS(GangStateNode node, int depth)
        {
            if (depth == 0 && node.IsSolution())
                return node;
            else if (depth > 0)
            {
                var moves = node.GetPossibleMoves();
                foreach (var child in moves)
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
        private LinkedList<GangStateNode> FindPath(GangStateNode solution)
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
