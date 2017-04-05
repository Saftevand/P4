using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace HaCS.SymbolTable
{
    public class DefPhase : HaCSBaseListener
    {
        private ParseTreeProperty<IScope> _scopes = new ParseTreeProperty<IScope>();
        private GlobalScope _global = new GlobalScope(null);
        private IScope _currentScope;

        public ParseTreeProperty<IScope> Scopes
        {
            get { return _scopes; }
        }

        public GlobalScope Global
        {
            get { return _global; }
        }

        public override void EnterProgram(HaCSParser.ProgramContext context)
        {
            _currentScope = _global;
        }

        public override void EnterMain( HaCSParser.MainContext context)
        {
            string name = context.MAIN().GetText();
            BaseSymbol.HaCSType type = Toolbox.getType(5);

            FunctionSymbol function = new FunctionSymbol(name, type, _currentScope);
            _scopes.Put(context, function);
            _currentScope = function;
        }

        public override void ExitMain( HaCSParser.MainContext context)
        {
            _currentScope = _currentScope.EnclosingScope;
        }

        public override void EnterFunctionDecl(HaCSParser.FunctionDeclContext context)
        {
            string name = context.IDENTIFIER().GetText();
            int typeTokentype = context.type().Start.Type;
            BaseSymbol.HaCSType type = Toolbox.getType(typeTokentype);

            FunctionSymbol function = new FunctionSymbol(name, type, _currentScope);
            _currentScope.Define(function);
            _scopes.Put(context, function);
            _currentScope = function;
        }

        public override void ExitFunctionDecl( HaCSParser.FunctionDeclContext context)
        {
            _currentScope = _currentScope.EnclosingScope;
        }

        public override void EnterBody(HaCSParser.BodyContext context)
        {
            _currentScope = new LocalScope(_currentScope);
            _scopes.Put(context, _currentScope);
        }

        public override void ExitBody( HaCSParser.BodyContext context)
        {
            _currentScope = _currentScope.EnclosingScope;
        }

        public override void ExitFormalParam(HaCSParser.FormalParamContext context)
        {
            DefineVariable(context.type(), context.IDENTIFIER().Symbol.Text);
        }

        public override void ExitVarDcl( HaCSParser.VarDclContext context)
        {
            if (context.Start.Text.Contains("List"))
            {
                DefineVariable(context.listType(), context.IDENTIFIER().Symbol.Text);
            }
            else DefineVariable(context.primitiveType(), context.IDENTIFIER().Symbol.Text);
        }

        public void DefineVariable(HaCSParser.TypeContext context, string name)
        {
            int typeTokenType = context.Start.Type;
            BaseSymbol.HaCSType type = Toolbox.getType(typeTokenType);
            VariableSymbol varSym = new VariableSymbol(name, type, _currentScope);
            _currentScope.Define(varSym);
        }

        public void DefineVariable(HaCSParser.PrimitiveTypeContext context, string name)
        {
            int typeTokenType = context.Start.Type;
            BaseSymbol.HaCSType type = Toolbox.getType(typeTokenType);
            VariableSymbol varSym = new VariableSymbol(name, type, _currentScope);
            _currentScope.Define(varSym);
        }

        public void DefineVariable(HaCSParser.ListTypeContext context, string name)
        {
            int typeTokenType = context.Start.Type;
            BaseSymbol.HaCSType type = Toolbox.getType(typeTokenType);
            VariableSymbol varSym = new VariableSymbol(name, type, _currentScope);
            _currentScope.Define(varSym);
        }
    }
}
