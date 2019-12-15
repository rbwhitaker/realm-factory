using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.IO
{
    public class FileWriterManager<T>
    {
        private FileWriter<T> DefaultFileWriter { get; set; }

        private List<FileWriter<T>> FileWriters { get; set; }

        public FileWriterManager()
        {
            FileWriters = new List<FileWriter<T>>();
        }

        public void SetDefaultFileWriter(FileWriter<T> defaultFileWriter)
        {
            DefaultFileWriter = defaultFileWriter;
        }

        public void AddFileWriter(FileWriter<T> newFileWriter)
        {
            FileWriters.Add(newFileWriter);
        }

        public void Write(string filename, T objectToWrite)
        {
            foreach (FileWriter<T> writer in FileWriters)
            {
                if (writer.CanWrite(filename))
                {
                    writer.Write(filename, objectToWrite);
                    return;
                }
            }

            if (DefaultFileWriter != null)
            {
                DefaultFileWriter.Write(filename, objectToWrite);
                return;
            }

            throw new Exception("Could not write to file.  No file reader exists for the given file name, and no default file writer is defined.");
        }

        public string BuildFilter()
        {
            string filterString = "";
            foreach (FileWriter<T> fileWriter in FileWriters)
            {
                filterString += fileWriter.DisplayName + "|" + "*" + fileWriter.Extension + "|";
            }

            filterString += "All Files(*.*)|*.*";

            return filterString;
        }
    }
}
