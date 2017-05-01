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
    public class RefPhase : HaCSBaseListener 
    {
        private GlobalScope _global;
        private ParseTreeProperty<IScope> _scopes;
        private IScope _currentScope;

        public RefPhase(GlobalScope global, ParseTreeProperty<IScope> scopes)
        {
            this._global = global;
            this._scopes = scopes;
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

        public override void ExitVar(HaCSParser.VarContext context)
        {
            string name = context.IDENTIFIER().GetText();
            BaseSymbol var = _currentScope.Resolve(name);
            if(var == null)
            {
                Console.WriteLine(name + " variable not declared");
            }
            else if (var is FunctionSymbol)
            {
                Console.WriteLine(name + " is a function not a variable");
            }
        }

        public override void ExitFunc(HaCSParser.FuncContext context)
        {
            string name = context.IDENTIFIER().GetText();
            BaseSymbol func = _currentScope.Resolve(name);
            if(func == null)
            {
                Console.WriteLine(name + " function not declared");
            }
            else if (func is VariableSymbol)
            {
                Console.WriteLine(name + " is a variable not a function");
            }
        }


    }
}
