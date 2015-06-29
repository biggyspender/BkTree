namespace BkTree
{
    public class HammingDistanceMetric : IDistanceMetric<ulong>
    {
        public int CalculateDistance(ulong v1, ulong v2)
        {
            return NumberOfSetBits(v1 ^ v2);
        }
        private static int NumberOfSetBits(ulong i)
        {
            //magic
            i = i - ((i >> 1) & 0x5555555555555555UL);
            i = (i & 0x3333333333333333UL) + ((i >> 2) & 0x3333333333333333UL);
            return (int)(unchecked(((i + (i >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
        }

    }
}