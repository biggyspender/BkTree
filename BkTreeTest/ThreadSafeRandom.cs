using System;
using System.Threading;

namespace BkTreeTest
{
    public static class ThreadSafeRandom
    {
        private static int _seed = 10237534;

        private static readonly ThreadLocal<Random> RngTl = new ThreadLocal<Random>(() =>
        {
            var currentSeed = Interlocked.Add(ref _seed, 24359);
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long epochTimeTicks = ts.Ticks;
            int intSeed;
            unchecked
            {
                long newSeed = epochTimeTicks + currentSeed;
                newSeed *= 393342739;
                intSeed = (int) (newSeed & 0xffffffffL);
            }
            return new Random(intSeed);
        });

        public static Random Rng
        {
            get { return RngTl.Value; }
        }
    }
}