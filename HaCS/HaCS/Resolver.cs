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
    class Resolver : HaCSBaseVisitor<Object>
    {
        private ParseTreeProperty<IScope> _scopes;
        private ParseTreeProperty<BaseSymbol.HaCSType> _types;
        public Resolver(ParseTreeProperty<IScope> scopes, ParseTreeProperty<BaseSymbol.HaCSType> types)
        {
            _scopes = scopes;
            _types = types;
        }

        public override Object VisitExponent( HaCSParser.ExponentContext context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Exponent");
            }
            return null;
        }

        public override Object VisitArith1(HaCSParser.Arith1Context context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Arithmetic MUL/DIV/MOD");
            }

            return null;
        }

        public override Object VisitArith2([NotNull] HaCSParser.Arith2Context context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Arithmetic PLUS/MINUS");
            }

            return null;
        }

        public override object VisitCompare(HaCSParser.CompareContext context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Comparison");
            }

            return null;
        }

        public override object VisitEquality(HaCSParser.EqualityContext context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Equality");
            }

            return null;
        }

        public override object VisitPipe(HaCSParser.PipeContext context)
        {
            string name = context.IDENTIFIER().GetText();
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Pipe at variable" + name);
            }

            return null;
        }

        public override object VisitAnd(HaCSParser.AndContext context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: And");
            }

            return null;
        }

        public override object VisitOr(HaCSParser.OrContext context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Equality");
            }

            return null;
        }

        public override object VisitFunc(HaCSParser.FuncContext context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Function");
            }

            return null; ;
        }

        public override object VisitVar(HaCSParser.VarContext context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Variable");
            }

            return null;
        }

        public override object VisitVarDcl(HaCSParser.VarDclContext context)
        {
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + "" + " - Errortype: Declaration of variable");
            }

            return null;
        }
    }
}
