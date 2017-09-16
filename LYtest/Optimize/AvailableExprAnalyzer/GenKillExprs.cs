using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYtest.CFG;
using LYtest.BaseBlocks;
using LYtest.LinearRepr;
using LYtest.LinearRepr.Values;

namespace LYtest.Optimize.AvailableExprAnalyzer
{
    public class GenKillExprs
    {
        public readonly Dictionary<IBaseBlock, List<StringValue>> BlockDefs = new Dictionary<IBaseBlock, List<StringValue>>();

        public readonly Dictionary<IBaseBlock, List<Expression>> Gen = new Dictionary<IBaseBlock, List<Expression>>();
        public readonly Dictionary<IBaseBlock, List<Expression>> Remove = new Dictionary<IBaseBlock, List<Expression>>();
        public readonly List<Expression> AllExpressions = new List<Expression>();

        public GenKillExprs(CFGraph cfg)
        {
            var blocks = cfg.Blocks;

            foreach (var block in blocks)
            {
                BlockDefs[block] = new List<StringValue>();
                Gen[block] = new List<Expression>();
                Remove[block] = new List<Expression>();

                var countOfElems = block.Enumerate().Count();

                var lineCount = 0;
                var exprCount = 0;
                foreach (var elem in block.Enumerate().Reverse())
                {
                    exprCount++;
                    if (elem.IsBinOp())
                    {
                        BlockDefs[block].Add(elem.Destination);

                        if (elem.Operation != Operation.NoOperation)
                        {
                            var expr = new Expression(elem, block, block.Enumerate().Count() - lineCount);

                            var hasThisExpr = AllExpressions.Any(iexpr => iexpr.Dist == expr.Dist && iexpr.LeftOper == expr.LeftOper && iexpr.RightOper == expr.RightOper && iexpr.Op == expr.Op);
                            if (!hasThisExpr)
                            {
                                AllExpressions.Add(expr);
                            }

                            var a1 = BlockDefs[block].Contains(elem.LeftOperand);
                            var a2 = BlockDefs[block].Contains(elem.RightOperand);
                            if (!BlockDefs[block].Contains(elem.LeftOperand) && !BlockDefs[block].Contains(elem.RightOperand))
                            {
                                Gen[block].Add(expr);
                            }
                        }
                    }
                    lineCount++;
                }

            }

            foreach (Expression e in AllExpressions)
                foreach (BaseBlock block in blocks)
                {
                    var t = Gen[block].Contains(e);
                    var lt = BlockDefs[block].Contains(e.LeftOper);
                    var rt = BlockDefs[block].Contains(e.RightOper);

                    if (!Gen[block].Contains(e) &&
                        (BlockDefs[block].Contains(e.Dist)))
                        Remove[block].Add(e);
                }
        }
    }
}
