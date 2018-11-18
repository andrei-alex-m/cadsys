using System;

namespace Caly.Common
{
    public class Point: IEquatable<Point>
    {
        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }

        public bool Equals(Point other)
        {
            return (Math.Abs(X - other.X) < 0.0001 && Math.Abs(Y - other.Y) < 0.0001);
        }
    }
}
