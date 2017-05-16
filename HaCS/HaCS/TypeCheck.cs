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
        private IScope _currentScope;
        private int _errorCounter = 0;
        private tLIST createdType = null;
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
        public int ErrorCounter
        {
            get { return _errorCounter; }
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
                HaCSType type2 = (HaCSType)Visit(context.right);                            //The right side of the context - for instance 3+5^2   the expression 2 is visited
                HaCSType type1 = (HaCSType)Visit(context.left);                             //The left side of the context - for instance 3+5^2   the expression 3+5 is visited
                HaCSType type3 = _determineType(type1, type2);                              //The types of the left and right-hand side are compared to check if the types can be evaluated.
                if (type3 is tINVALID || type3 is tCHAR || type3 is tBOOL || type3 is tLIST)//Type promotion will happen between int and float. tINVALID is returned if the two types can't be evaluated.
                {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected INT or FLOAT, but got " + type1 + " and " + type2);
                _types.Put(context, new tINVALID());                
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
                _errorCounter++;
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
                                                                                            //Type promotion will happen between int and float. tINVALID is returned if the two types can't be evaluated.
            if (type3 is tINVALID || type3 is tCHAR || type3 is tBOOL || type3 is tLIST)
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);

                _types.Put(context, new tINVALID());                                        //If the type is not an int or float tINVALID is added to the parsetreeproperty
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
            if (type3 is tINVALID || type3 is tCHAR || type3 is tBOOL || type3 is tLIST)
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, new tINVALID());
            }
            else
            {
                _types.Put(context, type3);
            }
            return type3;
        }

        public override object VisitCompare(HaCSParser.CompareContext context)              //Same procedure, but when encountering (> || >= || < || <= ) expressions
        {
            HaCSType type1 = (HaCSType)Visit(context.left);
            HaCSType type2 = (HaCSType)Visit(context.right);
            HaCSType type3 = _determineType(type1, type2);
            if (type3 is tINVALID || type3 is tLIST)
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected int or float, but got " + type1 + " and " + type2);
                _types.Put(context, new tINVALID());
                return new tINVALID();
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
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected similar types on each side of equality sign, but got " + type1 + " and " + type2);
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
            _types.Put(context, new tBOOL());
            return new tBOOL();
        }

        public override object VisitFunc(HaCSParser.FuncContext context)                    //When encountering a function call - a lot like when encountering a variable
        {
            string name = context.IDENTIFIER().GetText();                                   //The identifier of the function
            FunctionSymbol sym = (FunctionSymbol)_currentScope.Resolve(name);               //Lookup in the current scope to find the function corresponding to the identifier
            bool correctFuncCall = true;
            int i = 0;
            foreach (var item in sym.Symbols)                                               //Iteration through all symbols(functions and variables) declared within the function
            {
                if(i < context.expression().Count())
                {
                    if (!item.Value.SymbolType.Equals((HaCSType)Visit(context.expression()[i])))//Checks whether the types of the actual parameters corresponds to the formal parameters.
                    {                                                                           //Gives error if the types actual and formal parameters don't correspond.
                        correctFuncCall = false;                                                //Sets flag to false
                        _errorCounter++;
                        Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected " + item.Value.SymbolType + ", but got " + (HaCSType)Visit(context.expression()[i]));
                    }
                }
                else
                {
                    correctFuncCall = false;
                    _errorCounter++;
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Missing input:" + item.Value.SymbolType);
                }
               
                i++;
            }
            if(correctFuncCall){                                                            //If no error with the parameters
                _types.Put(context, sym.SymbolType);                                        //The type of the function is added to the parsetreeproperty
                return sym.SymbolType;                                                      //Returns the type of the function - The function's return type
            }
            _types.Put(context, new tINVALID());
            return new tINVALID();
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
                _errorCounter++;
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
                _errorCounter++;
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
                List<ITerminalNode> tokenList = Toolbox.getFlatTokenList(context.lambdaBody().expression());    //getFlatTokenList returns a list containing the tokens from the expression
                    foreach (ITerminalNode terminal in context.IDENTIFIER())                //It is checked whether all the identifiers of the parameters are used in the expression 
                    {
                    if (tokenList.Find(x => x.Symbol.Text == terminal.Symbol.Text) == null) //If an parameter is not to be found in the list of tokens, the flag is set to false
                    {
                        terminalChecker = false;
                    }
                }
            }
            else
            {
                List<ITerminalNode> tokenList = Toolbox.getFlatTokenList(context.lambdaBody().body());
                foreach (ITerminalNode terminal in context.IDENTIFIER())                    //Same as above, but just for a body instead of an expression
                {
                    if (tokenList.Find(x => x.Symbol.Text == terminal.Symbol.Text) == null)
                    {
                        terminalChecker = false;
                    }
                }
            }
            if(terminalChecker == false)                                                   //If the flag is false, an error is given, that not all parameters were used
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: missing identifier(s) in lambda expression");
                _types.Put(context, new tINVALID());                                    //The type is added to the parsetreeproperty
        }
            return Visit(context.lambdaBody());                                         //The type of the lambdabody is returned (left-hand side of '=>') by visiting the lambdaBody

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
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Conflicting types, expected INT as index of LIST, but got " + type1 + " as index of " + type2);
                _types.Put(context,new tINVALID());                                        //tINVALID is added to the parsetreeproperty and the tINVALID is returned
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
                    HaCSParser.VarContext varParent = Toolbox.FindLastContext<HaCSParser.VarContext>(context);  //Gets the context of the variable(list) on which the list operation is used on
                    tLIST listType = (tLIST)_currentScope.Resolve(varParent.IDENTIFIER().GetText()).SymbolType; //Gets the list from the current scope corresponding to the identifier of the varContext

                    HaCSType result = _determineType(listType.InnerType, expType);                              //Determines whether the expression evaluates to the type of the innermost list
                    _types.Put(context, result);                                                                //Adds the type to the parsetreeproperty
                    _currentScope = _currentScope.EnclosingScope;                                               //Sets the current scope back to the lambdaExp's scope
                    return result;
                }
                else
                {
                    List<HaCSType> returnTypes = new List<HaCSType>();                                        //Much like the procedure for where,find and indexOf but the the innnerlist much match the return type of the lambdaExp body
                    foreach (HaCSParser.StmtContext stmt in context.body().stmt().Where(x => x.ifStmt() != null)) //Iterates through the if/else/else statements within the body
                    {
                        returnTypes.Add((HaCSType)Visit(stmt.ifStmt().body().returnStmt()));                    //Adds the return type of the statement to the list of return types
                    }
                    returnTypes.Add((HaCSType)Visit(context.body().returnStmt()));                              //Adds the body's return type to the list                         
                    HaCSParser.VarContext varParent = Toolbox.FindLastContext<HaCSParser.VarContext>(context);
                    HaCSType expectedType = null;
                    HaCSType result = null;
                    if (varParent == null)
                    {
                        HaCSParser.VarDclContext varDclParent = Toolbox.FindLastContext<HaCSParser.VarDclContext>(context);
                        expectedType = _currentScope.Resolve(varDclParent.IDENTIFIER().GetText()).SymbolType;
                    }
                    else expectedType = (tLIST)_currentScope.Resolve(varParent.IDENTIFIER().GetText()).SymbolType; //Gets the list on which the list operation is performed on

                    foreach (HaCSType type in returnTypes)                                                      //Iterates through the list returntypes which contains all the types the body can return
                    {
                        if (expectedType is tLIST) result = _determineType((expectedType as tLIST).InnerType, type);             //Uses determine type to check what the type of the innermost list and the return type evaluates to
                        else result = _determineType(expectedType, type);
                        if (result is tINVALID)                                                                 //If it's tINVALID it will be added to the parsetreeproperty, the scope will be set back to the lambdaExp's scope
                        {
                            _types.Put(context, result);
                            _currentScope = _currentScope.EnclosingScope;
                            return result;
                        }
                    }
                    _types.Put(context, result);                                                              //If all return types match the type of the innermost list, the type will be added to the parsetreeproperty and the scope will be set back to the lambdaExp's scope
                    _currentScope = _currentScope.EnclosingScope;
                    return result;
                }
            }
        }

        public override object VisitIfStmt(HaCSParser.IfStmtContext context)                //When an IfStmt is encountered it is checked whether the expression evaluates to tBOOL
        {
            HaCSType type1 = (HaCSType)Visit(context.exp1);                                 //Checks wether the expression within the '()' evaluates to a tBOOL

            if (type1 is tBOOL)                                                             //If it does it is added to the parsetreeproperty and returned
            {
                _types.Put(context, type1);
                return type1;
            }
            else                                                                            //Else it gives an error and adds the type to the parsetreeproperty and returs the type
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1);
                return new tINVALID();
            }
        }

        public override object VisitElseifStmt(HaCSParser.ElseifStmtContext context)        //When an ElseIfStmt is encountered it is checked whether the expression evaluates to tBOOL
        {                                                                                   //Same as if
            HaCSType type1 = (HaCSType)Visit(context.exp2);

            if (type1 is tBOOL)
            {
                _types.Put(context, type1);
                return type1;
            }
            else
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: expected bool, but got " + type1);
                return new tINVALID();
            }

        }

        public override object VisitReturnStmt(HaCSParser.ReturnStmtContext context)        //When encountering a ReturnStmt - Checks whether the returned expression matches the expected
        {
            HaCSParser.LambdaExpContext lambdaContext = Toolbox.FindLastContext<HaCSParser.LambdaExpContext>(context);      //Gets the last LambdaExpContext
            HaCSType returnType = null;
            HaCSType Exptype = (HaCSType)Visit(context.expression());                                                       //Gets the type the expression evaluates to
            HaCSType resultingType = null;
            if (lambdaContext != null)                                                                                      //If a lambdaExpcontext was found 
            {
                if(lambdaContext.parent is HaCSParser.WhereContext || lambdaContext.parent is HaCSParser.FindContext || lambdaContext.parent is HaCSParser.IndexOfContext)
                {
                    resultingType = _determineType(Exptype, new tBOOL());
                }
                else
                {
                    HaCSParser.VarContext Varparent = Toolbox.FindLastContext<HaCSParser.VarContext>(context);                     //Gets the list on which the list operation was used
                    if(Varparent == null)
                    {
                        HaCSParser.VarDclContext VarDclParent = Toolbox.FindLastContext<HaCSParser.VarDclContext>(context);
                        returnType = _currentScope.Resolve(VarDclParent.IDENTIFIER().GetText()).SymbolType;
                    }
                    else
                    {
                        tLIST type = (tLIST)_currentScope.Resolve(Varparent.IDENTIFIER().GetText()).SymbolType;
                        returnType = type.InnerType;                                                                            //The type of elements the innermost list contains
                    }
                                                                                                   
                    resultingType = _determineType(returnType, Exptype);                                                        //Determines whether the expression in the return statement corresponds to the type of elements the innermost list has
                }    
            }
            else                                                                                                            //Enters else, as the return must be wihtin a function
            {
                RuleContext parentContext = Toolbox.FindLastContext<HaCSParser.FunctionDeclContext>(context);                       //Gets the last declared function and checks whether the expression of the returnstms corresponds to the type of the function
                if (parentContext is HaCSParser.FunctionDeclContext)
                {
                    returnType = _currentScope.Resolve((parentContext as HaCSParser.FunctionDeclContext).IDENTIFIER().GetText()).SymbolType;
                }
                else returnType = new tINT();                                                                               //Exception when it comes to the 'main' which must return an int
                resultingType = _determineType(returnType, Exptype);
            }
            if (resultingType is tINVALID)                                                                                  //If resultingType is tINVALID an error is given and the tINValid is added to the parsetreeproperty and returned
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Incorrect Return type: " + Exptype + ", expected: " + returnType);
                _types.Put(context, resultingType);
                return resultingType;
            }
            _types.Put(context, resultingType);
            return resultingType;
        }

        #region List and Var dcl

        public override object VisitListDcls(HaCSParser.ListDclsContext context)            //When encountering listDcl : listType IDENTIFIER ASSIGN LCURLBRACKET listDcls RCURLBRACKET;
        {
            HaCSParser.ListDclContext dclParent = Toolbox.FindLastContext<HaCSParser.ListDclContext>(context);
            tLIST dclType = (tLIST)_currentScope.Resolve(dclParent.IDENTIFIER().GetText()).SymbolType;
            bool correctListDcl = true;                                                     //A flag used for indicating whether the list is initialised correctly
            HaCSType valueType;                                                             //A reference to the type of the expression on the right side of the listdcl - List<type> = {expression}
            if (context.expression().Count() != 0)                                          //If there is atleast one expression at the right side
            {
                foreach (HaCSParser.ExpressionContext exp in context.expression())          //For each expression
                {
                    valueType = (HaCSType)Visit(exp);                                       //The type the expression evaluates to is added to the valueType
                    createdType.inputTypeRecursively(valueType);
                    if (!dclType.Equals(valueType) && !dclType.InnerType.Equals(valueType) && !dclType.Equals(createdType))
                    {
                        //If certain requirements aren't met, an error is given
                        //The type of the first element of the ListDcl(the declared type e.g. List<List<int>> numbers = ... ) must match the type of the right side of the dcl.
                        //Furthermore the type of the elements in the innermost list and the elements the expression on the right sight must match.
                        //Finally the level of nested lists must be equal e.g.  List<List<List<int>>> x = {{{{1,3,4,5,6}}}};

                        _errorCounter++;
                        Console.WriteLine("Error at line: " + context.Start.Line + ": conflicting types, expected " + dclType + ", but got " + valueType);
                        correctListDcl = false;
                    }
                }
            }
            else                                                                            //If there are no errors in the declarion and initialisation
            {
                createdType.inputTypeRecursively(new tLIST());
                foreach (HaCSParser.ListDclsContext listdcl in context.listDcls())          //Iterates through the listDcls(the right-hand side of =)
                {
                    if (Visit(listdcl) is tINVALID)                                         //Evaluates whether the listdcl is tINVALID
                    {
                        correctListDcl = false;                                             //Sets flag to false
                    }
                }
            }
            if(correctListDcl == false)                                                     //If the flag is false, there has been an error at the list declaration
            {                                                                               //The tINVALID type is added to the parsetreeproperty and the type is returned
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
            else                                                                            //If the flag isn't == fals, the list declaration was correct 
            {                                                                               //And the type of the list is added to the parsetreeproperty and returned
                _types.Put(context, dclType);
                return dclType;
            }
        }

        public override object VisitVarDcl(HaCSParser.VarDclContext context)                //When encountering Variable declarations
        {                                                                                   //varDcl : left=primitiveType IDENTIFIER ASSIGN right=expression | listDcl;
            HaCSType resultType;
            if (context.right == null)                                                      //If the right side is not a expression, it's a list
            {
                resultType = TypeCheckListDcl(context.listDcl());                           //The type of the list is returned by the method TypeCheckListDcl
            }
            else
            {
                resultType = TypeCheckPrimitiveDcl(context);                                //If it's not a list TypeCheckPrimitiveDcl determines the  type
            }
            _types.Put(context, resultType);                                                //The type is added to the parsetreeproperty
            return resultType;                                                              //The type of the variable is returned
        }
        #endregion
        #region List opreations
        public override object VisitFind(HaCSParser.FindContext context)                    //Encountering the list operation .Find
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;           //Gets the VarContext corresponding to the variable on which the list operation is used on
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType; //Gets the type of the list on which the operation is performed
            HaCSType expType = (HaCSType)Visit(context.lambdaExp());                        //Gets the type the expression evaluates to
            HaCSType resultingType = _determineType(type.InnerType, expType);               //Determines the type the two types evaluates t.

            if(resultingType is tINVALID)                                                   //Gives an error if the resulting type is tINVALID, adds the type to the parsetreeproperty and returns the type
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected: " + type.InnerType + " ,but got: " + expType);
                _types.Put(context, resultingType);
                return resultingType;
            }
            else
            {
                _types.Put(context, resultingType);                                         //Adds the type to the parsetreeproperty
                return resultingType;                                                       //Returns the type
            } 
        }

        public override object VisitWhere(HaCSParser.WhereContext context)                  //Encountering the list operation .Where - follows same procedure as .Find
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType expType = (HaCSType)Visit(context.lambdaExp());
            HaCSType resultingType = _determineType(type.InnerType, expType);

            if (resultingType is tINVALID)
            {
                _errorCounter++;
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

        public override object VisitMap(HaCSParser.MapContext context)                      //Encountering the list operation .Map - follows same procedure as .Find
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType lambdaExp = (HaCSType)Visit(context.lambdaExp());
            if(lambdaExp is tINVALID)
            {
                _errorCounter++;
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

        public override object VisitReduce(HaCSParser.ReduceContext context)                //Encountering the list operation .Reduce - follows same procedure as .Find
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType lambdaExp = (HaCSType)Visit(context.lambdaExp());
            if (lambdaExp is tINVALID)
            {
                _errorCounter++;
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

        public override object VisitContains( HaCSParser.ContainsContext context)           //Encountering the list operation .Contains - follows same procedure as .Find
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType expType = (HaCSType)Visit(context.expression());
            if (!type.InnerType.Equals(expType))
            {
                _errorCounter++;
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

        public override object VisitInclude( HaCSParser.IncludeContext context)             //Encountering the list operation .Include - follows same procedure as .Find
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
                    _errorCounter++;
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Unable to Include type: " + invalidType + ", in type: " + type.InnerType);
                }
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
        }          

        public override object VisitExclude( HaCSParser.ExcludeContext context)             //Encountering the list operation .Exlude - follows same procedure as .Find
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
                    _errorCounter++;
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Unable to Exclude type: " + invalidType + ", in type: " + type.InnerType);
                }
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
        }          

        public override object VisitExcludeAt( HaCSParser.ExcludeAtContext context)         //Encountering the list operation .ExcludeAt - follows same procedure as .Find

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
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: Must provide ExcludeAt with type: tINT, ONLY");
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
        }      
        public override object VisitIndexOf(HaCSParser.IndexOfContext context)              //Encountering the list operation .IndexOf - follows same procedure as .Find
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            HaCSType expType = (HaCSType)Visit(context.lambdaExp());
            HaCSType resultingType = _determineType(type.InnerType, expType);

            if (resultingType is tINVALID)
            {
                _errorCounter++;
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

        public override object VisitLength(HaCSParser.LengthContext context)                //Encountering the list operation .Length
        {
            HaCSType intType = new tINT();                                                  //Creates an int
            _types.Put(context, intType);                                                   //Adds the type to the parsetreeproperty
            return intType;                                                                 //Returns the type
        }             

        public override object VisitFirst(HaCSParser.FirstContext context)                  //Encountering the list operation .First
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;           //Gets the varcontext on which the list operation is used
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType; //Gets the type of the variable
            _types.Put(context, type.InnerType);                                                 //Adds the type the innermost list contains
            return type.InnerType;                                                               //Returns the type
        }               

        public override object VisitLast(HaCSParser.LastContext context)                    //Encountering the list operation .Last - follows same procedure as .First
        {
            HaCSParser.VarContext parent = (HaCSParser.VarContext)context.Parent;
            tLIST type = (tLIST)_currentScope.Resolve(parent.IDENTIFIER().GetText()).SymbolType;
            _types.Put(context, type.InnerType);
            return type.InnerType;
        }                 
        #endregion
        #endregion

        #region Methods
        #region Private Methods
        private HaCSType TypeCheckPrimitiveDcl(HaCSParser.VarDclContext context)            //Checks whether the given right-hand side of a vardcl corresponds to the type of the left side
        {
            HaCSType type1 = (HaCSType)Visit(context.right);                                
            HaCSType type2 = Toolbox.getType(context.primitiveType().Start.Type);
            if (type1.GetType() == type2.GetType())                                         //Checks whether the given right-hand side of a vardcl corresponds to the type of the left side
            {
                _types.Put(context, type1);                                                 //The commmon type is added to the parsetreeproperty and returned
                return type1;
            }
            else if ((type1 is tINT && type2 is tFLOAT) || (type2 is tINT && type1 is tFLOAT)) //If the types are int and float the int will be type promoted
            {
                _types.Put(context, new tFLOAT());                                             //The type float is added
                return new tFLOAT();
            }
            else                                                                               //If none of the above conditional statements are true, an error is given
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected similar types, but got " + type1 + " and " + type2);
                return new tINVALID();
            }
        }       

        private HaCSType TypeCheckListDcl(HaCSParser.ListDclContext context)                 //Checks whether the type of each level of the left-handside of the list declaration corresponds to the corresponding leve at the right-hand side
        {
            createdType = new tLIST();
            HaCSType dclType = _currentScope.Resolve(context.IDENTIFIER().GetText()).SymbolType;                           //Gets the type of the declared list
            HaCSType valueType = (HaCSType)Visit(context.listDcls());
            HaCSType result = _determineType(dclType, valueType);
            if (result is tINVALID)
            {
                _errorCounter++;
                Console.WriteLine("Error at line: " + context.Start.Line + ": Inconsistent list declaration, value does not match declaration");
            }
            return result;                                                                  //The type of the complete list is returned.
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
        }            //Determine the type two types evaluates to

        private HaCSParser.VarContext FindLastVarContext(RuleContext context)               //Returns the last variablecontext 
        {
            if (context is HaCSParser.VarContext)
            {
                return (HaCSParser.VarContext)context;
            }
            else return FindLastVarContext(context.parent);                                 //Recursively calls itself with the parent of the current context till the last varContext is found
        }            

        private RuleContext FindLastFuncDclContext(RuleContext context)                     //Returns the last FunctionDclContext
        {
            if (context is HaCSParser.FunctionDeclContext || context is HaCSParser.MainContext)
            {
                return context;
            }
            else return FindLastFuncDclContext(context.parent);                            //Recursively calls itself with the parent of the current context till the last FuncDclContext is found
        }

        private HaCSParser.LambdaExpContext FindLastLambdaContext(RuleContext context)      //Returns the last LambdaContext
        {
            if (context is HaCSParser.LambdaExpContext)
            {
                return context as HaCSParser.LambdaExpContext;
            }
            if (context.parent != null) return FindLastLambdaContext(context.parent);       //As previous methods - recursion
            else return null;
        }

        private HaCSType IsBoolLambda(HaCSType type, HaCSParser.LambdaBodyContext context)  //Checks whether a lambdaexpression returns a bool - returns either tBOOL or tINVALID
        {
            if (type is tBOOL)                                                              //Here the type is the type the expression of the lambda body evaluates to
            {                                                                               //If it's bool, the type the list contains elementents of in the innermost list is found from the current scope
                HaCSParser.VarContext VarParent = Toolbox.FindLastContext<HaCSParser.VarContext>(context);              //The type is added to parsetreeproperty and returned
                tLIST listType = (tLIST)_currentScope.Resolve(VarParent.IDENTIFIER().GetText()).SymbolType;
                _types.Put(context, listType.InnerType);
                return listType.InnerType;
            }
            else                                                                            //If it's not a tBOOL an error is given and the tINVALID is added to the parsetreepropery and returned 
            {
                Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected " + new tBOOL() + " but got " + type);
                _types.Put(context, new tINVALID());
                return new tINVALID();
            }
        }

        private HaCSType IsBoolLambda(List<HaCSType> types, HaCSParser.LambdaBodyContext context) //Same as above, but used when a lambdabody contains a body with return statements
        {                                                                                         //Here each return value must be of type tBOOL
            foreach (HaCSType type in types)
            {
                if (!(type is tBOOL))                                                       //If not, an error is given and the type is added to the parsetreeproperty and returned
                {
                    Console.WriteLine("Error at line: " + context.Start.Line + " - Error: conflicting types, expected " + new tBOOL() + " but got " + type);
                    _types.Put(context, new tINVALID());
                    return new tINVALID();
                }
            }
            HaCSParser.VarContext VarParent = Toolbox.FindLastContext<HaCSParser.VarContext>(context);
            tLIST listType = (tLIST)_currentScope.Resolve(VarParent.IDENTIFIER().GetText()).SymbolType;
            _types.Put(context, listType.InnerType);
            return listType.InnerType;
        }
        #endregion

        #region Public Methods

        #endregion
        #endregion

    }
}
