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
            var cancellationTokenSources = Enumerable.Range(1, 5)
                .Select(_ => new CancellationTokenSource()).ToList();
            /**
             * output:
             *  4 disposed
             *  foo
             *  --
             *  bar
            */
            // cancellationTokenSources.LastOrDefault()?.Cancel(); // all CTR will be disposed expect the last
            /**
             * output:
             *  foo
             *  --
             *  bar
            */
            // cancellationTokenSources.FirstOrDefault()?.Cancel(); // no one CTR will be disposed
            var compositeChangeToken = new CompositeChangeToken(
                cancellationTokenSources.Select(it => new SpyChangeToken(new CancellationChangeToken(it.Token))).ToList());
            compositeChangeToken.RegisterChangeCallback(s => Console.WriteLine(s), "foo");
            Console.WriteLine("--");
            compositeChangeToken.RegisterChangeCallback(s => Console.WriteLine(s), "bar");
            /**
             * output:
             *  --
             *  bar
             *  foo
             *  5 disposed
            */
            cancellationTokenSources.FirstOrDefault()?.Cancel(); // all CTR will be disposed
        }
    }
}
