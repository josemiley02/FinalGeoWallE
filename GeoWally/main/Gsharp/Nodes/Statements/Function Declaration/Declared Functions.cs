using System.Collections.Generic;
using System.Linq;
namespace Gsharp
{
    public sealed class DeclaredFunctionExpression : ExecutableFunction
    {
        private List<string> parametersName;
        private IExpression Body;
        private Scope localScope;

        public override int Count => parametersName.Count;
        public override WalleType ReturnType => Body.ReturnType;

        public DeclaredFunctionExpression(string name, List<string> parameters, IExpression body) : base(name)
        {
            parametersName = parameters;
            Body = body;
        }
        public override void GetScope(Scope actual)
        {
            localScope = new Scope(actual);
            Body.GetScope(localScope);
            CompilatorTools.AddFunction(this);
        }
        public override WalleType CheckSemantics()
        {
            foreach (var parameter in parametersName)
            {
                localScope.CreateVariableInstance(parameter , WalleType.Undefined);
            }
            return Body.CheckSemantics();
        }
        
        public override void Execute(){}
        public override object Run(List<IExpression> arguments)
        {
            for (int i = 0; i < parametersName.Count; i++)
            {
                localScope.AssignVariable(parametersName.ElementAt(i), arguments.ElementAt(i).Evaluate() , arguments.ElementAt(i).ReturnType);
            }
            System.Console.WriteLine(Body.ReturnType);
            return Body.Evaluate();
        }
        public override void ResetScope()
        {
            foreach (var item in parametersName)
            {
                localScope.RemoveLast(item);
            }
        }
        public override string ToString()
        {
            string output = Name + "(";
            for (int i = 0; i < parametersName.Count; i++)
            {
                output += parametersName.ElementAt(i).ToString();
                if (i != parametersName.Count - 1) { output += ", "; }
            }
            return output + $") = {Body}";
        }
    }
}