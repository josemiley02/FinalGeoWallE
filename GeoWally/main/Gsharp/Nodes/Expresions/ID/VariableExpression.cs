using System;

namespace Gsharp 
{    
    public class VariableExpression : IExpression
    {
        public string Name;
        public Scope localScope;
        public WallyType ReturnType => (localScope != null) ? localScope.GetVariableType(Name) : WallyType.Undefined;

        public VariableExpression(Token id)
        {
            Name = id.Value;
        }
        
        public void GetScope(Scope actual)
        {
            localScope = actual;
        }
        public void CheckSemantics()
        {
            // el chequep semantico siempre se hace luego de GetScope por tanto la variable debe de estar definida en su scope
            if(localScope is null)
                throw new NullReferenceException($"Variable \"{this}\" does not exists in current context");
            
            localScope.GetVariableType(Name); // si la variable no ha sido definida da error
        }
        
        public object Evaluate()
        {
            System.Console.WriteLine(localScope.GetVariable(Name));
            return localScope.GetVariable(Name);
        }
        public bool ConvertToBool()
        {
            if(ReturnType == WallyType.Sequence)
            {
                var SequenceExp = new SequenceLiteralExpression((Sequence<IExpression>)Evaluate());
                return SequenceExp.ConvertToBool();
            }
            return (double)Evaluate() != 0 ;
        }
        public override string ToString() => Name ;
    }
}