using System;

namespace ContinuationMonad
{
    public delegate TAnswer K<T, TAnswer>(Func<T, TAnswer> k);

    public static class ContinuationExtensions
    {
        public static K<T, TAnswer> ToContinuation<T, TAnswer>(this T value)
        {
            return c => c(value);
        }

        private static K<TU, TAnswer> SelectMany<T, TU, TAnswer>(this K<T, TAnswer> m, Func<T, K<TU, TAnswer>> k)
        {
            return c => m(x => k(x)(c));
        }

        public static K<TV, TAnswer> SelectMany<T, TU, TV, TAnswer>(this K<T, TAnswer> m, Func<T, K<TU, TAnswer>> k, Func<T, TU, TV> s)
        {
            return m.SelectMany(x => k(x).SelectMany(y => s(x, y).ToContinuation<TV, TAnswer>()));
        }
    }
}
