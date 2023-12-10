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
        }
        public override WalleType CheckSemantics() => WalleType.Undefined ; 
        
        public override void Execute() => CompilatorTools.AddFunction(this);
        public override object Run(List<IExpression> arguments)
        {
            for (int i = 0; i < parametersName.Count; i++)
            {
                var valueTypePair = (arguments.ElementAt(i).Evaluate(), arguments.ElementAt(i).ReturnType);
                localScope.AssignVariable(parametersName.ElementAt(i), valueTypePair);
            }
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