using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel.Properties
{
    public abstract class ObjectPropertyTemplate<T>
    {
        /// <summary>
        /// Gets or sets the name for the property.  This is displayed in GUIs.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a description for the property.  This explains what the property is used for
        /// at the object type level.  This is also displayed in tool tips or in other parts of the
        /// GUI, as needed to provide the artist with the information they need to make the right choice
        /// for values for it.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the default value for this particular property template.
        /// </summary>
        public T DefaultValue { get; set; }

        /// <summary>
        /// Validates a specific instance for correctness.  The default implementation is that all instances
        ///  are valid--there's no real validation.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual bool Validate(ObjectProperty<T> instance)
        {
            return true;
        }

        /// <summary>
        /// All property templates must be able to duplicate themselves.
        /// </summary>
        /// <returns></returns>
        public abstract ObjectPropertyTemplate<T> Copy();

        public abstract ObjectProperty<T> GenerateProperty();
    }
}
