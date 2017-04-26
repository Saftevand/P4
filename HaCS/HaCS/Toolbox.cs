using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaCS.SymbolTable;
using HaCS.Types;

namespace HaCS
{
    public static class Toolbox
    {
        public static HaCSType getType(int tokenType)
        {
            switch (tokenType)
            {
                case HaCSParser.INT: return new tINT();
                case HaCSParser.INT_Type: return new tINT();
                case HaCSParser.FLOAT: return new tFLOAT();
                case HaCSParser.FLOAT_Type: return new tFLOAT();
                case HaCSParser.CHAR: return new tCHAR();
                case HaCSParser.CHAR_Type: return new tCHAR();
                case HaCSParser.BOOL: return new tBOOL();
                case HaCSParser.BOOL_Type: return new tBOOL();
                case HaCSParser.LIST: return new tLIST();
                default: return new tINVALID();
            }
        }
    }
}
