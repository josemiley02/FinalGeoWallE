using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gsharp;

namespace Geo_Wall_E.CallLogic
{
    public static class CallLogic
    {
        public static SyntaxTree WorkWithCode(string code)
        {
            CompilatorTools.LoadSystemFunctions();
            Lexer lexer = new Lexer(code);
            Parser parser = new Parser(lexer.GetTokenList());
            SyntaxTree syntax = parser.ParseCode();
            Scope scope = new Scope();
            foreach(var item in syntax.Program)
            {
                item.GetScope(scope);
            }
            foreach(var item in syntax.Program)
            {
                item.CheckSemantics();
            }
            return syntax;
        }
    }
}
