using System;

namespace BkTree
{
    public class PythagoreanDistanceMetric : IDistanceMetric<Point>
    {
        public int CalculateDistance(Point v1, Point v2)
        {
            return Convert.ToInt32(
                Math.Sqrt(Math.Pow(v1.X - v2.X, 2) +
                          Math.Pow(v1.Y - v2.Y, 2)
                    ));
        }
    }
}