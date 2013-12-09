using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Fibonacci
{
    public class FibonacciObservable
    {
        private class State
        {
            public ulong Value { get; set; }
            public ulong Accumulator { get; set; }
            public uint CurrentItemNumber { get; set; }
        }

        public Func<ushort, IObservable<ulong>> Get(IScheduler scheduler)
        {
            return nthItem => Observable.Create<ulong>(observer =>
            {
                var state = new State { Value = 0, Accumulator = 1, CurrentItemNumber = 1 };

                return scheduler.Schedule(state, (currentState, self) =>
                {
                    if (currentState.CurrentItemNumber == nthItem)
                    {
                        observer.OnNext(currentState.Accumulator);
                        observer.OnCompleted();
                    }
                    else
                    {
                        currentState.Accumulator = currentState.Accumulator + currentState.Value;
                        currentState.Value = currentState.Accumulator - currentState.Value;
                        currentState.CurrentItemNumber += 1;
                        self(currentState);
                    }
                });
            });
        }
    }
}
