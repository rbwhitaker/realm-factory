using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Common.IO
{
    public class FileReaderManager<T>
    {
        private FileReader<T> DefaultFileReader { get; set; }

        private List<FileReader<T>> FileReaders { get; set; }

        public FileReaderManager()
        {
            FileReaders = new List<FileReader<T>>();
        }

        public void SetDefaultFileReader(FileReader<T> defaultFileReader)
        {
            DefaultFileReader = defaultFileReader;
        }

        public void AddFileReader(FileReader<T> newFileReader)
        {
            FileReaders.Add(newFileReader);
        }

        public T Read(string filename)
        {
            foreach (FileReader<T> Reader in FileReaders)
            {
                if (Reader.CanRead(filename))
                {
                    return Reader.Read(filename);
                }
            }

            if (DefaultFileReader != null)
            {
                return DefaultFileReader.Read(filename);
            }

            throw new Exception("Could not read file.  No file reader exists for the given file name, and no default file reader is defined.");
        }

        public string BuildFilter()
        {
            string filterString = "";
            foreach (FileReader<T> fileReader in FileReaders)
            {
                filterString += fileReader.DisplayName + "|" + "*" + fileReader.Extension + "|";
            }

            filterString += "All Files(*.*)|*.*";

            return filterString;
        }
    }
}
