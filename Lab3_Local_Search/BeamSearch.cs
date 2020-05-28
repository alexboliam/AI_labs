using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_Local_Search
{
    public class BeamSearch
    {
        public static void Bms(int beamSize, CoinsState start = null)
        {
            if(start == null)
            {
                start = new CoinsState();
            }

            Func<CoinsState, int> heuristic = ca =>
            {
                if (ca.FirstCoin.UpperSide == start.FirstCoin.UpperSide && ca.SecondCoin.UpperSide == start.SecondCoin.UpperSide && ca.FirstCoin.UpperSide == start.ThirdCoin.UpperSide)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            };

            List<CoinsState> beam = new List<CoinsState>(); 
            List<CoinsState> set = new List<CoinsState>();
            List<CoinsState> visited = new List<CoinsState>();
            visited.Add(start);
            beam.Add(start);

            int step = 0;

            while (beam.Count != 0)
            {
                set.Clear();

                foreach (var c0 in beam)
                {
                    ++step;

                    foreach (var child in c0.FindMoves(c0))
                    {
                        if (visited.Contains(child)) continue;

                        visited.Add(child);
                        set.Add(child);

                        if (child.IsSolution())
                        {
                            if(step<3)
                            {
                                visited.Remove(child);
                            }
                            else
                            {
                                Console.WriteLine($"Beam search; beamSize:{beamSize}");
                                FindPath(child);
                                return;
                            }
                            
                        }
                    }
                }

                beam.Clear();
                beam.AddRange(set.OrderBy(heuristic).Take(beamSize));
            }

            Console.WriteLine($"Beam search; Not found; beamSize:{beamSize}");
        }

        public static void FindPath(CoinsState state)
        {
            Stack<CoinsState> s = new Stack<CoinsState>();

            while(true)
            {
                if (state.Parent != null) {
                    s.Push(state);
                    state = state.Parent;
                }
                else
                {
                    if(s.Count > 0)
                    {
                        var count = s.Count;
                        Console.WriteLine($"1. {Coin.Side.Eagle}-{Coin.Side.Tails}-{Coin.Side.Eagle}");
                        for (int i = 0; i<count;i++)
                        {
                            Console.WriteLine($"{i+2}. {s.Peek().FirstCoin.UpperSide}-{s.Peek().SecondCoin.UpperSide}-{s.Peek().ThirdCoin.UpperSide}");
                            s.Pop();
                        }
                        return;
                    }
                }
            }
        }
    }
}
