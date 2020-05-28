using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Lab3_Local_Search
{
    public class CoinsState
    {
        public CoinsState Parent = null;
        public Coin FirstCoin { get; set; }
        public Coin SecondCoin { get; set; }
        public Coin ThirdCoin { get; set; }
        public CoinsState()
        {
            this.FirstCoin = new Coin(Coin.Side.Eagle);
            this.SecondCoin = new Coin(Coin.Side.Tails);
            this.ThirdCoin = new Coin(Coin.Side.Eagle);

        }
        public CoinsState(Coin first, Coin second, Coin third, CoinsState parent)
        {
            this.FirstCoin = first;
            this.SecondCoin = second;
            this.ThirdCoin = third;
            this.Parent = parent;
        }

        public List<CoinsState> FindMoves(CoinsState state)
        {
            List<CoinsState> states = new List<CoinsState>();

            var one = state.Copy();
            one.Parent = state;
            one.FirstCoin.ChangeSide();
            one.SecondCoin.ChangeSide();
            states.Add(one);

            var two = state.Copy();
            two.Parent = state;
            two.FirstCoin.ChangeSide();
            two.ThirdCoin.ChangeSide();
            states.Add(two);

            var three = state.Copy();
            three.Parent = state;
            three.ThirdCoin.ChangeSide();
            three.SecondCoin.ChangeSide();
            states.Add(three);

            return states;
        }
        public CoinsState Copy()
        {
            CoinsState b = new CoinsState(new Coin(this.FirstCoin.UpperSide), new Coin(this.SecondCoin.UpperSide), new Coin(this.ThirdCoin.UpperSide), this);
            return b;
        }
        public bool IsSolution()
        {
            if(FirstCoin.UpperSide == Coin.Side.Eagle && SecondCoin.UpperSide == Coin.Side.Eagle && ThirdCoin.UpperSide == Coin.Side.Eagle)
            {
                return true;
            }
            else if(FirstCoin.UpperSide == Coin.Side.Tails && SecondCoin.UpperSide == Coin.Side.Tails && ThirdCoin.UpperSide == Coin.Side.Tails)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            if(this.FirstCoin.UpperSide == ((CoinsState)obj).FirstCoin.UpperSide && 
               this.SecondCoin.UpperSide == ((CoinsState)obj).SecondCoin.UpperSide && 
               this.ThirdCoin.UpperSide == ((CoinsState)obj).ThirdCoin.UpperSide)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
