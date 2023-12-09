using System;

namespace Gsharp
{
public abstract class UnaryOperatorExpression : IExpression
{
    protected IExpression Operand ;
    public WallyType ReturnType => WallyType.Number ;
    public string OperatorSymbol{get; private set;}
        
    public UnaryOperatorExpression(IExpression operand , string operatorSymbol)
    {
        Operand = operand;
        OperatorSymbol = operatorSymbol.ToString();
    }
    
    public void GetScope(Scope actual){ Operand.GetScope(actual);}
    public virtual void CheckSemantics() => Operand.CheckSemantics() ;
    public abstract object Evaluate();
    public abstract bool ConvertToBool();         
    public override string ToString() => $"{OperatorSymbol} {Operand}";
} 

public sealed class NegativeOperatorExpression : UnaryOperatorExpression
{
    public NegativeOperatorExpression(IExpression operand , string operatorSymbol) : base(operand , operatorSymbol){}

    public override void CheckSemantics()
    {
        base.CheckSemantics();
        if(Operand.ReturnType != WallyType.Number && Operand.ReturnType != WallyType.Measure && Operand.ReturnType != WallyType.Undefined)
            throw new InvalidOperationException($"Cannot apply {OperatorSymbol} to {Operand.ReturnType}"); 
    }
    public override object Evaluate()
    {
        if(Operand.ReturnType == WallyType.Measure)
            return -(measure)Operand.Evaluate();

        return -(double)Operand.Evaluate();
    }

    public override bool ConvertToBool() => (double)Evaluate() != 0 ;
}

public sealed class NotOperatorExpression : UnaryOperatorExpression
{
    public NotOperatorExpression(IExpression operand , string operatorSymbol) : base(operand , operatorSymbol){}

    public override object Evaluate()
    {
        if(! Operand.ConvertToBool())
            return 1.0 ;
        return 0.0 ;
    }
    public override bool ConvertToBool() => !Operand.ConvertToBool();
}
}
