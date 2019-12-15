using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common
{
    public abstract class CoordinateTransformer
    {
        public abstract Vector2 TransformVector(Vector2 v);

        public abstract Vector2 UntransformVector(Vector2 v);

        public abstract Vector2 TransformPoint(Vector2 v);

        public abstract Vector2 UntransformPoint(Vector2 v);
    }
}
