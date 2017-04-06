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
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Exponent");
            }
            return null;
        }

        public override Object VisitArith1(HaCSParser.Arith1Context context)
        {
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Arithmetic MUL/DIV/MOD");
            }

            return null;
        }

        public override Object VisitArith2([NotNull] HaCSParser.Arith2Context context)
        {
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Arithmetic PLUS/MINUS");
            }

            return null;
        }

        public override object VisitCompare(HaCSParser.CompareContext context)
        {
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Comparison");
            }

            return null;
        }

        public override object VisitEquality(HaCSParser.EqualityContext context)
        {
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Equality");
            }

            return null;
        }

        public override object VisitPipe(HaCSParser.PipeContext context)
        {
            int linenumber = context.Start.Line;

            string name = context.IDENTIFIER().GetText();
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Pipe at variable" + name);
            }

            return null;
        }

        public override object VisitAnd(HaCSParser.AndContext context)
        {
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: And");
            }

            return null;
        }

        public override object VisitOr(HaCSParser.OrContext context)
        {
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Equality");
            }

            return null;
        }

        public override object VisitFunc(HaCSParser.FuncContext context)
        {
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Function");
            }

            return null; ;
        }

        public override object VisitVar(HaCSParser.VarContext context)
        {
            int linenumber = context.Start.Line;

            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Variable");
            }

            return null;
        }

        public override object VisitVarDcl(HaCSParser.VarDclContext context)
        {
            int linenumber = context.Start.Line;
            if (_types.Get(context) == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + linenumber + " - Errortype: Declaration of variable");
            }

            return null;
        }
    }
}
