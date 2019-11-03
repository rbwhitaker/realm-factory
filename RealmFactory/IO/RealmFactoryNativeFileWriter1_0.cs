using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.Common.IO;
using Starbound.RealmFactory.DataModel;
using System.Xml;
using System.Drawing;

namespace Starbound.RealmFactory.IO
{
    public class RealmFactoryNativeFileWriter1_0 : FileWriter<Project>
    {
            public void Write(string filename, Project project)
            {
                Dictionary<object, int> objectIdDictionary = BuildObjectIdDictionary(project);

                XmlWriter xmlWriter = new XmlTextWriter(filename, Encoding.UTF8);

                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("realm-project");

                // This is the file format version.  If the format ever becomes 
                // incompatible with previous versions, it should be incremented.
                xmlWriter.WriteAttributeString("software-version", Constants.VersionString);
                xmlWriter.WriteAttributeString("file-format-version", "1.0");
                xmlWriter.WriteAttributeString("id", objectIdDictionary[project].ToString());
                
                xmlWriter.WriteAttributeString("default-back-color", 
                    project.Settings.BackColor.R + "," +
                    project.Settings.BackColor.G + "," +
                    project.Settings.BackColor.B + "," +
                    project.Settings.BackColor.A);

                xmlWriter.WriteAttributeString("default-rows",
                    project.Settings.Rows.ToString());
                xmlWriter.WriteAttributeString("default-columns",
                    project.Settings.Columns.ToString());
                xmlWriter.WriteAttributeString("default-width",
                    project.Settings.CellWidth.ToString());
                xmlWriter.WriteAttributeString("default-height",
                    project.Settings.CellHeight.ToString());

                xmlWriter.WriteStartElement("tiles");

                foreach (ImageObject2DType tile in project.Types)
                {
                    xmlWriter.WriteStartElement("image-tile-2d");

                    xmlWriter.WriteAttributeString("name", tile.Name);
                    xmlWriter.WriteAttributeString("id", objectIdDictionary[tile].ToString());
                    xmlWriter.WriteAttributeString("width", tile.Image.Width.ToString());
                    xmlWriter.WriteAttributeString("height", tile.Image.Height.ToString());

                    byte[] imageBytes = SerializeImageData(tile.Image);

                    xmlWriter.WriteStartElement("contents");
                    xmlWriter.WriteBase64(imageBytes, 0, imageBytes.Length);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("levels");

                foreach (Level level in project.Levels)
                {
                    WriteLevel(xmlWriter, level, objectIdDictionary);
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                xmlWriter.Close();
            }

        private void WriteLevel(XmlWriter xmlWriter, Level level, Dictionary<object, int> objectIdDictionary)
        {
            xmlWriter.WriteStartElement("level");

            xmlWriter.WriteAttributeString("name", level.Name);

            xmlWriter.WriteAttributeString("id", objectIdDictionary[level].ToString());

            xmlWriter.WriteAttributeString("back-color",
                level.Settings.BackColor.R + "," +
                level.Settings.BackColor.G + "," +
                level.Settings.BackColor.B + "," +
                level.Settings.BackColor.A);

            xmlWriter.WriteAttributeString("rows",
                level.Settings.Rows.ToString());
            xmlWriter.WriteAttributeString("columns",
                level.Settings.Columns.ToString());
            xmlWriter.WriteAttributeString("width",
                level.Settings.CellWidth.ToString());
            xmlWriter.WriteAttributeString("height",
                level.Settings.CellHeight.ToString());

            xmlWriter.WriteStartElement("level-content");
            string levelContent = "";
            for (int row = 0; row < level.Settings.Rows; row++)
            {
                for (int column = 0; column < level.Settings.Columns; column++)
                {
                    ImageObject2D tile = level.Get(column, row) as ImageObject2D;
                    if (tile != null)
                    {
                        levelContent += objectIdDictionary[tile].ToString();
                    }
                    else
                    {
                        levelContent += "X";
                    }

                    if (!((row == level.Settings.Rows - 1) && (column == level.Settings.Columns - 1)))
                    {
                        levelContent += ",";
                    }
                }
            }
            xmlWriter.WriteString(levelContent);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        private byte[] SerializeImageData(Bitmap bitmap)
        {
            byte[] allBytes = new byte[bitmap.Width * bitmap.Height * 4];

            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int column = 0; column < bitmap.Width; column++)
                {
                    Color color = bitmap.GetPixel(column, row);
                    allBytes[(column + row * bitmap.Width) * 4 + 0] = color.R;
                    allBytes[(column + row * bitmap.Width) * 4 + 1] = color.G;
                    allBytes[(column + row * bitmap.Width) * 4 + 2] = color.B;
                    allBytes[(column + row * bitmap.Width) * 4 + 3] = color.A;
                }
            }

            return allBytes;
        }

        private Dictionary<object, int> BuildObjectIdDictionary(Project project)
        {
            Dictionary<object, int> dictionary = new Dictionary<object, int>();

            dictionary[project] = 0;
            int currentIndex = 1;

            foreach (GameObjectType tile in project.Types)
            {
                dictionary[tile] = currentIndex;
                currentIndex++;
            }

            foreach (Level level in project.Levels)
            {
                dictionary[level] = currentIndex;
                currentIndex++;
            }

            return dictionary;
        }

        public bool CanWrite(string filename)
        {
            return filename.EndsWith(Extension);
        }

        public string DisplayName
        {
            get { return "Realm Factory Projects"; }
        }

        public string Extension
        {
            get { return ".realm"; }
        }
    }
}
