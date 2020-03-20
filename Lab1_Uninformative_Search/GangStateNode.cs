using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_Uninformative_Search
{
    public class GangStateNode
    {
        private Side Unit1 = new Side(Side.SideEnum.Left);
        private Side SubUnit1 = new Side(Side.SideEnum.Left);
        private Side Unit2 = new Side(Side.SideEnum.Left);
        private Side SubUnit2 = new Side(Side.SideEnum.Left);
        private Side Unit3 = new Side(Side.SideEnum.Left);
        private Side SubUnit3 = new Side(Side.SideEnum.Left);
        public GangStateNode Parent { get; set; }
        public GangStateNode()
        {

        }
        public GangStateNode(GangStateNode parent)
        {
            Parent = parent;
        }
        public GangStateNode(GangStateNode parent, Side Unit1, Side SubUnit1, Side Unit2, Side SubUnit2, Side Unit3, Side SubUnit3)
        {
            Parent = parent;
            this.Unit1 = Unit1;
            this.SubUnit1 = SubUnit1;
            this.Unit2 = Unit2;
            this.SubUnit2 = SubUnit2;
            this.Unit3 = Unit3;
            this.SubUnit3 = SubUnit3;
        }

        public bool IsSolution()
        {
            return Unit1.CurrentSide == Side.SideEnum.Right && SubUnit1.CurrentSide == Side.SideEnum.Right && 
                   Unit2.CurrentSide == Side.SideEnum.Right && SubUnit2.CurrentSide == Side.SideEnum.Right &&
                   Unit3.CurrentSide == Side.SideEnum.Right && SubUnit3.CurrentSide == Side.SideEnum.Right;
        }
        private void CheckAndAdd(LinkedList<GangStateNode> moves)
        {
            bool check = (Unit1.CurrentSide != SubUnit2.CurrentSide && Unit1.CurrentSide != SubUnit3.CurrentSide) &&
                          (Unit2.CurrentSide != SubUnit1.CurrentSide && Unit2.CurrentSide != SubUnit3.CurrentSide) &&
                          (Unit3.CurrentSide != SubUnit1.CurrentSide && Unit3.CurrentSide != SubUnit2.CurrentSide);

            if (!check) moves.AddLast(this);
        }
        private string SideToString(Side side)
        {
            return side.CurrentSide == Side.SideEnum.Left ? "Left" : "Right";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is GangStateNode))
                return false;

            GangStateNode pairsState = (GangStateNode)obj;

            return
                Unit1.CurrentSide == pairsState.Unit1.CurrentSide &&
                SubUnit1.CurrentSide == pairsState.SubUnit1.CurrentSide &&
                Unit2.CurrentSide == pairsState.Unit2.CurrentSide &&
                SubUnit2.CurrentSide == pairsState.SubUnit2.CurrentSide &&
                Unit3.CurrentSide == pairsState.Unit3.CurrentSide &&
                SubUnit3.CurrentSide == pairsState.SubUnit3.CurrentSide;
        }
        public override string ToString()
        {
            string solution = string.Empty;

            solution += "U1: " + SideToString(Unit1) + " ; ";
            solution += "SubU 1: " + SideToString(SubUnit1) + " || ";
            solution += "U2: " + SideToString(Unit2) + " ; ";
            solution += "SubU 2: " + SideToString(SubUnit2) + " || ";
            solution += "U3: " + SideToString(Unit3) + " ; ";
            solution += "SubU 3: " + SideToString(SubUnit3) + "\r\n";

            return solution;
        }

        public LinkedList<GangStateNode> GetPossibleMoves()
        {
            LinkedList<GangStateNode> moves = new LinkedList<GangStateNode>();

            if (Unit1.CurrentSide == SubUnit1.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    new Side(Unit1.ChangeSide()), new Side(SubUnit1.ChangeSide()),
                    Unit2, SubUnit2,
                    Unit3, SubUnit3)).CheckAndAdd(moves);
            }

            if (Unit2.CurrentSide == SubUnit2.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    Unit1, SubUnit1,
                    new Side(Unit2.ChangeSide()), new Side(SubUnit2.ChangeSide()),
                    Unit3, SubUnit3)).CheckAndAdd(moves);
            }

            if (Unit3.CurrentSide == SubUnit3.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    Unit1, SubUnit1,
                    Unit2, SubUnit2,
                    new Side(Unit1.ChangeSide()), new Side(SubUnit1.ChangeSide()))).CheckAndAdd(moves);
            }

            if (Unit1.CurrentSide == Unit2.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    new Side(Unit1.ChangeSide()), SubUnit1,
                    new Side(Unit2.ChangeSide()), SubUnit2,
                    Unit3, SubUnit3)).CheckAndAdd(moves);
            }


            if (Unit1.CurrentSide == Unit3.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    new Side(Unit1.ChangeSide()), SubUnit1,
                    Unit2, SubUnit2,
                    new Side(Unit3.ChangeSide()), SubUnit3)).CheckAndAdd(moves);
            }


            if (Unit2.CurrentSide == Unit3.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    Unit1, SubUnit1,
                    new Side(Unit2.ChangeSide()), SubUnit2,
                    new Side(Unit3.ChangeSide()), SubUnit3)).CheckAndAdd(moves);
            }

            if (SubUnit1.CurrentSide == SubUnit2.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    Unit1, new Side(SubUnit1.ChangeSide()),
                    Unit2, new Side(SubUnit2.ChangeSide()),
                    Unit3, SubUnit3)).CheckAndAdd(moves);
            }

            if (SubUnit1.CurrentSide == SubUnit3.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    Unit1, new Side(SubUnit1.ChangeSide()),
                    Unit2, SubUnit2,
                    Unit3, new Side(SubUnit3.ChangeSide()))).CheckAndAdd(moves);
            }

            if (SubUnit2.CurrentSide == SubUnit3.CurrentSide)
            {
                (new GangStateNode(
                    this,
                    Unit1, new Side(SubUnit1.ChangeSide()),
                    Unit2, SubUnit2,
                    Unit3, new Side(SubUnit3.ChangeSide()))).CheckAndAdd(moves);
            }

            (new GangStateNode(this, new Side(Unit1.ChangeSide()), SubUnit1,
                                                     Unit2, SubUnit2,
                                                     Unit3, SubUnit3)).CheckAndAdd(moves);

            (new GangStateNode(this, Unit1, SubUnit1,
                                           new Side(Unit2.ChangeSide()), SubUnit2,
                                           Unit3, SubUnit3)).CheckAndAdd(moves);

            (new GangStateNode(this, Unit1, SubUnit1,
                                           Unit2, SubUnit2,
                                           new Side(Unit3.ChangeSide()), SubUnit3)).CheckAndAdd(moves);

            (new GangStateNode(this, Unit1, new Side(SubUnit1.ChangeSide()),
                                           Unit2, SubUnit2,
                                           Unit3, SubUnit3)).CheckAndAdd(moves);

            (new GangStateNode(this, Unit1, SubUnit1,
                                           Unit2, new Side(SubUnit2.ChangeSide()),
                                           Unit3, SubUnit3)).CheckAndAdd(moves);

            (new GangStateNode(this, Unit1, SubUnit1,
                                           Unit2, SubUnit2,
                                           Unit3, new Side(SubUnit3.ChangeSide()))).CheckAndAdd(moves);

            return moves;
        }
    }
}
