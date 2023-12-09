using System.Collections.Generic;

namespace Gsharp
{
    public abstract class ExecutableFunction : IStatement
    {
        public string Name { get; private set; }
        public abstract int Count { get; }
        public abstract WallyType ReturnType { get; }

        public ExecutableFunction(string name)
        {
            Name = name;
        }
        public virtual void GetScope(Scope actual){}
        public virtual void CheckSemantics(){}
        public virtual void Execute(){}
        public virtual void ResetScope(){}
        public abstract object Run(List<IExpression> arguments);
        public bool Match(string name, int count)
        {
            return Name == name && Count == count;
        }
    }
}