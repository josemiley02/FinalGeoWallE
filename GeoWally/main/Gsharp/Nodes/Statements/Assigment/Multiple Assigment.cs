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
            valueExpression.GetScope(actual);
        }
        public override WalleType CheckSemantics()
        {
            WalleType returnType = valueExpression.CheckSemantics();

            if(valueExpression.ReturnType != WalleType.Sequence && valueExpression.ReturnType != WalleType.Undefined)
                throw new ArgumentException("MultiAssigment statement value must return a sequence or undefined");
            
            for(int i = 0 ; i < variableNames.Count ; i++)
            {
                if(variableNames[i] == "_") // se salta los comodines
                    continue ;

                if(returnType == WalleType.Undefined) // si el valor asignado devuelve undefined se les asigna undefined
                    referencedScope.CreateVariableInstance(variableNames[i] , WalleType.Undefined);
                
                else    // en estos casos el valor asignado devuelve una secuencia
                {
                    if(i == variableNames.Count - 1)  // si es la ultima variable su tipo es de secuencia
                        referencedScope.CreateVariableInstance(variableNames[i] , WalleType.Sequence);
                    
                    else // sino su tipo es el tipo de items que devuelva la secuencia
                    {
                        referencedScope.CreateVariableInstance(variableNames[i] , ((Sequence)valueExpression.Evaluate()).ItemsType);
                    }  
                }
            }
            return WalleType.Void ;          
        }
        public override void Execute()
        {
            if(valueExpression.ReturnType == WalleType.Undefined)
            {
                foreach (var item in variableNames)
                {
                    referencedScope.AssignVariable(item , 0 , WalleType.Undefined) ;
                }
            }

            Sequence values = (Sequence) valueExpression.Evaluate();
            var SequenceEnumerator = values.GetEnumerator();
            
            for(int i = 0 ; i < variableNames.Count ; i++)
            {
                if (SequenceEnumerator.MoveNext())
                {
                    if(variableNames[i] == "_")
                        continue ;

                    if(i == variableNames.Count - 1)
                        referencedScope.AssignVariable(variableNames.ElementAt(i), values.Rest(i) , WalleType.Sequence);
                    else
                        referencedScope.AssignVariable(variableNames.ElementAt(i), SequenceEnumerator.Current.Evaluate(), values.ItemsType);
                }
                else if(variableNames[i] != "_")
                    referencedScope.AssignVariable(variableNames.ElementAt(i), 0 , WalleType.Undefined);
            }
        }
    }
}