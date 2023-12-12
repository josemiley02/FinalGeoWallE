using System;

namespace Gsharp 
{
    public abstract class ArithmeticExpression : BinaryOperatorExpression
    {
        public ArithmeticExpression(IExpression left, IExpression right , string operatorSymbol) : base(left, right , operatorSymbol){}         
    }

    public sealed class SumExpression : ArithmeticExpression
    {
        public SumExpression(IExpression left, IExpression right, string operatorSymbol) : base(left , right , operatorSymbol)
        {
            validTypes[ (WalleType.Sequence , WalleType.Sequence) ] = WalleType.Sequence ;
            
            validTypes[ (WalleType.Measure , WalleType.Measure) ] = WalleType.Measure ; 
        }
        
        
        public override object Evaluate()
        {
            if(left.ReturnType == WalleType.Measure)
                return (Measure) left.Evaluate() + (Measure) right.Evaluate();
            
            // concatenar secuencias
            return (double)left.Evaluate() + (double)right.Evaluate();
        }
    }
        
    public sealed class RestExpression : ArithmeticExpression
    {
        public RestExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right , operatorSymbol)
        {
            
            validTypes[ (WalleType.Measure , WalleType.Measure) ] = WalleType.Measure ;
        }
        
        
        public override object Evaluate()
        {
            if(left.ReturnType == WalleType.Measure)
                return (Measure)left.Evaluate() - (Measure)right.Evaluate();
                
            return (double)left.Evaluate() - (double)right.Evaluate();        
        }
    }

    public sealed class MultiplicativeExpression : ArithmeticExpression
    {
        public MultiplicativeExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right , operatorSymbol)
        {
            
            validTypes[ (WalleType.Number , WalleType.Measure) ] = WalleType.Measure ;
            
            validTypes[ (WalleType.Measure , WalleType.Number) ] = WalleType.Measure ;
        }
        public override object Evaluate()
        {
            if(left.ReturnType == WalleType.Measure)
            {
                ReturnType = WalleType.Measure ;
                return(Measure)left.Evaluate() * (double)right.Evaluate() ; 
            }
            return (double)left.Evaluate() * (double)right.Evaluate();
        }
    }

    public sealed class DivisionExpression : ArithmeticExpression
    {
        public DivisionExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right, operatorSymbol)
        {
            
            validTypes[ (WalleType.Measure , WalleType.Measure) ] = WalleType.Number ;
        }
        
        public override object Evaluate()        
        {
            try
            {
                if(left.ReturnType == WalleType.Measure)
                    return (Measure)left.Evaluate() / (Measure)right.Evaluate();
                return (double)left.Evaluate() / (double)right.Evaluate();
            }
            catch(DivideByZeroException)
            {
                throw new DivideByZeroException($"{this}");
            }
        }
    }
}