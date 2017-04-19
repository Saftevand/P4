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
            string name = context.IDENTIFIER().GetText();
            BaseSymbol.HaCSType type = _currentScope.Resolve(name).SymbolType;
            _types.Put(context, type);
            _currentScope = _currentScope.EnclosingScope;

            return type;
        }

        public override object VisitMain( HaCSParser.MainContext context)
        {
            _currentScope = _scopes.Get(context);
            VisitChildren(context);
            _currentScope = _currentScope.EnclosingScope;
            //return base.VisitMain(context);
            return null;
        }

        public override object VisitBody(HaCSParser.BodyContext context)
        {
            _currentScope = _scopes.Get(context);
            VisitChildren(context);
            _currentScope = _currentScope.EnclosingScope;
            return null;
        }

        public override object VisitParens(HaCSParser.ParensContext context)
        {
            BaseSymbol.HaCSType type = (BaseSymbol.HaCSType)Visit(context.expression());
            _types.Put(context, type);
            return null;
        }

        public override object VisitExponent(HaCSParser.ExponentContext context)
        {
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type3 = _determineType(type1, type2);
            if (type3 == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
            }
            else
            {
                _types.Put(context, type3);
            }
            return null;

        }

        public override object VisitVar( HaCSParser.VarContext context)
        {
            string name = context.IDENTIFIER().GetText();
            BaseSymbol.HaCSType type = _currentScope.Resolve(name).SymbolType;

            _types.Put(context, type);

            return type;
        }

        public override object VisitArith2(HaCSParser.Arith2Context context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type3 = _determineType(type1, type2);
            if (type3 == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
            }
            else
            {
                _types.Put(context, type3);
            }
            return null;
        }

        public override object VisitArith1(HaCSParser.Arith1Context context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type3 = _determineType(type1, type2);
            if (type3 == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
            }
            else
            {
                _types.Put(context, type3);
            }
            return null;
        }

        public override object VisitCompare(HaCSParser.CompareContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type3 = _determineType(type1, type2);
            if (type3 == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
            }
            else
            {
                _types.Put(context, type3);
            }
            return null;
        }

        public override object VisitEquality(HaCSParser.EqualityContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);

            if (type1 != BaseSymbol.HaCSType.tCHAR && type2 != BaseSymbol.HaCSType.tCHAR)
            {
                if ((type1 == BaseSymbol.HaCSType.tFLOAT && type2 == BaseSymbol.HaCSType.tFLOAT) || (type1 == BaseSymbol.HaCSType.tINT && type2 == BaseSymbol.HaCSType.tINT) || (type1 == BaseSymbol.HaCSType.tBOOL && type2 == BaseSymbol.HaCSType.tBOOL))
                {
                    _types.Put(context, type1);
                }
                else if ((type1 == BaseSymbol.HaCSType.tFLOAT && type2 == BaseSymbol.HaCSType.tINT) || (type1 == BaseSymbol.HaCSType.tINT && type2 == BaseSymbol.HaCSType.tFLOAT))
                {
                    _types.Put(context, BaseSymbol.HaCSType.tFLOAT);
                }
                else
                {
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected similar types on each side of equality sign, but got " + type1 + " and " + type2);
                }
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected types int, float or bool, but got char");
            }
            return null;
        }

        public override object VisitPipe(HaCSParser.PipeContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type3 = _determineType(type1, type2);
            if (type3 == BaseSymbol.HaCSType.tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
            }
            else
            {
                _types.Put(context, type3);
            }
            return _determineType(type1, type2);
        }

        public override object VisitFunc(HaCSParser.FuncContext context)
        {

            string name = context.IDENTIFIER().GetText();
            FunctionSymbol sym = (FunctionSymbol)_currentScope.Resolve(name);
            int i = 0;
            foreach (var item in sym.Symbols)
            {
                if (item.Value.SymbolType == (BaseSymbol.HaCSType)Visit(context.expression()[i]))
                {
                    _types.Put(context, item.Value.SymbolType);
                }
                else
                {
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected " + item.Value.SymbolType + ", but got " + (BaseSymbol.HaCSType)Visit(context.expression()[i]));
                }
                i++;
            }
            
            return sym.SymbolType;
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
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected similar types, but got " + type1 + " and " + type2);
            }
            return null;
        }

        public override object VisitAnd(HaCSParser.AndContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);
            BaseSymbol.HaCSType type3;

            if (type1 == BaseSymbol.HaCSType.tBOOL && type2 == BaseSymbol.HaCSType.tBOOL)
            {
                _types.Put(context, type1);
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1 + " and " + type2);
            }
            return null;
        }

        public override object VisitOr(HaCSParser.OrContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.left);
            BaseSymbol.HaCSType type2 = (BaseSymbol.HaCSType)Visit(context.right);

            if (type1 == BaseSymbol.HaCSType.tBOOL && type2 == BaseSymbol.HaCSType.tBOOL)
            {
                _types.Put(context, type1);
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1 + " and " + type2);
            }
            
            return null;
        }

        public override object VisitLit(HaCSParser.LitContext context)
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

        public override object VisitIfStmt(HaCSParser.IfStmtContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.exp1);

            if (type1 == BaseSymbol.HaCSType.tBOOL)
            {
                _types.Put(context, type1);
            }
            else
            {                
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1);
            }
            return null;
        }

        public override object VisitElseifStmt(HaCSParser.ElseifStmtContext context)
        {
            BaseSymbol.HaCSType type1 = (BaseSymbol.HaCSType)Visit(context.exp2);

            if (type1 == BaseSymbol.HaCSType.tBOOL)
            {
                _types.Put(context, type1);
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1);
            }
            return null;
        }

        public override object VisitReturnStmt( HaCSParser.ReturnStmtContext context)
        {
            BaseSymbol.HaCSType type = Toolbox.getType(context.Start.Type);
            _types.Put(context, type);

            return null;
        }

    }
}
