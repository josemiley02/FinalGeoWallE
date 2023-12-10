using System.Collections.Generic;

namespace Gsharp 
{
    public sealed class LetInStatement : IStatement , IExpression
    {
        public List<IStatement> Statements { get; private set; }
        public IExpression Body { get; private set; }
        private Scope localScope ;
        public WalleType ReturnType => Body.ReturnType;

        public LetInStatement (List<IStatement> statements , IExpression body)
        {
            this.Statements = statements;
            this.Body = body;
        }

        public void GetScope(Scope actual)
        {
            localScope = new Scope(actual);
            foreach (var item in Statements)
            {
                item.GetScope(localScope);
            }
            Body.GetScope(localScope);
        }
        public WalleType CheckSemantics()
        {
            foreach (var statement in Statements)
            {
                statement.CheckSemantics();
            }
            return Body.CheckSemantics();
        }

        public object Evaluate()
        {
            foreach (var item in Statements)
            {
                item.Execute();
            }
            return Body.Evaluate();
        }

        public void Execute() => Evaluate();

        public bool ConvertToBool() => (double)Evaluate() != 0 ;
        public override string ToString()
        {
            string output = "let ";
            foreach (var item in Statements)
            {
                output += item.ToString() + "; ";
            }
            return output + $"in {Body}";
        }
    }
}