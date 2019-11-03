using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel.Properties
{
    /// <summary>
    /// Represents a text-based property.
    /// </summary>
    public class TogglePropertyTemplate : ObjectPropertyTemplate<bool>
    {
        /// <summary>
        /// Generate an instance of the property, for an instance of the object type.
        /// </summary>
        /// <returns></returns>
        public ToggleProperty Generate()
        {
            return new ToggleProperty() { Template = this };
        }

        public override ObjectPropertyTemplate<bool> Copy()
        {
            return new TogglePropertyTemplate { Name = this.Name, Description = this.Description, DefaultValue = this.DefaultValue };
        }

        public override ObjectProperty<bool> GenerateProperty()
        {
            return new ToggleProperty() { Template = this, UseDefault = true };
        }
    }
}
