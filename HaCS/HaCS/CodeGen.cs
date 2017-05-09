﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using HaCS.SymbolTable;
using HaCS.Types;

namespace HaCS
{
    class CodeGen : HaCSBaseVisitor<Object>
    {
        #region Variables
        public StringBuilder cCode = new StringBuilder();
        public StringBuilder cFunctionCode = new StringBuilder();
        public StringBuilder cPrototype = new StringBuilder();
        //public StringBuilder cList = new StringBuilder();
        List<string> cList = new List<string>();
        Dictionary<string, string> Identifier = new Dictionary<string, string>();
        Dictionary<string, string> TypeIdentifier = new Dictionary<string, string>();
        Dictionary<string, List<string>> IdentifierValue = new Dictionary<string, List<string>>();
        private ParseTreeProperty<HaCSType> _typeProperty;
        private int varCount;
        private int funcCount;
        #endregion

        public CodeGen(ParseTreeProperty<HaCSType> typeProperty)
        {
            _typeProperty = typeProperty;
        }

        #region Methods
        #region Overriding Methods
        public override object VisitTerminal(ITerminalNode node)
        {
            //cCode.Append(node.Symbol.Text + " ");
            return base.VisitTerminal(node);
        }

        public override object VisitProgram([NotNull] HaCSParser.ProgramContext context)
        {
            cPrototype.AppendLine("#include <stdio.h>" + Environment.NewLine + "#include <stdlib.h>" + Environment.NewLine + "#include <string.h>");
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
            string type = Visit(context.type()).ToString();
            if (type.Contains("List") || type.Contains("*"))
            {
                string name = context.GetChild(1).GetText();
                string typpe = context.GetChild(0).GetChild(0).GetChild(2).GetText();
                TypeIdentifier.Add(name, typpe);
            }
            cFunctionCode.Append(type + " " + context.IDENTIFIER().GetText() + context.LPAREN().GetText() + Code + context.RPAREN().GetText() + Visit(context.body()));

            cPrototype.Append(type + " " + context.IDENTIFIER().GetText() + context.LPAREN().GetText() + Code + context.RPAREN().GetText() + ";");
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
            if (context.returnStmt() != null)
            {
                code.Append(base.Visit(context.returnStmt()));
            }
          
            return "{ int i;" + code + " }";
        }

        public override object VisitMap([NotNull] HaCSParser.MapContext context)
        {
            string code = null;

            string size = null;
            string identifier = context.Parent.GetChild(0).GetText();
            string opeIden = context.Parent.Parent.Parent.GetChild(1).GetText();

            size = Identifier[identifier];

            string exp = Visit(context.lambdaExp()).ToString();
            exp = exp.Replace(context.lambdaExp().GetChild(2).GetText(), identifier + "[i]");

            code += "[" + size + "]; " + Environment.NewLine + "for(i=0;i<" + size + ";i++) { " + opeIden + "[i] = " + exp + "; }";

            return code;
        }

        public override object VisitListDcl([NotNull] HaCSParser.ListDclContext context)
        {
            cList.Clear();
            string type = context.GetChild(0).GetChild(2).GetChild(0).GetText();
            string variable = context.GetChild(1).GetText();

            //return "static " + type + " " + variable + "[" + size + "] = " + cList;

            //return "static " + type + " " + Visit(context.listDcls());
            return type + " " + Visit(context.listDcls());

        }

        public override object VisitListDcls([NotNull] HaCSParser.ListDclsContext context)
        {
            string code = null;
            string variable = context.Parent.GetChild(1).GetText();
            string size = null;
            string type = context.Parent.GetChild(0).GetChild(2).GetChild(0).GetText();
            TypeIdentifier.Add(variable, type);

            if (context.GetText().Contains(".."))
            {
                code += variable;
                code += " [";
                size = Visit(context.expression(0)).ToString();
                Identifier.Add(variable, size);
                code += size;
                code += "] = ";

                foreach (var item in cList)
                {
                    code += item.ToString();
                }

                IdentifierValue.Add(variable, cList);
                return code + ";";
                //return context.expression(0).GetChild(3).GetChild(0).GetText();
            }

            else if (context.GetChild(1) != null)
            {
                int i;
                List<string> value = new List<string>();
                int sizy = context.expression().Count() - 1;
                size = sizy.ToString();
                Identifier.Add(variable, size);
                code += variable;
                code += "[] = ";
                code += "{";

                for (i = 0; i < context.ChildCount - 1; i = i + 2)
                {
                    value.Add(context.GetChild(i).GetText() + ", ");
                    code += context.GetChild(i).GetText() + ", ";
                }
                value.Add(context.GetChild(i).GetText());
                IdentifierValue.Add(variable, value);
                code += context.GetChild(i).GetText() + " }";
                return code + ";";
            }
            else
            {
                return Visit(context.expression(0));
            }
        }

        public override object VisitInclude([NotNull] HaCSParser.IncludeContext context)
        {
            string code = null;
            string str = null;
            string name = null;
            string identifier = context.Parent.GetChild(0).GetText();
            string assigner = context.Parent.Parent.Parent.GetChild(1).GetText();
            code = " *" + assigner + " = malloc(strlen(" + identifier + ")"; 
            str = "strcpy(" + assigner + ", " + identifier +");" + Environment.NewLine;
            string test = null;
            foreach (var item in context.expression())
            {
                name = item.GetChild(0).GetText();
                if (name.Contains("'"))
                {
                    name = name.Replace('\'', '"');
                }
                code += " + strlen( " + name + ")";
                str += "strcat(" + assigner + ", " + name + ");";
            }
            code += "+2);";

            return code + str;
        }

        public override object VisitListType([NotNull] HaCSParser.ListTypeContext context)
        {
            return base.VisitListType(context);
        }

        public override object VisitVarDcl([NotNull] HaCSParser.VarDclContext context)
        {
            if (context.GetChild(0).GetText().Contains("List"))
            {
                return base.VisitVarDcl(context);
            }
            return context.GetChild(0).GetText() + " " + context.IDENTIFIER().GetText() + " = " + Visit(context.expression());
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
            string type = context.GetChild(0).GetText();
            if (!type.Contains("List"))
            {
                return type;
            }
            if (type == "BOOL")
            {
                type = "int";
                return type;
            }
            return context.GetChild(0).GetChild(2).GetChild(0).GetText() + " *";
            //return base.VisitType(context);
        }

        public override object VisitCompileUnit([NotNull] HaCSParser.CompileUnitContext context)
        {
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
            int iRange_to = 0;

            cList.Add("{ ");
            if (_typeProperty.Get(context.expression(0)) is tINT)
            {
                iRange_to = Convert.ToInt32(range_to);
                for (int i = Convert.ToInt32(range_from); i < iRange_to; i++)
                {
                    cList.Add(i + ", ");
                }
                cList.Add(range_to);
                iRange_to = Convert.ToInt32(range_to);
            }
            else if (_typeProperty.Get(context.expression(0)) is tCHAR)
            {
                char[] from = range_from.ToCharArray();
                char[] to = range_to.ToCharArray();
                for (char c = from[1]; c < to[1]; c++)
                {
                    cList.Add("'" + c + "', ");
                    iRange_to++;
                }
                cList.Add(range_to);
                iRange_to++;
            }
            cList.Add("}");
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
            return context.Parent.GetChild(0) + "[0];";
        }

        public override object VisitLast([NotNull] HaCSParser.LastContext context)
        {
            string name = context.Parent.GetChild(0).GetText();
            name += "[" + Identifier[name] + "];";

            return name;
        }

        public override object VisitExclude([NotNull] HaCSParser.ExcludeContext context)
        {
            string name = context.Parent.GetChild(0).GetText();
            string code = context.Parent.Parent.Parent.GetChild(1).GetText();
            string valueCode = null;
            int size = 0;
            List<string> value = IdentifierValue[name].ToList();
            foreach (var item in context.expression())
            {
                value.Remove(item.GetText() + ", ");
            }
            size = value.Count();
            foreach (var item in value)
            {
                valueCode += item;
            }
            if (value.Last().Contains(","))
            {
                valueCode.Remove(valueCode.Length - 2);
            }
            Identifier.Add(code, size.ToString());

            return code + "[" + size + "] = {" + valueCode + "};";
        }

        public override object VisitExcludeAt([NotNull] HaCSParser.ExcludeAtContext context)
        {
            string name = context.Parent.GetChild(0).GetText();
            string code = context.Parent.Parent.Parent.GetChild(1).GetText();
            string valueCode = null;
            int size = 0;
            int index;
            int offset = 0;
            List<string> value = IdentifierValue[name].ToList();
            foreach (var item in context.expression())
            {
                index = Convert.ToInt32(item.GetText());
                value.RemoveAt(index-(++offset));
            }
            foreach (var item in value)
            {
                valueCode += item;
            }
            if (value.Last().Contains(","))
            {
                valueCode.Remove(valueCode.Length - 2);
            }
            size = value.Count();
            Identifier.Add(code, size.ToString());
            return code + "[" + size + "] = {" + valueCode + "};";
        }

        public override object VisitWhere([NotNull] HaCSParser.WhereContext context)
        {
            string code = null;
            
            string name = context.Parent.Parent.Parent.GetChild(1).GetText();
            string Pname = context.Parent.GetChild(0).GetText();
            string size = null;

            string exp = Visit(context.lambdaExp()).ToString();
            exp = exp.Replace(context.lambdaExp().GetChild(2).GetText(), context.Parent.GetChild(0) + "[i]");
            if (Identifier.ContainsKey(Pname))
            {
                size = Identifier[Pname];
                code += " " + name + "[" + size + "];" + Environment.NewLine;
            }
            else
            {
                code += " *" + name + ";" + Environment.NewLine;
                size = "sizeof(" + Pname + ")";
            }
                
            code += "int j = 0; for(i=0;i<" + size + ";i++) { if(" +  exp + "){" + name + "[j++] = " + Pname + "[i];" + "} }";
            Identifier.Add(name,"j");
            return code;
        }

        public override object VisitReduce([NotNull] HaCSParser.ReduceContext context)
        {

            return base.VisitReduce(context);
        }

        public override object VisitContains([NotNull] HaCSParser.ContainsContext context)
        {
            string exp = Visit(context.expression()).ToString();
            string expType = findType(_typeProperty.Get(context.expression()).ToString());

            string pName = context.Parent.Parent.Parent.Parent.Parent.GetChild(0).GetText();
            string name = context.Parent.GetChild(0).GetText();
            string nameType = findType(_typeProperty.Get(context).ToString());   
            cFunctionCode.Append("int func" + ++funcCount + "(" + nameType + " *" + name + ", " + expType + " x)" + Environment.NewLine);
            cFunctionCode.Append("{ int i;");
            cFunctionCode.Append("for(i=0;i < " + "sizeof(x)" + "; i++)" + Environment.NewLine);
            cFunctionCode.Append("{if(" + name + "[i] == " + exp + "){ return 1;}" + "}");
            cFunctionCode.Append("return 0;}");
            string code = "func" + funcCount + "(" + name + ", " + exp + ")";

            cPrototype.AppendLine("int func" + funcCount + "(" + nameType + " *" + name + ", " + expType + " x);");
            return code;
        }

        public override object VisitLambdaExp([NotNull] HaCSParser.LambdaExpContext context)
        {
            return Visit(context.lambdaBody());
        }

        public override object VisitPrintStmt([NotNull] HaCSParser.PrintStmtContext context)
        {
            string code = null;
            string type = null;
            string name = null;
            if (_typeProperty.Get(context.expression(0)) is tLIST)
            {
                type = (_typeProperty.Get(context.expression(0)) as tLIST).InnerType.ToString();
            }
            else
                type = _typeProperty.Get(context.expression(0)).ToString();

            switch (type)
            {
                case "INT":
                    type = "d";
                    break;
                case "STRING":
                    type = "s";
                    break;
                case "CHAR":
                    type = "s";
                    break;
                default:
                    break;
            }
            name = context.expression(0).GetChild(0).GetText();
            if (TypeIdentifier.ContainsKey(name))
            {
                code = "for(i=0; i < " + Identifier[name] + "; i++)" + Environment.NewLine;
                code += "{ printf(" + '"' + "%" + type + " \\n" + '"' + "," + name + "[i] ); }";
            }
            else
            {
                code = "printf(" + '"' + "%" + type + "\\n" + '"' + "," + Visit(context.expression(0)) + ");";
            }

            return code;
        }
        #endregion

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

        private string findType(string type)
        {
            switch (type)
            {
                case "INT":
                    type = "int";
                    break;
                case "CHAR":
                    type = "char";
                    break;
                case "BOOL":
                    type = "int";
                    break;
                case "STRING":
                    type = "* char";
                    break;

                default:
                    break;
            }
            return type;
        }
        #endregion
    }
}
