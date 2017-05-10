using NUnit.Framework;
using HaCS.SymbolTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace HaCS.SymbolTable.Tests
{
    [TestFixture()]
    public class DefPhaseTests
    {
        [Test()]
        public void SymbolTableInitializationTest()
        {
            DefPhase a = new DefPhase();

            Assert.IsNotNull(a);
        }

        //[Test()]
        //public void EnterMainTest()
        //{
        //    string file = "C:\\Users\\M0107\\Documents\\P4\\HaCS\\HaCS\\bin\\Debug\\UnitTestFile.txt";
        //    StreamReader IS = new StreamReader(@file);
        //    AntlrInputStream input = new AntlrInputStream(IS.ReadToEnd());
        //    HaCSLexer lexer = new HaCSLexer(input);
        //    CommonTokenStream tokens = new CommonTokenStream(lexer);
        //    HaCSParser parser = new HaCSParser(tokens);
        //    IParseTree tree = parser.program();
        //    ParseTreeWalker walker = new ParseTreeWalker();
        //    DefPhase def = new DefPhase();
        //    IScope temp = def.CurrentScope;
        //    walker.Walk(def, tree);

        //    Assert.AreNotEqual(temp, def.CurrentScope); 
        //    Assert.Fail();
        //}

        //[Test()]
        //public void ExitMainTest()
        //{
            
        //}

        //[Test()]
        //public void EnterFunctionDeclTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void ExitFunctionDeclTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void EnterBodyTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void ExitBodyTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void ExitFormalParamTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void ExitVarDclTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void DefineVariableTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void DefineVariableTest1()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void DefineVariableTest2()
        //{
        //    Assert.Fail();
        //}
    }
}