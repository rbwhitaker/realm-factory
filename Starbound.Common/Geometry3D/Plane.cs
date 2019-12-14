using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Geometry
{
    public class Plane
    {
        public Vector3 Position { get; set; }
        public Vector3 Normal { get; set; }

        public double A
        {
            get
            {
                return Normal.X;
            }
        }

        public double B
        {
            get
            {
                return Normal.Y;
            }
        }

        public double C
        {
            get
            {
                return Normal.Z;
            }
        }

        public double D
        {
            get
            {
                return -(Normal.X * Position.X + Normal.Y * Position.Y + Normal.Z * Position.Z);
            }
        }

        public Plane(Vector3 position, Vector3 normal)
        {
            Position = position;
            Normal = normal;
        }

        public Plane(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Position = v1;
            Normal = Vector3.Cross(v2 - v1, v3 - v1).Normalize();
        }

        /// <summary>
        /// Does an orthogonal projection of the point onto the plane.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector3 Project(Vector3 point)
        {
            double a = A;
            double b = B;
            double c = C;
            double d = D;
            double u = point.X;
            double v = point.Y;
            double w = point.Z;

            double t0 = -(a * u + b * v + c * w + d) / (a * a + b * b + c * c);

            return new Vector3(u + a * t0, v + b * t0, w + c * t0);
        }

        public Vector2 ToPlaneCoordinates(Vector3 point)
        {
            List<Vector3> points = new List<Vector3>();
            points.Add(point);
            return ToPlaneCoordinates(points)[0];
        }

        /// <summary>
        /// Creates a list of points that are the equivalent to the input points
        /// in plane coordinates.  These points are projected onto the plane, and 
        /// assigned to a coordinate system based on the orientation of the plane.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public List<Vector2> ToPlaneCoordinates(IEnumerable<Vector3> points)
        {
            List<Vector2> results = new List<Vector2>();

            Vector3 u = ((Normal != Vector3.UnitY && Normal != -Vector3.UnitY) ? Vector3.Cross(Normal, -Vector3.UnitY) : Vector3.Cross(Normal, Vector3.UnitX));
            u = u.Normalize();
            Vector3 v = Vector3.Cross(Normal, u);
            v = v.Normalize();

            foreach (Vector3 point in points)
            {
                Vector3 translatedPoint = point - this.Position;
                Vector3 uVector = translatedPoint.ProjectOnto(u) / u;
                Vector3 vVector = translatedPoint.ProjectOnto(v) / v;

                if (!uVector.IsValid()) { throw new Exception("Vector is not valid"); }
                if (!vVector.IsValid()) { throw new Exception("Vector is not valid"); }
                results.Add(new Vector2(
                    (!Double.IsNaN(uVector.X) ? uVector.X : (!Double.IsNaN(uVector.Y) ? uVector.Y : uVector.Z)),
                    (!Double.IsNaN(vVector.X) ? vVector.X : (!Double.IsNaN(vVector.Y) ? vVector.Y : vVector.Z))));
            }

            return results;
        }

        public Vector3 ToWorldCoordinates(Vector3 point)
        {
            Vector3 u = ((Normal != Vector3.UnitY && Normal != -Vector3.UnitY) ? Vector3.Cross(Normal, -Vector3.UnitY) : Vector3.Cross(Normal, Vector3.UnitX));
            u = u.Normalize();
            Vector3 v = Vector3.Cross(Normal, u);
            v = v.Normalize();

            return (this.Position + u * point.X + v * point.Y + Normal * point.Z);
        }
    }
}
