namespace Gsharp
{
    public sealed class TextLiteralExpression : LiteralExpression
    {
        public override WalleType ReturnType => WalleType.Text;
        public TextLiteralExpression(object value) : base(value) { }

        public override string ToString() => $"\"{Value}\"";
    }
}