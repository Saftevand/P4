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
    public class GlobalScopeTests
    {
        [Test()]
        public void GlobalScopeTest()
        {
            GlobalScope a = new GlobalScope(null);
            Assert.IsNotNull(a); 
        }
    }
}