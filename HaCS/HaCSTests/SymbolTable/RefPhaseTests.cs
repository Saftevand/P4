using NUnit.Framework;
using HaCS.SymbolTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaCS;
using Antlr4;
using Antlr4.Runtime.Tree;


namespace HaCS.SymbolTable.Tests
{
    [TestFixture()]
    public class RefPhaseTests
    {
        [Test()]
        public void RefPhaseInitializationTest()
        {
            RefPhase a = new RefPhase(null, (ParseTreeProperty<IScope>)null);
            
            Assert.IsNotNull(a);
        }

        //[Test()]
        //public void EnterProgramTest()
        //{
        //    Assert.Fail();
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
        //public void ExitVarTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void ExitFuncTest()
        //{
        //    Assert.Fail();
        //}
    }
}