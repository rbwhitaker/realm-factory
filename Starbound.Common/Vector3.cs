using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3 Normalize()
        {
            double length = Length;

            if (length == 0) { throw new DivideByZeroException("cannot normalize a vector of length 0"); }

            return new Vector3(X / length, Y / length, Z / length);
        }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector2 v, double z)
        {
            X = v.X;
            Y = v.Y;
            Z = z;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator /(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
        }

        public static Vector3 operator /(Vector3 v, double scalar)
        {
            return new Vector3(v.X / scalar, v.Y / scalar, v.Z / scalar);
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }

        public static Vector3 operator *(Vector3 v, double scalar)
        {
            return new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }

        public static Vector3 operator *(double scalar, Vector3 v)
        {
            return new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public double Dot(Vector3 v)
        {
            return Dot(this, v);
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            double x = v1.Y * v2.Z - v1.Z * v2.Y;
            double y = v1.Z * v2.X - v1.X * v2.Z;
            double z = v1.X * v2.Y - v1.Y * v2.X;

            return new Vector3(x, y, z);
        }

        public static double Dot(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        /// <summary>
        /// Projects this vector onto another, returning the results.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3 ProjectOnto(Vector3 other)
        {
            return Project(this, other);
        }

        /// <summary>
        /// Projects the vector u on to the vector v.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Project(Vector3 u, Vector3 v)
        {
            double uDotV = Vector3.Dot(u, v);
            double vDotV = Vector3.Dot(v, v);

            Vector3 result = uDotV / vDotV * v;
            return result; 
        }

        public override string ToString()
        {
            return "<" + X + ", " + Y + ", " + Z + ">";
        }

        public static bool operator ==(Vector3 u, Vector3 v)
        {
            if (Object.ReferenceEquals(u, null) && Object.ReferenceEquals(v, null)) { return true; }
            if (Object.ReferenceEquals(u, null)) { return false; }
            if (Object.ReferenceEquals(v, null)) { return false; }

            return (u.X == v.X && u.Y == v.Y && u.Z == v.Z);
        }

        public static bool operator !=(Vector3 u, Vector3 v)
        {
            return !(u == v);
        }

        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 Ones = new Vector3(1, 1, 1);

        public bool IsValid()
        {
            if (X == Double.NaN) { return false; }
            if (Y == Double.NaN) { return false; }
            if (Z == Double.NaN) { return false; }

            return true;
        }
    }
}
