namespace Gsharp
{
    public sealed class NumberLiteralExpression : LiteralExpression
    {
        public override WallyType ReturnType => WallyType.Number;
        public NumberLiteralExpression(object value) : base(value) { }
        public override bool ConvertToBool() => (double)Evaluate() != 0;
        public override string ToString()=> $"{Value}";
    }
}