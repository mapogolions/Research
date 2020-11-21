using System;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Primitives;

namespace CompositeChangeTokenPrimitive
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            // strange behaviour caused by the following line of code
            // https://github.com/dotnet/runtime/blob/2f8959aa82e9c47b74aea2f3bce40ac918cbca69/src/libraries/Microsoft.Extensions.Primitives/src/CompositeChangeToken.cs#L97
            var cancellationTokenSources = Enumerable.Range(1, 5)
                .Select(_ => new CancellationTokenSource()).ToList();
            cancellationTokenSources.ToArray()[2]?.Cancel(); // dispose all up to third element (disposed 2 times)
            var compositeChangeToken = new CompositeChangeToken(
                cancellationTokenSources.Select(it => new SpyChangeToken(new CancellationChangeToken(it.Token))).ToList());
            compositeChangeToken.RegisterChangeCallback(s => Console.WriteLine(s), "foo");
            compositeChangeToken.RegisterChangeCallback(s => Console.WriteLine(s), "bar");
            cancellationTokenSources.FirstOrDefault()?.Cancel(); // nothing happens because cancellation token registration has already been disposed
            cancellationTokenSources.ToArray()[3]?.Cancel(); // dispose all 5 elements
        }
    }
}
