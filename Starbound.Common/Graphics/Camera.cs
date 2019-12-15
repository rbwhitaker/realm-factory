using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.Graphics
{
    /// <summary>
    /// Represents a simple camera in 3D space.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// The point that the camera is looking at.
        /// </summary>
        public Vector3 Target { get; set; }

        /// <summary>
        /// The position of the camera in 3D space.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The up vector for the camera.
        /// </summary>
        public Vector3 Up { get; set; }

        /// <summary>
        /// Returns the direction that the camera is looking.
        /// </summary>
        public Vector3 Direction
        {
            get
            {
                return (Target - Position).Normalize();
            }
        }

        public Camera(Vector3 target, Vector3 position, Vector3 up)
        {
            Target = target;
            Position = position;
            Up = up;
        }
    }
}
