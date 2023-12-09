using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsharp
{
    public sealed class MultiAssigmentStatement : AssigmentStatement
    {
        private readonly List<string> variableNames;
        public MultiAssigmentStatement(List<string> variables, IExpression value) : base(value)
        {
            variableNames = variables;
        }
        public override void GetScope(Scope actual)
        {
            referencedScope = actual;
            foreach (string variable in variableNames)
            {
                if(variable != "_")
                    referencedScope.CreateVariableInstance(variable, valueExpression.ReturnType);
            }
            valueExpression.GetScope(actual);
        }
        public override void CheckSemantics()
        {
            valueExpression.CheckSemantics();
            if(valueExpression.ReturnType != WallyType.Sequence && valueExpression.ReturnType != WallyType.Undefined)
                throw new ArgumentException("MultiAssigment statement value must return a sequence or undefined");
        }
        public override void Execute()
        {
            if(valueExpression.ReturnType == WallyType.Undefined)
            {
                foreach (var item in variableNames)
                {
                    referencedScope.AssignVariable(item , 0) ;
                }
            }

            Sequence values = (Sequence)valueExpression.Evaluate();
            var SequenceEnumerator = values.GetEnumerator();
            
            for(int i = 0 ; i < variableNames.Count ; i++)
            {
                if (SequenceEnumerator.MoveNext())
                {
                    if(variableNames[i] == "_")
                        continue ;

                    if(i == variableNames.Count - 1)
                        referencedScope.AssignVariable(variableNames.ElementAt(i), values.Rest(i));
                    else
                        referencedScope.AssignVariable(variableNames.ElementAt(i), SequenceEnumerator.Current.Evaluate());
                }
                else
                    referencedScope.AssignVariable(variableNames.ElementAt(i), 0);
            }
        }
    }
}