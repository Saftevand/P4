using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;

namespace HaCS.SymbolTable
{
    public class RefPhase : HaCSBaseListener                                                //RefPhase inherits from HaCSBaseListener and overwrites methods where the scope should change
    {                                                                                       //Common for the Enter methods is that the current scope is set to being the scope of the context. 
        private GlobalScope _global;                                                        //For Exit methods the current scope it set to being the enclosing scope of the context's scope.
        private ParseTreeProperty<IScope> _scopes;
        private IScope _currentScope;
        private int _errorCounter = 0;

        public RefPhase(GlobalScope global, ParseTreeProperty<IScope> scopes)
        {
            this._global = global;
            this._scopes = scopes;
        }

        public int ErrorCounter
        {
            get { return _errorCounter; }
        }

        public override void EnterProgram( HaCSParser.ProgramContext context)
        {
            _currentScope = _global;
        }

        public override void EnterFunctionDecl(HaCSParser.FunctionDeclContext context)
        {
            _currentScope = _scopes.Get(context);
        }

        public override void ExitFunctionDecl(HaCSParser.FunctionDeclContext context)
        {
            _currentScope = _currentScope.EnclosingScope;
        }

        public override void EnterBody(HaCSParser.BodyContext context)
        {
            _currentScope = _scopes.Get(context);
        }

        public override void ExitBody(HaCSParser.BodyContext context)
        {
            _currentScope = _currentScope.EnclosingScope;
        }
        public override void EnterLambdaExp(HaCSParser.LambdaExpContext context)
        {
            _currentScope = _scopes.Get(context);
        }

        public override void ExitLambdaExp(HaCSParser.LambdaExpContext context)
        {
            _currentScope = _currentScope.EnclosingScope;
        }

        public override void ExitVar(HaCSParser.VarContext context)                         //When a variable is used it is checked whether it exists in the current scope.
        {                                                                                   //If the variabe doesn't exist in the current scope an error will be printed.
            string name = context.IDENTIFIER().GetText();
            BaseSymbol var = _currentScope.Resolve(name);
            if(var == null)
            {
                Console.WriteLine(name + " variable not declared at line:" + context.start.Line);
                _errorCounter++;
            }
            else if (var is FunctionSymbol)
            {
                Console.WriteLine(name + " is a function not a variable");
                _errorCounter++;
            }
        }

        public override void ExitFunc(HaCSParser.FuncContext context)                   //Same as above, but for functions
        {
            string name = context.IDENTIFIER().GetText();
            BaseSymbol func = _currentScope.Resolve(name);
            if(func == null)
            {
                Console.WriteLine(name + " variable not declared at line:" + context.start.Line);
                _errorCounter++;
            }
            else if (func is VariableSymbol)
            {
                Console.WriteLine(name + " is a variable not a function");
                _errorCounter++;
            }
        }


    }
}
