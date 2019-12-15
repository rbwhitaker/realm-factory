using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Geometry2D
{
    public class LineSegment2D : Line2D
    {
        public LineSegment2D(Vector2 v1, Vector2 v2)
            :base(v1, v2)
        {
        }

        public bool Intersects(LineSegment2D other)
        {
            if (other.V1 == V1 || other.V1 == V2 || other.V2 == V1 || other.V2 == V2) { return false; }

            double x1 = V1.X;
            double x2 = V2.X;
            double x3 = other.V1.X;
            double x4 = other.V2.X;

            double y1 = V1.Y;
            double y2 = V2.Y;
            double y3 = other.V1.Y;
            double y4 = other.V2.Y;

            double denominator = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));

            if (denominator == 0) // they are parallel.
            {
                if (x2 - x1 == 0) // they are parallel and vertical
                {
                    if (x1 != x3) { return false; }

                    double min1 = Math.Min(y1, y2);
                    double min2 = Math.Min(y3, y4);
                    double max1 = Math.Max(y1, y2);
                    double max2 = Math.Max(y3, y4);

                    if (min1 > min2 && min1 < max2) { return true; }
                    if (max1 > min2 && max1 < max2) { return true; }
                    if (min2 > min1 && min2 < max1) { return true; }
                    if (max2 > min1 && max2 < max1) { return true; }

                    return false;
                }
                else // they are parallel, but non-vertical
                {
                    double slope = (y2 - y1) / (x2 - x1);
                    double b1 = y1 - slope * x1; // y-intercept for line 1
                    double b2 = y3 - slope * x3; // y-intercept for line 2

                    if (b1 != b2) { return false; } // If they do not have the same y-intercept, they don't overlap.
                    else // Otherwise, they are co-linear (and not vertical) so we check for overlap on the x-axis.
                    {
                        double min1 = Math.Min(x1, x2);
                        double min2 = Math.Min(x3, x4);
                        double max1 = Math.Max(x1, x2);
                        double max2 = Math.Max(x3, x4);

                        if (min1 > min2 && min1 < max2) { return true; }
                        if (max1 > min2 && max1 < max2) { return true; }
                        if (min2 > min1 && min2 < max1) { return true; }
                        if (max2 > min1 && max2 < max1) { return true; }

                        return false;
                    }
                }
            }
            else
            {
                double t1 = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / denominator;
                double t2 = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / denominator;

                if (t1 <= 0) { return false; }
                if (t1 >= 1) { return false; }
                if (t2 <= 0 || t2 >= 1) { return false; }
                return true;
            }
        }
    }
}
