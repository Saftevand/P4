using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace HaCS
{
    class CodeGenLis : HaCSBaseListener
    {
        StringBuilder Code = new StringBuilder();
        int varCount;

        public override void EnterProgram([NotNull] HaCSParser.ProgramContext context)
        {
            Code.Append("#include <stdlib.o>" + Environment.NewLine + "int main()");
            //base.EnterProgram(context);
        }

        public override void ExitProgram([NotNull] HaCSParser.ProgramContext context)
        {
            Console.WriteLine(Code);
        }

        public override void EnterBody([NotNull] HaCSParser.BodyContext context)
        {
            Code.AppendLine("{");
        }
        public override void ExitBody([NotNull] HaCSParser.BodyContext context)
        {
            Code.AppendLine("}");
        }

        public override void EnterStmt([NotNull] HaCSParser.StmtContext context)
        {
            base.EnterStmt(context);
        }

        public override void ExitStmt([NotNull] HaCSParser.StmtContext context)
        {
            Code.Append(";");
        }

        public override void EnterPrimitiveType([NotNull] HaCSParser.PrimitiveTypeContext context)
        {
            //Code.Append(context.GetChild(0));
        }

        public override void EnterVarDcl([NotNull] HaCSParser.VarDclContext context)
        {
            string variable = "variable" + varCount++.ToString();

            Code.Append(variable + " = " + context.GetChild(3).GetText());
        }
    }
}
