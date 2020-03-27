using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Informative_Search
{
    public class TableState<T> where T:IComparable
    {
        #region Properties
        public T[][] CurrentTable { get; private set; } // размещение фишек на данном этапе
        public T[][] TargetTable { get; private set; } // целевое размещение фишек
        public TableState<T> Parent { get; private set; } // предыдущее размещение фишек
        public List<TableState<T>> Moves { get; private set; } = new List<TableState<T>>(); // возможные новые шаги
        #endregion


        #region Ctors
        public TableState(T[][] CurrentTable, T[][] TargetTable, TableState<T> Parent = null)
        {
            this.CurrentTable = this.CopyTable(CurrentTable);
            this.TargetTable = this.CopyTable(TargetTable);
            this.Parent = Parent;
        } 
        #endregion


        #region Private methods
        private T[][] CopyTable(T[][] table) // копия двухмерного массива
        {
            return table.Select(x => x.ToArray()).ToArray();
        }
        private void MoveItem((int, int) itemCoords, (int,int) emptyCellCoords) // переместить фишку и добавить новое размещение в возможные шаги
        {
            var newTable = CopyTable(this.CurrentTable); // создаем новую доску(таблицу) для следующего шага

            // меняем местами фишку и пустую клетку
            T value = this.CurrentTable[itemCoords.Item1][itemCoords.Item2];
            newTable[itemCoords.Item1][itemCoords.Item2] = default(T); 
            newTable[emptyCellCoords.Item1][emptyCellCoords.Item2] = value;

            // если это начальная расстановка или она не была задействована раньше
            if(this.Parent == null || !this.ArraysEquals(newTable, this.Parent.CurrentTable))
            {
                // создаем с этой расстановкой шаг, добавляем его в список возможных шагов
                TableState<T> newState = new TableState<T>(newTable, this.TargetTable, this);
                this.Moves.Add(newState);
            }
        }
        private (int,int) FindEmptyCell() // находим пустую клетку
        {
            for (int i = 0; i < CurrentTable.Length; i++)
            {
                for (int j = 0; j < CurrentTable[i].Length; j++)
                {
                    if (CurrentTable[i][j].Equals(default(T)))
                        return (i, j); // когда находим, возвращаем координаты
                }
            }
            return (-1,-1); // если не находим возвращаем плохие координаты
        }
        #endregion

        #region Public methods
        public bool IsTargetState() // является ли данная расстановка фишек искомой
        {
            if (ArraysEquals(CurrentTable, TargetTable))
                return true;
            else
                return false;
        }
        public void FindMoves() // ищем возможные шаги и двигаем фишки
        {
            var emptyCell = FindEmptyCell(); // находим координаты пустой клетки

            // если можно двигать вверх, двигаем
            if (emptyCell.Item1 - 1 >= 0) 
                MoveItem((emptyCell.Item1 - 1, emptyCell.Item2), emptyCell);

            // если можно двигать вниз, двигаем
            if (emptyCell.Item1 + 1 < CurrentTable.Length) 
                MoveItem((emptyCell.Item1 + 1, emptyCell.Item2), emptyCell);

            // если можно двигать влево, двигаем
            if (emptyCell.Item2 - 1 >= 0) 
                MoveItem((emptyCell.Item1, emptyCell.Item2 - 1), emptyCell);

            // если можно двигать вправо, двигаем
            if (emptyCell.Item2 + 1 < CurrentTable[emptyCell.Item1].Length) 
                MoveItem((emptyCell.Item1, emptyCell.Item2 + 1), emptyCell);
        }
        public bool ArraysEquals(T[][] table1, T[][] table2) // одинаковые ли размещения
        {
            // если они не существуют или разных размерностей, ответ отрицательный
            if(table1 == null || table2 == null || table1.Length != table2.Length || table1[0].Length != table2[0].Length)
            {
                return false;
            }
            for(int i = 0; i<table1.Length;i++)
            {
                for (int j = 0; j < table1.Length; j++)
                {
                    if (!table1[i][j].Equals(table2[i][j]))
                        return false; // если соответствующие значения не совпали, ответ отрицательный
                }
            }
            return true; // если ничего плохого не нашлось, значит они одинаковые
        }
        public override string ToString() // записываем данную расстановку как строку
        {
            var result = String.Empty;

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
