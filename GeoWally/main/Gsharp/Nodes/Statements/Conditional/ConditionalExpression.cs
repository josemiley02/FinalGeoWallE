using System;

namespace Gsharp 
{
    public sealed class ConditionalExpression: IExpression, IStatement
        {
        private IExpression condition ;
        private IExpression trueBody ;
        private IExpression falseBody ;
        public WalleType ReturnType
        {
            get
            {
                try
                {
                    return trueBody.ReturnType ;
                }
                catch
                {
                    return WalleType.Undefined;
                }
            }
            private set{}
        }

        public ConditionalExpression(IExpression condition, IExpression trueBody, IExpression falseBody)
        {
            this.condition = condition;
            this.trueBody = trueBody;
            this.falseBody = falseBody;
        }

        public void GetScope(Scope actual)
        {
            condition.GetScope(actual);
            trueBody.GetScope(actual);
            falseBody.GetScope(actual);           
        }

        public WalleType CheckSemantics()
        {
            condition.CheckSemantics();
            var falseReturnType = falseBody.CheckSemantics(); 
            var trueReturnType = trueBody.CheckSemantics() ;

            if( trueReturnType != falseReturnType && falseReturnType != WalleType.Undefined && trueReturnType != WalleType.Undefined)
                throw new InvalidOperationException("Then-Else branches of conditional statement must have the same return type : {this}");

            ReturnType = trueBody.ReturnType ;
            return ReturnType ;
        }
        public object Evaluate()
        {
            if(condition.ConvertToBool())
                return trueBody.Evaluate();

            return falseBody.Evaluate();
        }
        public void Execute() => Evaluate();
        public bool ConvertToBool() => (double)Evaluate() != 0 ;
        public override string ToString() => $"if({condition}) then {trueBody} else {falseBody}" ;
    } 
}