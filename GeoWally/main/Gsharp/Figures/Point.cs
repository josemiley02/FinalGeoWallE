using System.Collections.Generic;
using System;
namespace Gsharp
{
    public class Point : IFigure
    {
        public Point(float x, float y, string name)
        {
            X = x;
            Y = y;
            this.Name = name;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public string Name { get; set; }

        public FigureType GetFigureType()
        {
            return FigureType.Point;
        }

        public IEnumerable<IExpression> Intersect(IFigure figures)
        {
            return figures.Intersect(this);
        }

        public IFigure Traslate(int movX, int movY)
        {
            return new Point(this.X + movX, this.Y + movY, this.Name);
        }
        public IEnumerable<Point> GetPoints(IFigure figure) => new List<Point>(){this};
        internal double DistanceToPoint(Point p2)
        {
            return Math.Sqrt(Math.Pow(this.X - p2.X, 2) + Math.Pow(this.Y - p2.Y, 2));
        }
    }
}