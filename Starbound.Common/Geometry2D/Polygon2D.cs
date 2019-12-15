using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.Common.Geometry;
using Starbound.Common.Triangulation;

namespace Starbound.Common.Geometry2D
{
    public class Polygon2D
    {
        public List<Vector2> Points { get; set; }

        public List<LineSegment2D> ConstructEdges()
        {
            List<LineSegment2D> edges = new List<LineSegment2D>();

            for (int index = 0; index < Points.Count; index++)
            {
                edges.Add(new LineSegment2D(Points[index], Points[(index + 1) % Points.Count]));
            }

            return edges;
        }

        public Polygon2D()
        {
            Points = new List<Vector2>();
        }

        public Polygon2D(List<Vector2> points)
        {
            Points = points;
        }

        public bool Contains(Vector2 point)
        {
            List<Triangle2D> polygonTriangles = PolygonTriangulator.Triangulate(this);
            foreach (Triangle2D triangle in polygonTriangles)
            {
                if (triangle.Contains(point)) { return true; }
            }

            return false;
        }

        public bool IsSimple()
        {
            List<LineSegment2D> edges = ConstructEdges();

            for (int edge1Index = 0; edge1Index < edges.Count; edge1Index++)
            {
                for (int edge2Index = edge1Index + 1; edge2Index < edges.Count; edge2Index++)
                {
                    LineSegment2D edge1 = edges[edge1Index];
                    LineSegment2D edge2 = edges[edge2Index];

                    if (edge1.V1 == edge2.V1 || edge1.V1 == edge2.V2 || edge1.V2 == edge2.V1 || edge1.V2 == edge2.V2)
                    {
                        continue;
                    }

                    if (edge1.Intersects(edge2))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public WindingOrder DetermineWindingOrder()
        {
            double angle = 0;
            for (int index = 0; index < Points.Count; index++)
            {
                Vector2 v1 = Points[index];
                Vector2 v2 = Points[(index + 1) % Points.Count];
                Vector2 v3 = Points[(index + 2) % Points.Count];

                angle += Vector2.SignedAngleBetween((v2 - v1), (v3 - v2));
            }

            if (angle > 0) { return WindingOrder.Clockwise; }
            else { return WindingOrder.Counterclockwise; }
        }
    }
}
