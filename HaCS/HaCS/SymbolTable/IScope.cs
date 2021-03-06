﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public interface IScope                                                                 //IScope contains fields describing the name of the scope, it's enclosing scope and two functions Define and Resolve. 
    {
        string ScopeName { get; }
        IScope EnclosingScope { get; }
        Dictionary<string, BaseSymbol> Symbols { get; }
        void Define(BaseSymbol sym);
        BaseSymbol Resolve(string name, bool resolveVarSymbol = true);       
    }
}
