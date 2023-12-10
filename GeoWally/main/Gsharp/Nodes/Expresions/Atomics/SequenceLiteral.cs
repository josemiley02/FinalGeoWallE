
using System;
using System.Collections.Generic;

namespace Gsharp
{
    public sealed class SequenceExpression : IExpression
    {
        public SequenceExpression(Sequence data)
        {
            sequence = data;
        }
        private Sequence sequence ; 
        public WalleType ReturnType => WalleType.Sequence ;

        public void GetScope(Scope actual)
        {
            if(!sequence.isInRange)
            {
                foreach (var item in sequence)
                {
                    item.GetScope(actual);
                }
            }
        }
        public WalleType CheckSemantics()
        {
            if(! sequence.isInRange)
            {
                foreach(var item in sequence)
                {
                    item.CheckSemantics();
                }
            }
            return WalleType.Sequence ;
        }
        public object Evaluate()
        {
            throw new NotImplementedException();
        }

        public bool ConvertToBool()
        {
            return sequence.Count() != 0 ;
        }
        public override string ToString()
        {
            return sequence.ToString();
        }
    }
}