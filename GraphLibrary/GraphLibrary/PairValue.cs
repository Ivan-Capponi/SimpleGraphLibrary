using System;

namespace GraphLibrary
{
    public class PairValue <T> : IPairValue<T>
    {
        private readonly T _t1;
        private readonly T _t2;
        public PairValue(T t1, T t2)
        {
            if (t1 == null || t2 == null)
                throw new ArgumentNullException();
            if (t1.GetType() != t2.GetType())
                throw new ArgumentException();
            _t1 = t1;
            _t2 = t2;
        }

        public T GetFirst()
        {
            return _t1;
        }

        public T GetSecond()
        {
            return _t2;
        }

        public bool Contains(T value)
        {
            return _t1.Equals(value) || _t2.Equals(value);
        }

        public override bool Equals(object o)
        {
            if (o == null || o.GetType() != typeof(PairValue<T>))
                return false;
            PairValue<T> casted = (PairValue<T>) o;
            return casted._t1.Equals(_t1) && casted._t2.Equals(_t2);
        }

        public override int GetHashCode()
        {
            return _t1.GetHashCode() + _t2.GetHashCode();
        }
    }
}
