
using System;
using System.Collections.Generic;

namespace Gsharp
{
    public sealed class SequenceLiteralExpression : IExpression
    {
        private Sequence<IExpression> sequence ;
        public WallyType ReturnType => WallyType.Sequence ;
        public WallyType itemsReturnType ;
        public SequenceLiteralExpression(Sequence<IExpression> sequence)
        {
            this.sequence = sequence;
            itemsReturnType = GetItemsReturnType();
        }
        public void GetScope(Scope actual)
        {
            if(sequence is RegularSequence<IExpression>)
            {
                foreach (var expression in sequence)
                    expression.GetScope(actual);
            }
        }
        public void CheckSemantics()
        {
            if(sequence is RegularSequence<IExpression>)
            {
                foreach(var expression in sequence)
                    expression.CheckSemantics();
            }
        }
        public object Evaluate() => sequence ;
        public IEnumerable<object> EvaluateElements()
        {
            foreach (var item in sequence)
            {
                yield return item.Evaluate(); 
            }
        }
        private WallyType GetItemsReturnType()
        {
            if(sequence is RangedSequence)
                return WallyType.Number;
            
            WallyType temp = WallyType.Undefined ;
            
            foreach (var expression in sequence)
            {
                if(temp != WallyType.Undefined)
                {
                    if(expression.ReturnType == WallyType.Undefined)
                        continue ;
                    if(expression.ReturnType != temp)
                        throw new ArgumentException("Return type of expressions in sequence must be same");
                }
                temp = expression.ReturnType ;    
            }
            return temp ;
        }
        public bool ConvertToBool()
        {
            return sequence.Count != 0 ;
        }
    }
    
}