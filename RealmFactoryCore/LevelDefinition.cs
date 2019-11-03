using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Starbound.RealmFactoryCore
{
    /// <summary>
    /// Represents a definition for asimple, single layered, grid-based 2D level.
    /// This represents the original blueprint for the level and should not be
    /// modified during gameplay.  Instead, a Level instance should be created from
    /// the LevelDefinition using the LevelDefinition.CreateInstance() method.
    /// </summary>
    public class LevelDefinition : AbstractLevelDefinition
    {
        /// <summary>
        /// Returns the total number of rows the level has, up and down.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Returns the total number of columns the level has, side to side.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Gets or sets the background color for the level.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the size of an individual cell in the level.
        /// </summary>
        public Vector2 CellSize { get; set; }

        /// <summary>
        /// Stores the tiles of the level.
        /// </summary>
        private AbstractTile[,] grid;

        /// <summary>
        /// Creates a new SimpleGrid2DLevel, with a given number of rows and columns,
        /// cell size, and background color.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="backColor"></param>
        /// <param name="cellSize"></param>
        public LevelDefinition(string name, int rows, int columns, Color backColor, Vector2 cellSize)
            : base(name)
        {
            Rows = rows;
            Columns = columns;
            BackColor = backColor;
            CellSize = cellSize;

            grid = new AbstractTile[Rows, Columns];
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    grid[row, column] = null;
                }
            }
        }

        /// <summary>
        /// Assigns the given tile into the level at the specified
        /// row and column.  null can be used for the tile to clear
        /// it out.
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void Put(AbstractTile tile, int row, int column)
        {
            grid[row, column] = tile;
        }

        /// <summary>
        /// Returns the index/ID for the tile at the chosen
        /// row and column in the level.  If the chosen cell is empty,
        /// -1 is returned instead.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int GetTileId(int row, int column)
        {
            if (grid[row, column] == null) { return -1; }
            return grid[row, column].Id;
        }

        /// <summary>
        /// Returns the tile at the chosen row and column in the level.
        /// If the chosen cell is empty, null is returned.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Tile GetTile(int row, int column)
        {
            return (Tile)grid[row, column];
        }

        /// <summary>
        /// Creates an instance of the level, based on this level definition.
        /// </summary>
        /// <returns></returns>
        public Level CreateInstance()
        {
            Level level = new Level(Name, Rows, Columns, BackColor, CellSize);

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    level.Put(this.grid[row, column], row, column);
                }
            }

            return level;
        }
    }
}
