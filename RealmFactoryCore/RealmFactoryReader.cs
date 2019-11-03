using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Starbound.RealmFactoryCore
{
    public class RealmFactoryReader : ContentTypeReader<LevelSet>
    {
        protected override LevelSet Read(ContentReader input, LevelSet existingInstance)
        {
            LevelSet project = new LevelSet();

            int tileCount = input.ReadInt32();

            for (int index = 0; index < tileCount; index++)
            {
                int id = input.ReadInt32();
                string name = input.ReadString();

                Texture2D texture = input.ReadObject<Texture2D>();

                Tile tile = new Tile() { Id = id, Name = name, Texture = texture };
                project.Tiles.Add(tile);
            }

            int levelCount = input.ReadInt32();

            for (int index = 0; index < levelCount; index++)
            {
                string name = input.ReadString();
                int rows = input.ReadInt32();
                int columns = input.ReadInt32();
                Vector2 cellSize = input.ReadVector2();
                Color backColor = input.ReadColor();

                LevelDefinition level = new LevelDefinition(name, rows, columns, backColor, cellSize);

                for (int row = 0; row < level.Rows; row++)
                {
                    for (int column = 0; column < level.Columns; column++)
                    {
                        int tileId = input.ReadInt32();
                        if (tileId != -1)
                        {
                            level.Put(project.GetTile(tileId), row, column);
                        }
                    }
                }

                project.Levels.Add(level);
            }

            return project;
        }
    }
}
