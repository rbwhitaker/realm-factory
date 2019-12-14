using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.IO
{
    /// <summary>
    /// An interface for all file readers to extend.  Each type of file reader
    /// is allowed to choose their own extension, method of implementation (that's
    /// what the interface is for) and type of object that they read (that's what
    /// the generic T type is for).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface FileReader<T>
    {
        /// <summary>
        /// Reads an object of the generic T type from the specified file.  It can
        /// safely be assumed that during this call, CanRead(filename)
        /// would always be true.  It is the responsibility of anyone
        /// calling this method to ensure that, in advance, the FileReader
        /// can, in fact, read from the given filename/format.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="objectToWrite"></param>
        T Read(string filename);

        /// <summary>
        /// Indicates whether the FileReader can read from the given file.  In particular,
        /// the file reader should check for the proper extension, and possibly, the correct
        /// format version number, embedded somewhere within the file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool CanRead(string filename);

        /// <summary>
        /// Returns a display name that can be shown in the GUI for the end user to see.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Returns the extension of the file that this FileReader intends to read from.
        /// </summary>
        string Extension { get; }
    }
}
