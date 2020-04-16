using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.IO
{
    /// <summary>
    /// An interface for all file writers to extend.  Each type of file writer
    /// is allowed to choose their own extension, method of implementation (that's
    /// what the interface is for) and type of object that they write (that's what
    /// the generic T type is for).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface FileWriter<T>
    {
        /// <summary>
        /// Writes the given object to the specified file.  It can
        /// safely be assumed that during this call, CanWrite(filename)
        /// would always be true.  It is the responsibility of anyone
        /// calling this method to ensure that, in advance, the FileWriter
        /// can, in fact, write to the given filename/format.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="objectToWrite"></param>
        void Write(string filename, T objectToWrite);

        /// <summary>
        /// Returns whether the FileWriter can write to a particular file.
        /// In particular, this is concerned with ensuring that the file extension
        /// indicates that the format that the user thinks it is being written to
        /// is in fact the extension for the format this FileWriter is expecting
        /// to write with.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool CanWrite(string filename);

        /// <summary>
        /// Indicates the name of the FileWriter that can be displayed to the user
        /// in the program.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Returns the extension that this file writer will write to.
        /// </summary>
        string Extension { get; }
    }
}
