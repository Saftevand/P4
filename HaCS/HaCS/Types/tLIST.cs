using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.Types
{
    public class tLIST : HaCSType
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
        public HaCSType LastType()
        {
            if(_innerType is tLIST)
            {
                return (_innerType as tLIST).LastType();
            }
            else return _innerType;
        }

        public override string ToString()
        {
            return base.ToString() + "<" + InnerType.ToString() + ">";
        }
        #endregion
    }
}
