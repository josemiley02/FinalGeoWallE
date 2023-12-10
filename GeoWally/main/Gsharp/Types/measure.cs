using System;

namespace Gsharp
{
    public sealed class Measure : IComparable<Measure>
    {
        public double Value {get ; private set;}
        public Measure(double value)
        {
            Value = value;
        }

        public int CompareTo(Measure? other)
        {
            if (other == null)
                throw new ArgumentNullException("Object instance not set to any value ");
            return Value.CompareTo(other.Value);
        }

        public static Measure operator +(Measure a) => new Measure (a.Value);
        public static Measure operator -(Measure a) => new Measure (-a.Value);
        public static Measure operator +(Measure a , Measure b) => new Measure (a.Value + b.Value);
        public static Measure operator -(Measure a, Measure b) => new Measure(Math.Abs(a.Value - b.Value));

        public static Measure operator *(Measure a, double scalar)=> new Measure(a.Value * Math.Truncate(Math.Abs(scalar)));
        public static double  operator /(Measure a, Measure b) => (b.Value != 0)? a.Value / b.Value : throw new ArgumentException("Zero Division Error");
    
        public double ToDouble() => Value ;
        public float ToFloat() => (float)Value ;
        public override string ToString()
        {
            return Value.ToString();
        }
    } 

}
