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
        public static HaCSType getType(int tokenType)                                       //Method used for getting the HaCSType corresponding to the int determined by ANTLR4
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

        public static List<ITerminalNode> getFlatTokenList(IParseTree tree)                 //Returns the tokens within the given context as a List<ITerminalNode> by usingInorderTraversal 
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

        public static T FindLastContext<T>(RuleContext context) where T : RuleContext
        {
            if (context is T)
            {
                return context as T;
            }
            if (context.parent != null) return FindLastContext<T>(context.parent);
            else return null;
        }

        public static T FindLastContext<T>(RuleContext context, string identifier) where T : RuleContext
        {
            if (context is T && context.GetText().Contains(identifier))
            {
                return context as T;
            }
            if (context.parent != null) return FindLastContext<T>(context.parent, identifier);
            else return null;
        }
    }
}
