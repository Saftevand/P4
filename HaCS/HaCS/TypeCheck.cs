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
    public class TypeCheck : HaCSBaseVisitor<Object>
    {
        private ParseTreeProperty<BaseSymbol.HaCSType> _types = new ParseTreeProperty<BaseSymbol.HaCSType>();
        private ParseTreeProperty<IScope> _scopes;

        public TypeCheck(ParseTreeProperty<IScope> scopes)
        {
            _scopes = scopes;
        }

        public ParseTreeProperty<BaseSymbol.HaCSType> Types
        {
            get { return _types; }
        }

        public override object VisitParens(HaCSParser.ParensContext context)
        {
            BaseSymbol.HaCSType type = (BaseSymbol.HaCSType)VisitExpression(context.expression());
            _types.Put(context, type);
            return null;
        }

        public override object VisitExponent(HaCSParser.ExponentContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitExpression(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);
            _types.Put(context, _determineType(type1, type2)); //hhahah du troede lige det ville være en god kommentar, men nej.
            return null;
        }

        public override object VisitArith2(HaCSParser.Arith2Context context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitExpression(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);
            _types.Put(context, _determineType(type1, type2));
            return null;
        }

        public override object VisitArith1(HaCSParser.Arith1Context context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitExpression(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);
            _types.Put(context, _determineType(type1, type2));
            return null;
        }

        public override object VisitCompare(HaCSParser.CompareContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitExpression(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);
            _types.Put(context, _determineType(type1, type2));
            return null;
        }

        public override object VisitEquality(HaCSParser.EqualityContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitExpression(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);
            _types.Put(context, _determineType(type1, type2));
            return null;
        }

        public override object VisitPipe(HaCSParser.PipeContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitExpression(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);
            _types.Put(context, _determineType(type1, type2));
            return null;
        }

        public override object VisitFunc(HaCSParser.FuncContext context)
        {
            string name = context.IDENTIFIER().GetText();
            IScope currentScope = _scopes.Get(context);
            _types.Put(context,currentScope.Resolve(name).SymbolType);
            return null;
        }

        //public override object VisitVarDcl(HaCSParser.VarDclContext context)
        //{
        //    string name = context.IDENTIFIER().GetText();
        //    IScope currentScope = _scopes.Get(context);
        //    BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitPrimitiveType(context.left);
        //    BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);


        //    return null;
        //}

        public override object VisitAnd(HaCSParser.AndContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitExpression(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);
            _types.Put(context, _determineType(type1, type2));
            return null;
        }

        public override object VisitOr(HaCSParser.OrContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)VisitExpression(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)VisitExpression(context.right);
            _types.Put(context, _determineType(type1, type2));
            return null;
        }

        public override Object VisitLit(HaCSParser.LitContext context)
        {
            int typeTokenType = context.Start.Type;
            SymbolTable.BaseSymbol.HaCSType type = Toolbox.getType(typeTokenType);
            _types.Put(context, type);
            return type;
        }
        
   
        private BaseSymbol.HaCSType _determineType(BaseSymbol.HaCSType type1, BaseSymbol.HaCSType type2)
        {
            if (type1 != BaseSymbol.HaCSType.tBOOL || type1 != BaseSymbol.HaCSType.tCHAR || type2 != BaseSymbol.HaCSType.tBOOL || type2 != BaseSymbol.HaCSType.tCHAR)
            {
                if ((type1 == BaseSymbol.HaCSType.tFLOAT && type2 == BaseSymbol.HaCSType.tFLOAT) || (type1 == BaseSymbol.HaCSType.tINT && type2 == BaseSymbol.HaCSType.tINT))
                {
                    return type1;
                }
                else if (type1 == BaseSymbol.HaCSType.tFLOAT && type2 == BaseSymbol.HaCSType.tINT)
                {
                    return BaseSymbol.HaCSType.tFLOAT;
                }
                else
                {
                    return BaseSymbol.HaCSType.tFLOAT;
                }
            }
            return BaseSymbol.HaCSType.tINVALID;
        }

    }
}
