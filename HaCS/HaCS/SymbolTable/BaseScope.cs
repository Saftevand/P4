using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public abstract class BaseScope : IScope
    {
        private IScope _enclosingScope;                                                     //Reference to the scope from where it was declared
        private Dictionary<string, BaseSymbol> _symbols = new Dictionary<string, BaseSymbol>(); //A dictionary of key-value pairs of respectively strings and BaseSymbols, where the string corresponds to the identifier of the BaseSymbol.

        public BaseScope(IScope enclosingScope)                                                 //The constructor which takes a scope as input. The scope should correspond to the scope from where .this was declared.
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

        public void Define(BaseSymbol sym)                                                  //Takes a BaseSymbol as a function parameter and adds the symbol to the dictionary: _symbol. The function Resolve at line 23 on the other hand is used for retrieving symbols from the dictionary.
        {
            _symbols.Add(sym.Name, sym);
            sym.ParentScope = this;
        }

        public BaseSymbol Resolve(string name, bool resolveVar = true)                                             //Lookups the dictionary and returns the symbol with the identifier matching the input.
        {
            if (resolveVar)
            {
                if (_symbols.ContainsKey(name))
                {
                    if (_symbols[name] is VariableSymbol)
                    {
                        BaseSymbol sym = _symbols[name];
                        return sym;
                    }
                    else if (_enclosingScope != null) return _enclosingScope.Resolve(name);
                    else return null;
                }
                else if (_enclosingScope != null) return _enclosingScope.Resolve(name);        //If the symbol isn't in the scope, it will recursively look up the enclosing scope to find it.
                else return null;
            }
            else
            {
                if (_symbols.ContainsKey(name))
                {
                    if (_symbols[name] is FunctionSymbol)
                    {
                        BaseSymbol sym = _symbols[name];
                        return sym;
                    }
                    else if (_enclosingScope != null) return _enclosingScope.Resolve(name, false);
                    else return null;
                }
                else if (_enclosingScope != null) return _enclosingScope.Resolve(name,false);        //If the symbol isn't in the scope, it will recursively look up the enclosing scope to find it.
                else return null;
            }
            
        }
    }
}
