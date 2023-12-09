using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gsharp
{
    public abstract class Sequence<T> : IEnumerable<T>
    {
        public abstract int Count {get;}
        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public static IEnumerable<T> Concatenate(Sequence<T> sequence1 , Sequence<T> sequence2)
        {
            foreach(var item in sequence1)
                yield return item ;

            foreach (var item in sequence2)
            {
                yield return item ;
            }
        }

        public abstract Sequence<T> Rest(int i);
    }
    public sealed class RegularSequence<T> : Sequence<T>
    {
        private IEnumerable<T> data ;
        public override int Count => data.Count();
        public RegularSequence(IEnumerable<T> values)
        {
            data = values;
        }
        public override IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public static RegularSequence<T> operator + (RegularSequence<T> sequence1, Sequence<T>sequence2)
        {
            return new RegularSequence<T>(Concatenate(sequence1 , sequence2));
        }
        public override Sequence<T> Rest(int position)
        {
            return new RegularSequence<T>(RestEnumerable(position));
        }
        public IEnumerable<T> RestEnumerable(int position)
        {
            int i = 0 ;
            foreach (var item in data)
            {
                if( i >= position)
                    yield return item ;
                i++ ;
            }
        }
        public override string ToString()
        {
            string output = "{" ;
            foreach (var item in data)
            {
                output += item.ToString() + "," ;
            }
            return output + "}" ;
        }
    }

    public sealed class RangedSequence : Sequence<IExpression>
    {
        public int Min { get ; private set  ;} 
        public int Max { get ; private set ;}
        public override int Count => (Max == int.MaxValue)? -1 : Max - Min ;

        public RangedSequence(int min, int max = int.MaxValue)
        {
            Min = min ;
            Max = max ;
        }
        public override IEnumerator<IExpression> GetEnumerator()
        {
            for(int k = Min ; k <= Max ; k++) yield return new NumberLiteralExpression(k) ;
        }
        public static RegularSequence<NumberLiteralExpression> operator + (RangedSequence sequence1, Sequence<IExpression> sequence2)
        {
            return new RegularSequence<NumberLiteralExpression>((IEnumerable<NumberLiteralExpression>)Concatenate(sequence1 , sequence2));
        }
        public override Sequence<IExpression> Rest(int position)
        {
            return new RangedSequence(position + 1 , Max);
        }

        public override string ToString()
        {
            string output = $"{{ {Min} ... " ;
            
            if(Max != int.MaxValue){output += Max.ToString();}

            return output + " }" ;
        }

    }
}