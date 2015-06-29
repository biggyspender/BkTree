using System;

namespace BkTree
{
    public class PythagoreanDistanceMetric : IDistanceMetric<Point2D>
    {
        public int CalculateDistance(Point2D v1, Point2D v2)
        {
            return Convert.ToInt32(
                Math.Sqrt(Math.Pow(v1.X - v2.X, 2) +
                          Math.Pow(v1.Y - v2.Y, 2)
                    ));
        }
    }
}