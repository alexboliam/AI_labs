using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Informative_Search
{
    public class TableState<T> where T:IComparable
    {
        #region Properties
        public T[][] CurrentTable { get; private set; }
        public T[][] TargetTable { get; private set; }
        public TableState<T> Parent { get; private set; }
        public List<TableState<T>> Moves { get; private set; }
        public int Heuristic { get; private set; } = 0;
        #endregion


        #region Ctors
        public TableState(T[][] CurrentTable, T[][] TargetTable, TableState<T> Parent = null)
        {
            this.CurrentTable = this.CopyTable(CurrentTable);
            this.TargetTable = this.CopyTable(TargetTable);
            this.Parent = Parent;
            CalcHeuristic();
        } 
        #endregion


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
        private void MoveItem((int, int) itemCoords, (int,int) emptyCellCoords)
        {
            var newTable = CopyTable(this.CurrentTable);
            T value = this.CurrentTable[itemCoords.Item1][itemCoords.Item2];
            newTable[itemCoords.Item1][itemCoords.Item2] = default(T);
            newTable[emptyCellCoords.Item1][emptyCellCoords.Item2] = value;

            if(this.Parent == null || !this.ArraysEquals(newTable, this.Parent.CurrentTable))
            {
                TableState<T> newState = new TableState<T>(newTable, this.TargetTable, this);
                this.Moves.Add(newState);
            }
        }
        private (int,int) FindEmptyCell()
        {
            for (int i = 0; i < CurrentTable.Length; i++)
            {
                for (int j = 0; j < CurrentTable[i].Length; j++)
                {
                    if (CurrentTable[i][j].Equals(default(T)))
                        return (i, j);
                }
            }
            return (-1,-1);
        }
        #endregion

        #region Public methods
        public void FindMoves()
        {
            var emptyCell = FindEmptyCell();

            if (emptyCell.Item1 - 1 >= 0) 
                MoveItem((emptyCell.Item1 - 1, emptyCell.Item2), emptyCell);

            if (emptyCell.Item1 + 1 < CurrentTable.Length)
                MoveItem((emptyCell.Item1 + 1, emptyCell.Item2), emptyCell);

            if (emptyCell.Item2 - 1 >= 0)
                MoveItem((emptyCell.Item1, emptyCell.Item2 - 1), emptyCell);

            if (emptyCell.Item2 + 1 < CurrentTable[emptyCell.Item1].Length)
                MoveItem((emptyCell.Item1, emptyCell.Item2 + 1), emptyCell);
        }
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
