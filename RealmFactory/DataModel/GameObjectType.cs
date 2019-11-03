using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.RealmFactory.DataModel.Properties;

namespace Starbound.RealmFactory.DataModel
{
    /// <summary>
    /// A generic object type defines what things all object types must be able to do.
    /// All object types should be derived from this class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GameObjectType
    {
        /// <summary>
        /// Object types must be able to create a copy of themselves for the undo/redo
        /// system.
        /// </summary>
        /// <returns></returns>
        public abstract GameObjectType Copy();

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Object types must be able to generate new instances of the object.
        /// This is done through this method.
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GenerateNew();

        /// <summary>
        /// Creates a new game object type with an empty set of properties and no name.
        /// </summary>
        public GameObjectType()
        {
            PropertyTemplates = new PropertyTemplateCollection();
        }

        public PropertyTemplateCollection PropertyTemplates { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
