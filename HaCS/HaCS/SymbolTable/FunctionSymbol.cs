﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaCS.Types;

namespace HaCS.SymbolTable
{
    public class FunctionSymbol : BaseSymbol, IScope                                        //Inherits from the BaseSymbol and implements the IScope interface
    {                                                                                       //Hereby a FunctionSymbol will be a scope itself, which can contain symbols.
                                                                                            //Further explanation can be found in BaseSymbol and IScope
        private Dictionary<string, BaseSymbol> _symbols = new Dictionary<string, BaseSymbol>();

        public FunctionSymbol(string name, HaCSType symbolType, IScope parentScope) : base(name,symbolType,parentScope)
        {
        }

        public IScope EnclosingScope
        {
            get { return ParentScope;}
        }

        public string ScopeName
        {
            get { return Name;}
        }

        public void Define(BaseSymbol sym)
        {
            _symbols.Add(sym.Name, sym);
            sym.ParentScope = this;
        }

        public BaseSymbol Resolve(string name)
        {
            if (_symbols.ContainsKey(name))
            {
                BaseSymbol sym = _symbols[name];
                return sym;
            }
            else if (ParentScope != null) return ParentScope.Resolve(name);
            else return null;
        }

        public Dictionary<string, BaseSymbol> Symbols
        {
            get { return _symbols; }
        }
    }
}
