using System;

namespace Gsharp 
{
    public abstract class ArithmeticExpression : BinaryOperatorExpression
    {
        public ArithmeticExpression(IExpression left, IExpression right , string operatorSymbol) : base(left, right , operatorSymbol){}
        
        protected override bool SemanticCondition{
            get
            { 
                if(left.ReturnType == WallyType.Undefined ||  right.ReturnType == WallyType.Undefined )
                    return true ;
            
                else if(left.ReturnType == WallyType.Number && right.ReturnType == WallyType.Number)
                    return true ;
                
                return false ;
            }
        }
    }

    public sealed class SumExpression : ArithmeticExpression
    {
        public SumExpression(IExpression left, IExpression right, string operatorSymbol) : base(left , right , operatorSymbol){}
        protected override bool SemanticCondition
        {
            get
            {
                if(base.SemanticCondition || (left.ReturnType == WallyType.Measure && right.ReturnType == WallyType.Measure))
                    return true;
                else if(left.ReturnType == WallyType.Sequence && right.ReturnType == WallyType.Sequence)
                {
                    // chequear que tengan el mismo tipo de elementos(todavia no lo he hecho)
                    return true ;
                }
                return false ;
            }
        }
        public override object Evaluate()
        {
            if(left.ReturnType == WallyType.Measure)
                return (measure) left.Evaluate() + (measure) right.Evaluate();
            
            // concatenar secuencias

            return (double)left.Evaluate() + (double)right.Evaluate();
        }
    }
        
    public sealed class RestExpression : ArithmeticExpression
    {
        public RestExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right , operatorSymbol){}
        protected override bool SemanticCondition => base.SemanticCondition || left.ReturnType == WallyType.Measure && right.ReturnType == WallyType.Measure ;
        public override object Evaluate()
        {
            if(left.ReturnType == WallyType.Measure)
                return (measure)left.Evaluate() - (measure)right.Evaluate();
                
            return (double)left.Evaluate() - (double)right.Evaluate();        
        }
    }

    public sealed class MultiplicativeExpression : ArithmeticExpression
    {
        public MultiplicativeExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right , operatorSymbol){}

        protected override bool SemanticCondition
        {
            get
            {
                return base.SemanticCondition || (left.ReturnType == WallyType.Number && right.ReturnType == WallyType.Measure) || (left.ReturnType == WallyType.Measure && right.ReturnType == WallyType.Number);
            }
        }
        public override object Evaluate()
        {
            if(left.ReturnType == WallyType.Measure)
            {
                ReturnType = WallyType.Measure ;
                return(measure)left.Evaluate() * (double)right.Evaluate() ; 
            }
            return (double)left.Evaluate() * (double)right.Evaluate();
        }
    }

    public sealed class DivisionExpression : ArithmeticExpression
    {
        public DivisionExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right, operatorSymbol){}
        protected override bool SemanticCondition => base.SemanticCondition || (left.ReturnType == WallyType.Measure && right.ReturnType == WallyType.Measure);
        public override object Evaluate()        
        {
            try
            {
                if(left.ReturnType == WallyType.Measure)
                    return (measure)left.Evaluate() / (measure)right.Evaluate();
                return (double)left.Evaluate() / (double)right.Evaluate();
            }
            catch(DivideByZeroException)
            {
                throw new DivideByZeroException($"{this}");
            }
        }
    }
}