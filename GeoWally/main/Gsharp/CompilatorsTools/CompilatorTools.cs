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
            AddFunction(new PredefinedFunction("count", 1, WalleType.Number, SystemFunctionsPool.Count));
            AddFunction(new PredefinedFunction("print", 1, WalleType.Text, SystemFunctionsPool.Print));
            AddFunction(new PredefinedFunction("measure", 2, WalleType.Measure, SystemFunctionsPool.Measure));
            AddFunction(new PredefinedFunction("line", 2, WalleType.Line, SystemFunctionsPool.Line));
            AddFunction(new PredefinedFunction("segment", 2, WalleType.Line, SystemFunctionsPool.Segment));
            AddFunction(new PredefinedFunction("ray", 2, WalleType.Line, SystemFunctionsPool.Ray));
            AddFunction(new PredefinedFunction("circle", 2, WalleType.Circle, SystemFunctionsPool.Circle));
            AddFunction(new PredefinedFunction("arc", 4, WalleType.Arc, SystemFunctionsPool.Arc));
            AddFunction(new PredefinedFunction("intersect", 2, WalleType.Sequence, SystemFunctionsPool.Intersect));
            
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
            WalleType expressionType = expr.ReturnType ;
            if(expressionType == WalleType.Sequence)
            {
                return true ;
            }
                
            WalleType[] figures = new WalleType[]{WalleType.Point, WalleType.Segment, WalleType.Ray, WalleType.Line, WalleType.Circle, WalleType.Arc, WalleType.Undefined};

            
            foreach (var type in figures)
            {
                if(expressionType == type)
                    return true ;
            }
            return false ;
        }

        internal static int CompareExpressions(IExpression a, IExpression b)
        {
            if (a.ReturnType == WalleType.Number && b.ReturnType == WalleType.Number)
            {
                double first = (double)a.Evaluate();
                double second = (double)b.Evaluate();
                return first.CompareTo(second);
            }

            else if(a.ReturnType == WalleType.Measure && b.ReturnType == WalleType.Measure)
                return ((Measure)a.Evaluate()).CompareTo((Measure)b.Evaluate());
            
            if(a.ReturnType == WalleType.Undefined)
                throw new ArgumentNullException($"{a}");
            if(b.ReturnType == WalleType.Undefined)
                throw new ArgumentNullException($"{b}");

            throw new ArgumentException($"Impossible to compare {a.ReturnType} type with {b.ReturnType} type");
        }
    }
}