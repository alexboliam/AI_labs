using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_Local_Search
{
    public class CoinsState
    {
        public CoinsState Parent;
        public Coin FirstCoin { get; set; }
        public Coin SecondCoin { get; set; }
        public Coin ThirdCoin { get; set; }

        public CoinsState()
        {
            this.FirstCoin = new Coin(Coin.Side.Eagle);
            this.SecondCoin = new Coin(Coin.Side.Tails);
            this.ThirdCoin = new Coin(Coin.Side.Eagle);

        }
        public CoinsState(Coin first, Coin second, Coin third)
        {
            this.FirstCoin = first;
            this.SecondCoin = second;
            this.ThirdCoin = third;
        }




    }
}
