namespace Gsharp
{
    public abstract class LiteralExpression : IExpression
    {
        protected object Value { get; }
        public abstract WallyType ReturnType { get; }
        public LiteralExpression(object value)
        {
            Value = value;
        }
        public void GetScope(Scope Actual){}
        public void CheckSemantics(){}
        public virtual object Evaluate() => Value ; 
        public virtual bool ConvertToBool() => true ; 

        
    }
}