using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Geometry
{
    public class Line
    {
        public Vector3 V1 { get; set; }
        public Vector3 V2 { get; set; }

        public Vector3 Direction
        {
            get
            {
                return (V2 - V1).Normalize();
            }
        }

        public Vector3 DirectionNormalized
        {
            get
            {
                return (V2 - V1).Normalize();
            }
        }

        public Line(Vector3 v1, Vector3 v2)
        {
            V1 = v1;
            V2 = v2;
        }

        public Vector3 At(double t)
        {
            return V1 + (V2 - V1) * t;
        }

        public double CalculateT(Vector3 p)
        {
            Vector3 l1 = V2 - V1;
            Vector3 p1 = Vector3.Project(p - V1, l1); // adjust to V1, because it is currently a point, not a vector
            double t1 = p1.Length / l1.Length;

            Vector3 l2 = V1 - V2;
            Vector3 p2 = Vector3.Project(p - V2, l2); // adjust to V2 (the start of the line) because it is currently a point, not a vector.
            double t2 = p2.Length / l2.Length;

            if (t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1) { return t1; }
            else if (t1 > t2) { return t1; }
            else { return -t1; }
        }
    }
}
