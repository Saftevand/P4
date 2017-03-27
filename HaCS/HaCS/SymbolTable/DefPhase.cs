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
        ParseTreeProperty<IScope> scopes = new ParseTreeProperty<IScope>();
        GlobalScope global = new GlobalScope(null);
        IScope currentScope;
        public override void EnterProgram(HaCSParser.ProgramContext context)
        {
            currentScope = global;
        }

        public override void EnterFunctionDecl(HaCSParser.FunctionDeclContext context)
        {
            string name = context.IDENTIFIER().GetText();
            //int typeToken = context.type().primitiveType().
            int typeTokentype = context.type().Start.Type;
            BaseSymbol.HaCSType type = Toolbox.getType(typeTokentype);

            FunctionSymbol function = new FunctionSymbol(name, type, currentScope);
            currentScope.Define(function);
            scopes.Put(context, function);
            currentScope = function;
        }

        public override void ExitFunctionDecl( HaCSParser.FunctionDeclContext context)
        {
            currentScope = currentScope.EnclosingScope;
        }

        public override void EnterBody(HaCSParser.BodyContext context)
        {
            currentScope = new LocalScope(currentScope);
            scopes.Put(context, currentScope);
        }

        public override void ExitBody( HaCSParser.BodyContext context)
        {
            currentScope = currentScope.EnclosingScope;
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
            VariableSymbol varSym = new VariableSymbol(name, type, currentScope);
            currentScope.Define(varSym);
        }

        public void DefineVariable(HaCSParser.PrimitiveTypeContext context, string name)
        {
            int typeTokenType = context.Start.Type;
            BaseSymbol.HaCSType type = Toolbox.getType(typeTokenType);
            VariableSymbol varSym = new VariableSymbol(name, type, currentScope);
            currentScope.Define(varSym);
        }

        public void DefineVariable(HaCSParser.ListTypeContext context, string name)
        {
            int typeTokenType = context.Start.Type;
            BaseSymbol.HaCSType type = Toolbox.getType(typeTokenType);
            VariableSymbol varSym = new VariableSymbol(name, type, currentScope);
            currentScope.Define(varSym);
        }
    }
}
