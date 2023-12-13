namespace Gsharp
{
    public enum WalleType
    {
        Number, Text, Undefined, Sequence,Void, Line , Point , Segment , Ray , Arc , Circle, Measure
    }

    public enum FigureType
    {
        Arc, Circle, Line, Point, Ray, Segment,
    }
    public enum TokenType
    {
        // Tokens
        BadToken, EndOfFileToken, WhiteSpaceToken, NumberLiteralToken, IdentifierToken, StringLiteralToken, CommentaryToken,

        // Keywords
        AndKwToken, ArcKwToken, CircleKwToken, ColorKwToken, DrawKwToken, ElseKwToken, IfKwToken, InKwToken, ImportKwToken,
        LetKwToken, LineKwToken, NotKwToken, OrKwToken, PointKwToken, RayKwToken, RestoreKwToken,
        SegmentKwToken, SequenceKwToken, ThenKwToken, UndefinedKwToken, SystemFunctionKwToken,

        // Colores
        ColorValueToken ,

        // Delimitadores
        OpenParenthesisToken, CloseParenthesisToken, OpenKeyToken, CloseKeyToken, CommaSeparatorToken, SemiColonToken,

        // Operadores
        TripleDotToken,

        // Logicos
        EqualsToken, EqualsEqualsToken, NotEqualsToken, LessToken, LessEqualsToken, GreaterToken, GreaterEqualsToken,

        // Aritmeticos
        PlusSignToken, MinusSignToken, StarToken, SlashToken, PercentageSignToken, ExponentToken,
    }
}