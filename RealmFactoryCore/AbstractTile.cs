using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactoryCore
{
    /// <summary>
    /// Represents a tile in a grid-based game.  A tile fills a single cell.
    /// </summary>
    public class AbstractTile
    {
        /// <summary>
        /// Gets or sets the numeric ID, unique througout a project, for
        /// the given tile.  Id values are assigned when the tile is added
        /// to a project, and should not be adjusted manually in most cases.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this tile.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Overrides the Tile object to be more human readable while debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Tile[Id=" + Id + ",Name=" + Name + "]";
        }
    }
}
