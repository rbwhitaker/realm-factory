using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.RealmFactory.DataModel.Properties;

namespace Starbound.RealmFactory.DataModel
{
    public abstract class GameObject
    {
        /// <summary>
        /// The parent type for the image object, where values should be inherited from.
        /// </summary>
        public GameObjectType ParentType { get; set; }

        /// <summary>
        /// Creates a new GameObject instance.
        /// </summary>
        public GameObject()
        {
        }

        /// <summary>
        /// Stores the object's properties.
        /// </summary>
        private PropertyCollection properties;

        /// <summary>
        /// Gets or sets the object's properties.
        /// </summary>
        public PropertyCollection Properties
        {
            get
            {
                return properties;
            }
            set
            {
                properties = value;
            }
        }

        /// <summary>
        /// Makes a copy of a game object for the sake of storing in the
        /// undo/redo system.
        /// </summary>
        /// <returns></returns>
        public abstract GameObject Copy();
    }
}
