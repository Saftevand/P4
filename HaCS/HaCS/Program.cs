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
    public class Program
    {
        public static int Main(string[] args)
        {
            StreamReader inputStream;
            if (args.Count() == 0)
            {
                inputStream = promptInputStream();
            }
            else inputStream = promptInputStream(args[0]);
            AntlrInputStream input = new AntlrInputStream(inputStream.ReadToEnd());             //Creates a CharStream that reads from the given input.
            HaCSLexer lexer = new HaCSLexer(input);                                             //The lexer is created and takes the AntlrInputStream as input.
            CommonTokenStream tokens = new CommonTokenStream(lexer);                            //CommoTokenStream is a buffer between the lexer and parser containing tokens.
            HaCSParser parser = new HaCSParser(tokens);                                         //The tokens are used for creating the parser.
            IParseTree tree = parser.program();                                                 //The input is parsed from the program rule making a parsetree.
            if (parser.NumberOfSyntaxErrors == 0)
            {
                ParseTreeWalker walker = new ParseTreeWalker();                                 //A walker is initialised which can walk/traverse in a way specified by its input.
                SymbolTable.DefPhase Def = new SymbolTable.DefPhase();                          //The DefPhase which contains methods for declaring scopes and variables
                walker.Walk(Def, tree);                                                         //The walker traverses the parsetree using the methods from the defPhase and annotates the parsetree through the use of parsetreeproperties.
                if(Def.ErrorCounter == 0)
                {
                    SymbolTable.RefPhase Ref = new SymbolTable.RefPhase(Def.Global, Def.Scopes);    //The refPhase uses the parsetreeproperties and checks whether symbols are available from where they are tried to be used. 
                    walker.Walk(Ref, tree);                                                         //The walker traverses the parsetree using the methods from the refPhase and reports errors if any.
                    if (Ref.ErrorCounter == 0)
                    {
                        TypeCheck typechecker = new TypeCheck(Def.Scopes);
                        typechecker.Visit(tree);
                        if(typechecker.ErrorCounter == 0)
                        {
                            CodeGen codeGen = new CodeGen(typechecker.Types);
                            codeGen.Visit(tree);
                            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\Dank\\Google Drev\\P4\\GOLD\\GOLDParser\\ccode.c");
                            file.WriteLine(codeGen.cPrototype.ToString() + codeGen.cCode.ToString() + codeGen.cFunctionCode.ToString());
                            file.Close();
                            Console.WriteLine("Compile complete");
                            return 1;
                            
                        }
                    }
                }  
            }
            return 0;
            Console.ReadKey();
        }

        private static StreamReader promptInputStream(string Argument = "")
        {
            string file;
            if (Argument == "")
            {
                Console.WriteLine("Specify file to compile:");
                file = Console.ReadLine();
            }
            else file = Argument;
                
            try
            {
                return new StreamReader(@file);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("File not found. Try again.");
                return promptInputStream();
            }
            
        }
    }
}
