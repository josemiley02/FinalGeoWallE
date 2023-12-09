using System;
using System.Collections.Generic;
namespace Gsharp
{
    public class CompilatorTools
    {   public static Dictionary<string, TokenType> KeywordsPool = new Dictionary<string, TokenType>()
        {
            {"and" , TokenType.AndKwToken},
            {"arc" , TokenType.ArcKwToken},
            {"black" , TokenType.ColorValueToken},
            {"blue" , TokenType.ColorValueToken},
            {"circle" , TokenType.CircleKwToken},
            {"color" , TokenType.ColorKwToken},
            {"count" , TokenType.SystemFunctionKwToken},
            {"cyan" , TokenType.ColorValueToken},
            {"draw" , TokenType.DrawKwToken},
            {"else" , TokenType.ElseKwToken},
            {"gray" , TokenType.ColorValueToken},
            {"green" , TokenType.ColorValueToken},
            {"if" , TokenType.IfKwToken},
            {"in" , TokenType.InKwToken},
            {"intersect" , TokenType.SystemFunctionKwToken},
            {"import" , TokenType.ImportKwToken},
            {"let" , TokenType.LetKwToken},
            {"line" , TokenType.LineKwToken} ,
            {"magenta" , TokenType.ColorValueToken},
            {"measure" , TokenType.SystemFunctionKwToken},
            {"not" , TokenType.NotKwToken},
            {"or" , TokenType.OrKwToken},
            {"point" , TokenType.PointKwToken},
            {"points" , TokenType.SystemFunctionKwToken},
            {"print" , TokenType.SystemFunctionKwToken} ,
            {"randoms" , TokenType.SystemFunctionKwToken},
            {"ray" , TokenType.RayKwToken},
            {"red" , TokenType.ColorValueToken},
            {"restore" , TokenType.RestoreKwToken},
            {"samples" , TokenType.SystemFunctionKwToken},
            {"segment" , TokenType.SegmentKwToken},
            {"sequence" , TokenType.SequenceKwToken},
            {"then" , TokenType.ThenKwToken},
            {"undefined" , TokenType.UndefinedKwToken},
            {"white" , TokenType.ColorValueToken},
            {"yellow" , TokenType.ColorValueToken},
        };

        public static Dictionary<string, TokenType> OperatorsPool = new Dictionary<string, TokenType>()
        {
            {"+"  , TokenType.PlusSignToken},
            {"-"  , TokenType.MinusSignToken},
            {"*"  , TokenType.StarToken},
            {"/"  , TokenType.SlashToken},
            {"^"  , TokenType.ExponentToken} ,
            {"%"  , TokenType.PercentageSignToken},
            {"("  , TokenType.OpenParenthesisToken},
            {")"  , TokenType.CloseParenthesisToken},
            {"{"  , TokenType.OpenKeyToken},
            {"}"  , TokenType.CloseKeyToken},
            {","  , TokenType.CommaSeparatorToken},
            {";"  , TokenType.SemiColonToken},
            {"!=" , TokenType.NotEqualsToken},
            {"="  , TokenType.EqualsToken},
            {"==" , TokenType.EqualsEqualsToken},
            {">"  , TokenType.GreaterToken},
            {">=" , TokenType.GreaterEqualsToken},
            {"<"  , TokenType.LessToken},
            {"<=" , TokenType.LessEqualsToken},
            {"...", TokenType.TripleDotToken},
            {"\0" , TokenType.EndOfFileToken},
        };
        // Lexer
        public static TokenType GetKeyWordKind(string word)
        {
            if (KeywordsPool.ContainsKey(word))
                return KeywordsPool[word];

            else
                return TokenType.IdentifierToken;
        }
        // Funciones
        public static List<ExecutableFunction> FunctionPool = new List<ExecutableFunction>();
        
        internal static ExecutableFunction SearchFunction(string name, int count)
        {
            foreach (var item in FunctionPool)
            {
                if (item.Match(name, count))
                    return item;
            }

            throw new InvalidOperationException($"!Syntactic Error : Function \"{name}\" receiving {count} arguments does not exist");
        }

        internal static void AddFunction(ExecutableFunction function)
        {
            FunctionPool.Add(function);
        }
        internal static void LoadSystemFunctions()
        {
            AddFunction(new PredefinedFunction("count", 1, WallyType.Number, SystemFunctionsPool.Count));
            AddFunction(new PredefinedFunction("print", 1, WallyType.Text, SystemFunctionsPool.Print));
            AddFunction(new PredefinedFunction("measure", 2, WallyType.Measure, SystemFunctionsPool.Measure));
            AddFunction(new PredefinedFunction("line", 2, WallyType.Line, SystemFunctionsPool.Line));
            AddFunction(new PredefinedFunction("segment", 2, WallyType.Line, SystemFunctionsPool.Segment));
            AddFunction(new PredefinedFunction("ray", 2, WallyType.Line, SystemFunctionsPool.Ray));
            AddFunction(new PredefinedFunction("circle", 2, WallyType.Circle, SystemFunctionsPool.Circle));
            AddFunction(new PredefinedFunction("arc", 4, WallyType.Arc, SystemFunctionsPool.Arc));
            AddFunction(new PredefinedFunction("intersect", 2, WallyType.Sequence, SystemFunctionsPool.Intersect));
            
        }
        // Colores
        public static Stack<string> ColorPool = new Stack<string>();
        
        internal static void AddColor(string color)
        {
            ColorPool.Push(color);
        }
        internal static void RestoreColor()
        {
            if (ColorPool.Count > 1)
                ColorPool.Pop();
        }
        internal static bool IsFigure(IExpression expr)
        {
            WallyType expressionType = expr.ReturnType ;
            if(expressionType == WallyType.Sequence)
            {
                return true ;
            }
                
            WallyType[] figures = new WallyType[]{WallyType.Point, WallyType.Segment, WallyType.Ray, WallyType.Line, WallyType.Circle, WallyType.Arc, WallyType.Undefined};

            
            foreach (var type in figures)
            {
                if(expressionType == type)
                    return true ;
            }
            return false ;
        }

        internal static int CompareExpressions(IExpression a, IExpression b)
        {
            if (a.ReturnType == WallyType.Number && b.ReturnType == WallyType.Number)
            {
                double first = (double)a.Evaluate();
                double second = (double)b.Evaluate();
                return first.CompareTo(second);
            }

            else if(a.ReturnType == WallyType.Measure && b.ReturnType == WallyType.Measure)
                return ((measure)a.Evaluate()).CompareTo((measure)b.Evaluate());
            
            if(a.ReturnType == WallyType.Undefined)
                throw new ArgumentNullException($"{a}");
            if(b.ReturnType == WallyType.Undefined)
                throw new ArgumentNullException($"{b}");

            throw new ArgumentException($"Impossible to compare {a.ReturnType} type with {b.ReturnType} type");
        }
    }
}