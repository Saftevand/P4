using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.Types
{
    public abstract class HaCSType
    {
        #region Methods
        public override bool Equals(object obj)                                             //Simple Equal method to compare HaCS types
        {
            HaCSType type = (HaCSType)obj;
            if(type == null)
            {
                return false;
            }
            else if(type is tINT && this is tINT)
            {
                return true;
            }
            else if(type is tFLOAT && this is tFLOAT)
            {
                return true;
            }
            else if(type is tCHAR && this is tCHAR)
            {
                return true;
            }
            else if(type is tBOOL && this is tBOOL)
            {
                return true;
            }
            else if(type is tLIST && this is tLIST)                                         //Ensures that the level of nested lists corresponds and the innermost list contains elements of the same type. 
            {
                return (this as tLIST).InnerType.Equals((type as tLIST).InnerType);
            }
            else if(type is tINVALID && this is tINVALID)
            {
                return true;
            }
            else return false;
        }

        public override string ToString()
        {
            return base.ToString().Remove(0,12);
        }
        #endregion
    }
}
