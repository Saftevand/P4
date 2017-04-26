using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.Types
{
    public class tLIST : HaCSType
    {
        private HaCSType _innerType;

        public HaCSType InnerType
        {
            get { return _innerType; }
            set { _innerType = value; }
        }
    }
}
