using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.RealmFactory.DataModel
{
    /// <summary>
    /// An enumeration of all of the toosl in the system.  I can imagine that
    /// ultimately, this will need to be refactored, and each tool will have its own
    /// class.
    /// </summary>
    public enum Tool
    {
        /// <summary>
        /// The Pencil tool, used to make changes on a cell by cell basis.
        /// </summary>
        Pencil,

        /// <summary>
        /// The Paint Bucket tool, used to change lots of adjacent cells all at
        /// once.
        /// </summary>
        Bucket,

        /// <summary>
        /// The Eraser tool, used to delete cells one by one.
        /// </summary>
        Eraser
    }
}
