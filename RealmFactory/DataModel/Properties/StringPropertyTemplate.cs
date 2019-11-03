using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel.Properties
{
    /// <summary>
    /// Represents a text-based property.
    /// </summary>
    public class StringPropertyTemplate : ObjectPropertyTemplate<string>
    {
        /// <summary>
        /// Generate an instance of the property, for an instance of the object type.
        /// </summary>
        /// <returns></returns>
        public StringProperty Generate()
        {
            return new StringProperty() { Template = this };
        }

        public override ObjectPropertyTemplate<string> Copy()
        {
            return new StringPropertyTemplate { Name = this.Name, Description = this.Description, DefaultValue = this.DefaultValue };
        }

        public override ObjectProperty<string> GenerateProperty()
        {
            return new StringProperty() { Template = this, UseDefault = true };
        }
    }
}
