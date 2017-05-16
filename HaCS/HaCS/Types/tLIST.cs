using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.Types
{
    public class tLIST : HaCSType                                                           //The tLIST type can contains a variable _innerType which is used as a reference to the list or type of elemenents it might contain  
    {
        #region Variables
        private HaCSType _innerType = null;
        #endregion

        #region Properties
        public HaCSType InnerType
        {
            get { return _innerType; }
            set { _innerType = value; }
        }
        #endregion

        #region Methods
        public HaCSType LastType()                                                          //Does recursively go through the innerType, hereby all the possible nested lists and returns the variable with the type of the innermost list's elements
        {
            if(_innerType is tLIST)
            {
                return (_innerType as tLIST).LastType();
            }
            else return _innerType;
        }

        public void inputTypeRecursively(HaCSType inputType)
        {
            if (_innerType == null)
            {
                _innerType = inputType;
            }
            else if(_innerType is tLIST) (_innerType as tLIST).inputTypeRecursively(inputType);
        }

        public override string ToString()
        {
            return base.ToString() + "<" + InnerType.ToString() + ">";
        }
        #endregion
    }
}
