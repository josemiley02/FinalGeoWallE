using System;

namespace Gsharp
{
public abstract class UnaryOperatorExpression : IExpression
{
    protected IExpression Operand ;
    public WalleType ReturnType => WalleType.Number ;
    public string OperatorSymbol{get; private set;}
        
    public UnaryOperatorExpression(IExpression operand , string operatorSymbol)
    {
        Operand = operand;
        OperatorSymbol = operatorSymbol.ToString();
    }
    
    public void GetScope(Scope actual){ Operand.GetScope(actual);}
    public abstract WalleType CheckSemantics(); 
    public abstract object Evaluate();
    public abstract bool ConvertToBool();         
    public override string ToString() => $"{OperatorSymbol} {Operand}";
} 

public sealed class NegativeOperatorExpression : UnaryOperatorExpression
{
    public NegativeOperatorExpression(IExpression operand , string operatorSymbol) : base(operand , operatorSymbol){}

    public override WalleType CheckSemantics()
    {
        WalleType operandType = Operand.CheckSemantics();   

        if(operandType != WalleType.Number && operandType != WalleType.Measure && operandType != WalleType.Undefined)
            throw new InvalidOperationException($"Cannot apply {OperatorSymbol} to {operandType} type."); 
        return WalleType.Number ;
    }
    public override object Evaluate()
    {
        if(Operand.ReturnType == WalleType.Measure)
            return -(Measure)Operand.Evaluate();

        return -(double)Operand.Evaluate();
    }

    public override bool ConvertToBool() => (double)Evaluate() != 0 ;
}

public sealed class NotOperatorExpression : UnaryOperatorExpression
{
    public NotOperatorExpression(IExpression operand , string operatorSymbol) : base(operand , operatorSymbol){}
        public override WalleType CheckSemantics() => WalleType.Number;
        public override object Evaluate()
    {
        if(! Operand.ConvertToBool())
            return 1.0 ;
        return 0.0 ;
    }
    public override bool ConvertToBool() => !Operand.ConvertToBool();
}
}
