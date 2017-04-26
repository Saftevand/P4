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
        #region Scope handling
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
        #endregion

        #region Expression handling
        public override object VisitParens(HaCSParser.ParensContext context)
        {
            HaCSType type = (HaCSType)Visit(context.expression());
            _types.Put(context, type);
            return type;
        }

        public override object VisitExponent(HaCSParser.ExponentContext context)
        {
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, type3);
            }
            else
            {
                _types.Put(context, type3);
            }
            return type3;

        }

        public override object VisitVar( HaCSParser.VarContext context)
        {
            string name = context.IDENTIFIER().GetText();
            HaCSType type = _currentScope.Resolve(name).SymbolType;
            if (context.listOpp() != null && type is tLIST)
            {
                type = (HaCSType)Visit(context.listOpp());
            }
            else if(context.listOpp() != null)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + ": Use of list operator on " + type + ", expected type: " + new tLIST());
                type = new tINVALID();
            }
            
            _types.Put(context, type);
            return type;
        }

        public override object VisitArith2(HaCSParser.Arith2Context context)
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, type3);
            }
            else
            {
                _types.Put(context, type3);
            }
            return type3;
        }

        public override object VisitArith1(HaCSParser.Arith1Context context)
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, type3);
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
            if (type3 is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, type3);
            }
            else
            {
                _types.Put(context, type3);
            }
            return type3;
        }

        public override object VisitEquality(HaCSParser.EqualityContext context)
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);

            if(type1 == type2)
            {
                HaCSType result = new tBOOL();
                _types.Put(context, result);
                return result;
            }
            else if((type1 is tFLOAT && type2 is tINT) || (type1 is tINT && type2 is tFLOAT))
            {
                HaCSType result = new tBOOL();
                _types.Put(context, result);
                return result;
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected similar types on each side of equality sign, but got " + type1 + " and " + type2);
                return new tINVALID();
            }
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

        #region List and Var dcl

        public override object VisitListDcls(HaCSParser.ListDclsContext context)
        {
            _typeListValue.Add(new tLIST());
            HaCSType dclType = _typeListDcl.Last();
            HaCSType valueType;
            if (context.expression().Count() != 0)
            {
                foreach (HaCSParser.ExpressionContext exp in context.expression())
                {
                    valueType = (HaCSType)Visit(exp);
                    _typeListValue.Add(valueType);
                    if (dclType.GetType() != valueType.GetType())
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
            HaCSType result = (HaCSType)Visit(context.type());
            return result;
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
            HaCSType result = new tLIST();
            _typeListDcl.Add(result);
            Visit(context.listType());
            result = CreateListType(result as tLIST,1);
            Visit(context.listDcls());
            return result;
        }

        private tLIST CreateListType(tLIST listType, int typeCounter)
        {
            listType.InnerType = _typeListDcl[typeCounter];
            
            if(typeCounter < _typeListDcl.Count && _typeListDcl[typeCounter] is tLIST)
            {
                typeCounter++;
                CreateListType(listType.InnerType as tLIST,typeCounter);
            }
            return listType;
        }

        public override object VisitVarDcl(HaCSParser.VarDclContext context)
        {
            HaCSType resultType;
            if (context.right == null)
            {
                resultType = TypeCheckListDcl(context);
            }
            else
            {
                resultType = TypeCheckPrimitiveDcl(context);
            }
            _types.Put(context, resultType);
            return resultType;           
        }
        #endregion
        #region List opreations
        public override object VisitFind(HaCSParser.FindContext context)
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType innerType = type.LastType();
            HaCSType expType = (HaCSType)Visit(context.expression());

            if(innerType.GetType() == expType.GetType())
            {
                _types.Put(context, innerType);
                return innerType;
            }
            else if(innerType is tFLOAT && expType is tINT || innerType is tINT && expType is tFLOAT)
            {
                _types.Put(context, new tFLOAT());
                return new tFLOAT();
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected similar types, but got " + innerType + " and " + expType);
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
            
        }

        public override object VisitWhere(HaCSParser.WhereContext context)
        {
            return base.VisitWhere(context);
        }
        #endregion
        #endregion

        private HaCSType TypeCheckPrimitiveDcl(HaCSParser.VarDclContext context)
        {
            HaCSType type1 = (HaCSType)Visit(context.right);
            HaCSType type2 = Toolbox.getType(context.primitiveType().Start.Type);
            if (type1.GetType() == type2.GetType())
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

        private HaCSType TypeCheckListDcl(HaCSParser.VarDclContext context)
        {
            HaCSType result = (HaCSType)Visit(context.listDcl());
            int i = 0;
            bool typeError = false;
            foreach (HaCSType type in _typeListDcl.Where(x => x is tLIST))
            {
                if (_typeListValue[i].GetType() != type.GetType())
                {
                    typeError = true;
                }
                i++;
            }
            if (typeError)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + ": Inconsistent list declaration, value does not match declaration");
            }
            return result;
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
