namespace Gsharp  
{
    public abstract class ComparativeExpression : BinaryOperatorExpression
    {
        protected ComparativeExpression(IExpression left , IExpression right , string operatorSymbol) : base(left, right,operatorSymbol)
        {
            validTypes[(WalleType.Measure , WalleType.Measure)] = WalleType.Number;
        }            
    }
    public sealed class GreaterExpression : ComparativeExpression
    {
        public GreaterExpression(IExpression left , IExpression right , string operatorSymbol) : base(left,right,operatorSymbol){}
            
        public override object Evaluate()  // ? cambiar y usar delegados
        {
            if(CompilatorTools.CompareExpressions(left, right) > 0)
                return 1.0 ;
            return 0.0 ;
        }
    }
        
    public sealed class GreaterEqualsExpression : ArithmeticExpression
    {
        public GreaterEqualsExpression(IExpression left , IExpression right, string operatorSymbol) : base(left,right,operatorSymbol){}
            
        public override object Evaluate()
        {
            if(CompilatorTools.CompareExpressions(left, right) >= 0)
                return 1.0 ;
            return 0.0 ;
        }
    }

    public sealed class LessExpression : ArithmeticExpression
    {
        public LessExpression(IExpression left , IExpression right, string operatorSymbol) : base(left,right,operatorSymbol){}
            
        public override object Evaluate()
        {
            if(CompilatorTools.CompareExpressions(left, right) < 0)
                return 1.0 ;
            return 0.0 ;
        }
    }

    public sealed class LessEqualsExpression : ArithmeticExpression
    {
        public LessEqualsExpression(IExpression left , IExpression right, string operatorSymbol) : base(left,right,operatorSymbol){}
            
        public override object Evaluate()
        {
            if(CompilatorTools.CompareExpressions(left, right) <= 0)
                return 1.0 ;
            return 0.0 ;
        }
    }

    public sealed class EqualityExpression : ComparativeExpression
    {
        public EqualityExpression(IExpression left, IExpression right, string operatorSymbol) : base(left , right,operatorSymbol){}

        public override object Evaluate()
        {
            if(CompilatorTools.CompareExpressions(left, right) == 0)
            {
                return 1.0 ;
            }
            return 0.0 ;
        }
    }

    public sealed class NotEqualityExpression : ComparativeExpression
    {
        public NotEqualityExpression(IExpression left, IExpression right, string operatorSymbol) : base(left , right,operatorSymbol){}

        public override object Evaluate()
        {
            if(CompilatorTools.CompareExpressions(left, right) != 0)
                return 1.0 ;
            return 0.0 ;
        }
    }
}