using System;

namespace Gsharp 
{
    public abstract class ArithmeticExpression : BinaryOperatorExpression
    {
        public ArithmeticExpression(IExpression left, IExpression right , string operatorSymbol) : base(left, right , operatorSymbol){}
        
        protected override bool SemanticCondition(WalleType leftType , WalleType rightType , out WalleType returnType)
        { 
            if(leftType == WalleType.Undefined)
            {
                returnType = rightType ;
                return true;
            }
            else if(rightType == WalleType.Undefined)
            {
                returnType = leftType ;
                return true;
            }
            
            else if(leftType == WalleType.Number && rightType == WalleType.Number)
            {
                returnType = WalleType.Number;
                return true ;
            }
            returnType = WalleType.Undefined ;        
            return false ;
        }
    }

    public sealed class SumExpression : ArithmeticExpression
    {
        public SumExpression(IExpression left, IExpression right, string operatorSymbol) : base(left , right , operatorSymbol){}
        protected override bool SemanticCondition(WalleType leftType , WalleType rightType , out WalleType returnType)
        {
            if(base.SemanticCondition(leftType , rightType , out returnType))
                return true ;
        
            else if(leftType == WalleType.Measure && rightType == WalleType.Measure)
            {
                returnType = WalleType.Measure ;
                return true;
            }

            else if(leftType == WalleType.Sequence && rightType == WalleType.Sequence)
            {
                returnType = WalleType.Sequence ;    
                return true ;
            }
            
            return false ;
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
        public RestExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right , operatorSymbol){}
        protected override bool SemanticCondition(WalleType leftType , WalleType rightType , out WalleType returnType)
        {
            if(base.SemanticCondition(leftType, rightType , out returnType))
                return true ;
            
            else if(leftType == WalleType.Measure && rightType == WalleType.Measure)
            {
                returnType = WalleType.Measure ;
                return true ;
            }
            return false ;
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
        public MultiplicativeExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right , operatorSymbol){}

        protected override bool SemanticCondition(WalleType leftType , WalleType rightType , out WalleType returnType)
        {
            if(base.SemanticCondition(leftType, rightType, out returnType))
                return true ;
            
            else if((leftType == WalleType.Number && rightType == WalleType.Measure) || (leftType == WalleType.Measure && rightType == WalleType.Number))
            {
                returnType = WalleType.Measure ;
                return true;
            }
            
            return false ;
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
        public DivisionExpression(IExpression left, IExpression right , string operatorSymbol) : base(left , right, operatorSymbol){}
        protected override bool SemanticCondition(WalleType leftType , WalleType rightType , out WalleType returnType)
        {
            if(base.SemanticCondition(leftType,rightType,out returnType))
                return true ;
            else if(leftType == WalleType.Measure && rightType == WalleType.Measure)
            {
                returnType = WalleType.Measure ;
                return true; 
            }
            return false ;
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