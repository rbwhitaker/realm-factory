using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel.Properties
{
    /// <summary>
    /// Represents an abstract property for an object or level.
    /// </summary>
    public abstract class ObjectProperty<T>
    {
        public T Value
        {
            get
            {
                if (UseDefault) { return ((ObjectPropertyTemplate<T>)Template).DefaultValue; }
                else return AssignedValue;
            }
            set
            {
                AssignedValue = value;
            }
        }

        private T assignedValue;

        private T AssignedValue
        {
            get
            {
                return assignedValue;
            }
            set
            {
                assignedValue = value;
                UseDefault = false;
            }
        }

        public bool UseDefault { get; set; }

        public ObjectPropertyTemplate<T> Template { get; set; }

        public ObjectProperty()
        {
        }
    }
}
