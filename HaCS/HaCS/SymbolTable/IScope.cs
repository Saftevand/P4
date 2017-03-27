using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public interface IScope
    {
        string ScopeName { get; }
        IScope EnclosingScope { get; }
        void Define(BaseSymbol sym);
        BaseSymbol Resolve(string name);
    }
}
