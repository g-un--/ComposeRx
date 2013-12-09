using System;
using System.Reactive.Concurrency;
using Xunit;

namespace Fibonacci.Tests
{
    public class FibonacciObservableTests
    {
        private readonly FibonacciObservable _fibonacci;

        public FibonacciObservableTests()
        {
            _fibonacci = new FibonacciObservable();
        }

        [Fact]
        public void FibonnaciItemNumber5Returns5()
        {
            ulong computedResult = 0;

            _fibonacci.Get(Scheduler.Immediate)(5).Subscribe(result =>
            {
                computedResult = result;
            });

            Assert.Equal<ulong>(computedResult, 5);
        }

        [Fact]
        public void FibonnaciItemNumber7Returns13()
        {
            ulong computedResult = 0;

            _fibonacci.Get(Scheduler.Immediate)(7).Subscribe(result =>
            {
                computedResult = result;
            });

            Assert.Equal<ulong>(computedResult, 13);
        }
    }
}
