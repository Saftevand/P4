using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaCS.SymbolTable;

namespace HaCS
{
    public static class Toolbox
    {
        public static BaseSymbol.HaCSType getType(int tokenType)
        {
            switch (tokenType)
            {
                case HaCSParser.INT_Type: return BaseSymbol.HaCSType.tINT;
                case HaCSParser.FLOAT_Type: return BaseSymbol.HaCSType.tFLOAT;
                case HaCSParser.CHAR_Type: return BaseSymbol.HaCSType.tCHAR;
                case HaCSParser.BOOL_Type: return BaseSymbol.HaCSType.tBOOL;
                case HaCSParser.LIST: return BaseSymbol.HaCSType.tLIST;
                default: return BaseSymbol.HaCSType.tINVALID;
            }
        }
    }
}
