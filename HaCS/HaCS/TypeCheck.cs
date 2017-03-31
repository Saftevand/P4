using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace HaCS
{
    public class TypeCheck : HaCSBaseListener
    {
        public override void EnterParens(HaCSParser.ParensContext context)
        {
            int typeTokentype = context
        }
        public override void EnterLit(HaCSParser.LitContext context)
        {
            int typeTokenType = context.Start.Type;
            SymbolTable.BaseSymbol.HaCSType type = Toolbox.getType(typeTokenType);
        }

    }
}
