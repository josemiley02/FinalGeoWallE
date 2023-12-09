using System;

namespace Gsharp
{
    public abstract class BinaryOperatorExpression : IExpression
    {
        protected IExpression left;
        protected IExpression right;
        protected string OperatorSymbol{get; private set ;}
        public WallyType ReturnType{get; protected set;}
        protected string semanticErrorMsg ;

        protected BinaryOperatorExpression(IExpression left, IExpression right , string operatorSymbol)
        {
            this.left = left;
            this.right = right;

            OperatorSymbol = operatorSymbol;
            ReturnType = WallyType.Number;
            semanticErrorMsg = $"Operator \"{OperatorSymbol}\" cannot be applied to WallyType.{left.ReturnType} and WallyType.{right.ReturnType} : {this}";
        }
        public void GetScope(Scope actual)
        {
            left.GetScope(actual);
            right.GetScope(actual);
        }
        public void CheckSemantics()
        {
            left.CheckSemantics();
            right.CheckSemantics();
            if(!SemanticCondition)
                throw new InvalidOperationException(semanticErrorMsg);
        }
        protected abstract bool SemanticCondition{get;}       
        public abstract object Evaluate() ;
        public bool ConvertToBool() => (double)Evaluate() != 0 ;
        public override string ToString() => $"{left} {OperatorSymbol} {right}";
    }
}