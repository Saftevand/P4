using System;
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
    public class TypeCheck : HaCSBaseVisitor<Object>
    {
        private ParseTreeProperty<HaCSType> _types = new ParseTreeProperty<HaCSType>();
        private List<HaCSType> _typeListDcl = new List<HaCSType>();
        private List<HaCSType> _typeListValue = new List<HaCSType>();
        private ParseTreeProperty<IScope> _scopes;
        private IScope _currentScope;

        public TypeCheck(ParseTreeProperty<IScope> scopes)
        {
            _scopes = scopes;
        }

        public ParseTreeProperty<HaCSType> Types
        {
            get { return _types; }
        }

        public override object VisitFunctionDecl(HaCSParser.FunctionDeclContext context)
        {
            _currentScope = _scopes.Get(context);
            VisitChildren(context);
            string name = context.IDENTIFIER().GetText();
            HaCSType type = _currentScope.Resolve(name).SymbolType;
            _types.Put(context, type);
            _currentScope = _currentScope.EnclosingScope;

            return type;
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

        public override object VisitParens(HaCSParser.ParensContext context)
        {
            HaCSType type = (HaCSType)Visit(context.expression());
            _types.Put(context, type);
            return null;
        }

        public override object VisitExponent(HaCSParser.ExponentContext context)
        {
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 == tINVALID)
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
            HaCSType type = _currentScope.Resolve(name).SymbolType;

            _types.Put(context, type);

            return type;
        }

        public override object VisitArith2(HaCSParser.Arith2Context context)
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 == tINVALID)
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
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 == tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
            }
            else
            {
                _types.Put(context, type3);
            }
            return type3;
        }

        public override object VisitCompare(HaCSParser.CompareContext context)
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 == tINVALID)
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
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);

            if (type1 != tCHAR && type2 != tCHAR)
            {
                if ((type1 is tFLOAT && type2 is tFLOAT) || (type1 is tINT && type2 is tINT) || (type1 is tBOOL && type2 is tBOOL))
                {
                    _types.Put(context, type1);
                }
                else if ((type1 is tFLOAT && type2 is tINT) || (type1 is tINT && type2 is tFLOAT))
                {
                    _types.Put(context, tFLOAT);
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
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                return type3;
            }
            else
            {
                _types.Put(context, type3);
                return type3;
            }
        }

        public override object VisitFunc(HaCSParser.FuncContext context)
        {

            string name = context.IDENTIFIER().GetText();
            FunctionSymbol sym = (FunctionSymbol)_currentScope.Resolve(name);
            int i = 0;
            foreach (var item in sym.Symbols)
            {
                if (item.Value.SymbolType == (HaCSType)Visit(context.expression()[i]))
                {
                    _types.Put(context, item.Value.SymbolType);
                }
                else
                {
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected " + item.Value.SymbolType + ", but got " + (HaCSType)Visit(context.expression()[i]));
                }
                i++;
            }
            
            return sym.SymbolType;
        }

        public override object VisitListDcls(HaCSParser.ListDclsContext context)
        {
            _typeListValue.Add(new tLIST());
            HaCSType dclType = _typeListDcl.Last();
            HaCSType valueType;
            if(context.expression().Count() != 0)
            {
                foreach (HaCSParser.ExpressionContext exp in context.expression())
                {
                    valueType = (HaCSType)Visit(exp);
                    _typeListValue.Add(valueType);
                    if (dclType != valueType)
                    {
                        Console.WriteLine("Error at line: " + context.Start.Line + ": conflicting types, expected " + dclType + ", but got " + valueType);
                    }
                }
            }
            VisitChildren(context);
            return new tLIST();
        }

        public override object VisitListType(HaCSParser.ListTypeContext context)
        {
            Visit(context.type());
            return _typeListDcl;
        }

        public override object VisitType(HaCSParser.TypeContext context)
        {
            HaCSType type;
            if (context.listType() == null)
            {
                type = Toolbox.getType(context.primitiveType().Start.Type);
                _typeListDcl.Add(type);
                
            }
            else
            {
                type = Toolbox.getType(context.listType().Start.Type);
                _typeListDcl.Add(type);
                VisitChildren(context);
                
            }
            return type;
        }

        public override object VisitListDcl(HaCSParser.ListDclContext context)
        {
            _typeListDcl.Clear();
            _typeListValue.Clear();
            _typeListDcl.Add(new tLIST());
            Visit(context.listType());
            Visit(context.listDcls());
            return null;
        }

        private HaCSType TypeCheckListDcl(HaCSParser.VarDclContext context)
        {
            Visit(context.listDcl());
            int i = 0;
            bool typeError = false;
            foreach (HaCSType type in _typeListDcl.Where(x => x is tLIST))
            {
                if(_typeListValue[i] != type)
                {
                    typeError = true;
                }
                i++;
            }
            if(typeError)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + ": Inconsistent list declaration, value does not match declaration");
                return new tINVALID();
            }
            return new tLIST();            
        }

        public override object VisitVarDcl(HaCSParser.VarDclContext context)
        {
            HaCSType resultType, type1, type2;
            if(context.right == null)
            {                
                resultType = TypeCheckListDcl(context);
            }
            else
            {
                //resultType = TypeCheckPrimitiveDcl(context);
               
            }

            type1 = (HaCSType)Visit(context.right);
            type2 = Toolbox.getType(context.primitiveType().Start.Type);
            if (type1 == type2)
            {
                _types.Put(context, type1);
                return type1;
            }
            else if ((type1 is tINT && type2 is tFLOAT) || (type2 is tINT && type1 is tFLOAT))
            {
                _types.Put(context, new tFLOAT());
                return new tFLOAT();
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected similar types, but got " + type1 + " and " + type2);
                return new tINVALID();
            }            
        }

        public override object VisitAnd(HaCSParser.AndContext context)
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);

            if (type1 is tBOOL && type2 is tBOOL)
            {
                _types.Put(context, type1);
                return type1;
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1 + " and " + type2);
                return new tINVALID();
            }
        }

        public override object VisitOr(HaCSParser.OrContext context)
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);

            if (type1 is tBOOL && type2 is tBOOL)
            {
                _types.Put(context, type1);
                return type1;
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1 + " and " + type2);
                return new tINVALID();
            }
        }

        public override object VisitLit(HaCSParser.LitContext context)
        {
            int typeTokenType = context.Start.Type;
            HaCSType type = Toolbox.getType(typeTokenType);
            _types.Put(context, type);
            return type;
        }
   
        private HaCSType _determineType(HaCSType type1, HaCSType type2)
        {
            if (type1 is tBOOL || type1 is tCHAR || type2 is tBOOL || type2 is tCHAR)
            {
                return new tINVALID();
            }
            else
            {
                if ((type1 is tFLOAT && type2 is tFLOAT) || (type1 is tINT && type2 is tINT))
                {
                    return type1;
                }
                else
                {
                    return new tFLOAT();
                }
            }
                
            
            
        }

        public override object VisitIfStmt(HaCSParser.IfStmtContext context)
        {
            HaCSType type1 = (HaCSType)Visit(context.exp1);

            if (type1 is tBOOL)
            {
                _types.Put(context, type1);
                return type1;
            }
            else
            {                
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1);
                return new tINVALID();
            }
        }

        public override object VisitElseifStmt(HaCSParser.ElseifStmtContext context)
        {
            HaCSType type1 = (HaCSType)Visit(context.exp2);

            if (type1 is tBOOL)
            {
                _types.Put(context, type1);
                return type1;
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1);
                return new tINVALID();
            }
            
        }

        public override object VisitReturnStmt( HaCSParser.ReturnStmtContext context)
        {
            HaCSType type = Toolbox.getType(context.Start.Type);
            _types.Put(context, type);

            return null;
        }

    }
}
