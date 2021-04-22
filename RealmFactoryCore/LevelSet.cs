using System.Collections.Generic;

namespace RealmFactory.Core
{
    /// <summary>
    /// Represents a set of levels, consisting of a set
    /// of tile types and level definitions, created in Realm Factory.
    /// </summary>
    public class LevelSet
    {
        /// <summary>
        /// Gets or sets the list of tiles used in the level set.
        /// </summary>
        public List<AbstractTile> Tiles { get; set; }

        /// <summary>
        /// Gets or sets the level definitions contained within the project.
        /// </summary>
        public List<AbstractLevelDefinition> Levels { get; set; }

        /// <summary>
        /// Creates a new level set with no tiles, and no levels.
        /// </summary>
        public LevelSet()
        {
            Tiles = new List<AbstractTile>();
            Levels = new List<AbstractLevelDefinition>();
        }

        /// <summary>
        /// Adds a specific new tile to the level set for use in various
        /// definitions.
        /// </summary>
        /// <param name="tile"></param>
        public void AddTile(AbstractTile tile)
        {
            Tiles.Add(tile);
            tile.Id = Tiles.Count - 1;
        }

        /// <summary>
        /// Looks up a project based on a name.  If more than one level
        /// in the project has the same name, the first one found will always
        /// be returned.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public LevelDefinition GetLevel(string name)
        {
            foreach (AbstractLevelDefinition level in Levels)
            {
                if (level.Name == name) { return (LevelDefinition)level; }
            }

            return null;
        }

        /// <summary>
        /// Returns the Tile object that corresponds to the given tile ID.
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public Tile GetTile(int tileId)
        {
            for (int index = 0; index < Tiles.Count; index++)
            {
                if (Tiles[index].Id == tileId) { return (Tile)Tiles[index]; }
            }

            return null;
        }
    }
}
