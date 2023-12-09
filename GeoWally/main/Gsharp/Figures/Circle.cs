using System;
using System.Collections.Generic;

namespace Gsharp
{
    public class Circle : IFigure
    {
        public Circle(Point center, float ratio, string name)
        {
            this.Center = center;
            this.Ratio = ratio;
            this.Name = name;
        }

        public Point Center { get; private set; }
        public float Ratio { get; private set; }
        public string Name { get; set; }

        public FigureType GetFigureType()
        {
            return FigureType.Circle;
        }

        public IEnumerable<Point> GetPoints(IFigure figures)
        {
            if (figures is Point)
            {
                Point P = (Point)figures;
                float suposs = ((P.X - Center.X) * (P.X - Center.X)) + ((P.Y - Center.Y) * (P.Y - Center.Y));
                if (suposs == Ratio * Ratio)
                {
                    yield return P;
                }
            }
            if (figures is Line || figures is Segment || figures is Ray)
            {
                foreach (var item in figures.GetPoints(this))
                {
                    yield return item;
                }
            }
            if (figures is Circle)
            {
                Circle other = (Circle)figures;
                double distanceCenters = Math.Sqrt(Math.Pow(Center.X - other.Center.X, 2) + Math.Pow(Center.Y - other.Center.Y, 2));
                if (distanceCenters > Ratio + other.Ratio || distanceCenters < Math.Abs(Ratio - other.Ratio)) { yield break; }

                double d = distanceCenters;
                double a = (Math.Pow(Ratio, 2) - Math.Pow(other.Ratio, 2) + Math.Pow(d, 2)) / (d * 2);
                double h = Math.Sqrt(Math.Pow(Ratio, 2) - Math.Pow(a, 2));

                double x3 = Center.X + a * (other.Center.X - Center.X) / d;
                double y3 = Center.Y + a * (other.Center.Y - Center.Y) / d;

                double x1 = x3 + h * (other.Center.Y - Center.Y) / d;
                double y1 = y3 - h * (other.Center.X - Center.X) / d;

                double x2 = x3 - h * (other.Center.Y - Center.Y) / d;
                double y2 = y3 + h * (other.Center.X - Center.X) / d;

                yield return new Point((float)x1, (float)y1, "IC1");
                yield return new Point((float)x2, (float)y2, "IC2");
            }
            if (figures is Arc)
            {
                Arc arc = (Arc)figures;
                IEnumerable<Point> points = arc.GetPoints(this);
                foreach (var item in points)
                {
                    yield return item;
                }
            }
            yield break;
        }

        public IEnumerable<IExpression> Intersect(IFigure figures)
        {
            foreach (var item in GetPoints(figures))
            {
                yield return new PointExpression(item.Name, item.X, item.Y);
            }
        }

        public IFigure Traslate(int movX, int movY)
        {
            Point newCenter = new Point(Center.X + movX, Center.Y + movY, Center.Name);
            this.Center = newCenter;
            return this;
        }
    }
}