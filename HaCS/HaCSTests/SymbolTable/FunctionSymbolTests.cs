using NUnit.Framework;
using HaCS.SymbolTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable.Tests
{
    [TestFixture()]
    public class FunctionSymbolTests
    {
        [Test()]
        public void FunctionInitializationSymbolTest()
        {
            FunctionSymbol a = new FunctionSymbol("TestName", new Types.tFLOAT(), null);

            Assert.IsNotNull(a);
        }

        //[Test()]
        //public void DefineTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void ResolveTest()
        //{
        //    Assert.Fail();
        //}
    }
}