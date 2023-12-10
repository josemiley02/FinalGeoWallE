using System;

namespace Gsharp 
{
    public sealed class ConditionalExpression: IExpression, IStatement
        {
        private IExpression condition ;
        private IExpression trueBody ;
        private IExpression falseBody ;
        public WalleType ReturnType => WalleType.Number;

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
            
            if(trueBody.CheckSemantics() != falseBody.CheckSemantics())
                throw new InvalidOperationException("Then-Else branches of conditional statement must have the same return type : {this}");

            return trueBody.ReturnType ;
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