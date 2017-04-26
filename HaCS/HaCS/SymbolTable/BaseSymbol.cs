﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaCS.Types;

namespace HaCS.SymbolTable
{
    public abstract class BaseSymbol
    {
        private string _name;
        private HaCSType _symbolType;
        private IScope _parentScope;

        public BaseSymbol(string name, HaCSType symbolType, IScope parentScope)
        {
            this._name = name;
            this._symbolType = symbolType;
            this._parentScope = parentScope;
        }

        public HaCSType SymbolType
        {
            get { return _symbolType; }
            set { _symbolType = value; }
        }

        public IScope ParentScope
        {
            get { return _parentScope; }
            set { _parentScope = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
