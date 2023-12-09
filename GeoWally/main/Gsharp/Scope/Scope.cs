using System;
using System.Collections.Generic;

namespace Gsharp
{
    public class Scope
    {
        private Dictionary<string , Stack<object>> variableValues ;
        private Dictionary<string, WallyType> variableTypes;
        private readonly Scope parentScope ;

        public Scope(Scope parent = null)
        {
            variableValues = new Dictionary<string, Stack<object>>();
            variableTypes = new Dictionary<string, WallyType>();
            parentScope = parent;
        }

        public void CreateVariableInstance(string name , WallyType type)
        {
            if(variableTypes.ContainsKey(name))
                throw new InvalidOperationException($"Cannot re-define constant {name}");
            variableTypes[name] = type ; 
        }

        public void AssignVariable(string name , object value)
        {   
            if(! variableValues.ContainsKey(name))
                variableValues[name] = new Stack<object>();

            variableValues[name].Push(value) ;
        }
        public object GetVariable(string name)
        {
            if(variableValues.ContainsKey(name))
                return variableValues[name].Peek();

            else if(parentScope != null)
                return parentScope.GetVariable(name);
            
            throw new NullReferenceException($"Variable \"{name}\" has not been initialized");
        }
        public WallyType GetVariableType(string name)
        {
            if(variableTypes.ContainsKey(name))
                return variableTypes[name];
            
            else if(parentScope != null)
                return parentScope.GetVariableType(name);

            throw new NullReferenceException($"Use of undefined variable \"{name}\"");
        }

        public void RemoveLast(string name)
        {
            if(variableValues.ContainsKey(name) && variableValues[name].Count > 0)
            {
                variableValues[name].Pop();
            }
        }
    }      
}