using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.RealmFactory.DataModel;
using System.Drawing;
using System.IO;

namespace Starbound.RealmFactory.DataModel
{
    public class ImageObject2DType : GameObjectType
    {
        /// <summary>
        /// Gets or sets the image that is used to represent this object.
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// Creates a new ImageObject2D from a given image file path.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ImageObject2DType FromFile(string filename)
        {
            ImageObject2DType newType = new ImageObject2DType();

            Bitmap fromFile = (Bitmap)Bitmap.FromFile(filename);
            newType.Image = new Bitmap(fromFile);
            fromFile.Dispose();

            newType.Name = Path.GetFileNameWithoutExtension(filename);

            return newType;
        }

        /// <summary>
        /// Copies the template, including property templates from the generic
        /// GameObjectType.
        /// </summary>
        /// <returns></returns>
        public override GameObjectType Copy()
        {
            ImageObject2DType copy = new ImageObject2DType();

            copy.Image = this.Image; // Shallow copy.
            copy.Name = this.Name;

            return copy;
        }

        public override GameObject GenerateNew()
        {
            return new ImageObject2D() { ParentType = this, Properties = this.PropertyTemplates.GenerateProperties() };
        }
    }
}
