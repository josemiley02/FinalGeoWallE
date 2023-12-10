namespace Gsharp
{
    public abstract class LiteralExpression : IExpression
    {
        protected object Value { get; }
        public abstract WalleType ReturnType { get; }
        public LiteralExpression(object value)
        {
            Value = value;
        }
        public void GetScope(Scope Actual){}
        public WalleType CheckSemantics()
        {
            return ReturnType ;
        }
        public virtual object Evaluate() => Value ; 
        public virtual bool ConvertToBool() => true ; 

        
    }
}