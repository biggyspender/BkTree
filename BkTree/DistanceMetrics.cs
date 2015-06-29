namespace BkTree
{
    public static class DistanceMetrics
    {
        public static IDistanceMetric<Point2D> Pythagorean
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