namespace Gsharp 
{
    public abstract class LogicOperatorExpression : BinaryOperatorExpression
    {
        public LogicOperatorExpression(IExpression left, IExpression right, string operatorSymbol): base(left , right, operatorSymbol){}
        protected override bool SemanticCondition(WalleType leftType , WalleType rightType , out WalleType returnType)
        {
            returnType = WalleType.Number;
            return true ;
        }
    }

    public sealed class AndExpression : LogicOperatorExpression
    {
        public AndExpression(IExpression left, IExpression right,string operatorSymbol) : base(left, right,operatorSymbol){}
        public override object Evaluate()
        {
            if(left.ConvertToBool() && right.ConvertToBool())
            {
                return 1.0 ;
            }
            return 0.0 ;
        }      
    }

    public sealed class OrExpression : LogicOperatorExpression
    {
        public OrExpression(IExpression left, IExpression right, string operatorSymbol) : base(left, right, operatorSymbol){}
        public override object Evaluate()
        {
            if(left.ConvertToBool() || right.ConvertToBool())
            {
                return 1.0 ;
            }
            return 0.0 ; 
        }       
    }    
}