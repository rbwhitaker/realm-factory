using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Starbound.RealmFactory.DataModel
{
    /// <summary>
    /// A collection of settings for a project or level.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Gets or sets the width in pixels of a single cell.
        /// </summary>
        public int CellWidth { get; set; }

        /// <summary>
        /// Gets or sets the height in pixels of a single cell.
        /// </summary>
        public int CellHeight { get; set; }

        /// <summary>
        /// Gets or sets how many rows a level has.
        /// </summary>
        public int Rows { get; set; }
        
        /// <summary>
        /// Gets or sets how many columns a level has.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Gets or sets the background color for the level.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Creates a new Settings object, with some default values.
        /// </summary>
        public Settings()
        {
            CellWidth = 25;
            CellHeight = 25;
            Rows = 15;
            Columns = 15;
            BackColor = Color.Black;
        }

        /// <summary>
        /// Returns a deep copy of this Settings object.
        /// </summary>
        /// <returns></returns>
        public Settings Copy()
        {
            return new Settings() { 
                Rows = this.Rows, 
                Columns = this.Columns, 
                BackColor = this.BackColor, 
                CellWidth = this.CellWidth, 
                CellHeight = this.CellHeight };
        }
    }
}
