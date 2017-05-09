﻿using NUnit.Framework;
using HaCS.SymbolTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable.Tests
{
    [TestFixture()]
    public class LocalScopeTests
    {
        [Test()]
        public void LocalScopeTest()
        {
            LocalScope a = new LocalScope(null);
            Assert.IsNotNull(a);
        }
    }
}