using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gsharp
{
    public sealed class Sequence : IEnumerable<IExpression>
    {
        public Sequence(IEnumerable<IExpression> data)
        {
            this.data = data;
            isInRange = false ;
        }
        public Sequence(int min , int max)
        {
            this.min = min ;
            this.max = max ;
            isInRange = true ;
            data = new List<IExpression>();
        }
        public bool isInRange { get; }
        private readonly int min ;
        private readonly int max ;
        public WalleType ItemsType
        {
            get{
                if(isInRange)
                    return WalleType.Number ;
                
                return GetItemsType();
            }
        }

        private WalleType GetItemsType()
        {
            var classEnumerator = GetEnumerator() ;

            if(! classEnumerator.MoveNext())
                return WalleType.Undefined ;
            
            WalleType returnType = classEnumerator.Current.ReturnType ;
            foreach (var item in data)
            {
                if(item.ReturnType != returnType && item.ReturnType != WalleType.Undefined)
                    throw new ArgumentException($"Sequence's elements must have the same return type : {item}");
            }
            return returnType ;
        }

        private readonly IEnumerable<IExpression> data ;
        
        public int Count()
        {
            if(isInRange)
                return (max == int.MaxValue)? -1 : max - min ;
            return data.Count() ;
        }

        public IEnumerator<IExpression> GetEnumerator()
        {
            if(isInRange)
                return new InRangeEnumerator(min , max);
            return data.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public static Sequence operator +(Sequence a , Sequence b)
        {
            return new Sequence(Concatenate(a,b));
        }

        public static Sequence operator +(Sequence a , Undefined b)
        {
            return a;
        }
        public static IEnumerable<IExpression> Concatenate(Sequence a, Sequence b)
        {
            foreach (var itemA in a)
            {
                yield return itemA ;
            }
            foreach (var itemB in b)
            {
                yield return itemB ;
            }
        }
        public Sequence Rest(int position)
        {
            if(isInRange)
                return new Sequence(position + 1 , max);
            return new Sequence(RestAsEnumerable(position));
        }

        private IEnumerable<IExpression> RestAsEnumerable(int position)
        {
            int i = 0 ;
            foreach (var item in data)
            {
                if(i > position)
                    yield return item ;
                i ++ ;
            }
        }
    }

    public class Undefined
    {
        
        public static Undefined operator +(Undefined a , object b)
        {
            return new Undefined();
        }
    }

    internal class InRangeEnumerator : IEnumerator<IExpression>
    {
        public InRangeEnumerator(int min, int max)
        {
            Min = min;
            Max = max;
            Reset();
        }
        public int Min { get; }
        public int Max { get; }
        private int num ;
        private bool isCurrent ;

        public IExpression Current
        {
            get
            {
                if(isCurrent) return new NumberLiteralExpression(num);
                throw new InvalidOperationException("SequenceInRange enumerator out of position");
            }
            
        }

        object IEnumerator.Current => Current;

        public void Dispose(){}

        public bool MoveNext()
        {
            num ++ ;
            isCurrent = num < Max;
            return isCurrent ;
        }

        public void Reset()
        {
            num = Min - 1 ;
        }
    }
}