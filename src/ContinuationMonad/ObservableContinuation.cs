using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace ContinuationMonad
{
    public static class ObservableContinuation
    {
        public static IObservable<T> ToObservable<T>(this T value, IScheduler scheduler)
        {
            return Observable.Return(value, scheduler);
        }
    }
}
