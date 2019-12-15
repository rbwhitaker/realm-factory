using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Geometry.Intersections
{
    public static class IntersectionEngine
    {
        public static Vector3 Intersect(Plane plane, Line line)
        {
            double t = (plane.Position - line.V1).Dot(plane.Normal) / line.Direction.Dot(plane.Normal);
            return line.At(t);
        }

        public static Vector3 Intersect(Triangle triangle, Line line)
        {
            Vector3 v0 = triangle.V1;
            Vector3 v1 = triangle.V2;
            Vector3 v2 = triangle.V3;
            Vector3 normal = triangle.Normal;

            double a = v0.X - v1.X;
            double b = v0.X - v2.X;
            double c = line.Direction.X;
            double d = v0.X - line.V1.X;

            double e = v0.Y - v1.Y;
            double f = v0.Y - v2.Y;
            double g = line.Direction.Y;
            double h = v0.Y - line.V1.Y;

            double i = v0.Z - v1.Z;
            double j = v0.Z - v2.Z;
            double k = line.Direction.Z;
            double l = v0.Z - line.V1.Z;

            double m = f * k - g * j;
            double n = h * k - g * l;
            double p = f * l - h * j;
            double q = g * i - e * k;
            double s = e * j - f * i;

            double inverseDenominator = 1.0 / (a * m + b * q + c * s);

            double e1 = d * m - b * n - c * p;
            double beta = e1 * inverseDenominator;

            if (beta < 0) { return null; }

            double r = e * l - h * i;
            double e2 = a * n + d * q + c * r;
            double gamma = e2 * inverseDenominator;

            if (gamma < 0) { return null; }

            if (beta + gamma > 1) { return null; }

            double e3 = a * p - b * r + d * s;
            double t = e3 * inverseDenominator;
            return line.At(t);
        }
    }
}
