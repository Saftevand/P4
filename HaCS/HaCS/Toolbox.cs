using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaCS.SymbolTable;
using HaCS.Types;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

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

        public static List<ITerminalNode> getFlatTokenList(IParseTree tree)
        {
            List<ITerminalNode> tokens = new List<ITerminalNode>();
            inOrderTraversal(tokens, tree);
            return tokens;
        }


        private static void inOrderTraversal(List<ITerminalNode> tokens, IParseTree parent)
        {
            // Iterate over all child nodes of `parent`.
            for (int i = 0; i < parent.ChildCount; i++)
            {

                // Get the i-th child node of `parent`.
                IParseTree child = parent.GetChild(i);

                if (child is ITerminalNode) {
                // We found a leaf/terminal, add its Token to our list.
                ITerminalNode node = (ITerminalNode)child;
                tokens.Add(node);
            }
        else {
                // No leaf/terminal node, recursively call this method.
                inOrderTraversal(tokens, child);
            }
        }
    }
}
}
