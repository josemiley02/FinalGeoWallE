using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gsharp
{
    public class Ray : IFigure
    {
        public Point p1;
        public Point p2;
        public string Name { get; set; }

        public Ray(Point p1, Point p2, string name)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.Name = name;
        }
        public IEnumerable<Point> GetPoints(IFigure figures)
        {
            Line aux = new Line(p1, p2, "aux");
            IEnumerable<Point> points = aux.GetPoints(figures);
            (float, float) vector = GetVector();
            foreach (var item in points)
            {
                (float, float) vector2 = (item.Y - p1.Y, item.X - p1.X);
                if ((Math.Sign(vector.Item1) == Math.Sign(vector2.Item1)) || (Math.Sign(vector.Item2) == Math.Sign(vector2.Item2)))
                {
                    yield return item;
                }
            }
        }
        public float GetPendiente()
        {
            return (p2.Y - p1.Y) / (p2.X - p1.X);
        }
        public (float, float) GetVector()
        {
            return (p2.X - p1.X, p2.Y - p1.Y);
        }
        public FigureType GetFigureType()
        {
            return FigureType.Ray;
        }
        public float GetAngle()
        {
            float m = GetPendiente();
            float angle = (float)(Math.Atan(m) * 180 / Math.PI);
            if (m >= 0)
            {
                angle = p1.Y > p2.Y ? -180 + angle : angle;
            }
            else
            {
                angle = p1.Y > p2.Y ? angle : 180 + angle;
            }
            return angle;
        }

        public IFigure Traslate(int movX, int movY)
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
    }
    public class Segment : IFigure
    {
        public Point p1;
        public Point p2;
        public string Name { get; set; }
        public Segment(Point p1, Point p2, string name)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.Name = name;
        }
        public IEnumerable<Point> GetPoints(IFigure figures)
        {
            Line aux = new Line(p1, p2, "aux");
            IEnumerable<Point> points = aux.GetPoints(figures);
            foreach (var item in points)
            {
                if (item.X < Math.Max(p1.X, p2.X) && item.X > Math.Min(p1.X, p2.X)) yield return item;
            }
        }
        public float GetPendiente()
        {
            return (p2.Y - p1.Y) / (p2.X - p1.X);
        }
        public FigureType GetFigureType()
        {
            return FigureType.Segment;
        }

        public IFigure Traslate(int movX, int movY)
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
    }
}
