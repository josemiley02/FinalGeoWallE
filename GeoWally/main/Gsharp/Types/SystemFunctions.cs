﻿using System.Collections.Generic;
using System.Linq;

namespace Gsharp
{
    public delegate object SystemFunction(List<IExpression> args);
    public static class SystemFunctionsPool
    {
        public static object Count(List<IExpression> expression)
        {
            var sequence = (SequenceLiteralExpression)expression.ElementAt(0);

            if (sequence.ReturnType == WallyType.Undefined)
                return new UndefinedExpression();

            int count = ((Sequence)sequence.Evaluate()).Count();

            if (count < 0)
                return new UndefinedExpression();

            return new NumberLiteralExpression(count);
        }
        public static object Print(List<IExpression> expression)
        {
            string output = expression.ElementAt(0).Evaluate().ToString();
            System.Console.WriteLine(output);
            return new TextLiteralExpression(output);
        }
        public static object Measure (List<IExpression> expression)
        {
            Point p1 = (Point)expression.ElementAt(0).Evaluate() ;
            Point p2 = (Point)expression.ElementAt(1).Evaluate() ;
            return new measure(p1.DistanceToPoint(p2));
        }

        public static object Line(List<IExpression> expression)
        {
            Point p1 = (Point) expression.ElementAt(0).Evaluate();
            Point p2 = (Point) expression.ElementAt(1).Evaluate();
            return new Line(p1,p2, "LineFromTo " + p1.Name + " " + p2.Name);
        }
        public static object Segment(List<IExpression> expression)
        {
            Point p1 = (Point) expression.ElementAt(0).Evaluate();
            Point p2 = (Point) expression.ElementAt(1).Evaluate();
            return new Segment(p1,p2, "SegmentFromTo " + p1.Name + " " + p2.Name);
        }
        public static object Ray(List<IExpression> expression)
        {
            Point p1 = (Point) expression.ElementAt(0).Evaluate();
            Point p2 = (Point) expression.ElementAt(1).Evaluate();
            return new Ray(p1,p2,"RayFromTo " + p1.Name + " " + p2.Name);
        }

        public static object Circle(List<IExpression> expression)
        {
            Point p = (Point) expression.ElementAt(0).Evaluate();
            measure r = (measure) expression.ElementAt(1).Evaluate();
            return new Circle(p,r.ToFloat() , "CircleCenter " + p.Name);
        }

        public static object Arc(List<IExpression>expressions)
        {
            Point p1 = (Point) expressions.ElementAt(0).Evaluate();
            Point p2 = (Point) expressions.ElementAt(1).Evaluate();
            Point p3 = (Point) expressions.ElementAt(2).Evaluate();
            measure m = (measure) expressions.ElementAt(3).Evaluate();

            return new Arc(p1,p2,p3,m.ToFloat(), "ArcCenter " + p1.Name);
        }
        public static object Intersect(List<IExpression> expressions)
        {
            IFigure fig1 = (IFigure) expressions.ElementAt(0).Evaluate();
            IFigure fig2 = (IFigure) expressions.ElementAt(1).Evaluate();
            return new Sequence(fig1.Intersect(fig2)) ;
        }
    }
}