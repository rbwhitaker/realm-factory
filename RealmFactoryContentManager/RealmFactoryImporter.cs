using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Starbound.RealmFactoryCore;
using System.Xml;

using TImport = Starbound.RealmFactoryCore.LevelSet;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Starbound.RealmFactoryContentManager
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// </summary>
    [ContentImporter(".realm", DisplayName = "Realm Factory Project Importer", DefaultProcessor = "RealmFactoryProcessor")]
    public class RealmFactoryImporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            context.Logger.PushFile(filename);
            
            LevelSet project = new LevelSet();

            Dictionary<int, object> objectIdDictionary = new Dictionary<int, object>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);

            XmlNode projectNode = xmlDocument.DocumentElement;
            
            objectIdDictionary[Convert.ToInt32(projectNode.Attributes["id"].Value)] = project;

            foreach (XmlNode child in projectNode.ChildNodes)
            {
                if (child.Name == "tiles")
                {
                    foreach (XmlNode tileNode in child.ChildNodes)
                    {
                        project.AddTile(ParseTileNode(tileNode, project, objectIdDictionary));
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

            context.Logger.PopFile();

            return project;
        }

        private AbstractLevelDefinition ParseLevelNode(XmlNode levelNode, LevelSet project, Dictionary<int, object> objectIdDictionary)
        {
            string name = levelNode.Attributes["name"].Value;
            int rows = Convert.ToInt32(levelNode.Attributes["rows"].Value);
            int columns = Convert.ToInt32(levelNode.Attributes["columns"].Value);
            Color backColor = ParseColor(levelNode.Attributes["back-color"].Value);
            Vector2 cellSize = new Vector2(
                Convert.ToSingle(levelNode.Attributes["width"].Value),
                Convert.ToSingle(levelNode.Attributes["height"].Value));

            LevelDefinition level = new LevelDefinition(name, rows, columns, backColor, cellSize);
            objectIdDictionary[Convert.ToInt32(levelNode.Attributes["id"].Value)] = level;

            foreach (XmlNode child in levelNode.ChildNodes)
            {
                if (child.Name == "level-content")
                {
                    string levelContent = child.InnerText;
                    string[] tokens = levelContent.Split(',');

                    for (int index = 0; index < tokens.Length; index++)
                    {
                        if (tokens[index] == "X") { continue; }

                        int row = index / columns;
                        int column = index % columns;
                        int id = Convert.ToInt32(tokens[index]);
                        level.Put((Tile)objectIdDictionary[id], row, column);
                    }
                }
            }

            return level;
        }

        private AbstractTile ParseTileNode(XmlNode tileNode, LevelSet project, Dictionary<int, object> objectIdDictionary)
        {
            SimpleTile2DTransitionObject tile = new SimpleTile2DTransitionObject();
            tile.Name = tileNode.Attributes["name"].Value;
            int id = Convert.ToInt32(tileNode.Attributes["id"].Value);
            int height = Convert.ToInt32(tileNode.Attributes["height"].Value);
            int width = Convert.ToInt32(tileNode.Attributes["width"].Value);
            objectIdDictionary[id] = tile;

            Texture2DContent textureContent = new Texture2DContent();

            foreach (XmlNode childNode in tileNode.ChildNodes)
            {
                if (childNode.Name == "contents")
                {
                    byte[] binaryData = Convert.FromBase64String(childNode.InnerText);

                    PixelBitmapContent<Color> rawData = new PixelBitmapContent<Color>(width, height);
                    rawData.SetPixelData(binaryData);
                    textureContent.Mipmaps = new MipmapChain(rawData);
                }
            }

            tile.TextureContent = textureContent;

            return tile;
        }

        private Color ParseColor(string colorString)
        {
            string[] tokens = colorString.Split(',');

            int red = Convert.ToInt32(tokens[0]);
            int green = Convert.ToInt32(tokens[1]);
            int blue = Convert.ToInt32(tokens[2]);
            int alpha = Convert.ToInt32(tokens[3]);

            return new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
        }
    }
}