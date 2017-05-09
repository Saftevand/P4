using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public class GlobalScope : BaseScope
    {
        public GlobalScope(IScope parentScope) : base(parentScope)
        {
        }

        #region Properties
        public string GlobalScopeName
        {
            get { return "Global"; }
        }
        #endregion
    }
}
