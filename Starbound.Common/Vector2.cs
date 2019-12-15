using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common
{
    public class Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }

        public Vector2 Normalize()
        {
            double length = Length;
            return new Vector2(X / length, Y / length);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vector2 operator /(Vector2 v, double scalar)
        {
            return new Vector2(v.X / scalar, v.Y / scalar);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2 operator *(Vector2 v, double scalar)
        {
            return new Vector2(v.X * scalar, v.Y * scalar);
        }

        public static Vector2 operator *(double scalar, Vector2 v)
        {
            return new Vector2(v.X * scalar, v.Y * scalar);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 FromPolarCoordinates(double angle, double length)
        {
            return new Vector2(Math.Cos(angle) * length, Math.Sin(angle) * length);
        }

        public static double Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public double Angle
        {
            get
            {
                double angle = Math.Atan(Y / X);
                if (X < 0) { angle += Math.PI; }
                return angle;
            }
        }

        public static double SignedAngleBetween(Vector2 v1, Vector2 v2)
        {
            double angle = Math.Acos(Vector2.Dot(v1.Normalize(), v2.Normalize()));
            if (Vector3.Cross(new Vector3(v1.Normalize(), 0), new Vector3(v2.Normalize(), 0)).Z < 0)
            {
                angle = -angle;
            }

            return angle;
        }

        public override string ToString()
        {
            return "<" + X + ", " + Y + ">";
        }
    }
}
