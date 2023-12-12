using System;
namespace Gsharp
{
    public class LineExpression : IExpression
    {
        public Line line { get; private set; }
        public string name;
        public TokenType lineTypes;
        public WalleType ReturnType => WalleType.Line;

        public LineExpression(string name, TokenType lineTypes)
        {
            this.name = name;
            this.lineTypes = lineTypes;
        }
        public void GetScope(Scope actual){}
        public WalleType CheckSemantics() => WalleType.Line;

        public object Evaluate()
        {
            Random r = new Random();
            Point p1 = new Point (r.Next(0, 300), r.Next(0, 300), name + "p1");
            Point p2 = new Point (r.Next(0, 300), r.Next(0, 300), name + "p2");
            if(lineTypes == TokenType.LineKwToken)
            {
                return new Line(p1, p2, name);
            }
            else if(lineTypes == TokenType.RayKwToken)
            {
                return new Ray(p1, p2, name);
            }
            else if(lineTypes == TokenType.SegmentKwToken)
            {
                return new Segment(p1, p2, name);
            }
            throw new InvalidCastException();
        }
        public bool ConvertToBool() => true ;
    }
}