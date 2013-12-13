using System;
using System.Reactive.Concurrency;
using Xunit;

namespace Factorials.Tests
{
    public class FactorialObservableTests
    {
        private readonly FactorialObservable _factorial;

        public FactorialObservableTests()
        {
            _factorial = new FactorialObservable();
        }

        [Fact]
        public void FactorialOf1Returns1()
        {
            ulong computedResult = 0;

            _factorial.Get(Scheduler.Immediate)(1).Subscribe(result =>
            {
                computedResult = result;
            });

            Assert.Equal<ulong>(computedResult, 1);
        }

        [Fact]
        public void FactorialOf5Returns120()
        {
            ulong computedResult = 0;

            _factorial.Get(Scheduler.Immediate)(5).Subscribe(result =>
            {
                computedResult = result;
            });

            Assert.Equal<ulong>(computedResult, 120);
        }
    }
}
