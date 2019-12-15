using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common
{
    public class DefaultCoordinateTransformer : CoordinateTransformer
    {
        public Vector2 Scale { get; set; }

        public Vector2 OriginOffset { get; set; }

        public DefaultCoordinateTransformer()
        {
            Scale = new Vector2(1, 1);
            OriginOffset = new Vector2(0, 0);
        }

        public override Vector2 TransformVector(Vector2 v)
        {
            return new Vector2(v.X * Scale.X, v.Y * Scale.Y);
        }

        public override Vector2 UntransformVector(Vector2 v)
        {
            return new Vector2(v.X / Scale.X, v.Y / Scale.Y);
        }

        public override Vector2 TransformPoint(Vector2 v)
        {
            return new Vector2(v.X * Scale.X + OriginOffset.X, v.Y * Scale.Y + OriginOffset.Y);
        }

        public override Vector2 UntransformPoint(Vector2 v)
        {
            return new Vector2((v.X - OriginOffset.X) / Scale.X, (v.Y - OriginOffset.Y) / Scale.Y);
        }
    }
}
