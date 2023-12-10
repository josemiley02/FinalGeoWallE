using System;

namespace Gsharp
{
    public abstract class BinaryOperatorExpression : IExpression
    {
        protected IExpression left;
        protected IExpression right;
        protected string OperatorSymbol{get; private set ;}
        public WalleType ReturnType{get; protected set;}

        protected BinaryOperatorExpression(IExpression left, IExpression right , string operatorSymbol)
        {
            this.left = left;
            this.right = right;

            OperatorSymbol = operatorSymbol;
            ReturnType = WalleType.Undefined;
        }
        public void GetScope(Scope actual)
        {
            left.GetScope(actual);
            right.GetScope(actual);
        }
        public WalleType CheckSemantics()
        {
            WalleType returnType ;
            var leftType = left.CheckSemantics();
            var rightType = right.CheckSemantics();

            if(!SemanticCondition(leftType, rightType , out returnType))
                throw new InvalidOperationException($"Operator \"{OperatorSymbol}\" cannot be applied to WalleType.{left.ReturnType} and WalleType.{right.ReturnType} : {this}");

            ReturnType = returnType;
            return ReturnType ;
        }
        protected abstract bool SemanticCondition(WalleType leftType , WalleType rightType , out WalleType returnType);    
        public abstract object Evaluate();
        public bool ConvertToBool() => (double)Evaluate() != 0 ;
        public override string ToString() => $"{left} {OperatorSymbol} {right}";
    }
}