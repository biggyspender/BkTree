using System;

namespace BkTree
{
    public class Point2D : IEquatable<Point2D>
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point2D other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point2D) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public static bool operator ==(Point2D left, Point2D right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Point2D left, Point2D right)
        {
            return !Equals(left, right);
        }
    }
}