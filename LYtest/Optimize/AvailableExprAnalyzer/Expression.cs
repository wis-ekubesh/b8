using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYtest.BaseBlocks;
using LYtest.LinearRepr;
using LYtest.LinearRepr.Values;

namespace LYtest.Optimize.AvailableExprAnalyzer
{
    public class Expression
    {
        public IValue Dist { get; set; }
        public Operation Op { get; set; }
        public IValue LeftOper { get; set; }
        public IValue RightOper { get; set; }
        public IBaseBlock Block { get; set; }
        public int Line;

        public Expression(IThreeAddressCode elem, IBaseBlock block, int line)
        {
            Dist = elem.Destination;
            Op = elem.Operation;
            LeftOper = elem.LeftOperand;
            RightOper = elem.RightOperand;
            Block = block;
            Line = line;
        }

        public Expression()
        {
            Dist = null;
            LeftOper = null;
            RightOper = null;
            Op = Operation.NoOperation;
        }

        public override bool Equals(object obj)
        {
            if (obj is Expression)
            {
                Expression Other = (Expression)obj;

                return Other.Op == this.Op &&
                    //Коммутативный случай
                    ((LinearHelper.IsBinOp(this.Op) || this.Op == Operation.Mult || this.Op == Operation.Plus) &&
                    (Other.LeftOper.Equals(this.LeftOper) && Other.RightOper.Equals(this.RightOper) || Other.LeftOper.Equals(this.RightOper) && Other.RightOper.Equals(this.LeftOper)) ||
                    //Некоммутативный случай
                    Other.LeftOper.Equals(this.LeftOper) && Other.RightOper.Equals(this.RightOper));
            }
            else
                return false;
        }

        public override string ToString()
        {
            return "{"+ Dist + "} = " + LeftOper + " " + Op + " " + RightOper;
        }

        public override int GetHashCode()
        {
            return Dist.GetHashCode() + LeftOper.GetHashCode() + RightOper.GetHashCode();
        }
    }
}
