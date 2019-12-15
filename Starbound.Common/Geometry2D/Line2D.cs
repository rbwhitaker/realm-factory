using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Geometry2D
{
    public class Line2D
    {
        public Vector2 V1 { get; set; }
        public Vector2 V2 { get; set; }

        public Line2D(Vector2 v1, Vector2 v2)
        {
            V1 = v1;
            V2 = v2;
        }

        public bool SameSide(Vector2 a, Vector2 b)
        {
            double zForA = Vector3.Cross(new Vector3(V2 - V1, 0), new Vector3(a - V2, 0)).Z;
            double zForB = Vector3.Cross(new Vector3(V2 - V1, 0), new Vector3(b - V2, 0)).Z;

            return (Math.Sign(zForA) == Math.Sign(zForB));
        }
    }
}
