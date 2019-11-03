using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.Common.IO;
using System.Xml;
using System.Drawing;
using Starbound.RealmFactory.DataModel;

namespace Starbound.RealmFactory.IO
{
    public class RealmFactoryNativeFileReader1_0 : FileReader<Project>
    {
        public Project Read(string filename)
        {
            Project project = new Project();

            Dictionary<int, object> objectIdDictionary = new Dictionary<int, object>();

            XmlReader reader = new XmlTextReader(filename);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);

            XmlNode projectNode = xmlDocument.DocumentElement;
            objectIdDictionary[Convert.ToInt32(projectNode.Attributes["id"].Value)] = project;

            Settings settings = new Settings();
            settings.CellWidth = Convert.ToInt32(projectNode.Attributes["default-width"].Value);
            settings.CellHeight = Convert.ToInt32(projectNode.Attributes["default-height"].Value);
            settings.Rows = Convert.ToInt32(projectNode.Attributes["default-rows"].Value);
            settings.Columns = Convert.ToInt32(projectNode.Attributes["default-columns"].Value);
            settings.BackColor = ParseColor(projectNode.Attributes["default-back-color"].Value);
            project.Settings = settings;

            foreach (XmlNode child in projectNode.ChildNodes)
            {
                if (child.Name == "tiles")
                {
                    foreach (XmlNode tileNode in child.ChildNodes)
                    {
                        project.Types.Add(ParseTileNode(tileNode, project, objectIdDictionary));
                    }
                }
            }

            foreach (XmlNode child in projectNode.ChildNodes)
            {
                if (child.Name == "levels")
                {
                    foreach (XmlNode levelNode in child.ChildNodes)
                    {
                        project.Levels.Add(ParseLevelNode(levelNode, project, objectIdDictionary));
                    }
                }
            }

            return project;
        }

        private Level ParseLevelNode(XmlNode levelNode, Project project, Dictionary<int, object> objectIdDictionary)
        {
            Level level = new Level(levelNode.Attributes["name"].Value);
            objectIdDictionary[Convert.ToInt32(levelNode.Attributes["id"].Value)] = level;

            Settings settings = new Settings();
            settings.CellWidth = Convert.ToInt32(levelNode.Attributes["width"].Value);
            settings.CellHeight = Convert.ToInt32(levelNode.Attributes["height"].Value);
            settings.Rows = Convert.ToInt32(levelNode.Attributes["rows"].Value);
            settings.Columns = Convert.ToInt32(levelNode.Attributes["columns"].Value);
            settings.BackColor = ParseColor(levelNode.Attributes["back-color"].Value);
            level.Settings = settings;

            foreach (XmlNode child in levelNode.ChildNodes)
            {
                if (child.Name == "level-content")
                {
                    string levelContent = child.InnerText;
                    string[] tokens = levelContent.Split(',');

                    for (int index = 0; index < tokens.Length; index++)
                    {
                        if(tokens[index] == "X") { continue; }

                        int row = index / settings.Columns;
                        int column = index % settings.Columns;
                        int id = Convert.ToInt32(tokens[index]);
                        level.Put((ImageObject2D)objectIdDictionary[id], column, row);
                    }
                }
            }

            return level;
        }

        private GameObjectType ParseTileNode(XmlNode tileNode, Project project, Dictionary<int, object> objectIdDictionary)
        {
            ImageObject2DType tile = new ImageObject2DType();
            tile.Name = tileNode.Attributes["name"].Value;
            int id = Convert.ToInt32(tileNode.Attributes["id"].Value);
            int height = Convert.ToInt32(tileNode.Attributes["height"].Value);
            int width = Convert.ToInt32(tileNode.Attributes["width"].Value);
            objectIdDictionary[id] = tile;
            Bitmap image = new Bitmap(width, height);

            foreach (XmlNode childNode in tileNode.ChildNodes)
            {
                if (childNode.Name == "contents")
                {
                    byte[] binaryData = Convert.FromBase64String(childNode.InnerText);

                    for (int row = 0; row < height; row++)
                    {
                        for (int column = 0; column < width; column++)
                        {
                            byte red = binaryData[(column + row * image.Width) * 4 + 0];
                            byte green = binaryData[(column + row * image.Width) * 4 + 1];
                            byte blue = binaryData[(column + row * image.Width) * 4 + 2];
                            byte alpha = binaryData[(column + row * image.Width) * 4 + 3];

                            System.Drawing.Color color = System.Drawing.Color.FromArgb(alpha, red, green, blue);

                            image.SetPixel(column, row, color);
                        }
                    }
                }
            }

            tile.Image = image;

            return tile;
        }

        private System.Drawing.Color ParseColor(string colorString)
        {
            string[] tokens = colorString.Split(',');

            int red = Convert.ToInt32(tokens[0]);
            int green = Convert.ToInt32(tokens[1]);
            int blue = Convert.ToInt32(tokens[2]);
            int alpha = Convert.ToInt32(tokens[3]);

            return Color.FromArgb(alpha, red, green, blue);
        }

        public bool CanRead(string filename)
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
