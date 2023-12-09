namespace Gsharp
{
    public sealed class TextLiteralExpression : LiteralExpression
    {
        public override WallyType ReturnType => WallyType.Text;
        public TextLiteralExpression(object value) : base(value) { }

        public override string ToString() => $"\"{Value}\"";
    }
}