namespace Gsharp
{
    public sealed class NumberLiteralExpression : LiteralExpression
    {
        public override WalleType ReturnType => WalleType.Number;
        public NumberLiteralExpression(object value) : base(value) { }
        public override bool ConvertToBool() => (double)Evaluate() != 0;
        public override string ToString()=> $"{Value}";
    }
}