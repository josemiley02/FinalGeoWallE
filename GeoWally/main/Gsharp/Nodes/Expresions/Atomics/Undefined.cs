namespace Gsharp
{
    public sealed class UndefinedExpression : LiteralExpression
    {
        public override WallyType ReturnType => WallyType.Undefined;

        public UndefinedExpression() : base(0) { }

        public override bool ConvertToBool() => false ; 
        public override string ToString() => "undefined" ; 
    }
}