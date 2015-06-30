using System;
using System.Collections.Generic;

namespace BkTree
{
    public static class RangeGenerators
    {
        public static IEnumerable<int> RangeMinMaxInclusive(int min, int max)
        {
            return RangeMinMax(min, max, true);
        }

        public static IEnumerable<int> RangeMinMax(int min, int max, bool inclusive = false)
        {
            var count = Math.Max(max - min, 0);
            return Range(min, count, inclusive);
        }

        public static IEnumerable<int> RangeInclusive(int start, int count)
        {
            return Range(start, count, true);
        }
        public static IEnumerable<int> Range(int start, int count, bool inclusive = false)
        {
            if (inclusive)
            {
                for (int i = 0; i <= count; ++i)
                {
                    yield return start + i;
                }
            }
            else
            {
                for (int i = 0; i < count; ++i)
                {
                    yield return start + i;
                }
            }
        }
    }
}