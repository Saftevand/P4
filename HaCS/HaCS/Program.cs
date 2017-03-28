using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace HaCS
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader inputStream = new StreamReader("C:\\Users\\GryPetersen\\Desktop\\MarkusTest.txt");
            AntlrInputStream input = new AntlrInputStream(inputStream.ReadToEnd());
            HaCSLexer lexer = new HaCSLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            HaCSParser parser = new HaCSParser(tokens);
            IParseTree tree = parser.program();
            ParseTreeWalker walker = new ParseTreeWalker();
            HaCS.SymbolTable.DefPhase Def = new SymbolTable.DefPhase();
            walker.Walk(Def, tree);
            SymbolTable.RefPhase Ref = new SymbolTable.RefPhase(Def.Global, Def.Scopes);
            walker.Walk(Ref, tree);
            
            Console.ReadKey();
        }
    }
}
