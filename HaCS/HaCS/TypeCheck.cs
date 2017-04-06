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
        private IScope _currentScope;

        public TypeCheck(ParseTreeProperty<IScope> scopes)
        {
            _scopes = scopes;
        }

        public ParseTreeProperty<BaseSymbol.HaCSType> Types
        {
            get { return _types; }
        }

        public override object VisitFunctionDecl(HaCSParser.FunctionDeclContext context)
        {
            _currentScope = _scopes.Get(context);
            VisitChildren(context);
            _currentScope = _currentScope.EnclosingScope;
            return null;
        }

        public override object VisitMain( HaCSParser.MainContext context)
        {
            _currentScope = _scopes.Get(context);
            VisitChildren(context);
            _currentScope = _currentScope.EnclosingScope;
            return null;
        }

        public override object VisitBody(HaCSParser.BodyContext context)
        {
            _currentScope = _scopes.Get(context);
            VisitChildren(context);
            _currentScope = _currentScope.EnclosingScope;
            return null;
        }

        public override Object VisitParens(HaCSParser.ParensContext context)
        {
            BaseSymbol.HaCSType type = (BaseSymbol.HaCSType)Visit(context.expression());
            _types.Put(context, type);
            return type;
        }

        public override Object VisitExponent(HaCSParser.ExponentContext context)
        {
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            _types.Put(context, _determineType(type1, type2)); //hhahah du troede lige det ville være en god kommentar, men nej. 
            return _determineType(type1, type2);

        }

        public override object VisitVar( HaCSParser.VarContext context)
        {
            string name = context.IDENTIFIER().GetText();
            BaseSymbol.HaCSType type = _currentScope.Resolve(name).SymbolType;

            _types.Put(context, type);

            return type;
        }

        public override Object VisitArith2(HaCSParser.Arith2Context context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            _types.Put(context, _determineType(type1, type2));
            return _determineType(type1, type2);
        }

        public override Object VisitArith1(HaCSParser.Arith1Context context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            _types.Put(context, _determineType(type1, type2));
            return _determineType(type1, type2);
        }

        public override Object VisitCompare(HaCSParser.CompareContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            _types.Put(context, _determineType(type1, type2));
            return _determineType(type1, type2);
        }

        public override Object VisitEquality(HaCSParser.EqualityContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type3;

            if (type1 != BaseSymbol.HaCSType.tCHAR && type2 != BaseSymbol.HaCSType.tCHAR)
            {
                if ((type1 == BaseSymbol.HaCSType.tFLOAT && type2 == BaseSymbol.HaCSType.tFLOAT) || (type1 == BaseSymbol.HaCSType.tINT && type2 == BaseSymbol.HaCSType.tINT) || (type1 == BaseSymbol.HaCSType.tBOOL && type2 == BaseSymbol.HaCSType.tBOOL))
                {
                    type3 = type1;
                }
                else if ((type1 == BaseSymbol.HaCSType.tFLOAT && type2 == BaseSymbol.HaCSType.tINT) || (type1 == BaseSymbol.HaCSType.tINT && type2 == BaseSymbol.HaCSType.tFLOAT))
                {
                    type3 = BaseSymbol.HaCSType.tFLOAT;
                }
                else
                {
                    type3 = BaseSymbol.HaCSType.tINVALID;
                }
            }
            else
            {
                type3 = BaseSymbol.HaCSType.tINVALID;
            }
            _types.Put(context, type3);
            return type3;
        }

        public override Object VisitPipe(HaCSParser.PipeContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            _types.Put(context, _determineType(type1, type2));
            return _determineType(type1, type2);
        }

        public override Object VisitFunc(HaCSParser.FuncContext context)
        {
            string name = context.IDENTIFIER().GetText();
            BaseSymbol.HaCSType type = _currentScope.Resolve(name).SymbolType;
            _types.Put(context,type);
            return type;
        }

        public override object VisitVarDcl(HaCSParser.VarDclContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type2 = Toolbox.getType(context.primitiveType().Start.Type); 

            if (type1 == type2)
            {
                _types.Put(context, type1);
            }
            else if ((type1 == BaseSymbol.HaCSType.tINT && type2 == BaseSymbol.HaCSType.tFLOAT) || (type2 == BaseSymbol.HaCSType.tINT && type1 == BaseSymbol.HaCSType.tFLOAT))
            {
                _types.Put(context, BaseSymbol.HaCSType.tFLOAT);
            }
            else
            {
                _types.Put(context, BaseSymbol.HaCSType.tINVALID);
            }
            return null;
        }

        public override Object VisitAnd(HaCSParser.AndContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type3;

            if (type1 == BaseSymbol.HaCSType.tBOOL && type2 == BaseSymbol.HaCSType.tBOOL)
            {
                type3 = BaseSymbol.HaCSType.tBOOL;
            }
            else
            {
                type3 = BaseSymbol.HaCSType.tINVALID;
            }
            _types.Put(context, type3);
            return null;
        }

        public override Object VisitOr(HaCSParser.OrContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type3;

            if (type1 == BaseSymbol.HaCSType.tBOOL && type2 == BaseSymbol.HaCSType.tBOOL)
            {
                type3 = BaseSymbol.HaCSType.tBOOL;
            }
            else
            {
                type3 = BaseSymbol.HaCSType.tINVALID;
            }
            _types.Put(context, type3);
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
