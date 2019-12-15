using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Geometry
{
    public class Triangle
    {
        public Vector3 V1 { get; set; }

        public Vector3 V2 { get; set; }

        public Vector3 V3 { get; set; }

        public Vector3 Normal
        {
            get
            {
                return Vector3.Cross(V2 - V1, V3 - V1).Normalize();
            }
        }

        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }
    }
}
