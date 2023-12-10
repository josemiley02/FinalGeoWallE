using System;
using System.Collections.Generic;
namespace Gsharp
{
    public class Scope
    {
        private Dictionary<string , Stack<object>> variableValues ;
        private Dictionary<string, WalleType> variableTypes;
        private readonly Scope? parentScope ;

        public Scope(Scope? parent = null)
        {
            variableValues = new Dictionary<string, Stack<object>>();
            variableTypes = new Dictionary<string, WalleType>();
            parentScope = parent;
        }

        public void CreateVariableInstance(string name , WalleType type)
        {
            if(variableTypes.ContainsKey(name))
                throw new InvalidOperationException($"Cannot re-define constant {name}");

            variableTypes[name] = type ;
            variableValues[name] = new Stack<object>(); 
        }

        public void AssignVariable(string name , object value) => variableValues[name].Push(value) ;
        public WalleType GetVariableType(string name)
        {
            if(variableTypes.ContainsKey(name))
                return variableTypes[name];
            
            else if(parentScope != null)
                return parentScope.GetVariableType(name);

            throw new NullReferenceException($"Use of undefined variable \"{name}\"");
        }
        public object GetVariableValue(string name)
        {
            if(variableValues.TryGetValue(name, out Stack<object>? valuesStack))
            {
                if(valuesStack.TryPeek(out object? value))
                {
                    return value ;
                }
                throw new InvalidOperationException($"Variable \"{name}\" has not been initialized");
            }
            else if(parentScope != null)
                return parentScope.GetVariableValue(name);
            
            throw new NullReferenceException($"Variable \"{name}\" has not been initialized");
        }
        public void RemoveLast(string name)
        {
                variableValues[name].Pop();
        }
    }      
}