using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Factorials
{
    public class FactorialObservable
    {
        private class State
        {
            public ushort Value { get; set; }
            public ulong Accumulator { get; set; }
        }

        public Func<ushort, IObservable<ulong>> Get(IScheduler scheduler)
        {
            return value => Observable.Create<ulong>(observer =>
            {
                var state = new State {Value = value, Accumulator = 1};

                return scheduler.Schedule(state, (currentState, self) =>
                {
                    if (currentState.Value == 0)
                    {
                        observer.OnNext(currentState.Accumulator);
                        observer.OnCompleted();
                    }
                    else
                    {
                        currentState.Accumulator *= currentState.Value;
                        currentState.Value -= 1;
                        self(currentState);
                    }
                });
            });
        }
    }
}
