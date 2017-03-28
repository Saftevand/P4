﻿using System;
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
                case HaCSParser.INT: return BaseSymbol.HaCSType.tINT;
                case HaCSParser.FLOAT: return BaseSymbol.HaCSType.tFLOAT;
                case HaCSParser.CHAR: return BaseSymbol.HaCSType.tCHAR;
                case HaCSParser.BOOL: return BaseSymbol.HaCSType.tBOOL;
                default: return BaseSymbol.HaCSType.tINVALID;
            }
        }
    }
}