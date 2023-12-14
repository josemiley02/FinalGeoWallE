using System;
using System.Collections.Generic;
namespace Gsharp
{
    public class Line : ILine
    {
        public Line(Point p1, Point p2, string name)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.Name = name;
        }

        public Point p1 { get; private set; }
        public Point p2 { get; private set; }
        public string Name { get; set; }

        public FigureType GetFigureType()
        {
            return FigureType.Line;
        }

        public virtual IFigure Traslate(int movX, int movY)
        {
            Point newP1 = new Point(p1.X + movX, p1.Y + movY, p1.Name);
            Point newP2 = new Point(p2.X + movX, p2.Y + movY, p2.Name);
            this.p1 = newP1;
            this.p2 = newP2;
            return this;
        }

        public IEnumerable<IExpression> Intersect(IFigure figures)
        {
            foreach (var item in GetPoints(figures))
            {
                yield return new PointExpression(item.Name, item.X, item.Y);
            }
        }
        public IEnumerable<Point> GetPoints(IFigure figures)
        {
            double m = GetPendiente();
            double n = p1.Y - m * p1.X;
            if (figures is Point)
            {
                Point p = (Point)figures;
                if (p.Y == m * p.X + n)
                {
                    yield return new Point(p.X,p.Y, "P");
                    yield break;
                }
            }
            if (figures is Line)
            {
                Line l = (Line)figures;
                double mL = l.GetPendiente();
                double nL = l.p1.Y - mL * l.p1.X;
                if (mL == m)
                {
                    while (nL == n)
                    {
                        yield return new Point(p1.X += 1, (float)(m * p1.X + n),"P");
                    }
                    yield break;
                }
                else
                {
                    float newX = (float)((nL - n) / (m - mL));
                    yield return new Point(newX, (float)(mL * newX + nL), "P");
                }
            }
            if(figures is Ray)
            {
                Ray r = (Ray)figures;
                Line rayLine = new Line(r.p1, r.p2, r.Name);
                IEnumerable<Point> pointsIntersect = GetPoints(rayLine);
                (float, float) vector = r.GetVector();
                foreach (var item in pointsIntersect)
                {
                    (float, float) vector2 = (item.Y - r.p1.Y, item.X - r.p1.X);
                    if((Math.Sign(vector.Item1) == Math.Sign(vector2.Item1)) || (Math.Sign(vector.Item2) == Math.Sign(vector2.Item2)))
                    {
                        yield return item;
                    }
                }
                yield break;
            }
            if(figures is Segment)
            {
                Segment s = (Segment)figures;
                Line segmentLine = new Line(s.p1, s.p2, s.Name);
                IEnumerable<Point> pointsIntersect = GetPoints(segmentLine);
                foreach(var item in pointsIntersect)
                {
                    if (item.X < Math.Max(s.p1.X, s.p2.X) && item.X > Math.Min(s.p1.X, s.p2.X)) yield return item;
                }
                yield break;
            }
            if (figures is Circle)
            {
                Circle c = (Circle)figures;
                double p = n - c.Center.Y;
                double A = m * m + 1;
                double B = (2 * p * m) - 2 * c.Center.X;
                double C = (c.Center.X * c.Center.X) + (p * p) - (c.Ratio * c.Ratio);

                double D = (B * B) - (4 * A * C);
                if(D > 0)
                {
                    double x1 = (-B + Math.Sqrt(D)) / (2 * A);
                    double x2 = (-B - Math.Sqrt(D)) / (2 * A);
                    double y1 = m * x1 + n;
                    double y2 = m * x2 + n;
                    yield return new Point((float)x1, (float)y1, "IC1");
                    yield return new Point((float)x2, (float)y2, "IC2");
                }
                else if(D < 0)
                {
                    yield break;
                }
                else
                {
                    float X = (float)(-B / (A * 2));
                    yield return new Point(X, (float)(m * X + n), "IC1");
                }
            }
            if(figures is Arc)
            {
                Arc arc = (Arc)figures;
                IEnumerable <Point> points = arc.GetPoints(this);
                foreach(var item in points)
                {
                    yield return item;
                }
            }
            yield break;
        }
        public virtual float GetPendiente()
        {
            return (p2.Y - p1.Y) / (p2.X - p1.X);
        }
    }
}