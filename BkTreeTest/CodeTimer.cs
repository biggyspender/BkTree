using System;
using System.Diagnostics;

namespace BkTreeTest
{
    public sealed class CodeTimer : IDisposable
    {
        private readonly Stopwatch stopwatch;

        public CodeTimer()
        {
            stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            Console.WriteLine("Took : {0:##,###}ms", stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}