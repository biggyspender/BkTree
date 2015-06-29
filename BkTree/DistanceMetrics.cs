namespace BkTree
{
    public static class DistanceMetrics
    {
        public static IDistanceMetric<Point> Pythagorean
        {
            get
            {
                return new PythagoreanDistanceMetric();
            }
        }
        public static IDistanceMetric<ulong> Hamming
        {
            get
            {
                return new HammingDistanceMetric();
            }
        }
    }
}