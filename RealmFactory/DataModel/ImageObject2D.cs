using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Starbound.RealmFactory.DataModel
{
    /// <summary>
    /// A simple image-based object in a 2D grid.
    /// </summary>
    public class ImageObject2D : GameObject
    {
        /// <summary>
        /// Creates a new ImageObject2D object.
        /// </summary>
        public ImageObject2D()
        {
        }

        /// <summary>
        /// Creates a shallow copy of the object.
        /// </summary>
        /// <returns></returns>
        public override GameObject Copy()
        {
            ImageObject2D copy = new ImageObject2D();

            copy.ParentType = this.ParentType;
            copy.Properties = new Starbound.RealmFactory.DataModel.Properties.PropertyCollection();

            return copy;
        }
    }
}
