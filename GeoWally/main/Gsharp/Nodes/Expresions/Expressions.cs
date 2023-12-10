namespace Gsharp
{
    public interface IExpression : INode
    {
        WalleType ReturnType { get; }
        object Evaluate();
        bool ConvertToBool();
    }
}