using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common
{
    public class AFRectangle
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public bool Contains(Vector2 v)
        {
            if (v.X > Position.X && v.X < Position.X + Size.X &&
                v.Y > Position.Y && v.Y < Position.Y + Size.Y)
            {
                return true;
            }

            return false;
        }
    }
}
