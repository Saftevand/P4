using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using HaCS.SymbolTable;
using HaCS.Types;

namespace HaCS
{
    class CodeGen : HaCSBaseVisitor<Object>
    {
        private char indexer = 'i';
        public StringBuilder cCode = new StringBuilder();                           //C code areas which are added to by the templates visited.    
        public StringBuilder cFunctionCode = new StringBuilder();
        public StringBuilder cPrototype = new StringBuilder();
        List<string> cList = new List<string>();
        Dictionary<string, string> Identifier = new Dictionary<string, string>();   //Dictionaries that allow this class understand distinctions of identifiers.
        Dictionary<string, string> TypeIdentifier = new Dictionary<string, string>();
        Dictionary<string, List<string>> IdentifierValue = new Dictionary<string, List<string>>();
        private ParseTreeProperty<HaCSType> _typeProperty;                          //Containing information about types.
        private int funcCount;                                                      //Integer allowing the templates to create distinct functions based on this indexer.
        public CodeGen(ParseTreeProperty<HaCSType> typeProperty)
        {
            _typeProperty = typeProperty;
        }

        public override object VisitTerminal(ITerminalNode node)
        {
            return node.GetText();
            //return base.VisitTerminal(node);
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
            cFunctionCode.Append(type + " " + context.IDENTIFIER().GetText() + context.LPAREN().GetText() + Code + context.RPAREN().GetText() + Visit(context.body()) + Environment.NewLine);

            cPrototype.Append(type + " " + context.IDENTIFIER().GetText() + context.LPAREN().GetText() + Code + context.RPAREN().GetText() + ";" + Environment.NewLine);
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
            string type = Visit(context.type()).ToString();                     //Visit the type of the context.
            string identifier = context.IDENTIFIER().GetText();                 //Output the identifier of the context.

            return type + " " + identifier;                                     //The method return the equilevant code for C.
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
            string code = null;

            foreach (var item in context.stmt())
            {
                code += base.Visit(item);
            }
            if (context.returnStmt() != null)
            {
                code += base.Visit(context.returnStmt());
            }
            string index = null;
            if (context.Parent.Parent.Parent == null)
            {
                index = "int i;" + Environment.NewLine;
            }
            return "{" + index + code + " }";
        }

        public override object VisitMap([NotNull] HaCSParser.MapContext context)
        {
            string code = null;
            string size = null;
            string exp = null;
            string name = context.Parent.Parent.Parent.GetChild(1).GetText();
            string Pname = FindLastIdentifier(context, true).IDENTIFIER().GetText();
            string nameType = TypeIdentifier[name];

            string sizeCalc = "strlen";
            if (nameType.Contains("int"))
            {
                sizeCalc = "sizeof";
            }
            if (Identifier.ContainsKey(name))
            {
                size = Identifier[name];
                code += " " + Pname + "[" + size + "];" + Environment.NewLine;
            }
            else
            {
                code += " = malloc(" + sizeCalc + "(" + Pname + "));" + Environment.NewLine;
                size = sizeCalc + "(" + Pname + "-1)";
            }

            code += "for(i=0;i<" + size + ";i++)";
            exp = Visit(context.lambdaExp()).ToString();
            exp = exp.Replace(context.lambdaExp().GetChild(2).GetText(), Pname + "[i]");
            if (context.lambdaExp().lambdaBody().body() == null)
            {
                code += name + "[i] = " + exp;
            }
            else
            {
                code += exp;
            }

            code += Environment.NewLine;

            return code;
        }

        public override object VisitListDcl([NotNull] HaCSParser.ListDclContext context)
        {
            cList.Clear();
            string variable = context.GetChild(1).GetText();
            string type = Visit(context.listType()).ToString();
            TypeIdentifier.Add(variable, type);
            string exp = Visit(context.listDcls()).ToString();
            if (!exp.Contains("for"))
            {
                type = type.Replace("*", "");
            }
            return type + " " + variable + exp;
        }

        public override object VisitListDcls([NotNull] HaCSParser.ListDclsContext context)
        {
            string code = null;
            string variable = FindIdentifier(context).IDENTIFIER().GetText();
            string type = TypeIdentifier[variable];
            int count = type.Count(x => x == '*');
            string size = null;
            int arrayCount = 0;
            //string type = context.Parent.GetChild(0).GetChild(2).GetChild(0).GetText();

            if (context.GetText().Contains(".."))
            {
                for (int i = 1; i <= count; i++)
                {
                    code += "[" + "]";
                }
                code += " = ";
                size = Visit(context.expression(0)).ToString();
                Identifier.Add(variable, size);
                //code += size;
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
                List<string> value = new List<string>();
                string arraySize = null;
                int elements = 0;
                code += " = {";
                foreach (var item in context.listDcls())
                {
                    code += "{";
                    arrayCount++;
                    code += item.GetText();
                    code += ", '\\0' },";
                    if (elements < item.expression().Count())
                    {
                        elements = item.expression().Count();
                    }
                }
                code = code.Remove(code.Length - 1);
                if (context.listDcls().Count() <= 1)
                {
                    code += "{";
                    int i;
                    for (i = 0; i < context.ChildCount - 1; i = i + 2)
                    {
                        value.Add(context.GetChild(i).GetText() + ", ");
                        code += context.GetChild(i).GetText() + ", ";
                    }
                    value.Add(context.GetChild(i).GetText());
                    IdentifierValue.Add(variable, value);
                    code += context.GetChild(i).GetText() + ", '\\0'";
                    arrayCount = i / 2 + 2;
                }

                code += "}";
                for (int i = 1; i <= count; i++)
                {
                    if (i == count && elements > arrayCount)
                    {
                        arrayCount = elements + 1;
                    }
                    arraySize += "[" + arrayCount + "]";
                }

                Identifier.Add(variable, arrayCount.ToString());
                return arraySize + code + ";";
            }
            else
            {
                string exp = Visit(context.expression(0)).ToString();

                return exp;
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
            str = "strcpy(" + assigner + ", " + identifier + ");" + Environment.NewLine;
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
            string type = Visit(context.type()).ToString();
            type = type.Replace("List<", "").Replace(">", "*");
            return type + "*";
        }

        public override object VisitVarDcl([NotNull] HaCSParser.VarDclContext context)
        {
            if (context.GetChild(0).GetText().Contains("List"))
            {
                return base.VisitVarDcl(context);
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
            return context.GetChild(0).GetChild(2).GetChild(0).GetText() + "*";
            //return base.VisitType(context);
        }

        public override object VisitCompileUnit([NotNull] HaCSParser.CompileUnitContext context)
        {
            return base.VisitCompileUnit(context);
        }

        private T FindLastContext<T>(RuleContext context) where T : RuleContext
        {
            if (context is T)
            {
                return context as T;
            }
            if (context.parent != null) return FindLastContext<T>(context.parent);
            else return null;
        }

        public override object VisitReturnStmt([NotNull] HaCSParser.ReturnStmtContext context)
        {
            string Identifier;
            string exp = Visit(context.expression()).ToString();
            if ((FindLastContext<HaCSParser.LambdaExpContext>(context)) != null)
            {
                Identifier = FindLastContext<HaCSParser.ListDclContext>(context).IDENTIFIER().GetText();
                return Identifier + "[i] = " + exp + ";" + Environment.NewLine;
            }
            return context.RETURN().ToString() + " " + exp + ";" + Environment.NewLine;
        }

        public override object VisitElement([NotNull] HaCSParser.ElementContext context)
        {
            return context.IDENTIFIER().ToString() + context.LBRACKET().ToString() + Visit(context.expression()).ToString() + context.RBRACKET().ToString();
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
                for (int i = Convert.ToInt32(range_from); i <= iRange_to; i++)
                {
                    cList.Add(i + ", ");
                }
                iRange_to = Convert.ToInt32(range_to);
            }
            else if (_typeProperty.Get(context.expression(0)) is tCHAR)
            {
                char[] from = range_from.ToCharArray();
                char[] to = range_to.ToCharArray();
                for (char c = from[1]; c <= to[1]; c++)
                {
                    cList.Add("'" + c + "', ");
                    iRange_to++;
                }
                iRange_to++;
            }
            cList.Add("'\\0'");
            cList.Add("}");
            return iRange_to + 1;
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
                value.RemoveAt(index - (++offset));
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
            string nameType = TypeIdentifier[name];
            string size = null;
            string sizeCalc = "strlen";
            string PnameType = "char";
            if (TypeIdentifier.ContainsKey(Pname))
            {
                PnameType = TypeIdentifier[Pname];
            }
            string nested = null;
            if (PnameType.Contains("int"))
            {
                sizeCalc = "sizeof";
                nested += "*";
            }

            for (int i = 1; i < nameType.Count(x => x == '*'); i++)
            {
                nested += "*";
            }
            string exp = Visit(context.lambdaExp()).ToString();
            exp = exp.Replace(context.lambdaExp().GetChild(2).GetText(), context.Parent.GetChild(0) + "[i]");

            size = sizeCalc + "(" + nested + Pname + ")";
            code += " = (" + nameType + ")malloc(sizeof(" + Pname + "));" + Environment.NewLine;
            //code += listAllocation(name);

            code += "int " + ++indexer + " = 0; for(i=0;i<" + size + ";i++) { if(" + exp + "){" + name + "[" + indexer + "++] = " + Pname + "[i];" + "} }";
            Identifier.Add(name, indexer.ToString());
            return code;
        }

        public override object VisitIndexOf([NotNull] HaCSParser.IndexOfContext context)
        {
            HaCSParser.VarContext varContext = FindLastIdentifier(context);
            string name = varContext.IDENTIFIER().GetText();
            string nameType = TypeIdentifier[name];
            string sizeCalc = "strlen";
            if (nameType.Contains("int"))
            {
                sizeCalc = "sizeof";
            }
            string exp = Visit(context.lambdaExp()).ToString();
            string expType = Visit(context.lambdaExp().type(0)).ToString();

            string Pname = FindLastIdentifier(context, true).IDENTIFIER().GetText();

            exp = exp.Replace(context.lambdaExp().GetChild(2).GetText(), name + "[i]");

            cFunctionCode.Append("int func" + ++funcCount + "(" + nameType + " " + name + ", " + expType + " x" + "){" + "int i; for(i = 0; i < " + sizeCalc + "(" + name + "); i++){if(" + exp + "){return i;}}}");
            cPrototype.Append("int func" + funcCount + "(" + nameType + " " + name + ", " + expType + " x" + ");");
            return "func" + funcCount + "(" + name + ", " + Pname + "[i])";
        }

        public override object VisitReduce([NotNull] HaCSParser.ReduceContext context)
        {
            HaCSParser.VarContext varContext = FindLastIdentifier(context);
            string name = varContext.IDENTIFIER().GetText();
            string nameType = TypeIdentifier[name];
            string exp = context.lambdaExp().lambdaBody().expression().GetText();

            string accumulater = context.lambdaExp().GetChild(2).GetText();
            string iterator = context.lambdaExp().GetChild(5).GetText();

            exp = exp.Replace(iterator, name + "[i]").Insert(accumulater.Length + 2, "= ");

            cFunctionCode.Append("func" + ++funcCount + "(" + nameType + " " + name + ")" + Environment.NewLine);
            cFunctionCode.Append("{int " + accumulater + " = 0; int i; for(i = 0; i < " + Identifier[name] + "; i++){" + exp + ";} return " + accumulater + ";}");
            cPrototype.Append("int func" + funcCount + "(" + nameType + " " + name + ");");
            return "func" + funcCount + "(" + name + ")";
        }

        public override object VisitLength([NotNull] HaCSParser.LengthContext context)
        {
            string name = context.Parent.GetChild(0).GetText();
            if (Identifier.ContainsKey(name))
            {
                return Identifier[name];
            }
            return "strlen(" + name + ")";
        }

        public override object VisitContains([NotNull] HaCSParser.ContainsContext context)
        {
            string exp = Visit(context.expression()).ToString();
            string name = FindLastIdentifier(context).IDENTIFIER().GetText();
            string nameType = TypeIdentifier[name];
            string sizeCalc = "strlen";
            string expType = nameType.Replace('*', ' ');
            if (nameType.Contains("int"))
            {
                sizeCalc = "sizeof";
            }

            cFunctionCode.Append("int func" + ++funcCount + "(" + nameType + name + ", " + expType + " x)" + Environment.NewLine);
            cFunctionCode.Append("{ int i;" + Environment.NewLine);
            cFunctionCode.Append("for(i=0;i < " + sizeCalc + "(" + name + ")" + "; i++)" + Environment.NewLine);
            cFunctionCode.Append("{if(" + name + "[i] == " + exp + ")" + Environment.NewLine + "{ return 1;}" + "}" + Environment.NewLine);
            cFunctionCode.Append("return 0;}" + Environment.NewLine);
            string code = "func" + funcCount + "(" + name + ", " + exp + ")" + Environment.NewLine;

            cPrototype.AppendLine("int func" + funcCount + "(" + nameType + name + ", " + expType + " x);" + Environment.NewLine);
            return code;
        }

        public override object VisitLambdaExp([NotNull] HaCSParser.LambdaExpContext context)
        {
            return Visit(context.lambdaBody());
        }

        public override object VisitCompare([NotNull] HaCSParser.CompareContext context)
        {
            return Visit(context.expression(0)).ToString() + context.GetChild(1).GetText() + Visit(context.expression(1)).ToString();
            return base.VisitCompare(context);
        }

        public override object VisitLambdaBody([NotNull] HaCSParser.LambdaBodyContext context)
        {
            return base.VisitLambdaBody(context);
        }

        public override object VisitPrintStmt([NotNull] HaCSParser.PrintStmtContext context)
        {
            string code = null;
            string name = Visit(context.expression(0)).ToString();
            string type = null;
            bool list = false;
            string printType = null;
            if (_typeProperty.Get(context.expression(0)) is tLIST)
            {
                type = (_typeProperty.Get(context.expression(0)) as tLIST).InnerType.ToString();
            }
            else
                type = _typeProperty.Get(context.expression(0)).ToString();

            if (type.Contains("INT"))
            {
                printType = "d";
            }
            else
            {
                printType = "s";
            }
            code = "printf(" + '"' + "%" + printType + "\\n" + '"' + "," + name + ");";

            if (TypeIdentifier.ContainsKey(name))
            {
                type = TypeIdentifier[name];
                string nest = null;
                for (int i = 1; i < type.Count(x => x == '*'); i++)
                {
                    nest += "*";
                }

                code = "for(i=0;i < sizeof(" + name + ") / sizeof(" + nest + name + ")" + ";i++){printf(" + '"' + "%" + printType + "\\n" + '"' + "," + name + "[i]);}";
            }

            return code;
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

        private HaCSParser.VarContext FindLastIdentifier(RuleContext context, bool findOriginal = false)
        {
            if (findOriginal == false)
            {
                if (context.parent is HaCSParser.VarContext)
                {
                    return context.parent as HaCSParser.VarContext;
                }
                else return (FindLastIdentifier(context.parent));
            }
            else
            {
                if (context.parent is HaCSParser.ListDclContext)
                {
                    return context.GetChild(0) as HaCSParser.VarContext;
                }
                else return FindLastIdentifier(context.parent, true);
            }
        }

        private HaCSParser.ListDclContext FindIdentifier(HaCSParser.ListDclsContext context)
        {
            if (context.Parent is HaCSParser.ListDclContext)
            {
                HaCSParser.ListDclContext Parent = context.Parent as HaCSParser.ListDclContext;
                return Parent;
            }
            return FindIdentifier(context.Parent as HaCSParser.ListDclsContext);
        }

    }
}
