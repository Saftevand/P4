using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.Types
{
    public abstract class HaCSType
    {
        public override bool Equals(object obj)
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
            else if(type is tLIST && this is tLIST)
            {
                return (this as tLIST).InnerType.Equals((type as tLIST).InnerType);
            }
            else return false;
        }

        public override string ToString()
        {
            return base.ToString().Remove(0,12);
        }
    }
}
