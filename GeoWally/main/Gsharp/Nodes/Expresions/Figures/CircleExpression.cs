using System;
namespace Gsharp
{
    public sealed class CircleExpression : IExpression
    {
        string name;
        Circle circle;
        public WalleType ReturnType => WalleType.Circle;

        public CircleExpression(string name)
        {
            this.name = name;
        }
        public void GetScope(Scope actual){}
        
        public WalleType CheckSemantics() => WalleType.Void;

        public object Evaluate()
        {
            Random r = new Random();
            circle = new Circle(new Point(r.Next(0, 900), r.Next(0, 570), "center"), r.Next(50, 100), name);
            return circle;
        }
        public bool ConvertToBool() => true ;
    }
}