using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Geometry
{
    public static class Projections
    {
        public static Line Project(Vector3 point, Vector3 direction)
        {
            return new Line(point, point + direction);
        }

        public static Vector3 Project(Vector3 point, Plane plane)
        {
            // This hasn't been unit tested or anything...
            // I simplified it from the Project(Vector3, Plane, Vector3) below,
            // thinking that the direction would be the plane's normal,
            // and simplifying because normal DOT normal = ||x^2 + y^2 + z^2||^2
            // and assuming the normal is normalized, that would be 1.  Right?
            double d = (plane.Position - point).Dot(plane.Normal);
            return point + plane.Normal * d;
        }

        public static Vector3 Project(Vector3 point, Plane plane, Vector3 direction)
        {
            double d = (plane.Position - point).Dot(plane.Normal) / direction.Dot(plane.Normal);
            return point + direction * d;
        }
    }
}
