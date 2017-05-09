using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public interface IScope
    {
        #region Properties
        string ScopeName { get; }
        IScope EnclosingScope { get; }
        #endregion

        #region Methods
        void Define(BaseSymbol sym);
        BaseSymbol Resolve(string name);
        #endregion
    }
}
