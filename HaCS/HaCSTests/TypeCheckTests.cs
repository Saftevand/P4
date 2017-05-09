using NUnit.Framework;
using HaCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace HaCS.Tests
{
    [TestFixture()]
    public class TypeCheckTests
    {
        [Test()]
        public void TypeCheckConstructorTest()
        {
            HaCS.SymbolTable.DefPhase Def = new SymbolTable.DefPhase();
            TypeCheck a = new TypeCheck(Def.Scopes);

            Assert.AreEqual(a.Scopes, Def.Scopes);

        }

    //    [Test()]
    //    public void VisitFunctionDeclTest()
    //    {
    //        //HaCS.HaCSParser.FunctionDeclContext context = new HaCSParser.FunctionDeclContext(new ParserRuleContext(), 1/*Ved ikke lige hvilken int værdi der skal bruges her...*/);
    //        //HaCS.SymbolTable.DefPhase Def = new SymbolTable.DefPhase();
    //        //TypeCheck a = new TypeCheck(Def.Scopes);

    //        //a.VisitFunctionDecl(context);

    //        //a.Types.Get(context);

    //        HaCS.SymbolTable.DefPhase Def = new SymbolTable.DefPhase();
    //        TypeCheck a = new TypeCheck(Def.Scopes);

    //        a.VisitFunctionDecl(new HaCSParser.FunctionDeclContext(new ParserRuleContext(), -1));

    //        Assert.IsTrue(false/*Fikse*/);
    //    }

    //[Test()]
    //    public void VisitMainTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitBodyTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitParensTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitExponentTest()
    //    {
    //        Assert.Fail();
    //    } //Her er der en 50/50 chance for en god kommentar
    //      //Du slår med terningen og ruller en 1'er
    //      //RNGesus var ikke med dig og du møder istedet en dårlig kommentar

    //    [Test()]
    //    public void VisitVarTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitArith2Test()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitArith1Test()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitCompareTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitEqualityTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitPipeTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitFuncTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitVarDclTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitAndTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitOrTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitLitTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitIfStmtTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitElseifStmtTest()
    //    {
    //        Assert.Fail();
    //    }

    //    [Test()]
    //    public void VisitReturnStmtTest()
    //    {
    //        Assert.Fail();
    //    }
    }
}