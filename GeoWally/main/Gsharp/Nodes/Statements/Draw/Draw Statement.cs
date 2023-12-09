using System;
using System.Collections.Generic;

namespace Gsharp
{
    public sealed class DrawStatement : IStatement, IExpression
    {
        private readonly IExpression parameter;
        private readonly List<IFigure> figures = new List<IFigure>();

        public event Action<IFigure> DrawThis;

        public WallyType ReturnType => WallyType.Void;

        public DrawStatement(IExpression argument)
        {
            parameter = argument;
        }

        public void GetScope(Scope actual)
        {
            parameter.GetScope(actual);
        }
        public void CheckSemantics()
        {
            parameter.CheckSemantics();
            if (!CompilatorTools.IsFigure(parameter))
                throw new ArgumentException("Draw expression parameter must be a figure");
        }
        public void Execute()
        {
            if (parameter.ReturnType == WallyType.Sequence)
            {
                Sequence<IExpression> sequence = (Sequence<IExpression>)parameter.Evaluate();

                foreach (IExpression element in sequence)
                {
                    try
                    {
                        DrawThis.Invoke((IFigure)element.Evaluate());
                    }
                    catch { };
                }
            }
            if(parameter.ReturnType == WallyType.Sequence)
            {
                Sequence<IExpression> sequence = (Sequence<IExpression>)parameter.Evaluate();
                
                foreach(IExpression element in sequence)
                {
                    try{
                        DrawThis.Invoke((IFigure)element.Evaluate());
                    }
                    catch{};
                }
            }
            else
            {
                figures.Add((IFigure)parameter.Evaluate());

                foreach (var item in figures)
                {
                    DrawThis.Invoke(item);
                }
            }
        }
        public object Evaluate()
        {
            Execute();
            return new object();
        }
        public bool ConvertToBool() => true;
        public override string ToString() => $"draw {parameter}";
    }
}