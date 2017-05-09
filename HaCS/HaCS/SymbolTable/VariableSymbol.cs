using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaCS.Types;

namespace HaCS.SymbolTable
{
    public class VariableSymbol : BaseSymbol                                                //Inherits from BaseSymbol. A variable symbol is used for primitive and constructor types. 
    {
        public VariableSymbol(string name, HaCSType symbolType, IScope parentScope) : base(name,symbolType,parentScope)
        {

        }
    }
}
