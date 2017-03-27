using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public abstract class BaseScope : IScope
    {
        private IScope _enclosingScope;
        private Dictionary<string, BaseSymbol> _symbols = new Dictionary<string, BaseSymbol>();

        public BaseScope(IScope enclosingScope)
        {
            this._enclosingScope = enclosingScope;
        }

        public Dictionary<string, BaseSymbol> Symbols
        {
            get { return _symbols; }
            set { _symbols = value; }
        }

        public string ScopeName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IScope EnclosingScope
        {
            get{return _enclosingScope;}
        }

        public void Define(BaseSymbol sym)
        {
            _symbols.Add(sym.Name, sym);
            sym.ParentScope = this;
        }

        public BaseSymbol Resolve(string name)
        {
            BaseSymbol sym = _symbols[name];
            if (sym != null) return sym;
            else if (_enclosingScope != null) return _enclosingScope.Resolve(name);
            else return null;
        }
    }
}
