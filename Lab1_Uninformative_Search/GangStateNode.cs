using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace Lab1_Uninformative_Search
{
    public class GangStateNode
    {
        public enum MoveDir { ToLeft, ToRight }
        public MoveDir LastMoveDir { get; set; }

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
            //bool check = (Unit1.CurrentSide != SubUnit2.CurrentSide && Unit1.CurrentSide != SubUnit3.CurrentSide) &&
            //              (Unit2.CurrentSide != SubUnit1.CurrentSide && Unit2.CurrentSide != SubUnit3.CurrentSide) &&
            //              (Unit3.CurrentSide != SubUnit1.CurrentSide && Unit3.CurrentSide != SubUnit2.CurrentSide);
            bool check = (Unit1.CurrentSide != SubUnit1.CurrentSide && (SubUnit1.CurrentSide == Unit2.CurrentSide || SubUnit1.CurrentSide == Unit3.CurrentSide))
                      || (Unit2.CurrentSide != SubUnit2.CurrentSide && (SubUnit2.CurrentSide == Unit1.CurrentSide || SubUnit2.CurrentSide == Unit3.CurrentSide))
                      || (Unit3.CurrentSide != SubUnit3.CurrentSide && (SubUnit3.CurrentSide == Unit2.CurrentSide || SubUnit3.CurrentSide == Unit1.CurrentSide));

            if (!check) {
                if (this.Parent == null)
                    this.LastMoveDir = MoveDir.ToLeft; // допустим, начальное состояние такое (для метода рассчета направления)
                else
                {
                    SetDirection();
                    if(!CheckMoveDirs())
                    {
                        return;
                    }
                }
                    
                moves.AddLast(this); 
            }
            else
            {
                return;
            }
            
        }
        private bool CheckMoveDirs()
        {
            int leftcount = this.ToString().Count(x => x == 'L');
            int rightcount = this.ToString().Count(x => x == 'R');
            int leftperent = this.Parent.ToString().Count(x => x == 'L');
            int rightperent = this.Parent.ToString().Count(x => x == 'R');
            if (this.LastMoveDir == MoveDir.ToLeft && rightcount >= rightperent)
            {
                return false;
            }
            if (leftcount == leftperent && rightcount == rightperent)
            {
                return false;
            }
            if (this.LastMoveDir == MoveDir.ToRight && leftcount >= leftperent)
            {
                return false;
            }

            return true;
        }
        private void SetDirection()
        {
            if (this.Parent.LastMoveDir == MoveDir.ToLeft)
                this.LastMoveDir = MoveDir.ToRight;
            else
                this.LastMoveDir = MoveDir.ToLeft;
        }
        private string SideToString(Side side)
        {
            return side.CurrentSide == Side.SideEnum.Left ? "L" : "R";
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

            solution += "U1: " + SideToString(Unit1) + "; ";
            solution += "S1: " + SideToString(SubUnit1) + " || ";
            solution += "U2: " + SideToString(Unit2) + "; ";
            solution += "S2: " + SideToString(SubUnit2) + " || ";
            solution += "U3: " + SideToString(Unit3) + "; ";
            solution += "S3: " + SideToString(SubUnit3) + /*";  last dir: " + this.LastMoveDir.ToString() +*/ "\r\n";
            
            return solution;
        }
        public string MyToString()
        {
            string solution = string.Empty;

            solution += SideToString(Unit1) == "L" ? "U1, " : "";
            solution += SideToString(SubUnit1) == "L" ? "S1, " : "";
            solution += SideToString(Unit2) == "L" ? "U2, " : "";
            solution += SideToString(SubUnit2) == "L" ? "S2, " : "";
            solution += SideToString(Unit3) == "L" ? "U3, " : "";
            solution += SideToString(SubUnit3) == "L" ? "S3, " : "";
            solution += " || ";
            solution += SideToString(Unit1) == "R" ? "U1, " : "";
            solution += SideToString(SubUnit1) == "R" ? "S1, " : "";
            solution += SideToString(Unit2) == "R" ? "U2, " : "";
            solution += SideToString(SubUnit2) == "R" ? "S2, " : "";
            solution += SideToString(Unit3) == "R" ? "U3, " : "";
            solution += SideToString(SubUnit3) == "R" ? "S3, " : "";
            solution += " ;  last dir: " + this.LastMoveDir.ToString() + "\r\n";

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

            if(Unit1.CurrentSide != SubUnit1.CurrentSide)
            {
                (new GangStateNode(this, new Side(Unit1.ChangeSide()), SubUnit1,
                                                     Unit2, SubUnit2,
                                                     Unit3, SubUnit3)).CheckAndAdd(moves);
            }

            if (Unit2.CurrentSide != SubUnit2.CurrentSide)
            {
                (new GangStateNode(this, Unit1, SubUnit1,
                                           new Side(Unit2.ChangeSide()), SubUnit2,
                                           Unit3, SubUnit3)).CheckAndAdd(moves);
            }

            if (Unit3.CurrentSide != SubUnit3.CurrentSide)
            {
                (new GangStateNode(this, Unit1, SubUnit1,
                                           Unit2, SubUnit2,
                                           new Side(Unit3.ChangeSide()), SubUnit3)).CheckAndAdd(moves);
            }
                
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
