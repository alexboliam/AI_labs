using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Informative_Search
{
    public class TableState<T> where T:IComparable
    {
        public T[][] CurrentTable { get; private set; }
        public T[][] TargetTable { get; private set; }
        public TableState<T> Parent { get; private set; }
        public List<TableState<T>> Moves { get; private set; }
        public int Heuristic { get; private set; } = 0;

        public TableState(T[][] CurrentTable, T[][] TargetTable, TableState<T> Parent = null)
        {
            this.CurrentTable = this.CopyTable(CurrentTable);
            this.TargetTable = this.CopyTable(TargetTable);
            this.Parent = Parent;
            CalcHeuristic();
        }


        #region Private methods
        private T[][] CopyTable(T[][] table)
        {
            return table.Select(x => x.ToArray()).ToArray();
        }
        private void CalcHeuristic()
        {
            for (int i = 0; i < CurrentTable.Length; i++)
            {
                for (int j = 0; j < CurrentTable[i].Length; j++)
                {
                    if (!CurrentTable[i][j].Equals(TargetTable[i][j]))
                        Heuristic++;
                }
            }
        }
        #endregion

        #region Public methods
        public bool ArraysEquals(T[][] table1, T[][] table2)
        {
            if(table1 == null || table2 == null || table1.Length != table2.Length || table1[0].Length != table2[0].Length)
            {
                return false;
            }
            for(int i = 0; i<table1.Length;i++)
            {
                for (int j = 0; j < table1.Length; j++)
                {
                    if (!table1[i][j].Equals(table2[i][j]))
                        return false;
                }
            }
            return true;
        }
        public override string ToString()
        {
            var result = String.Empty;
            result += $"Wrong positions: {Heuristic}\n";

            foreach(var item in CurrentTable)
            {
                foreach(var subitem in item)
                {
                    result += subitem.ToString() + " ";
                }
                result += "\n";

            }
            return result;
        }
        #endregion
    }
}
