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
    public class VariableSymbolTests
    {
        [Test()]
        public void VariableSymbolTest()
        {
            VariableSymbol a = new VariableSymbol("Test", new Types.tFLOAT(), null);

            Assert.IsNotNull(a);
        }
    }
}