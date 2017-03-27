using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public abstract class BaseSymbol
    {
        public enum HaCSType { tINT, tFLOAT, tCHAR, tINVALID, tBOOL};
        private string _name;
        private HaCSType _symbolType;
        private List<BaseSymbol> _symbols;
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

        public List<BaseSymbol> Symbols
        {
            get { return _symbols; }
            set { _symbols = value; }
        }

    }
}
