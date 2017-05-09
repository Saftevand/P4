﻿using System;
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
            StreamReader inputStream;
            if (args.Count() == 0)
            {
                inputStream = promptInputStream();
            }
            else inputStream = promptInputStream(args[0]);
            AntlrInputStream input = new AntlrInputStream(inputStream.ReadToEnd());
            HaCSLexer lexer = new HaCSLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            HaCSParser parser = new HaCSParser(tokens);
            IParseTree tree = parser.program();
            if(parser.NumberOfSyntaxErrors == 0)
            {
                ParseTreeWalker walker = new ParseTreeWalker();
                SymbolTable.DefPhase Def = new SymbolTable.DefPhase();
                walker.Walk(Def, tree);
                SymbolTable.RefPhase Ref = new SymbolTable.RefPhase(Def.Global, Def.Scopes);
                walker.Walk(Ref, tree);
                if(Ref.ErrorCounter == 0)
                {
                    TypeCheck typechecker = new TypeCheck(Def.Scopes);
                    typechecker.Visit(tree);
                    if(typechecker.ErrorCounter == 0)
                    {
                        //CodeGen codeGen = new CodeGen();
                        //codeGen.Visit(tree);
                        Console.WriteLine("Compile complete");
                    }
                }
                
            }
            
            

            //Console.WriteLine("Test"+Environment.NewLine + codeGen.cFunctionCode + Environment.NewLine + codeGen.cCode);
            //File.WriteAllText(@"C:\Users\Dank\Google Drev\P4\GOLD\GOLDParser\ccode.c", codeGen.cFunctionCode.ToString() + codeGen.cCode.ToString());

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
