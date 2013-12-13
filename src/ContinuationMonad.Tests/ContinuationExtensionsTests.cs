using System.Globalization;
using Xunit;

namespace ContinuationMonad.Tests
{
    public class ContinuationExtensionsTests
    {
        [Fact]
        public void TestContinuation()
        {
            var query = from x in 1.ToContinuation<int, string>()
                        from y in 1.ToContinuation<int, string>()
                        select x + y;

           var result = query(x => x.ToString(CultureInfo.InvariantCulture));

           Assert.Equal(result, "2");
        }
    }
}
