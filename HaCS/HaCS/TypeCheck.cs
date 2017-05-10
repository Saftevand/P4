using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using HaCS.SymbolTable;
using HaCS.Types;
using Antlr4.Runtime;
namespace HaCS
{
    public class TypeCheck : HaCSBaseVisitor<Object>                                        //The TypeChecker inherits from the BaseVisitor and overrides the behavior of how different contexts are visited in order to typecheck
    {
        #region Variables
        private ParseTreeProperty<HaCSType> _types = new ParseTreeProperty<HaCSType>();     //A parsetreeproperty used to annotate the nodes of the parsetree with their corresponding type.
        private ParseTreeProperty<IScope> _scopes;                                          //A parsetreeproperty used to annotate the nodes of the parsetree with their corresponding scope.
        private List<HaCSType> _typeListDcl = new List<HaCSType>();                         
        private List<HaCSType> _typeListValue = new List<HaCSType>();
        private IScope _currentScope;
        #endregion

        public TypeCheck(ParseTreeProperty<IScope> scopes)
        {
            _scopes = scopes;
        }

        #region Properties
        public ParseTreeProperty<HaCSType> Types
        {
            get { return _types; }
        }

        public ParseTreeProperty<IScope> Scopes
        {
            get { return _scopes; }
        }
        #endregion

        #region Scope handling
        public override object VisitFunctionDecl(HaCSParser.FunctionDeclContext context)
        {
            _currentScope = _scopes.Get(context);
            string name = context.IDENTIFIER().GetText();
            HaCSType type = _currentScope.Resolve(name).SymbolType;
            _types.Put(context, type);
            _currentScope = _currentScope.EnclosingScope;
            Visit(context.body());
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
        public override object VisitParens(HaCSParser.ParensContext context)                //When a parenthesis expression is encounterered the expression of the context is visited.
        {                                                                                   //Hereby the type is 'bubbled' up till the type is found.
            HaCSType type = (HaCSType)Visit(context.expression());                          //The type is determined
            _types.Put(context, type);                                                      //The type is added to the parsetreeproperty containing the types
            return type;                                                                    //The type of the expression - the type the expression within the parenthesis is returned.
        }

        public override object VisitExponent(HaCSParser.ExponentContext context)            //When a exponent expression is encounterered the expression of the context is visited and type promotion might occur.
        {
            HaCSType type2 = (HaCSType)Visit(context.right);                                //The right side of the context - for instance 3+5^2   the expression 2 is visited
            HaCSType type1 = (HaCSType)Visit(context.left);                                 //The left side of the context - for instance 3+5^2   the expression 3+5 is visited
            HaCSType type3 = _determineType(type1, type2);                                  //The types of the left and right-hand side are compared to check if the types can be evaluated.
            if (type3 is tINVALID)                                                          //Type promotion will happen between int and float. tINVALID is returned if the two types can't be evaluated.
            {                                                                               
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, type3);                                                 //The type determined by _determineType is added to the parsetreeproperty - tINVALID in this case
            }
            else
            {
                _types.Put(context, type3);                                                 //The type determined by _determineType is added to the parsetreeproperty
            }
            return type3;

        }

        public override object VisitVar( HaCSParser.VarContext context)                     //When a Variable expression is encounterered the Identifier of the context is looked up in the current scope. 
        {                                                                                   
            string name = context.IDENTIFIER().GetText();                                   //The identifier of the variable
            HaCSType type = _currentScope.Resolve(name).SymbolType;                         //Lookup in the current scope's dictionary of symbols.
            if (context.listOpp() != null && type is tLIST)                                 //Checks if the type is a list and is has a list operation.
            {
                type = (HaCSType)Visit(context.listOpp());                                  //The type of the list operation is returned
            }
            else if(context.listOpp() != null)                                              //If the context contains a list operation, but the context isn't a list an error will given. 
            {
                Console.WriteLine("Error at line: " + context.Start.Line + ": Use of list operator on " + type + ", expected type: " + new tLIST());
                type = new tINVALID();
            }
            
            _types.Put(context, type);                                                      //The type of the expression is added to the parsetreeproperty
            return type;                                                                    //The type of the expression is returned
        }

        public override object VisitArith2(HaCSParser.Arith2Context context)                //When a Arith2 expression is encounterered(* || / || %) the left and right expressions are visited.
        {
            HaCSType type1 = (HaCSType)Visit(context.left);                                 //The left side of the context - for instance (5+2*2+4) the expression 5+2 is visited
            HaCSType type2 = (HaCSType)Visit(context.right);                                //The right side of the context - for instance (5+2*2+4) the expression 2+4 is visited
            HaCSType type3 = _determineType(type1, type2);                                  //The types of the left and right-hand side are compared to check if the types can be evaluated.
            if (type3 is tINVALID)                                                          //Type promotion will happen between int and float. tINVALID is returned if the two types can't be evaluated.
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, type3);                                                 //The type determined by _determineType is added to the parsetreeproperty
            }
            else
            {
                _types.Put(context, type3);                                                 //The type determined by _determineType is added to the parsetreeproperty
            }
            return type3;                                                                   //The type of the expression is returned
        }

        public override object VisitArith1(HaCSParser.Arith1Context context)                //Same procedure, but when encountering (+ || -) expressions
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

        public override object VisitCompare(HaCSParser.CompareContext context)      //Same procedure, but when encountering (> || >= || < || <= ) expressions
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, type3);
                return type3;
            }
            else
            {
                _types.Put(context, new tBOOL());
                return new tBOOL();
            }
        }

        public override object VisitEquality(HaCSParser.EqualityContext context)            //Same procedure, but when encountering (== || != ) expressions
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType resultingType = _determineType(type1, type2);
            if(resultingType is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected similar types on each side of equality sign, but got " + type1 + " and " + type2);
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
            _types.Put(context, new tBOOL());
            return new tBOOL();
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

        public override object VisitFunc(HaCSParser.FuncContext context)                    //When encountering a function call - a lot like when encountering a variable
        {
            string name = context.IDENTIFIER().GetText();                                   //The identifier of the function
            FunctionSymbol sym = (FunctionSymbol)_currentScope.Resolve(name);               //Lookup in the current scope to find the function corresponding to the identifier
            int i = 0;
            foreach (var item in sym.Symbols)                                              //Iteration through all symbols(functions and variables) declared within the function
            {
                if (item.Value.SymbolType.Equals((HaCSType)Visit(context.expression()[i]))) //Checks whether the types of the actual parameters corresponds to the formal parameters.
                {
                    _types.Put(context, item.Value.SymbolType);                             //The type of the symbol is added to the parsetreeproperty
                }
                else                                                                        //Gives error if actual and formal parameters don't correspond.
                {
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected " + item.Value.SymbolType + ", but got " + (HaCSType)Visit(context.expression()[i]));
                }
                i++;
            }
            
            return sym.SymbolType;                                                          //Returns the type of the function - The function's return type
        }

        public override object VisitAnd(HaCSParser.AndContext context)                      //Same procedure, but when encountering (&&) expressions
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

        public override object VisitOr(HaCSParser.OrContext context)                        //Same procedure, but when encountering (||) expressions
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

        public override object VisitLit(HaCSParser.LitContext context)                      //When encountering literals (char, bool , int, floats)
        {
            int typeTokenType = context.Start.Type;                                         //Gets the integer of the type(ANTLR4)
            HaCSType type = Toolbox.getType(typeTokenType);                                 //Gets the HaCS type correponding to the int given by ANTLR
            _types.Put(context, type);                                                      //Adds the type to the parsetreeproperty
            return type;                                                                    //Returns the type of the expression
        }

        public override object VisitLambdaExp(HaCSParser.LambdaExpContext context)          //When encountering lambda expressions
        {
            _currentScope = _scopes.Get(context);                                           //Sets the current scope to the scope of the lambda expression
            bool terminalChecker = true;                                                    //Used as a flag to test whether all parameters in the lambda expression are used.
            if(context.lambdaBody().expression() != null)                                   //If the lambdabody(the left-hand side of the '=>') is an expression...
            {
                foreach (ITerminalNode terminal in context.IDENTIFIER())                    //It is checked whether all the identifiers of the parameters are used in the expression 
                {
                    List<ITerminalNode> tokenList = Toolbox.getFlatTokenList(context.lambdaBody().expression()); //getFlatTokenList returns a list containing the tokens from the expression
                    if (tokenList.Find(x => x.Symbol.Text == terminal.Symbol.Text) == null)                      //If an parameter is not to be found in the list of tokens, the flag is set to false 
                    {
                        terminalChecker = false;
                    }
                }
            }
            else
            {
                foreach (ITerminalNode terminal in context.IDENTIFIER())                    //Same as above, but just for a body instead of an expression
                {
                    List<ITerminalNode> tokenList = Toolbox.getFlatTokenList(context.lambdaBody().body());
                    if (tokenList.Find(x => x.Symbol.Text == terminal.Symbol.Text) == null)
                    {
                        terminalChecker = false;
                    }
                }
            }
            if(terminalChecker == false)                                                   //If the flag is false, an error is given, that not all parameters were used
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: missing identifier(s) in lambda expression");
                _types.Put(context, new tINVALID());                                      //The type is added to the parsetreeproperty
                _currentScope = _currentScope.EnclosingScope;                             //The scope is set back to the lambdabody's enclosing scope
                return new tINVALID();                                                    //The type tINVALID is returned since not all parameters were used
            }
            
            return Visit(context.lambdaBody());                                             //The type of the lambdabody is returned (left-hand side of '=>') by visiting the lambdaBody
        }

        public override object VisitElement(HaCSParser.ElementContext context)              //When encountering an Element expression - (list[2])
        {
            HaCSType type1 = (HaCSType)Visit(context.expression());                         //Gets the type of the expression - list[expression]
            HaCSType type2 = _currentScope.Resolve(context.IDENTIFIER().GetText()).SymbolType;//Gets the type of the identifier(should be a list) which the element expression is used on
            if (type2 is tLIST && type1 is tINT)                                            //Checks whether it's a list and the expression within '[]' evaluates to an integer  
            {
                _types.Put(context, (type2 as tLIST).InnerType);                            //Adds the type of the innermost list to the parsetreeproperty - for instance List<List<int>>
                return (type2 as tLIST).InnerType;                                          //Returns the type of the innermost list
            }
            else                                                                            //If it's not a list or the expression within'[]' doesn't evaluate to an integer, an error is given 
            {                                                                               //tINVALID is added to the parsetreeproperty and the tINVALID is returned 
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected tINT as index of tLIST, but got " + type1 + " as index of " + type2);
                _types.Put(context,new tINVALID());
                return new tINVALID();
            }
        }

        public override object VisitRange(HaCSParser.RangeContext context)                  //When encountering a Range expression - [0..9]
        {
            HaCSType type1 = (HaCSType)Visit(context.left);                                 //Gets the type of the expression on the left-hand side of '..'
            HaCSType type2 = (HaCSType)Visit(context.right);                                //Gets the type of the expression on the right-hand side of '..'
            HaCSType resultingType = _determineType(type1, type2);                          //Determines the type the left and right-hand evaluates to. Type promotion might happen
            _types.Put(context, resultingType);                                             //The type is added to the parsetreeproperty
            return resultingType;                                                           //Returns the type of Range
        }


        public override object VisitLambdaBody(HaCSParser.LambdaBodyContext context)        //When encountering a lambdaBody - right handside of ('=>')
        {
            if (context.parent.parent is HaCSParser.WhereContext || context.parent.parent is HaCSParser.FindContext || context.parent.parent is HaCSParser.IndexOfContext)
            {
                if(context.expression() != null)                                            //If the lambdaBody is used within the list operation where,find or index of and the lambdabody is an expression
                {
                    HaCSType expType = (HaCSType)Visit(context.expression());               //Gets the type the expression of the lambdabody evaluates to
                    _currentScope = _currentScope.EnclosingScope;                           //Sets the scope to the scope of the lambdaExpr
                    return IsBoolLambda(expType,context);                                   //IsBoolLambda returns the type the expression evaluates to either tINVALID or bool. 
                }
                else                                                                        //If the lambdabody contains a body instead of an expression
                {
                    List<HaCSType> returnTypes = new List<HaCSType>();                      //A list to contain all the return types within the body
                    foreach (HaCSParser.StmtContext stmt in context.body().stmt().Where(x => x.ifStmt() != null)) //Iterates through the if/else/else statements within the body
                    {
                            returnTypes.Add((HaCSType)Visit(stmt.ifStmt().body().returnStmt()));                  //Adds the return type of the statement to the list of return types
                    }
                    
                    returnTypes.Add((HaCSType)Visit(context.body().returnStmt()));          //Adds the body's return type to the list
                    _currentScope = _currentScope.EnclosingScope;                           //Sets the scope back to the lamdaExp's scope
                    return IsBoolLambda(returnTypes, context);                              //IsBoolLambda returns bool if all the types within the list returnTypes are bool, else tINVALID
                }
            }
            else
            {
                if (context.expression() != null)                                           //The lambdaBody contains an expression
                {
                    HaCSType expType = (HaCSType)Visit(context.expression());               //Gets the type the expression evaluates to
                    HaCSParser.VarContext varParent = FindLastVarContext(context);          //Gets the variable(list) on which the list operation is used on
                    tLIST listType = (tLIST)_currentScope.Resolve(varParent.IDENTIFIER().GetText()).SymbolType;  //
                    HaCSType result = _determineType(listType.InnerType, expType);
                    _types.Put(context, result);
                    _currentScope = _currentScope.EnclosingScope;
                    return result;
                }
                else
                {
                    List<HaCSType> returnTypes = new List<HaCSType>();
                    foreach (HaCSParser.StmtContext stmt in context.body().stmt().Where(x => x.ifStmt() != null))
                    {
                        returnTypes.Add((HaCSType)Visit(stmt.ifStmt().body().returnStmt()));
                    }
                    returnTypes.Add((HaCSType)Visit(context.body().returnStmt()));
                    HaCSType bodyType = (HaCSType)Visit(context.body().returnStmt());
                    HaCSParser.VarContext varParent = FindLastVarContext(context);
                    tLIST listType = (tLIST)_currentScope.Resolve(varParent.IDENTIFIER().GetText()).SymbolType;
                    HaCSType result = null;
                    foreach (HaCSType type in returnTypes)
                    {
                        result = _determineType(listType.InnerType, type);
                        if (result is tINVALID)
                        {
                            _types.Put(context, result);
                            _currentScope = _currentScope.EnclosingScope;
                            return result;
                        }
                    }
                    _types.Put(context, result);
                    _currentScope = _currentScope.EnclosingScope;
                    return result;
                }
            }
        }

        #region List and Var dcl

        public override object VisitListDcls(HaCSParser.ListDclsContext context)
        {
            _typeListValue.Add(new tLIST());
            tLIST dclType = (tLIST)_typeListDcl.First();
            bool correctListDcl = true;
            HaCSType valueType;
            if (context.expression().Count() != 0)
            {
                foreach (HaCSParser.ExpressionContext exp in context.expression())
                {
                    valueType = (HaCSType)Visit(exp);
                    _typeListValue.Add(valueType);
                    if (!dclType.Equals(valueType) && !dclType.InnerType.Equals(valueType) && _typeListValue[_typeListDcl.Count-1] is tLIST)
                    {
                        Console.WriteLine("Error at line: " + context.Start.Line + ": conflicting types, expected " + dclType + ", but got " + valueType);
                        correctListDcl = false;
                    }
                }
            }
            else
            {
                foreach (HaCSParser.ListDclsContext listdcl in context.listDcls())
                {
                    if (Visit(listdcl) is tINVALID)
                    {
                        correctListDcl = false;
                    }
                }
            }
            if(correctListDcl == false)
            {
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
            else
            {
                _types.Put(context, dclType);
                return dclType;
            }
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
            HaCSType expType = (HaCSType)Visit(context.lambdaExp());
            HaCSType resultingType = _determineType(type.InnerType, expType);

            if(resultingType is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected: " + type.InnerType + " ,but got: " + expType);
                _types.Put(context, resultingType);
                return resultingType;
            }
            else
            {
                _types.Put(context, resultingType);
                return resultingType;
            } 
        }

        public override object VisitWhere(HaCSParser.WhereContext context)
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType expType = (HaCSType)Visit(context.lambdaExp());
            HaCSType resultingType = _determineType(type.InnerType, expType);

            if (resultingType is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected: " + type.InnerType + ", but got: " + expType);
                _types.Put(context, resultingType);
                return resultingType;
            }
            else
            {
                _types.Put(context, type);
                return type;
            }
        }

        public override object VisitFirst( HaCSParser.FirstContext context)
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            _types.Put(context, type.InnerType);
            return type.InnerType;
        }

        public override object VisitLast( HaCSParser.LastContext context)
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            _types.Put(context, type.InnerType);
            return type.InnerType;
        }

        public override object VisitMap(HaCSParser.MapContext context)
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType lambdaExp = (HaCSType)Visit(context.lambdaExp());
            if(lambdaExp is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Map is done on type: " +  lambdaExp + ", expected type: " + type.InnerType);
                _types.Put(context, lambdaExp);
                return lambdaExp;
            }
            else
            {
                _types.Put(context, type);
                return type;
            }
        }

        public override object VisitReduce(HaCSParser.ReduceContext context)
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType lambdaExp = (HaCSType)Visit(context.lambdaExp());
            if (lambdaExp is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Map is done on type: " + lambdaExp + ", expected type: " + type.InnerType);
                _types.Put(context, lambdaExp);
                return lambdaExp;
            }
            else
            {
                _types.Put(context, type.InnerType);
                return type.InnerType;
            }
        }

        public override object VisitContains( HaCSParser.ContainsContext context)
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType expType = (HaCSType)Visit(context.expression());
            if (!type.InnerType.Equals(expType))
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error:" + type + ",cannot Contain type: " + type.InnerType);
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
            else
            {
                _types.Put(context, new tBOOL());
                return new tBOOL();
            }
        }

        public override object VisitInclude( HaCSParser.IncludeContext context)
        {
            List<HaCSType> expressionTypes = new List<HaCSType>();
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            foreach (HaCSParser.ExpressionContext exp in context.expression())
            {
                expressionTypes.Add((HaCSType)Visit(exp));
            }
            List<HaCSType> invalidTypes = expressionTypes.Where(x => x.Equals(type.InnerType) == false && x.Equals(type) == false).ToList();
            if (invalidTypes.Count == 0)
            {
                _types.Put(context, expressionTypes[0]);
                return type;
            }
            else
            {
                foreach (HaCSType invalidType in invalidTypes)
                {
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Unable to Include type: " + invalidType + ", in type: " + type.InnerType);
                }
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
        }

        public override object VisitExclude( HaCSParser.ExcludeContext context)
        {
            List<HaCSType> expressionTypes = new List<HaCSType>();
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            foreach (HaCSParser.ExpressionContext exp in context.expression())
            {
                expressionTypes.Add((HaCSType)Visit(exp));
            }
            List<HaCSType> invalidTypes = expressionTypes.Where(x => x.Equals(type.InnerType) == false && x.Equals(type) == false).ToList();
            if (invalidTypes.Count == 0)
            {
                _types.Put(context, expressionTypes[0]);
                return type;
            }
            else
            {
                foreach (HaCSType invalidType in invalidTypes)
                {
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Unable to Exclude type: " + invalidType + ", in type: " + type.InnerType);
                }
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
        }

        public override object VisitExcludeAt( HaCSParser.ExcludeAtContext context)
        {
            List<HaCSType> expressionTypes = new List<HaCSType>();
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            foreach (HaCSParser.ExpressionContext exp in context.expression())
            {
                expressionTypes.Add((HaCSType)Visit(exp));
            }
            List<HaCSType> invalidTypes = expressionTypes.Where(x => x is tINT).ToList();
            if (expressionTypes.Count == invalidTypes.Count)
            {
                _types.Put(context, expressionTypes[0]);
                return type;
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Must provide ExcludeAt with type: tINT, ONLY");
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
        }

        public override object VisitLength( HaCSParser.LengthContext context)
        {
            HaCSType intType = new tINT();
            _types.Put(context, intType);
            return intType;
        }

        public override object VisitIndexOf(HaCSParser.IndexOfContext context)
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType expType = (HaCSType)Visit(context.lambdaExp());
            HaCSType resultingType = _determineType(type.InnerType, expType);

            if (resultingType is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected: " + type.InnerType + ", but got: " + expType);
                _types.Put(context, resultingType);
                return resultingType;
            }
            else
            {
                _types.Put(context, new tINT());
                return new tINT();
            }
        }

        //public override object VisitFold( HaCSParser.FoldContext context)
        //{
        //    HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
        //    tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
        //    if(type.InnerType is tLIST)
        //    {
        //        Console.WriteLine("Error at line: " + context.Start.Line + ": unable to use 'fold' on nested lists");
        //        _types.Put(context, new tINVALID());
        //        return new tINVALID();
        //    }
        //    _types.Put(context, type.InnerType);
        //    return type.InnerType;
        //}
        #endregion
        #endregion

        #region Methods
        #region Private Methods
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

        private HaCSType _determineType(HaCSType dclType, HaCSType valueType)
        {
            if (dclType.Equals(valueType))
            {
                return dclType;
            }
            else if (dclType is tINT && valueType is tFLOAT || valueType is tINT && dclType is tFLOAT)
            {
                return new tFLOAT();
            }
            else return new tINVALID();            
        }

        private HaCSParser.VarContext FindLastVarContext(RuleContext context)
        {
            if (context is HaCSParser.VarContext)
            {
                return (HaCSParser.VarContext)context;
            }
            else return FindLastVarContext(context.parent);
        }

        private RuleContext FindLastFuncDclContext(RuleContext context)
        {
            if (context is HaCSParser.FunctionDeclContext || context is HaCSParser.MainContext)
            {
                return context;
            }
            else return FindLastFuncDclContext(context.parent);
        }

        private HaCSParser.LambdaExpContext FindLastLambdaContext(RuleContext context)
        {
            if (context is HaCSParser.LambdaExpContext)
            {
                return context as HaCSParser.LambdaExpContext;
            }
            if (context.parent != null) return FindLastLambdaContext(context.parent);
            else return null;
        }

        private HaCSType IsBoolLambda(HaCSType type, HaCSParser.LambdaBodyContext context)
        {
            if (type is tBOOL)
            {
                HaCSParser.VarContext VarParent = FindLastVarContext(context);
                tLIST listType = (tLIST)_currentScope.Resolve(VarParent.IDENTIFIER().GetText()).SymbolType;
                _types.Put(context, listType.InnerType);
                return listType.InnerType;
            }
            else
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected " + new tBOOL() + " but got " + type);
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
        }

        private HaCSType IsBoolLambda(List<HaCSType> types, HaCSParser.LambdaBodyContext context)
        {
            foreach (HaCSType type in types)
            {
                if (!(type is tBOOL))
                {
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected " + new tBOOL() + " but got " + type);
                    _types.Put(context, new tINVALID());
                    return new tINVALID();
                }
            }
            HaCSParser.VarContext grandGrandGrandParent = (HaCSParser.VarContext)context.parent.parent.parent.parent;
            tLIST listType = (tLIST)_currentScope.Resolve(grandGrandGrandParent.IDENTIFIER().GetText()).SymbolType;
            _types.Put(context, listType.InnerType);
            return listType.InnerType;
        }
        #endregion

        #region Public Methods
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
            HaCSParser.LambdaExpContext lambdaContext = FindLastLambdaContext(context);
            HaCSType returnType = null;
            HaCSType Exptype = (HaCSType)Visit(context.expression());
            HaCSType resultingType = null;
            if (lambdaContext != null)
            {
                HaCSParser.VarContext parent = FindLastVarContext(context);
                tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
                returnType = type.InnerType;
                resultingType = _determineType(returnType, Exptype);
            }
            else
            {
                RuleContext parentContext = FindLastFuncDclContext(context);
                if (parentContext is HaCSParser.FunctionDeclContext)
                {
                    returnType = _currentScope.Resolve((parentContext as HaCSParser.FunctionDeclContext).IDENTIFIER().GetText()).SymbolType;
                }
                else returnType = new tINT();
                resultingType = _determineType(returnType, Exptype);
            } 
            if(resultingType is tINVALID)
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Incorrect Return type: " + Exptype + ", expected: " + returnType);
                _types.Put(context, resultingType);
                return resultingType;
            }
            _types.Put(context, resultingType);
            return resultingType;
        }
        #endregion
        #endregion

    }
}
