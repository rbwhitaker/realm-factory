using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.RealmFactory.DataModel;

namespace RealmEngine.UndoRedo
{
    /// <summary>
    /// Represents the program's state, including the model's state, as well as the GUI's state, to some degree,
    /// to allow the program to return to a particular state.  This is used in the Undo/Redo system.
    /// </summary>
    public class ProgramState
    {
        /// <summary>
        /// Stores the complete project state.
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// Stores what level was selected at the state in time.
        /// </summary>
        public int ActiveLevelIndex { get; set; }

        /// <summary>
        /// Stores what tile was selected at the point in time that this ProgramState object was created.
        /// </summary>
        public int ActiveTypeIndex { get; set; }
    }
}
