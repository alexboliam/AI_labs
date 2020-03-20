using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_Uninformative_Search
{
    public class Side
    {
        public enum SideEnum { Left, Right }
        public SideEnum CurrentSide { get; set; }
        public Side(SideEnum side)
        {
            CurrentSide = side;
        }

        public SideEnum ChangeSide()
        {
            if (CurrentSide == SideEnum.Right) return SideEnum.Left;
            else return SideEnum.Right;
        }
    }
}
