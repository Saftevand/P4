using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.SymbolTable
{
    public class VariableSymbol : BaseSymbol
    {
        public VariableSymbol(string name, HaCSType symbolType, IScope parentScope) : base(name,symbolType,parentScope)
        {

        }
    }
}
