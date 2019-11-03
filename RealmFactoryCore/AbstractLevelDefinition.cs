using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactoryCore
{
    /// <summary>
    /// Represents a generic level, created in Realm Factory.
    /// This class is abstract.  Use one of the derived classes
    /// (like LevelDefinition) instead.
    /// </summary>
    public abstract class AbstractLevelDefinition
    {
        /// <summary>
        /// Gets or sets the name of the level.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a new level definition with the specified name.
        /// </summary>
        /// <param name="name"></param>
        public AbstractLevelDefinition(string name)
        {
            Name = name;
        }
    }
}
