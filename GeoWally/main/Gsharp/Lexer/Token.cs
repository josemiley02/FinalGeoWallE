namespace Gsharp
{
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string text)
        {
            Type = type;
            Value = text;
        }

        public override string ToString()
        {
            string output = $"{Type}";

            if (!string.IsNullOrEmpty(Value))
                output += $", \"{Value}\"";

            return output;
        }
    }
}