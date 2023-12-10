namespace Gsharp
{
    public abstract class AssigmentStatement : IStatement
    {
        protected IExpression valueExpression;
        protected Scope referencedScope;

        public AssigmentStatement(IExpression expression)
        {
            valueExpression = expression;
        }
        public virtual void GetScope(Scope actual)
        {
            referencedScope = actual;
            valueExpression.GetScope(actual);
        }
        public abstract WalleType CheckSemantics();

        public abstract void Execute();
    }
    public sealed class SimpleAssigmentStatement : AssigmentStatement
    {
        private string variableName;

        public SimpleAssigmentStatement(string target, IExpression value) : base(value)
        {
            this.variableName = target;
        }
        public override WalleType CheckSemantics()
        {
            referencedScope.CreateVariableInstance( variableName , valueExpression.CheckSemantics() );
            return WalleType.Void ;
        }
        public override void Execute()
        {
            referencedScope.AssignVariable(variableName, valueExpression.Evaluate());
        }
        public override string ToString() => $"{variableName} = {valueExpression}" ;
    }
}