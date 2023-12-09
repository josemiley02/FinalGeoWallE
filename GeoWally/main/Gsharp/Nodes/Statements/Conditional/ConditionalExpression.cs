using System;

namespace Gsharp 
{
    public sealed class ConditionalExpression: IExpression, IStatement
        {
        private IExpression condition ;
        private IExpression trueBody ;
        private IExpression falseBody ;
        public WallyType ReturnType => WallyType.Number;

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

        public void CheckSemantics()
        {
            condition.CheckSemantics();
            trueBody.CheckSemantics();
            falseBody.CheckSemantics();

            if(trueBody.ReturnType != falseBody.ReturnType)
                throw new InvalidOperationException("Then-Else branches of conditional statement must have the same return type : {this}");
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