using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.Common;
using Starbound.Common.Geometry;
using Starbound.Common.Geometry2D;

namespace Starbound.Common.Triangulation
{
    public static class PolygonTriangulator
    {
        public static List<Starbound.Common.Geometry.Triangle2D> Triangulate(Polygon2D polygon)
        {
            List<Vector2> points = new List<Vector2>();
            points.AddRange(polygon.Points);

            return Triangulate(points);
        }

        public static List<Starbound.Common.Geometry.Triangle2D> Triangulate(List<Vector2> points)
        {
            throw new Exception("I really need to go to bed...");
            // I'm ending up with duplicates in points, but the original polygon does not seem to have any duplicates.  I think I want to
            // rewrite this so that it isn't recursive.  Just remove one vertex at a time.
            EnsureCounterclockwisePolygon(points);
            Polygon2D polygon = new Polygon2D(points);
            bool isSimple = polygon.IsSimple();

            if (!isSimple)
            {
                isSimple = true;
            }

            if (points.Count == 3)
            {
                List<Starbound.Common.Geometry.Triangle2D> triangles = new List<Triangle2D>();
                triangles.Add(new Starbound.Common.Geometry.Triangle2D(points[0], points[1], points[2]));
                return triangles;
            }

            for (int index = 0; index < points.Count; index++)
            {
                Vector2 v1 = points[index];
                Vector2 v2 = points[(index + 1) % points.Count];
                Vector2 v3 = points[(index + 2) % points.Count];

                double angle = Vector2.SignedAngleBetween(v2 - v1, v3 - v2);
                if (angle < 0) { continue; }

                bool isIntersected = false;
                LineSegment2D currentSegment = new LineSegment2D(v1, v3);
                for (int checkingIndex = 0; checkingIndex < points.Count; checkingIndex++)
                {
                    Vector2 start = points[checkingIndex];
                    Vector2 end = points[(checkingIndex + 1) % points.Count];

                    if (new LineSegment2D(start, end).Intersects(currentSegment))
                    {
                        isIntersected = true;
                        break;
                    }

                    if (new Triangle2D(v1, v2, v3).Contains(start))
                    {
                        isIntersected = true;
                        break;
                    }
                }

                if (!isIntersected)
                {
                    List<Starbound.Common.Geometry.Triangle2D> triangles = new List<Triangle2D>();
                    triangles.Add(new Starbound.Common.Geometry.Triangle2D(v1, v2, v3));
                    points.Remove(v2);
                    triangles.AddRange(Triangulate(points));
                    return triangles;
                }
            }

            throw new Exception("something went horribly wrong.");
        }

        private static void EnsureCounterclockwisePolygon(List<Vector2> points)
        {
            double angle = 0;
            for (int index = 0; index < points.Count; index++)
            {
                Vector2 v1 = points[index];
                Vector2 v2 = points[(index + 1) % points.Count];
                Vector2 v3 = points[(index + 2) % points.Count];

                angle += Vector2.SignedAngleBetween(v2 - v1, v3 - v2);
            }

            if (angle < 0) { points.Reverse(); }
        }
    }
}
