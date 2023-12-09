namespace Gsharp
{
    public interface IExpression : INode
    {
        WallyType ReturnType { get; }
        object Evaluate();
        bool ConvertToBool();
    }
}