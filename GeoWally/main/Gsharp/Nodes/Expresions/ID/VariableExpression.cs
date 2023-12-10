using System;

namespace Gsharp 
{    
    public sealed class VariableExpression : IExpression
    {
        public string Name;
        private Scope referencedScope;
        public WalleType ReturnType => (referencedScope != null) ? referencedScope.GetVariableType(Name) : WalleType.Undefined;

        public VariableExpression(Token id)
        {
            Name = id.Value;
        }
        
        public void GetScope(Scope actual)
        {
            referencedScope = actual;
        }
        public WalleType CheckSemantics()
        {
            return referencedScope.GetVariableType(Name);
        }
        
        public object Evaluate()
        {
            return referencedScope.GetVariableValue(Name);
        }
        public bool ConvertToBool()
        {
            if(ReturnType == WalleType.Sequence)
            {
                var SequenceExp = new SequenceExpression((Sequence)Evaluate());
                return SequenceExp.ConvertToBool();
            }
            return (double)Evaluate() != 0 ;
        }
        public override string ToString() => Name ;
    }
}