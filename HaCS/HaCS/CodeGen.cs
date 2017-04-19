using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using HaCS.SymbolTable;

namespace HaCS
{
    class CodeGen : HaCSBaseVisitor<Object>
    {
        public StringBuilder cCode = new StringBuilder();
        public CodeGen()
        {

        }

        public override object VisitTerminal(ITerminalNode node)
        {
            cCode.Append(node.Symbol.Text);
            return base.VisitTerminal(node);
        }

        public override object VisitMain([NotNull] HaCSParser.MainContext context)
        {
            cCode.AppendLine("#include <stdlib.o>");
            return base.VisitMain(context);
        }

        public override object VisitArith1([NotNull] HaCSParser.Arith1Context context)
        {
            return base.VisitArith1(context);
        }

        public override object VisitStmt([NotNull] HaCSParser.StmtContext context)
        {
            return base.VisitStmt(context) + ";";
        }

        public override object VisitBody([NotNull] HaCSParser.BodyContext context)
        {
            return "{" + base.VisitBody(context) + "}";
        }

        public override object VisitPrimitiveType([NotNull] HaCSParser.PrimitiveTypeContext context)
        {
            return "int";
        }

        public override object VisitVarDcl([NotNull] HaCSParser.VarDclContext context)
        {
            return base.VisitVarDcl(context);
        }
    }
}
