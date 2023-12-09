using System;
using System.Collections.Generic;
namespace Gsharp
{
    class Arc : IFigure
    {
        public Point origin;
        public Point first;
        public Point second;
        public float measure;

        public string Name { get; set; }

        public Arc(Point origin, Point extenceLeft, Point extenceRight, float measure, string name)
        {
            this.origin = origin;
            this.first = extenceLeft;
            this.second = extenceRight;
            this.measure = measure;
            Name = name;
        }

        public IFigure Traslate(int X, int Y)
        {
            this.first.X += X;
            this.origin.X += X;
            this.second.X += X;
            this.first.Y += Y;
            this.origin.Y += Y;
            this.second.Y += Y;
            return this;
        }

        public FigureType GetFigureType()
        {
            return FigureType.Arc;
        }

        public IEnumerable<Point> GetPoints(IFigure figures)
        {
            Circle aux = new Circle(origin, measure, Name);
            IEnumerable<Point> pointsIntersec = aux.GetPoints(figures);
            Ray start = new Ray(origin, first, "Start");
            Ray end = new Ray(origin, second, "End");
            float startAngle = start.GetAngle();
            float endAngle = end.GetAngle();
            startAngle = startAngle < 0 ? 360 + startAngle : startAngle;
            endAngle = endAngle < 0 ? 360 + endAngle : endAngle;

            foreach(var item in pointsIntersec)
            {
                Ray current = new Ray(origin, item, "Current");
                float currentAngle = current.GetAngle();
                currentAngle = currentAngle < 0 ? 360 + currentAngle : currentAngle;

                if(startAngle < endAngle && currentAngle > startAngle && currentAngle < endAngle) { yield return item; }
                else if(currentAngle < endAngle || currentAngle > startAngle) { yield return item; }
            }
        }

        public IEnumerable<IExpression> Intersect(IFigure figures)
        {
            foreach (var item in GetPoints(figures))
            {
                yield return new PointExpression(item.Name, item.X, item.Y);
            }
        }
    }
}
