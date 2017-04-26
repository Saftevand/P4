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
        public StringBuilder cFunctionCode = new StringBuilder();
        public StringBuilder cList = new StringBuilder();
        private int varCount;
        private int funcCount;
        public CodeGen()
        {

        }

        public override object VisitTerminal(ITerminalNode node)
        {
            //cCode.Append(node.Symbol.Text + " ");
            return base.VisitTerminal(node);
        }

        public override object VisitProgram([NotNull] HaCSParser.ProgramContext context)
        {
            cFunctionCode.AppendLine("#include <stdio.h>" + Environment.NewLine + "#include <stdlib.h>");
            StringBuilder Code = new StringBuilder();
            Code.Append(Visit(context.main()));
            foreach (var item in context.functionDecl())
            {
                Code.Append(Visit(item));
            }
            return cCode.Append(Code);
        }

        public override object VisitMain([NotNull] HaCSParser.MainContext context)
        {
            return "int main ()" + Environment.NewLine + base.Visit(context.body()) + Environment.NewLine;
        }

        public override object VisitFunctionDecl([NotNull] HaCSParser.FunctionDeclContext context)
        {
            StringBuilder Code = new StringBuilder();

            foreach (var item in context.formalParam())
            {
                Code.Append(Visit(item));
                if (item != context.formalParam().Last())
                {
                    Code.Append(", ");
                }
            }

            cFunctionCode.Append(Visit(context.type()) + " " + context.IDENTIFIER().GetText() + context.LPAREN().GetText() + Code + context.RPAREN().GetText() + Visit(context.body()));
            return null;
        }

        public override object VisitFunc([NotNull] HaCSParser.FuncContext context)
        {
            string code = context.IDENTIFIER().GetText() + "( ";

            if (context.expression() != null)
            {
                foreach (var item in context.expression())
                {
                    code += Visit(item);
                    if (item != context.expression().Last())
                    {
                        code += ", ";
                    }
                }
                
            }

            code += " )";
            return code;
        }

        public override object VisitFormalParam([NotNull] HaCSParser.FormalParamContext context)
        {
            return Visit(context.type()) + " " + context.IDENTIFIER().GetText();
        }

        public override object VisitArith1([NotNull] HaCSParser.Arith1Context context)
        {
            string opperator = null;
            if (context.ADD() != null)
            {
                opperator = " + ";
            }
            else if (context.SUB() != null)
            {
                opperator = " - ";
            }
            return Visit(context.expression(0)) + opperator.ToString() + Visit(context.expression(1));
        }

        public override object VisitStmt([NotNull] HaCSParser.StmtContext context)
        {
            return Environment.NewLine + base.Visit(context.children[0]);
        }

        public override object VisitBody([NotNull] HaCSParser.BodyContext context)
        {
            StringBuilder code = new StringBuilder();

            foreach (var item in context.stmt())
            {
                code.Append(base.Visit(item));
            }

            return "{ " + code + " }";
        }

        public override object VisitMap([NotNull] HaCSParser.MapContext context)
        {
            string localFunc = "Func" + funcCount++;
            string type = context.children.GetType().ToString();
            string variable = "Var" + varCount++;
            string size = context.ChildCount.ToString();

            cFunctionCode.AppendLine(type + "*" + localFunc + "()");
            cFunctionCode.AppendLine("{");
            cFunctionCode.AppendLine("static " + type + variable + "[" + size + "];");

            cFunctionCode.AppendLine("int i;" + Environment.NewLine + "for(i = 0; i < " + size + "; i++)");
            cFunctionCode.AppendLine("{" + Environment.NewLine + variable + "[i] = " + context.MAP().ToString() + Environment.NewLine + ("}"));

            cFunctionCode.AppendLine("return " + variable + ";");
            cFunctionCode.AppendLine("}");
            return type + "*" + variable + " = " + localFunc + "();";
        }

        public override object VisitListDcl([NotNull] HaCSParser.ListDclContext context)
        {
            cList.Clear();
            string type = context.Parent.GetChild(0).GetChild(2).GetText();
            string variable = context.Parent.GetChild(1).GetText();

            //return "static " + type + " " + variable + "[" + size + "] = " + cList;

            return "static " + type + " " + variable + Visit(context.listDcls()); 
        }

        public override object VisitListDcls([NotNull] HaCSParser.ListDclsContext context)
        {
            string code = null;

            if (context.expression(0).GetText().Contains(".."))
            {
                code += " [";
                foreach (var item in context.expression())
                {
                    code += Visit(item);
                }
                code += "] = ";

                code += cList;
                return code;
                //return context.expression(0).GetChild(3).GetChild(0).GetText();
            }

            else if (context.expression(0).GetText().Contains("where"))
            {
                code += "[ 100 ];" + Environment.NewLine;
                code += Visit(context.expression(0));
                return code;
            }
            else
            {
                int i;

                code += "[" + (context.ChildCount - 2).ToString() + "] = ";
                code += "{";

                for (i = 0; i < context.ChildCount - 1; i = i + 2)
                {
                    code += context.GetChild(i).GetText() + ", ";
                }

                code += context.GetChild(i).GetText() + " }";


                return code;
            }
        }

        public override object VisitListType([NotNull] HaCSParser.ListTypeContext context)
        {
            return base.VisitListType(context);
        }

        public override object VisitVarDcl([NotNull] HaCSParser.VarDclContext context)
        {
            if (context.GetChild(0).GetText().Contains("List"))
            {
                return base.VisitVarDcl(context) + ";";
            }
            return context.GetChild(0).GetText() + " " + context.IDENTIFIER().GetText() + " = " + Visit(context.expression()) + ";";
        }

        public override object VisitPrimitiveType([NotNull] HaCSParser.PrimitiveTypeContext context)
        {
            return base.VisitPrimitiveType(context);
        }

        public override object VisitExpression([NotNull] HaCSParser.ExpressionContext context)
        {
            return context.GetChild(0).GetText();
        }

        public override object VisitType([NotNull] HaCSParser.TypeContext context)
        {
            string type = context.children[0].GetText();
            if (type != "List")
            {
                return type;
            }
            return base.VisitType(context);
        }

        public override object VisitCompileUnit([NotNull] HaCSParser.CompileUnitContext context)
        {
            Console.WriteLine(cFunctionCode);
            Console.WriteLine(cCode);
            return base.VisitCompileUnit(context);
        }

        public override object VisitReturnStmt([NotNull] HaCSParser.ReturnStmtContext context)
        {
            return context.RETURN().ToString() + " " + context.expression().GetText() + ";";
        }

        public override object VisitRange([NotNull] HaCSParser.RangeContext context)
        {
            cList.Clear();
            string range_from = context.expression(0).GetChild(0).GetText();
            string range_to = context.expression(1).GetChild(0).GetText();
            int iRange_to = Convert.ToInt32(range_to);

            cList.Append("{ ");
            for (int i = Convert.ToInt32(range_from); i < iRange_to; i++)
            {
                cList.Append(i + ", ");
            }
            cList.Append(iRange_to + " }");

            return iRange_to;
        }

        public override object VisitIfStmt([NotNull] HaCSParser.IfStmtContext context)
        {
            string code = "if (" + Visit(context.expression()) + ")" + Visit(context.body());

            if (context.elseifStmt() != null)
            {
                code += Visit(context.elseifStmt());
            }
            return code;
        }

        public override object VisitEquality([NotNull] HaCSParser.EqualityContext context)
        {
            return Visit(context.expression(0)) + " " + context.EQ().GetText() + " " + Visit(context.expression(1));
        }

        public override object VisitArith2([NotNull] HaCSParser.Arith2Context context)
        {
            string opperator = null;
            if (context.MUL() != null)
            {
                opperator = " * ";
            }
            else if (context.MOD() != null)
            {
                opperator = " % ";
            }
            else if (context.DIV() != null)
            {
                opperator = " / ";
            }

            return Visit(context.expression(0)) + opperator.ToString() + Visit(context.expression(1)); 
        }

        public override object VisitLit([NotNull] HaCSParser.LitContext context)
        {
            return context.GetChild(0).GetText();
        }

        public override object VisitVar([NotNull] HaCSParser.VarContext context)
        {
            if (context.DOT() != null)
            {
                return Visit(context.listOpp());
            }
            return context.IDENTIFIER().GetText();
        }

        public override object VisitElseifStmt([NotNull] HaCSParser.ElseifStmtContext context)
        {
            if (context.elseStmt() != null)
            {
                return Visit(context.elseStmt());
            }
            return null;
        }

        public override object VisitElseStmt([NotNull] HaCSParser.ElseStmtContext context)
        {
            return "else" + Visit(context.body());
        }

        public override object VisitFirst([NotNull] HaCSParser.FirstContext context)
        {
            return context.Parent.GetChild(0) + "[0]";
        }

        public override object VisitWhere([NotNull] HaCSParser.WhereContext context)
        {
            string code = null;
            string size = 100.ToString();
            string exp = Visit(context.expression()).ToString();
            exp = exp.Replace(context.expression().GetChild(0).GetChild(2).GetText(), context.Parent.GetChild(0) + "[i]");

            code += "int i; for(i=0;i<" + size + ";i++) { if(" +  exp + "){" + context.Parent.GetChild(0) + "[i] = " + context.Parent.Parent.Parent.Parent.GetChild(1) + "[i];" + "} }";
            
            return code;
        }

        public override object VisitLambdaExp([NotNull] HaCSParser.LambdaExpContext context)
        {
            return Visit(context.lambdaBody());
        }

        public override object VisitPrintStmt([NotNull] HaCSParser.PrintStmtContext context)
        {
            string type = null;
            type = "d";
            return "printf(" + '"' + "%" + type + "\\n" + '"' + "," + Visit(context.expression(0)) + ");";
        }

        private void printList(List<string> list)
        {
            foreach (string item in list)
            {
                cCode.Append(item.ToString());
                if (item != list.Last())
                {
                    cCode.Append(", ");
                }
            }
        }
    }
}
