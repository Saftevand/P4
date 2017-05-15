using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using HaCS.Types;
using Antlr4.Runtime;

namespace HaCS.SymbolTable
{
    /*
    DefPhase - Defines scopes and the types through overwritten enter and exit methods for the HaCSBaseListener
    The ParseTreeProperty _scopes will after the DefPhase contain all the scopes of the HaCS program and thereby all the types.
    */
    public class DefPhase : HaCSBaseListener
    {
        private ParseTreeProperty<IScope> _scopes = new ParseTreeProperty<IScope>();        //The parsetreeproperty on which the declared scopes are added
        private GlobalScope _global = new GlobalScope(null);                                   
        private tLIST _listType = null;                                                     //A reference used for holding the type of a list at declaration 
        private IScope _currentScope;                                                       //A reference to the current scope
        private int _errorCounter = 0;

        public ParseTreeProperty<IScope> Scopes
        {
            get { return _scopes; }
        }

        public GlobalScope Global
        {
            get { return _global; }
        }

        public int ErrorCounter
        {
            get { return _errorCounter; }
        }

        public override void EnterProgram(HaCSParser.ProgramContext context)
        {
            _currentScope = _global;
        }

        public override void EnterMain( HaCSParser.MainContext context)
        {
            string name = context.MAIN().GetText();                                         //Gets the name of the function
            HaCSType type = Toolbox.getType(5);                                             //Get the corresponding type in HaCS to the ANTLR4 int through the use of a switch. This is the return type of the function.

            FunctionSymbol function = new FunctionSymbol(name, type, _currentScope);        //The function is declared based on the name of the function, its return type and the scope from where it was declared.
            _scopes.Put(context, function);                                                 //The function which acts like a scope is added to the parsetreeproperty
            _currentScope = function;                                                       //The current scope is set to be the scope of the function.
        }

        public override void ExitMain( HaCSParser.MainContext context)
        {
            _currentScope = _currentScope.EnclosingScope;
        }

        public override void EnterFunctionDecl(HaCSParser.FunctionDeclContext context)      //The procedure of declaring a function
        {
            string name = context.IDENTIFIER().GetText();                                   //Gets the name of the function
            int typeTokentype = context.type().Start.Type;                                  //Gets the int corresponding to the type(ANTLR4 way of handling types).
            HaCSType type = Toolbox.getType(typeTokentype);                                 //Get the corresponding type in HaCS to the ANTLR4 int through the use of a switch. This is the return type of the function.
            if(type is tLIST)
            {
                type = CreateListType(_listType, context.type().listType());                //If it's type List, it will determine the type within the List through recursive use of CreateListType        
            }
            FunctionSymbol function = new FunctionSymbol(name, type, _currentScope);        //A new function is declared based on the name of the function, its return type and the scope from where it was declared.
            DefineBuffer(function);                                                 //The new function is added to the current scope. The function can then be resolved by its identifier.
            _scopes.Put(context, function);                                                 //The function which acts like a scope is added to the parsetreeproperty
            _currentScope = function;                                                       //The current scope is set to be the scope of the function.
        }

        public override void ExitFunctionDecl( HaCSParser.FunctionDeclContext context)      //When exiting a function declaration
        {
            _currentScope = _currentScope.EnclosingScope;                                   //The current scope is set back to the enclosing scope of the function(The scope from where the function was declared)
        }

        public override void EnterBody(HaCSParser.BodyContext context)                      //Entering a body ({})
        {
            _currentScope = new LocalScope(_currentScope);                                  //When entering a body a new scope is made with a reference to its enclosing scope.
            _scopes.Put(context, _currentScope);                                            //The scope is added to the parsetreeproperty.
        }

        public override void ExitBody( HaCSParser.BodyContext context)                      //Exiting body ({})
        {
            _currentScope = _currentScope.EnclosingScope;                                   //Setting the current scope back to the enclosing scope of the body.
        }

        public override void ExitFormalParam(HaCSParser.FormalParamContext context)         //Declaring the formal parameter in a function declaration
        {
            DefineVariable(context.type(), context.IDENTIFIER().Symbol.Text);               //Makes use of the function DefineVariable to define formal parameters. The type and identifier os the context is used.
        }

        public override void ExitVarDcl( HaCSParser.VarDclContext context)                  //Declaring variables.
        {
            if (context.Start.Text.Contains("List"))
            {
                DefineVariable(context.listDcl());                                          //If the type is a List the overloaded DefineVariable method will be used taking the listdeclaration context as input. 
            }
            else DefineVariable(context.primitiveType(), context.IDENTIFIER().Symbol.Text); //If it's not a List a primitive type is declared through the correct DefineVariable method.
        }

        public override void EnterLambdaExp(HaCSParser.LambdaExpContext context)            //When entering a lambda expression
        {
            _currentScope = new LocalScope(_currentScope);                                  //A new scope is made, as a lambda expression can contain parameters
            _scopes.Put(context, _currentScope);                                            //Adds the scope to the parsetreeproperty
            int i = 0;                                                                      //Counter used for iteration of variable declaration. 
            foreach (HaCSParser.TypeContext type in context.type())                         
            {
                DefineVariable(type, context.IDENTIFIER(i).Symbol.Text);                    //Each parameter in the lambda expression is declared as i is incremented
                i++;
            }
        }

        public override void ExitLambdaExp(HaCSParser.LambdaExpContext context)
        {
            _currentScope = _currentScope.EnclosingScope;                                   //When exiting the lambda expression, the current scope is set to the lambda's enclosing 
        }

        public override void EnterListDcl(HaCSParser.ListDclContext context)                //Declaration of Lists
        {
            _listType = new tLIST();                                                        //A new list is declared
            _listType = CreateListType(_listType,context.listType());                       //The CreateListType method is used for declaring lists with the tLIST and listType contex.
        }

        public void DefineVariable(HaCSParser.TypeContext context, string name)             //Variable is declared as either primitive or list type.
        {
            if(context.primitiveType() != null)
            {
                DefineVariable(context.primitiveType(), name);                              //Primitive type gets declared
            }
            else
            {
                _listType = CreateListType(_listType, context.listType());                  //The type of the inner list is determined
                VariableSymbol varSym = new VariableSymbol(name, _listType, _currentScope); //A new variable is created using the name, type of list, and the current scope.
                DefineBuffer(varSym);                                               //The variable is added to the current scope.
            }       
        }

        public void DefineVariable(HaCSParser.PrimitiveTypeContext context, string name) //Used for declaring primitive types
        {
            int typeTokenType = context.Start.Type;                                      //Gets the int corresponding to the type(ANTLR4)
            HaCSType type = Toolbox.getType(typeTokenType);                              //Gets HaCS type corresponding to the int determined by ANTLR4
            VariableSymbol varSym = new VariableSymbol(name, type, _currentScope);       //A new variable is declared based on the name of the variable, its type and the scope from where it was declared.  
            DefineBuffer(varSym);                                                //The variable is added to the current scope
        }

        public void DefineVariable(HaCSParser.ListDclContext context)
        {
            VariableSymbol varSym = new VariableSymbol(context.IDENTIFIER().GetText(), _listType, _currentScope);   //List is declared 
            DefineBuffer(varSym);                                                                           //The List is added to the current scope
        }

        private tLIST CreateListType(tLIST listType, HaCSParser.ListTypeContext context) 
        {
            listType.InnerType = Toolbox.getType(context.type().start.Type);                //Gets the type the list contains

            if (listType.InnerType is tLIST)
            {
               CreateListType(listType.InnerType as tLIST, context.type().listType());      //If the type the list contains is a tList is will call itself recursively 
            }
            return listType;                                                                //Returns he type the inner most list contains.
        }

        private void DefineBuffer(BaseSymbol sym)
        {
            if (_currentScope.Symbols.ContainsKey(sym.Name))
            {
                _errorCounter++;
                Console.WriteLine("Error: " + sym.Name + " already exists in this scope");
            }
            else _currentScope.Define(sym);
        }


    }
}
