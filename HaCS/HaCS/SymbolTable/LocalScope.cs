using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public class LocalScope : BaseScope
    {
        public LocalScope(IScope parentScope) : base(parentScope)
        {
        }

        #region Properties
        public string LocalScopeName
        {
            get { return "local"; }
        }
        #endregion
    }
}
