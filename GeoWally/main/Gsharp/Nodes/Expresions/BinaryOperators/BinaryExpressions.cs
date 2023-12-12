using System;

namespace Gsharp
{
    public abstract class BinaryOperatorExpression : IExpression
    {
        protected IExpression left;
        protected IExpression right;
        protected string OperatorSymbol{get; private set ;}
        public WalleType ReturnType{get; protected set;}
        protected Dictionary<(WalleType,WalleType) , WalleType> validTypes ;

        protected BinaryOperatorExpression(IExpression left, IExpression right , string operatorSymbol)
        {
            this.left = left;
            this.right = right;

            OperatorSymbol = operatorSymbol;
            ReturnType = WalleType.Undefined;

            validTypes = new Dictionary<(WalleType , WalleType), WalleType>();
            validTypes[(WalleType.Number , WalleType.Number)] = WalleType.Number ;
        }
        public void GetScope(Scope actual)
        {
            left.GetScope(actual);
            right.GetScope(actual);
        }
        public virtual WalleType CheckSemantics()
        {
            var leftType = left.CheckSemantics();
            var rightType = right.CheckSemantics();

            if (leftType == WalleType.Undefined)
                ReturnType = rightType ;
            
            else if(rightType == WalleType.Undefined)
                ReturnType = leftType ;
            
            else if(validTypes.TryGetValue((leftType , rightType), out var type))
                ReturnType = type ;
            
            else
                throw new InvalidOperationException($"Operator \"{OperatorSymbol}\" cannot be applied to : WalleType.{leftType} and WalleType.{rightType} at \n {this} ");  
            return ReturnType ;
        }
        public abstract object Evaluate();
        public bool ConvertToBool() => (double)Evaluate() != 0 ;
        public override string ToString() => $"{left} {OperatorSymbol} {right}";
    }
}