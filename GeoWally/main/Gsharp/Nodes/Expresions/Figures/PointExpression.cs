using System;

namespace Gsharp
{
    public sealed class PointExpression : IExpression
    {
        Point point;
        string name;
        Scope referencedScope ;
        public WalleType ReturnType => WalleType.Point;
        float X;
        float Y;

        public PointExpression(string name, float X = float.PositiveInfinity, float Y = float.PositiveInfinity)
        {
            this.name = name;
            this.X = X;
            this.Y = Y;
        }
        public void GetScope(Scope actual) 
        { 
            referencedScope = new Scope(actual);
        }
        public WalleType CheckSemantics() => WalleType.Point;
        public object Evaluate()
        {
            Random r = new Random();
            if (X.Equals(float.PositiveInfinity) && Y.Equals(float.PositiveInfinity))
                point = new Point(r.Next(0, 900), r.Next(0, 570), name);
            else
                point = new Point(X, Y, name);
            return point;
        }
        public bool ConvertToBool() => true;
    }
}