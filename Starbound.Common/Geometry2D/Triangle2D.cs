using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.Common.Geometry2D;

namespace Starbound.Common.Geometry
{
    public class Triangle2D
    {
        public Vector2 V1 { get; set; }

        public Vector2 V2 { get; set; }

        public Vector2 V3 { get; set; }

        public Triangle2D(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public bool Contains(Vector2 p)
        {
            if (!new LineSegment2D(V1, V2).SameSide(V3, p)) { return false; }
            if (!new LineSegment2D(V2, V3).SameSide(V1, p)) { return false; }
            if (!new LineSegment2D(V3, V1).SameSide(V2, p)) { return false; }

            return true;
        }
    }
}
