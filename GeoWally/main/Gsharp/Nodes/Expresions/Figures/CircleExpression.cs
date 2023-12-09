using System;
namespace Gsharp
{
    public sealed class CircleExpression : IExpression
    {
        string name;
        Circle circle;
        public WallyType ReturnType => WallyType.Circle;

        public CircleExpression(string name)
        {
            this.name = name;
        }
        public void GetScope(Scope Actual){}

        public void CheckSemantics(){}

        public object Evaluate()
        {
            Random r = new Random();
            circle = new Circle(new Point(r.Next(0, 900), r.Next(0, 570), "center"), r.Next(50, 100), name);
            return circle;
        }
        public bool ConvertToBool() => true ;
    }
}