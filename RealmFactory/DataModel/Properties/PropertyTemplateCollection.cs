using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel.Properties
{
    public class PropertyTemplateCollection
    {
        public List<ObjectPropertyTemplate<object>> Properties { get; set; }

        public PropertyTemplateCollection Copy()
        {
            PropertyTemplateCollection copy = new PropertyTemplateCollection();
            copy.Properties = new List<ObjectPropertyTemplate<object>>();

            foreach (ObjectPropertyTemplate<object> propertyTemplate in Properties)
            {
                copy.Properties.Add(propertyTemplate.Copy());
            }

            return copy;
        }

        internal PropertyCollection GenerateProperties()
        {
            PropertyCollection propertyCollection = new PropertyCollection();

            foreach (ObjectPropertyTemplate<object> propertyTemplate in Properties)
            {
                propertyCollection.Add(propertyTemplate.GenerateProperty());
            }

            return propertyCollection;
        }

        public PropertyTemplateCollection()
        {
            Properties = new List<ObjectPropertyTemplate<object>>();
        }
    }
}
