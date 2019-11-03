using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel.Properties
{
    public class PropertyCollection
    {
        public List<ObjectProperty<object>> Properties { get; set; }

        public PropertyCollection()
        {
            Properties = new List<ObjectProperty<object>>();
        }

        public void Add(ObjectProperty<object> objectProperty)
        {
            Properties.Add(objectProperty);
        }
    }
}
