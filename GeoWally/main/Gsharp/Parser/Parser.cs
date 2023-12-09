using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gsharp
{
    public sealed class Parser
    {
        private List<Token> tokens;
        private int position; // puntero para ir moviendose entre los tokens
        private Token Current => Peek(0);

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        private Token Peek(int d)
        {
            // devuelve el token que se encuentra a una distancia d del actual
            int index = Math.Min(position + d, tokens.Count - 1);
            return tokens.ElementAt(index);
        }
        private Token NextToken()
        {
            // devuelve el token actual y cambia el puntero de posicion hacia el siguiente
            var current = Current;
            position++;
            return current;
        }

        private Token MatchKind(TokenType kind)
        {
            /* Chequea que el token actual concuerde con el tipo esperado, en caso contrario genera un error
            al comprobar siempre se cambia de token para no tener que hacerlo manualmente en cada momento   */

            if (Current.Type == kind)
                return NextToken();

            else
            {
                if (kind == TokenType.SemiColonToken)
                    throw new Exception("!Syntactic Error : \";\" expected");
                else
                    throw new Exception($"!Syntactic Error : received {Current.Type} while expecting {kind}");
            }
        }

        public SyntaxTree ParseCode()
        {
            return new SyntaxTree(ParseStatementList());
        }
        public List<IStatement> ParseStatementList(TokenType stopper = TokenType.EndOfFileToken)
        {
            List<IStatement> output = new List<IStatement>();
            while (Current.Type != stopper)
            {
                var statement = ParseStatement();
                MatchKind(TokenType.SemiColonToken);
                output.Add(statement);
            }
            return output;
        }

        private IStatement ParseStatement()
        {
            switch (Current.Type)
            {
                case TokenType.IdentifierToken:
                    if (Peek(1).Type == TokenType.OpenParenthesisToken)
                        return ParseFunction();
                    else
                        return ParseAssigment();

                case TokenType.ColorKwToken:
                    NextToken();
                    return new ColorStatement(MatchKind(TokenType.ColorValueToken).Value);

                case TokenType.RestoreKwToken:
                    NextToken();
                    return new RestoreStatement();

                case TokenType.IfKwToken:
                    return ParseCondicionalStatement();

                case TokenType.ImportKwToken:
                    return ParseImportStatement();

                case TokenType.DrawKwToken:
                    NextToken();
                    return new DrawStatement(ParseExpression());

                case TokenType.LetKwToken:
                    return ParseLetInStatement();

                case TokenType.ArcKwToken:
                case TokenType.CircleKwToken:
                case TokenType.LineKwToken:
                case TokenType.PointKwToken:
                case TokenType.RayKwToken:
                case TokenType.SegmentKwToken:

                    if (Peek(1).Type == TokenType.OpenParenthesisToken)
                        return ParsePredefinedFunctionCall();

                    return ParseFigureStatement();

                case TokenType.SystemFunctionKwToken:
                    return ParsePredefinedFunctionCall();

                default:
                    throw new InvalidOperationException($"!Parsing Error : Invalid token {Current} in program flow");
            }
        }
        private IStatement ParseLetInStatement()
        {
            MatchKind(TokenType.LetKwToken);
            List<IStatement> statements = ParseStatementList(TokenType.InKwToken);
            MatchKind(TokenType.InKwToken);

            return new LetInStatement(statements, ParseExpression());
        }

        private IStatement ParseFunction()
        {
            string name = NextToken().Value;
            NextToken();  // saltando el parentesis

            List<IExpression> expressions = (Current.Type == TokenType.CloseParenthesisToken) ? new List<IExpression>() : ParseExpressionList();
            MatchKind(TokenType.CloseParenthesisToken);

            if (Current.Type == TokenType.EqualsToken)
            {
                NextToken();
                return new DeclaredFunctionExpression(name, ConvertToVariables(expressions), ParseExpression());
            }
            return new FunctionCallExpression(name, expressions);
        }

        private static List<string> ConvertToVariables(List<IExpression> expressions)
        {
            List<string> output = new List<string>();
            foreach (var item in expressions)
            {
                if (item is VariableExpression variable)
                    output.Add(variable.Name);
                else
                    throw new ArgumentException($"!Syntactic Error : Unexpected expression as function declaration parameter => {item}");
            }
            return output;
        }
        private IStatement ParseAssigment()
        {
            string identifier = NextToken().Value;

            if (Current.Type == TokenType.CommaSeparatorToken)
            {
                NextToken();
                List<string> variableNames = ParseIdList();
                variableNames.Insert(0, identifier);
                MatchKind(TokenType.EqualsToken);
                return new MultiAssigmentStatement(variableNames, ParseExpression());
            }
            else if (Current.Type == TokenType.EqualsToken)
            {
                NextToken();
                return new SimpleAssigmentStatement(identifier, ParseExpression());
            }
            throw new InvalidOperationException($"!Parsing Error : Invalid token {Current} in program flow");
        }
        private List<string> ParseIdList()
        {
            List<string> output = new List<string>();
            while (true)
            {
                output.Add(MatchKind(TokenType.IdentifierToken).Value);
                if (Current.Type != TokenType.CommaSeparatorToken)
                    break;
                else
                    NextToken();
            }
            return output;
        }
        private IStatement ParseCondicionalStatement()
        {
            NextToken(); // saltando el if
            MatchKind(TokenType.OpenParenthesisToken);
            IExpression condition = ParseExpression();

            MatchKind(TokenType.CloseParenthesisToken);
            MatchKind(TokenType.ThenKwToken);

            IExpression trueBody = ParseExpression();

            MatchKind(TokenType.ElseKwToken);
            IExpression falseBody = ParseExpression();

            return new ConditionalExpression(condition, trueBody, falseBody);
        }

        private IStatement ParseImportStatement()
        {
            NextToken();
            return new ImportStatement(MatchKind(TokenType.StringLiteralToken).Value);
        }

        private IStatement ParseFigureStatement()
        {
            IExpression figureExpression;

            switch (NextToken().Type)
            {
                case TokenType.PointKwToken:
                    figureExpression = new PointExpression(NextToken().Value);
                    break;

                case TokenType.CircleKwToken:
                    figureExpression = new CircleExpression(NextToken().Value);
                    break;

                case TokenType.ArcKwToken:
                    throw new NotImplementedException();

                case TokenType.LineKwToken:
                case TokenType.RayKwToken:
                case TokenType.SegmentKwToken:
                    figureExpression = new LineExpression(NextToken().Value, Peek(-2).Type);
                    break;

                default:
                    throw new ArgumentException($"!Syntactic Error : Unexpected token {Peek(-1)}");
            }

            return new SimpleAssigmentStatement(Peek(-1).Value, figureExpression);
        }

        private IStatement ParsePredefinedFunctionCall()
        {
            string name = NextToken().Value;
            MatchKind(TokenType.OpenParenthesisToken);
            List<IExpression> expressions = (Current.Type == TokenType.CloseParenthesisToken) ? new List<IExpression>() : ParseExpressionList();
            MatchKind(TokenType.CloseParenthesisToken);
            return new FunctionCallExpression(name, expressions);
        }
        private List<IExpression> ParseExpressionList()
        {
            List<IExpression> output = new List<IExpression>();
            while (true)
            {
                output.Add(ParseExpression());
                if (Current.Type != TokenType.CommaSeparatorToken)
                    break;
                NextToken();
            }
            return output;
        }
        private IExpression ParseExpression()
        {
            return ParseLogicalOr();
        }

        private IExpression ParseLogicalOr()
        {
            var left = ParseLogicalAnd();
            while (Current.Type == TokenType.OrKwToken)
            {
                var operatorToken = NextToken().Value;
                var right = ParseLogicalAnd();
                left = new OrExpression(left, right, operatorToken);
            }
            return left;
        }

        private IExpression ParseLogicalAnd()
        {
            var left = ParseEqualityOperator();
            while (Current.Type == TokenType.AndKwToken)
            {
                var operatorToken = NextToken().Value;
                var right = ParseEqualityOperator();
                left = new AndExpression(left, right, operatorToken);
            }
            return left;
        }

        private IExpression ParseEqualityOperator()
        {
            var left = ParseComparison();
            while (Current.Type == TokenType.EqualsEqualsToken || Current.Type == TokenType.NotEqualsToken)
            {
                var operatorToken = NextToken();
                var right = ParseComparison();
                if (operatorToken.Type == TokenType.EqualsEqualsToken)
                    left = new EqualityExpression(left, right, operatorToken.Value);
                else
                    left = new NotEqualityExpression(left, right, operatorToken.Value);
            }
            return left;
        }

        private IExpression ParseComparison()
        {
            var left = ParseSumOrRest();
            while (IsComparerOperator(Current))
            {
                var operatorToken = NextToken();
                var right = ParseSumOrRest();
                switch (operatorToken.Type)
                {
                    case TokenType.GreaterToken:
                        left = new GreaterExpression(left, right, operatorToken.Value);
                        break;

                    case TokenType.GreaterEqualsToken:
                        left = new GreaterEqualsExpression(left, right, operatorToken.Value);
                        break;

                    case TokenType.LessToken:
                        left = new LessExpression(left, right, operatorToken.Value);
                        break;

                    case TokenType.LessEqualsToken:
                        left = new LessEqualsExpression(left, right, operatorToken.Value);
                        break;
                }
            }
            return left;
        }

        private bool IsComparerOperator(Token current)
        {
            TokenType[] logicOperators = new TokenType[]
            {
            TokenType.GreaterToken,TokenType.GreaterEqualsToken , TokenType.LessToken , TokenType.LessEqualsToken
            };

            foreach (var item in logicOperators)
            {
                if (current.Type == item) { return true; }
            }
            return false;
        }

        private IExpression ParseSumOrRest()
        {
            var left = ParseMultiplicationOrDivision();
            while (Current.Type == TokenType.PlusSignToken || Current.Type == TokenType.MinusSignToken)
            {
                var operatorToken = NextToken();
                var right = ParseMultiplicationOrDivision();
                if (operatorToken.Type == TokenType.PlusSignToken)
                    left = new SumExpression(left, right, operatorToken.Value);
                else
                    left = new RestExpression(left, right, operatorToken.Value);
            }

            return left;
        }

        private IExpression ParseMultiplicationOrDivision()
        {
            var left = ParseTerm();
            while (Current.Type == TokenType.StarToken || Current.Type == TokenType.SlashToken)
            {
                var operatorToken = NextToken();
                var right = ParseTerm();

                if (operatorToken.Type == TokenType.StarToken)
                    left = new MultiplicativeExpression(left, right, operatorToken.Value);
                else
                    left = new DivisionExpression(left, right, operatorToken.Value);
            }
            return left;
        }

        private IExpression ParseTerm()
        {
            switch (Current.Type)
            {
                case TokenType.NotKwToken:
                    return ParseNotOperator();

                case TokenType.MinusSignToken:
                    return ParseNegativeOperator();

                case TokenType.NumberLiteralToken:
                    return new NumberLiteralExpression(double.Parse(NextToken().Value));

                case TokenType.StringLiteralToken:
                    return new TextLiteralExpression(NextToken().Value);

                case TokenType.OpenParenthesisToken:
                    NextToken();
                    var expression = ParseExpression();
                    MatchKind(TokenType.CloseParenthesisToken);
                    return expression;

                case TokenType.OpenKeyToken:
                    return ParseSequence();

                case TokenType.UndefinedKwToken:
                    NextToken();
                    return new UndefinedExpression();

                case TokenType.IdentifierToken:
                    if (Peek(1).Type == TokenType.OpenParenthesisToken)
                        return (IExpression)ParseFunction();
                    else
                        return new VariableExpression(NextToken());

                case TokenType.IfKwToken:
                    return (IExpression)ParseCondicionalStatement();

                case TokenType.LetKwToken:
                    return (IExpression)ParseLetInStatement();

                case TokenType.DrawKwToken:
                    NextToken();
                    return new DrawStatement(ParseExpression());

                case TokenType.SystemFunctionKwToken:
                case TokenType.ArcKwToken:
                case TokenType.CircleKwToken:
                case TokenType.LineKwToken:
                case TokenType.PointKwToken:
                case TokenType.RayKwToken:
                case TokenType.SegmentKwToken:

                    if (Peek(1).Type == TokenType.OpenParenthesisToken)
                        return (IExpression)ParsePredefinedFunctionCall();
                    else
                        throw new InvalidOperationException($"!Syntactic Error : Unexpected token in program flow {Current}");

                default:
                    throw new InvalidOperationException($"!Syntactic Error : Unexpected token in program flow {Current}");
            }
        }

        private IExpression ParseNegativeOperator()
        {
            var operatorToken = NextToken();
            var Term = ParseTerm();
            return new NegativeOperatorExpression(Term, operatorToken.Value);
        }

        private IExpression ParseNotOperator()
        {
            var operatorToken = NextToken();
            IExpression Term;

            if (Current.Type == TokenType.MinusSignToken)
                Term = ParseNegativeOperator();
            else
                Term = ParseTerm();

            return new NotOperatorExpression(Term, operatorToken.Value);
        }

        private IExpression ParseSequence()
        {
            NextToken(); // saltando el {

            if (Peek(1).Type == TokenType.TripleDotToken)
                return ParseSequenceInRange();
            else if (Current.Type == TokenType.CloseKeyToken)
            {
                NextToken();
                return new SequenceLiteralExpression(new Sequence(Array.Empty<IExpression>()));
            }
            return ParseRegularSequence();
        }

        private IExpression ParseRegularSequence()
        {
            List<IExpression> items = ParseExpressionList();
            MatchKind(TokenType.CloseKeyToken);
            var sequence = new Sequence(items);
            return new SequenceLiteralExpression(sequence);
        }

        private IExpression ParseSequenceInRange()
        {
            int min = int.Parse(MatchKind(TokenType.NumberLiteralToken).Value);
            NextToken(); // saltando los ...

            int max = (Current.Type == TokenType.CloseKeyToken) ? int.MaxValue : int.Parse(NextToken().Value);
            MatchKind(TokenType.CloseKeyToken);

            var sequence = new Sequence(min, max);
            return new SequenceLiteralExpression(sequence);
        }
    }
}
