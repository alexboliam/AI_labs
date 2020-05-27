using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_Local_Search
{
    public class Coin
    {
        public enum Side { Eagle, Tails }
        public Side UpperSide { get; set; }

        public Coin(Side side)
        {
            this.UpperSide = side;
        }

        public void ChangeSide()
        {
            this.UpperSide = this.UpperSide == Side.Eagle ? Side.Tails : Side.Eagle;
        }
    }
}
