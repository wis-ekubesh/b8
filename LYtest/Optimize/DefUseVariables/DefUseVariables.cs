using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYtest.BaseBlocks;
using LYtest.Optimize.AvailableExprAnalyzer;
using LYtest.CFG;
using LYtest.BaseBlocks;
using LYtest.LinearRepr;
using LYtest.LinearRepr.Values;

namespace LYtest.Optimize.DefUseVariables
{
    public class DefUseVariables
    {
        public Dictionary<KeyValuePair<string, IBaseBlock>, List<object>> DefUseList = new Dictionary<KeyValuePair<string, IBaseBlock>, List<object>>();

        public DefUseVariables(AvailableExprAnalyzer.AvailableExprAnalyzer exprAnalyzer, CFGraph cfg)
        {
            var cfgVertices = cfg.dominatorTree.graph.Vertices;
            var exprs = exprAnalyzer.genKills.AllExpressions;

            foreach(var expr in exprs)
            {
                var tKey = new KeyValuePair<string, IBaseBlock>(expr.Dist.ToString(), expr.Block);
               
                if (expr.Dist.ToString()[0] != '$')
                {
                    if (!DefUseList.ContainsKey(tKey))
                    {
                        DefUseList[tKey] = new List<object>();

                        var cfgBlock = cfgVertices.Where(e => e.CFGNode.Value == expr.Block).First();

                        this.lookupVars(cfgBlock, DefUseList[tKey], expr.Dist.ToString());

                    }
                }
            }
        }

        private void lookupVars(DominatorTree.DominatorTreeNode node, List<object> t, String oper)
        {
            var children = node.ChildrenNodes;
            if (children.Count > 0)
            {
                foreach (var child in children)
                {
                    var blockExprs = child.CFGNode.Value.Enumerate();
                    var line = 0;
                    var flag = false;

                    foreach(var expr in blockExprs)
                    {
                        if (expr.IsBinOp()) { line++; }
                        if ((expr.LeftOperand != null && expr.LeftOperand.Value.ToString() == oper) || 
                            (expr.RightOperand != null && expr.RightOperand.Value.ToString() == oper)
                            && expr.Destination.ToString() != oper)
                        {
                            t.Add(new { node = child.CFGNode.Value, line = line });
                        }

                        if (expr.Destination != null && expr.Destination.ToString() == oper)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag) { this.lookupVars(child, t, oper); }
                   
                }
            }
        }
    }
}
