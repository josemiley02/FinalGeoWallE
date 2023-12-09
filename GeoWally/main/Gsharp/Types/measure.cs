using System;

namespace Gsharp
{
    public sealed class measure : IComparable<measure>
    {
        public double Value {get ; private set;}
        public measure(double value)
        {
            Value = value;
        }

        public int CompareTo(measure other)
        {
            if (other == null)
                throw new ArgumentNullException("Object instance not set to any value ");
            return Value.CompareTo(other.Value);
        }

        public static measure operator +(measure a) => new measure (a.Value);
        public static measure operator -(measure a) => new measure (-a.Value);
        public static measure operator +(measure a , measure b) => new measure (a.Value + b.Value);
        public static measure operator -(measure a, measure b) => new measure(Math.Abs(a.Value - b.Value));

        public static measure operator *(measure a, double scalar)=> new measure(a.Value * Math.Truncate(Math.Abs(scalar)));
        public static double  operator /(measure a, measure b) => (b.Value != 0)? a.Value / b.Value : throw new ArgumentException("Zero Division Error");
    
        public double ToDouble() => Value ;
        public float ToFloat() => (float)Value ;
        public override string ToString()
        {
            return Value.ToString();
        }
    } 

}
