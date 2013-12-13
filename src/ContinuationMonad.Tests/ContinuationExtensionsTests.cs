using System.Globalization;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContinuationMonad.Tests
{
    public class ContinuationExtensionsTests
    {
        [Fact]
        public void TestContinuationMonad()
        {
            var query = from x in 1.ToContinuation<int, string>()
                        from y in 1.ToContinuation<int, string>()
                        select x + y;

           var result = query(x => x.ToString(CultureInfo.InvariantCulture));

           Assert.Equal(result, "2");
        }

        [Fact]
        public async Task TestObservableContinuation()
        {
            var query = from x in 1.ToObservable(Scheduler.Immediate)
                        from y in 1.ToObservable(Scheduler.Immediate)
                        select x + y;

            var result = await query.Select(x => x.ToString(CultureInfo.InvariantCulture)).Take(1);

            Assert.Equal(result, "2");
        }
    }
}
