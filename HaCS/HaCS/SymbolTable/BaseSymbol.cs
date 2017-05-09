using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaCS.Types;

namespace HaCS.SymbolTable
{
    public abstract class BaseSymbol
    {
        #region Variables
        private string _name;
        private HaCSType _symbolType;
        private IScope _parentScope;
        #endregion

        public BaseSymbol(string name, HaCSType symbolType, IScope parentScope)
        {
            this._name = name;
            this._symbolType = symbolType;
            this._parentScope = parentScope;
        }

        #region Properties
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
        #endregion
    }
}
