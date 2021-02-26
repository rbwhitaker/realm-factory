using Microsoft.Xna.Framework.Graphics;

namespace RealmFactory.Core
{
    /// <summary>
    /// Represents a simple tile, represented by a single Texture2D object.
    /// </summary>
    public class Tile : AbstractTile
    {
        /// <summary>
        /// Gets or sets the texture associated with this tile.
        /// </summary>
        public Texture2D Texture { get; set; }
    }
}
